using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BSelectOption<TItem, TValue> : BDomComponentBase
    {
        private string _key;

        [CascadingParameter]
        protected ISelect<TItem,TValue> Select { get; set; }

        [Parameter]
        public bool Highlighted { get; set; }

        protected bool Selected
        {
            get
            {
                if (Select != null)
                {
                    if (Select.Multiple && Select.Values != null)
                    {
                        return Select.Values.Contains(Value);
                    }
                    else if (Select.Value != null)
                    {
                        return Select.Value.Equals(Value);
                    }
                }

                return false;
            }
        }

        [Parameter]
        public TItem Item { get; set; }

        [Parameter]
        public TValue Value { get; set; }

        /// <summary>
        /// Gets or sets the key, default returns the <see cref="Value"/> if null.
        /// </summary>
        [Parameter]
        public string Key
        {
            get
            {
                return _key ?? Value.ToString();
            }
            set
            {
                _key = value;
            }
        }

        [Parameter]
        public string Label { get; set; }

        [Parameter]
        public string Icon { get; set; }

        [Parameter]
        public bool Disabled { get; set; }
    }
}
