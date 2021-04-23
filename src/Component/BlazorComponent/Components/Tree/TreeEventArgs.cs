using System;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    public class TreeEventArgs<TItem> : EventArgs
    {
        public TreeEventArgs() { }
        public TreeEventArgs(BTree<TItem> Btree) { BTree = Btree; }
        public TreeEventArgs(BTree<TItem> Btree, BTreeItem<TItem> node) { BTree = Btree; Node = node; }

        public TreeEventArgs(BTree<TItem> Btree, BTreeItem<TItem> node, MouseEventArgs originalEvent) { BTree = Btree; Node = node; OriginalEvent = originalEvent; }

        public BTree<TItem> BTree { get; set; }
        public BTreeItem<TItem> Node { get; set; }
        public MouseEventArgs OriginalEvent { get; set; }
    }
}
