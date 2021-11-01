using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    public partial class BProgressLinear : BDomComponentBase
    {
        /// <summary>
        /// 使用 indeterminate 属性，BProgressLinear 会保持动画状态。
        /// </summary>
        [Parameter]
        public bool Indeterminate { get; set; }

        public virtual Task HandleOnClickAsync(MouseEventArgs args)
        {
            return Task.CompletedTask;
        }
    }
}
