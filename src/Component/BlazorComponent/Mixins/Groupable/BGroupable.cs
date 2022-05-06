namespace BlazorComponent
{
    public abstract class BGroupable<TGroup> : BDomComponentBase, IGroupable
        where TGroup : ItemGroupBase
    {
        protected bool? _isActive;

        private readonly GroupType _groupType;

        public BGroupable(GroupType groupType)
        {
            _groupType = groupType;
        }

        [CascadingParameter]
        public TGroup ItemGroup { get; set; }

        [Parameter]
        public string ActiveClass { get; set; }

        [Parameter]
        public virtual bool Disabled { get; set; }

        private StringNumber _value;

        [Parameter]
        public StringNumber Value
        {
            get => _value;
            set
            {
                if (value == null) return;

                _value = value;
            }
        }

        protected string ComputedActiveClass => ActiveClass ?? ItemGroup?.ActiveClass;

        protected bool Matched => ItemGroup != null && (ItemGroup.GroupType == _groupType);

        protected bool ValueMatched => Matched && ItemGroup.Values.Contains(Value);

        public bool InternalIsActive { get; private set; }

        [Parameter]
        public bool IsActive
        {
            get => _isActive ?? false;
            set => _isActive = value;
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            if (!Matched) return;

            if (this is IGroupable item)
            {
                ItemGroup.Register(item);
            }
        }

        protected override async Task OnParametersSetAsync()
        {
            if (_isActive.HasValue) // if setting by [Parameter]IsActive, Matched is not required.
            {
                await SetInternalIsActive(_isActive.Value);
            }
            else if (Matched)
            {
                await SetInternalIsActive(ValueMatched);
            }
        }

        protected virtual async Task ToggleAsync()
        {
            if (!Matched) return;

            await ItemGroup.ToggleAsync(Value);
        }

        protected bool IsBooted { get; set; }
        private bool _afterFirstBooted;

        protected async Task SetInternalIsActive(bool val)
        {
            if (!IsBooted)
            {
                if (val)
                {
                    IsBooted = true;

                    _afterFirstBooted = true;

                    await Task.Delay(16);

                    StateHasChanged();
                }
            }
            else if (InternalIsActive != val)
            {
                if (_afterFirstBooted)
                {
                    await Task.Delay(16);
                    _afterFirstBooted = false;
                }

                InternalIsActive = val;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (Matched && this is BGroupable<ItemGroupBase> item)
            {
                ItemGroup.Unregister(item);
            }

            base.Dispose(disposing);
        }
    }
}
