namespace BlazorComponent
{
    public partial class BExpansionPanels : BItemGroup
    {
        public BExpansionPanels() : base(GroupType.ExpansionPanels)
        {
        }

        public List<StringNumber?> NextActiveKeys { get; } = new();

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

        protected override List<StringNumber?> UpdateInternalValues(StringNumber? value)
        {
            var internalValues = InternalValues.ToList();

            if (internalValues.Contains(value))
            {
                internalValues.Remove(value);
                RemoveNextActiveKey(value);
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
                    internalValues.Add(value);
                    AddNextActiveKey(value);
                }
            }

            if (Mandatory && internalValues.Count == 0)
            {
                internalValues.Add(value);
                AddNextActiveKey(value);
            }

            return internalValues;
        }

        private void AddNextActiveKey(StringNumber? value)
        {
            var index = AllValues.IndexOf(value);
            if (index > 1)
            {
                NextActiveKeys.Add(AllValues[index - 1]);
            }
        }

        private void RemoveNextActiveKey(StringNumber? value)
        {
            var index = AllValues.IndexOf(value);
            if (index > 1)
            {
                NextActiveKeys.Remove(AllValues[index - 1]);
            }
        }
    }
}
