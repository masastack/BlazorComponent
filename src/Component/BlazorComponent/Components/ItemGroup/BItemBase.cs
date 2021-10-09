using System;
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

        /// <summary>
        /// Determine whether the <see cref="BGroupItem{TGroup}"/> can be grouped.
        /// The <see cref="GroupType.ItemGroup"/> cans group any item of <see cref="GroupType"/>.
        /// </summary>
        public bool Groupable => ItemGroup != null && (ItemGroup._groupType == _groupType);

        [Parameter]
        public virtual bool IsActive
        {
            get => _isActive ?? Groupable && ItemGroup.Values.Contains(Value);
            set => _isActive = value;
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            if (!Groupable) return;

            if (Value == null)
                Value = ItemGroup.AllKeys.Count;

            ItemGroup.AllKeys.Add(Value);

            if (ItemGroup.Items.All(item => item.Value?.ToString() != Value.ToString()))
            {
                if (this is BItemBase<BItemGroup> item)
                {
                    ItemGroup.Items.Add(item);
                }
            }

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