﻿using Microsoft.AspNetCore.Components;

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
        public StringNumber Max { get; set; }

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

        protected override void UpdateValue(StringNumber value)
        {
            if (InternalValues.Contains(value))
            {
                InternalValues.Remove(value);
            }
            else
            {
                if (!Multiple)
                {
                    InternalValues.Clear();
                }

                if (Max == null || InternalValues.Count < Max.TryGetNumber().number)
                {
                    InternalValues.Add(value);
                }
            }

            if (Mandatory && InternalValues.Count == 0)
            {
                InternalValues.Add(value);
            }
        }
    }
}
