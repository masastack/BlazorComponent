using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BSelectOption<TItem, TValue> : BDomComponentBase
    {
        private string _key;

        [CascadingParameter]
        protected ISelect<TItem,TValue> SelectWrapper { get; set; }

        protected bool Selected
        {
            get
            {
                if (SelectWrapper != null)
                {
                    if (SelectWrapper.Multiple && SelectWrapper.Values != null)
                    {
                        return SelectWrapper.Values.Contains(Value);
                    }
                    else if (SelectWrapper.Value != null)
                    {
                        return SelectWrapper.Value.Equals(Value);
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
