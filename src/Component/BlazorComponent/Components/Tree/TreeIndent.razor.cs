using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public abstract partial class TreeIndent<TItem> : ComponentBase
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

        [Parameter]
        public int TreeLevel { get; set; }
    }
}
