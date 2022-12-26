using Microsoft.AspNetCore.Components;
using OneOf;

namespace BlazorComponent
{
    public abstract class ItemGroupBase : BDomComponentBase
    {
        [Parameter]
        public string ActiveClass { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public GroupType? TargetGroup { get; set; }

        [Parameter]
        public bool Mandatory { get; set; }

        [Parameter]
        public bool Multiple { get; set; }

        [Parameter]
        public StringNumberOrMore? Value { get; set; }

        [Parameter]
        public EventCallback<StringNumberOrMore> ValueChanged { get; set; }

        // [Parameter]
        // public StringNumber? Value
        // {
        //     get => _values.LastOrDefault();
        //     set
        //     {
        //         _values.Clear();
        //         _values.Add(value);
        //         SetValue(value);
        //     }
        // }
        //
        // [Parameter]
        // public EventCallback<StringNumber> ValueChanged { get; set; }
        //
        // [Parameter]
        // public List<StringNumber> Values
        // {
        //     get => _values;
        //     set => _values = value;
        // }
        //
        // [Parameter]
        // public EventCallback<List<StringNumber>> ValuesChanged { get; set; }

        public ItemGroupBase(GroupType groupType)
        {
            GroupType = groupType;
        }

        protected List<StringNumber> _values;

        public GroupType GroupType { get; private set; }

        public List<IGroupable> Items { get; } = new();

        public List<StringNumber> AllValues => Items.Select(item => item.Value).ToList();

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            if (TargetGroup.HasValue)
            {
                GroupType = TargetGroup.Value;
            }

            RefreshItemsState();
        }

        private void RefreshItemsState()
        {
            Items.ForEach(item => item.RefreshState());
        }

        public virtual void Register(IGroupable item)
        {
            item.Value ??= Items.Count;

            Items.Add(item);

            // if no value provided and mandatory
            // assign first registered item
            if (Mandatory && Value == null)
            {
                Value = item.Value;
                ValueChanged.InvokeAsync(item.Value);
            }

            RefreshItemsState();
        }

        public virtual void Unregister(IGroupable item)
        {
            Items.Remove(item);
        }

        private async Task UpdateMandatoryAsync(bool last = false)
        {
            if (Items.Count == 0) return;

            var items = new List<IGroupable>(Items);

            if (last) items.Reverse();

            var item = items.FirstOrDefault(item => !item.Disabled);

            if (item == null) return;

            await ToggleAsync(item.Value);
        }

        public async Task ToggleAsync(StringNumber key)
        {
            Value ??= new List<StringNumber>();
            _values = Value.ToList();

            UpdateValue(key);

            if (Mandatory)
            {
                Value = _values.First();
            }
            else if (Multiple)
            {
                Value = _values;
            }
            else
            {
                Value = _values.FirstOrDefault();
            }

            if (ValueChanged.HasDelegate)
            {
                await ValueChanged.InvokeAsync(Value);
            }
            else
            {
                StateHasChanged();
            }

            RefreshItemsState();
        }

        protected virtual void UpdateValue(StringNumber key)
        {
            if (_values.Contains(key))
            {
                _values.Remove(key);
            }
            else
            {
                if (!Multiple)
                {
                    _values.Clear();
                }

                _values.Add(key);
            }

            if (Mandatory && _values.Count == 0)
            {
                _values.Add(key);
            }
        }
    }
}
