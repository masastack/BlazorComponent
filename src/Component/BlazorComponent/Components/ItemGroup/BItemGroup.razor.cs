using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BItemGroup : ItemGroupBase
    {
        public BItemGroup() : base(GroupType.ItemGroup)
        {
        }

        public BItemGroup(GroupType groupType) : base(groupType)
        {
        }

        [Parameter]
        public StringNumber? Max { get; set; }

        [Parameter]
        public bool Dark { get; set; }

        [Parameter]
        public bool Light { get; set; }

        [CascadingParameter(Name = "IsDark")]
        public bool CascadingIsDark { get; set; }

        public virtual bool IsDark
        {
            get
            {
                if (Dark)
                {
                    return true;
                }

                if (Light)
                {
                    return false;
                }

                return CascadingIsDark;
            }
        }

        protected override List<StringNumber?> UpdateInternalValues(StringNumber? value)
        {
            var internalValues = InternalValues.ToList();

            if (internalValues.Contains(value))
            {
                internalValues.Remove(value);
            }
            else
            {
                if (!Multiple)
                {
                    internalValues.Clear();
                }

                if (Max == null || internalValues.Count < Max.TryGetNumber().number)
                {
                    internalValues.Add(value);
                }
            }

            if (Mandatory && internalValues.Count == 0)
            {
                internalValues.Add(value);
            }

            return internalValues;
        }
    }
}
