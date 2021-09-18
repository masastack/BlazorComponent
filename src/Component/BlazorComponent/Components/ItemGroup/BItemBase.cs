using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public abstract class BItemBase<TGroup> : BDomComponentBase
        where TGroup : BItemGroup
    {
        /// <summary>
        /// Set by [Parameter]IsActive if has value.
        /// </summary>
        protected bool? _isActive;

        /// <summary>
        /// Determine the <see cref="GroupType"/> of <see cref="BGroupItem{TGroup}"/>.
        /// </summary>
        private readonly GroupType _groupType;

        public BItemBase(GroupType groupType)
        {
            _groupType = groupType;
        }

        [CascadingParameter]
        public TGroup ItemGroup { get; set; }

        [Parameter]
        public string ActiveClass { get; set; }

        [Parameter]
        public StringNumber Value { get; set; }

        [Parameter]
        public bool NoGroup { get; set; }

        /// <summary>
        /// Determine whether the <see cref="BGroupItem{TGroup}"/> can be grouped.
        /// The <see cref="GroupType.ItemGroup"/> cans group any item of <see cref="GroupType"/>.
        /// </summary>
        public bool Groupable => !NoGroup && ItemGroup != null && (ItemGroup._groupType == GroupType.ItemGroup || ItemGroup._groupType == _groupType);

        [Parameter]
        public virtual bool IsActive
        {
            get => _isActive ?? Groupable && ItemGroup.Values.Contains(Value);
            set => _isActive = value;
        }

        protected override void OnInitialized()
        {
            if (!Groupable) return;

            if (Value == null)
                Value = ItemGroup.AllKeys.Count;

            ItemGroup.AllKeys.Add(Value);

            ItemGroup.Items.Add(this as BItemBase<BItemGroup>);

            if (ItemGroup.Mandatory && ItemGroup.Value == null)
            {
                ItemGroup.Toggle(ItemGroup.AllKeys[0]);
            }
        }

        protected virtual async Task ToggleItem()
        {
            if (Groupable)
            {
                await ItemGroup.Toggle(Value);
            }
        }
    }
}