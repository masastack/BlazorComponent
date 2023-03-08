using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BExpansionPanels : BItemGroup
    {
        public BExpansionPanels() : base(GroupType.ExpansionPanels)
        {
        }

        public List<StringNumber> NextActiveKeys { get; } = new();

        [Parameter]
        public bool Accordion { get; set; }

        [Parameter]
        public bool Disabled { get; set; }

        [Parameter]
        public bool Focusable { get; set; }

        [Parameter]
        public bool Flat { get; set; }

        [Parameter]
        public bool Hover { get; set; }

        [Parameter]
        public bool Inset { get; set; }

        [Parameter]
        public bool Popout { get; set; }

        [Parameter]
        public bool Readonly { get; set; }

        [Parameter]
        public bool Tile { get; set; }

        protected override List<StringNumber> UpdateInternalValues(StringNumber value)
        {
            var internalValues = InternalValues.ToList();

            if (internalValues.Contains(value))
            {
                Remove(value);
            }
            else
            {
                if (!Multiple)
                {
                    internalValues.Clear();
                    NextActiveKeys.Clear();
                }

                if (Max == null || internalValues.Count < Max.TryGetNumber().number)
                {
                    Add(value);
                }
            }

            if (Mandatory && internalValues.Count == 0)
            {
                Add(value);
            }

            return internalValues;
        }

        private void Add(StringNumber value)
        {
            InternalValues.Add(value);

            var index = AllValues.IndexOf(value);
            if (index > 1)
            {
                NextActiveKeys.Add(AllValues[index - 1]);
            }
        }

        private void Remove(StringNumber value)
        {
            InternalValues.Remove(value);

            var index = AllValues.IndexOf(value);
            if (index > 1)
            {
                NextActiveKeys.Remove(AllValues[index - 1]);
            }
        }
    }
}
