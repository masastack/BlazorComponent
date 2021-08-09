using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BCascaderMenuBody<TInput> where TInput : ICascader
    {
        protected bool ShowSubItems { get; set; }

        protected List<BCascaderNode> SubItems { get; set; }

        [Parameter]
        public List<BCascaderNode> Items { get; set; }

        public EventCallback<BCascaderNode> HandleOnItemClick => EventCallback.Factory.Create<BCascaderNode>(this, item =>
        {
            if (item.Children != null && item.Children.Count > 0)
            {
                ShowSubItems = true;
                SubItems = item.Children;
            }
            else
            {
                ShowSubItems = false;
            }
        });
    }
}
