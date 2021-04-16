using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    public abstract partial class BTreeNodeSwitcher<TItem> : ComponentBase
    {
        /// <summary>
        /// 树控件本身
        /// </summary>
        [CascadingParameter(Name = "BTree")]
        public BTree<TItem> BTreeComponent { get; set; }

        /// <summary>
        /// 当前节点
        /// </summary>
        [CascadingParameter(Name = "SelfNode")]
        public BTreeNode<TItem> SelfNode { get; set; }

        private bool IsShowLineIcon => !SelfNode.IsLeaf && BTreeComponent.ShowLine;

        private bool IsShowSwitchIcon => !SelfNode.IsLeaf && !BTreeComponent.ShowLine;

        /// <summary>
        /// 节点是否处于展开状态
        /// </summary>
        private bool IsSwitcherOpen => SelfNode.Expanded && !SelfNode.IsLeaf;

        /// <summary>
        /// 节点是否处于关闭状态
        /// </summary>
        private bool IsSwitcherClose => !SelfNode.Expanded && !SelfNode.IsLeaf;

        protected CssBuilder CssBuilder { get; } = new CssBuilder();

        private void SetClassMap()
        {
            CssBuilder.Clear().Add("ant-BTree-switcher")
                .AddIf("ant-BTree-switcher-noop", () => SelfNode.IsLeaf)
                .AddIf("ant-BTree-switcher_open", () => IsSwitcherOpen)
                .AddIf("ant-BTree-switcher_close", () => IsSwitcherClose);
        }

        protected override void OnInitialized()
        {
            SetClassMap();
            base.OnInitialized();
        }

        protected override void OnParametersSet()
        {
            SetClassMap();
            base.OnParametersSet();
        }

        [Parameter]
        public EventCallback<MouseEventArgs> OnSwitcherClick { get; set; }

        private async Task OnClick(MouseEventArgs args)
        {
            if (OnSwitcherClick.HasDelegate)
                await OnSwitcherClick.InvokeAsync(args);
        }
    }
}
