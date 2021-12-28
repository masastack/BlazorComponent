using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BSelectList<TItem, TItemValue, TValue> : BDomComponentBase
    {
        private string _key;

        [CascadingParameter]
        protected ISelect<TItem, TItemValue, TValue> Select { get; set; }

        [Parameter]
        public bool Highlighted { get; set; }

        [Parameter]
        public RenderFragment<SelectListItemProps<TItem>> ItemContent { get; set; }

        protected virtual bool Selected
        {
            get
            {
                if (Select != null)
                {
                    if (Select.Multiple)
                    {
                        return Select.Values.Contains(Value);
                    }
                    else if (Select.InternalValue != null)
                    {
                        return Select.InternalValue.Equals(Value);
                    }
                }

                return false;
            }
        }

        [Parameter]
        public TItem Item { get; set; }

        [Parameter]
        public TItemValue Value { get; set; }

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

        [Parameter]
        public StringBoolean Loading { get; set; }
    }
}
