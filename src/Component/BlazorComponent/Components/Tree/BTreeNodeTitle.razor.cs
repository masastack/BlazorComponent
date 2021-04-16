using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    public abstract partial class BTreeNodeTitle<TItem> : ComponentBase
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

        private bool CanDraggable => BTreeComponent.Draggable && !SelfNode.Disabled;

        private bool IsSwitcherOpen => SelfNode.Expanded && !SelfNode.IsLeaf;

        private bool IsSwitcherClose => !SelfNode.Expanded && !SelfNode.IsLeaf;

        protected CssBuilder CssBuilder { get; } = new CssBuilder();

        private void SetTitleCssBuilder()
        {
            CssBuilder.Clear().Add("ant-BTree-node-content-wrapper")
                .AddIf("draggable", () => CanDraggable)
                .AddIf("ant-BTree-node-content-wrapper-open", () => IsSwitcherOpen)
                .AddIf("ant-BTree-node-content-wrapper-close", () => IsSwitcherClose)
                .AddIf("ant-BTree-node-selected", () => SelfNode.Selected);
        }

        protected override void OnInitialized()
        {
            SetTitleCssBuilder();
            base.OnInitialized();
        }

        protected override void OnParametersSet()
        {
            SetTitleCssBuilder();
            base.OnParametersSet();
        }

        private async Task OnClick(MouseEventArgs args)
        {
            SelfNode.SetSelected(!SelfNode.Selected);
            if (BTreeComponent.OnClick.HasDelegate && args.Button == 0)
                await BTreeComponent.OnClick.InvokeAsync(new BTreeEventArgs<TItem>(BTreeComponent, SelfNode, args));
            else if (BTreeComponent.OnContextMenu.HasDelegate && args.Button == 2)
                await BTreeComponent.OnContextMenu.InvokeAsync(new BTreeEventArgs<TItem>(BTreeComponent, SelfNode, args));
        }

        private async Task OnDblClick(MouseEventArgs args)
        {
            if (BTreeComponent.OnDblClick.HasDelegate && args.Button == 0)
                await BTreeComponent.OnDblClick.InvokeAsync(new BTreeEventArgs<TItem>(BTreeComponent, SelfNode, args));
        }
    }
}
