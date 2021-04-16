using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public abstract partial class BTreeIndent<TItem> : ComponentBase
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

        [Parameter]
        public int BTreeLevel { get; set; }
    }
}
