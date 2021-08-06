using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BAutocompleteSelectBody<TItem> : BDomComponentBase
    {
        private string _queryText;

        [Parameter]
        public List<TItem> Items { get; set; }

        [Parameter]
        public Func<TItem, string> ItemText { get; set; }

        [Parameter]
        public EventCallback<TItem> OnItemClick { get; set; }

        [Parameter]
        public string QueryText
        {
            get
            {
                return _queryText;
            }
            set
            {
                if (value == null)
                {
                    return;
                }

                _queryText = value;
            }
        }

        [Parameter]
        public int HighlightIndex { get; set; }

        [Parameter]
        public TItem SelectedItem { get; set; }

        [Parameter]
        public List<TItem> SelectedItems { get; set; }

        [Parameter]
        public bool Multiple { get; set; }

        protected string HighlightClass { get; set; }

        protected string SelectedClass { get; set; }

        public EventCallback<MouseEventArgs> HandleClick(TItem item)
        {
            return EventCallback.Factory.Create<MouseEventArgs>(this, async () =>
            {
                if (OnItemClick.HasDelegate)
                {
                    await OnItemClick.InvokeAsync(item);
                }
            });
        }
    }
}
