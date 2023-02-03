using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public abstract class ItemGroupBase : BDomComponentBase
    {
        public ItemGroupBase(GroupType groupType)
        {
            GroupType = groupType;
        }

        [Parameter]
        public string? ActiveClass { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public GroupType? TargetGroup { get; set; }

        [Parameter]
        public bool Mandatory { get; set; }

        [Parameter]
        public bool Multiple { get; set; }

        [Parameter]
        public StringNumber? Value
        {
            get => InternalValues.LastOrDefault();
            set
            {
                InternalValues.Clear();
                InternalValues.Add(value);
                SetValue(value);
            }
        }

        [Parameter]
        public EventCallback<StringNumber> ValueChanged { get; set; }

        [Parameter]
        public List<StringNumber> Values
        {
            get => InternalValues;
            set => InternalValues = value;
        }

        [Parameter]
        public EventCallback<List<StringNumber>> ValuesChanged { get; set; }

        public GroupType GroupType { get; private set; }

        public List<IGroupable> Items { get; } = new();

        public List<StringNumber> AllValues => Items.Select(item => item.Value).ToList();

        protected List<StringNumber> InternalValues { get; set; } = new();

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
            UpdateValue(key);

            RefreshItemsState();

            if (Multiple)
            {
                if (ValuesChanged.HasDelegate)
                {
                    await ValuesChanged.InvokeAsync(InternalValues);
                }
                else
                {
                    Values = InternalValues;
                    StateHasChanged();
                }
            }
            else
            {
                var value = InternalValues.LastOrDefault();
                if (ValueChanged.HasDelegate)
                {
                    await ValueChanged.InvokeAsync(value);
                }
                else
                {
                    Value = value;
                    StateHasChanged();
                }
            }
        }

        protected virtual void UpdateValue(StringNumber key)
        {
            if (InternalValues.Contains(key))
            {
                InternalValues.Remove(key);
            }
            else
            {
                if (!Multiple)
                {
                    InternalValues.Clear();
                }

                InternalValues.Add(key);
            }

            if (Mandatory && InternalValues.Count == 0)
            {
                InternalValues.Add(key);
            }
        }
    }
}
