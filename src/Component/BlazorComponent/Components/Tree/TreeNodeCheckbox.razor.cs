using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    public abstract partial class TreeNodeCheckbox<TItem> : ComponentBase
    {
        /// <summary>
        /// 树控件本身
        /// </summary>
        [CascadingParameter(Name = "Tree")]
        public Tree<TItem> TreeComponent { get; set; }

        /// <summary>
        /// 当前节点
        /// </summary>
        [CascadingParameter(Name = "SelfNode")]
        public TreeNode<TItem> SelfNode { get; set; }

        protected CssBuilder cssBuilder { get; } = new CssBuilder();

        private void SetClassMap()
        {
            cssBuilder.Clear().Add("ant-tree-checkbox")
                .AddIf("ant-tree-checkbox-checked", () => SelfNode.Checked)
                .AddIf("ant-tree-checkbox-indeterminate", () => SelfNode.Indeterminate)
                .AddIf("ant-tree-checkbox-disabled", () => SelfNode.Disabled || SelfNode.DisableCheckbox);
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
        public EventCallback<MouseEventArgs> OnCheckBoxClick { get; set; }

        private async Task OnClick(MouseEventArgs args)
        {
            if (OnCheckBoxClick.HasDelegate)
                await OnCheckBoxClick.InvokeAsync(args);
        }
    }
}
