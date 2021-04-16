using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    public abstract partial class BTreeNodeCheckbox<TItem> : ComponentBase
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

        protected CssBuilder CssBuilder { get; } = new CssBuilder();

        private void SetClassMap()
        {
            CssBuilder.Clear().Add("ant-BTree-checkbox")
                .AddIf("ant-BTree-checkbox-checked", () => SelfNode.Checked)
                .AddIf("ant-BTree-checkbox-indeterminate", () => SelfNode.Indeterminate)
                .AddIf("ant-BTree-checkbox-disabled", () => SelfNode.Disabled || SelfNode.DisableCheckbox);
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
