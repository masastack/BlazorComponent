using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public abstract class Groupable<TGroup> : BDomComponentBase, IGroupable
        where TGroup : ItemGroupBase
    {
        protected bool? _isActive;

        private readonly GroupType _groupType;

        public Groupable(GroupType groupType)
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

        public string ComputedActiveClass => ActiveClass ?? ItemGroup?.ActiveClass;

        public bool Matched => ItemGroup != null && (ItemGroup.GroupType == _groupType);

        [Parameter]
        public bool IsActive
        {
            get => _isActive ?? Matched && ItemGroup.Values.Contains(Value);
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

        protected virtual async Task ToggleItem()
        {
            if (!Matched) return;

            await ItemGroup.Toggle(Value);
        }

        protected override void Dispose(bool disposing)
        {
            if (Matched && this is Groupable<ItemGroupBase> item)
            {
                ItemGroup.Unregister(item);
            }

            base.Dispose(disposing);
        }
    }
}