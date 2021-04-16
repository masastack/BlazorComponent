using System;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    public class BTreeEventArgs<TItem> : EventArgs
    {
        public BTreeEventArgs() { }
        public BTreeEventArgs(BTree<TItem> BTree) { BTree = BTree; }
        public BTreeEventArgs(BTree<TItem> BTree, BTreeNode<TItem> node) { BTree = BTree; Node = node; }

        public BTreeEventArgs(BTree<TItem> BTree, BTreeNode<TItem> node, MouseEventArgs originalEvent) { BTree = BTree; Node = node; OriginalEvent = originalEvent; }

        public BTree<TItem> BTree { get; set; }
        public BTreeNode<TItem> Node { get; set; }

        /// <summary>
        /// 原生事件
        /// </summary>
        public MouseEventArgs OriginalEvent { get; set; }
    }
}
