using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BListGroup : BDomComponentBase
    {
        private const string PREPEND = "prepend";
        private const string APPEND = "append";

        private bool _expanded;
        /// <summary>
        /// 控制列表组是否展开
        /// </summary>
        protected bool Expanded
        {
            get => _expanded;
            set
            {
                _expanded = value;
                Value = value;
            }
        }

        private bool _value;
        /// <summary>
        /// 初始化列表组是否展开，后续请使用<see cref="Expanded"/>控制行为和样式
        /// </summary>
        [Parameter]
        public bool Value
        {
            get => _value;
            set
            {
                if (value == _value) return;
                _value = value;
                ValueChanged.InvokeAsync(_value);
            }
        }

        [Parameter]
        public EventCallback<bool> ValueChanged { get; set; }

        [Parameter]
        public string PrependIcon { get; set; }

        [Parameter]
        public string AppendIcon { get; set; }

        [Obsolete("Use ActivatorContent instead.")]
        [Parameter]
        public RenderFragment Activator { get; set; }

        [Parameter]
        public RenderFragment ActivatorContent { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        protected override void OnInitialized()
        {
            _expanded = Value;
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            if (Activator != null)
            {
                ActivatorContent = Activator;
            }
        }

        protected void ToggleExpansion()
        {
            Expanded = !Expanded;
        }

        public void Contract()
        {
            if (Expanded)
            {
                Expanded = false;
            }
        }
    }
}
