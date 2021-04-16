using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public abstract partial class BTree<TItem> : BDomComponentBase
    {
        #region BTree

        /// <summary>
        /// 节点前添加展开图标
        /// </summary>
        [Parameter]
        public bool ShowExpand { get; set; } = true;

        /// <summary>
        /// 是否展示连接线
        /// </summary>
        [Parameter]
        public bool ShowLine { get; set; }

        /// <summary>
        /// 是否展示 BTreeNode title 前的图标
        /// </summary>
        [Parameter]
        public bool ShowIcon { get; set; }

        /// <summary>
        /// 是否节点占据一行
        /// </summary>
        [Parameter]
        public bool BlockNode { get; set; }

        /// <summary>
        /// 设置节点可拖拽
        /// </summary>
        [Parameter]
        public bool Draggable { get; set; }

        private void SetCssBuilder()
        {
            CssBuilder.Clear().Add("ant-BTree")
                .AddIf("ant-BTree-show-line", () => ShowLine)
                .AddIf("ant-BTree-icon-hide", () => ShowIcon)
                .AddIf("ant-BTree-block-node", () => BlockNode)
                .AddIf("draggable-BTree", () => Draggable);
        }

        #endregion BTree

        #region Node

        [Parameter]
        public RenderFragment Nodes { get; set; }

        [Parameter]
        public List<BTreeNode<TItem>> ChildNodes { get; set; } = new List<BTreeNode<TItem>>();

        
#pragma warning disable CS1572 // XML 注释中有“”的 param 标记，但是没有该名称的参数
/// <summary>
        /// 添加节点
        /// </summary>
        /// <param name="BTreeNode"></param>
        /// <param name=""></param>
        internal void AddNode(BTreeNode<TItem> BTreeNode)
#pragma warning restore CS1572 // XML 注释中有“”的 param 标记，但是没有该名称的参数
        {
            ChildNodes.Add(BTreeNode);
        }

        #endregion Node

        #region Selected

        /// <summary>
        /// 支持点选多个节点（节点本身）
        /// </summary>
        [Parameter]
        public bool Multiple { get; set; }

        /// <summary>
        /// 选中的树节点
        /// </summary>
        internal Dictionary<long, BTreeNode<TItem>> SelectedNodesDictionary { get; set; } = new Dictionary<long, BTreeNode<TItem>>();

        public List<string> SelectedTitles => SelectedNodesDictionary.Select(x => x.Value.Title).ToList();

        internal void SelectedNodeAdd(BTreeNode<TItem> BTreeNode)
        {
            if (SelectedNodesDictionary.ContainsKey(BTreeNode.NodeId) == false)
                SelectedNodesDictionary.Add(BTreeNode.NodeId, BTreeNode);

            UpdateBindData();
        }

        internal void SelectedNodeRemove(BTreeNode<TItem> BTreeNode)
        {
            if (SelectedNodesDictionary.ContainsKey(BTreeNode.NodeId) == true)
                SelectedNodesDictionary.Remove(BTreeNode.NodeId);

            UpdateBindData();
        }

        public void DeselectAll()
        {
            foreach (var item in SelectedNodesDictionary.Select(x => x.Value).ToList())
            {
                item.SetSelected(false);
            }
        }

        /// <summary>
        /// 选择的Key
        /// </summary>
        [Parameter]
        public string SelectedKey { get; set; }

        [Parameter]
        public EventCallback<string> SelectedKeyChanged { get; set; }

        /// <summary>
        /// 选择的节点
        /// </summary>
        [Parameter]
        public BTreeNode<TItem> SelectedNode { get; set; }

        [Parameter]
        public EventCallback<BTreeNode<TItem>> SelectedNodeChanged { get; set; }

        /// <summary>
        /// 选择的数据
        /// </summary>
        [Parameter]
        public TItem SelectedData { get; set; }

        [Parameter]
        public EventCallback<TItem> SelectedDataChanged { get; set; }

        /// <summary>
        /// 选择的Key集合
        /// </summary>
        [Parameter]
        public string[] SelectedKeys { get; set; }

        [Parameter]
        public EventCallback<string[]> SelectedKeysChanged { get; set; }

        /// <summary>
        /// 选择的节点集合
        /// </summary>
        [Parameter]
        public BTreeNode<TItem>[] SelectedNodes { get; set; }

        /// <summary>
        /// 选择的数据集合
        /// </summary>
        [Parameter]
        public TItem[] SelectedDatas { get; set; }

        /// <summary>
        /// 更新绑定数据
        /// </summary>
        private void UpdateBindData()
        {
            if (SelectedNodesDictionary.Count == 0)
            {
                SelectedKey = null;
                SelectedNode = null;
                SelectedData = default(TItem);
                SelectedKeys = Array.Empty<string>();
                SelectedNodes = Array.Empty<BTreeNode<TItem>>();
                SelectedDatas = Array.Empty<TItem>();
            }
            else
            {
                var selectedFirst = SelectedNodesDictionary.FirstOrDefault();
                SelectedKey = selectedFirst.Value?.Key;
                SelectedNode = selectedFirst.Value;
                SelectedData = selectedFirst.Value.DataItem;
                SelectedKeys = SelectedNodesDictionary.Select(x => x.Value.Key).ToArray();
                SelectedNodes = SelectedNodesDictionary.Select(x => x.Value).ToArray();
                SelectedDatas = SelectedNodesDictionary.Select(x => x.Value.DataItem).ToArray();
            }

            if (SelectedKeyChanged.HasDelegate) SelectedKeyChanged.InvokeAsync(SelectedKey);
            if (SelectedNodeChanged.HasDelegate) SelectedNodeChanged.InvokeAsync(SelectedNode);
            if (SelectedDataChanged.HasDelegate) SelectedDataChanged.InvokeAsync(SelectedData);
            if (SelectedKeysChanged.HasDelegate) SelectedKeysChanged.InvokeAsync(SelectedKeys);
        }

        #endregion Selected

        #region Checkable

        /// <summary>
        /// 节点前添加 Checkbox 复选框
        /// </summary>
        [Parameter]
        public bool Checkable { get; set; }

        public List<BTreeNode<TItem>> CheckedNodes => GetCheckedNodes(ChildNodes);

        public List<string> CheckedKeys => GetCheckedNodes(ChildNodes).Select(x => x.Key).ToList();

        public List<string> CheckedTitles => GetCheckedNodes(ChildNodes).Select(x => x.Title).ToList();

        private List<BTreeNode<TItem>> GetCheckedNodes(List<BTreeNode<TItem>> childs)
        {
            List<BTreeNode<TItem>> checkeds = new List<BTreeNode<TItem>>();
            foreach (var item in childs)
            {
                if (item.Checked) checkeds.Add(item);
                checkeds.AddRange(GetCheckedNodes(item.ChildNodes));
            }
            return checkeds;
        }

        //取消所有选择项目
        public void DecheckedAll()
        {
            foreach (var item in ChildNodes)
            {
                item.SetChecked(false);
            }
        }

        #endregion Checkable

        #region Search

        public string _searchValue;

        /// <summary>
        /// 按需筛选树,双向绑定
        /// </summary>
        [Parameter]
        public string SearchValue
        {
            get => _searchValue;
            set
            {
                if (_searchValue == value) return;
                _searchValue = value;
                if (string.IsNullOrEmpty(value)) return;
                foreach (var item in ChildNodes)
                {
                    SearchNode(item);
                }
            }
        }

        /// <summary>
        /// 返回一个值是否是页节点
        /// </summary>
        [Parameter]
        public Func<BTreeNode<TItem>, bool> SearchExpression { get; set; }

        /// <summary>
        /// 查询节点
        /// </summary>
        /// <param name="BTreeNode"></param>
        /// <returns></returns>
        private bool SearchNode(BTreeNode<TItem> BTreeNode)
        {
            if (SearchExpression != null)
                BTreeNode.Matched = SearchExpression(BTreeNode);
            else
                BTreeNode.Matched = BTreeNode.Title.Contains(SearchValue);

            var hasChildMatched = BTreeNode.Matched;
            foreach (var item in BTreeNode.ChildNodes)
            {
                var itemMatched = SearchNode(item);
                hasChildMatched = hasChildMatched || itemMatched;
            }
            BTreeNode.HasChildMatched = hasChildMatched;

            return hasChildMatched;
        }

        #endregion Search

        #region DataBind

        [Parameter]
        public IList<TItem> DataSource { get; set; }

        /// <summary>
        /// 指定一个方法，该表达式返回节点的文本。
        /// </summary>
        [Parameter]
        public Func<BTreeNode<TItem>, string> TitleExpression { get; set; }

        /// <summary>
        /// 指定一个返回节点名称的方法。
        /// </summary>
        [Parameter]
        public Func<BTreeNode<TItem>, string> KeyExpression { get; set; }

        /// <summary>
        /// 指定一个返回节点名称的方法。
        /// </summary>
        [Parameter]
        public Func<BTreeNode<TItem>, string> IconExpression { get; set; }

        /// <summary>
        /// 返回一个值是否是页节点
        /// </summary>
        [Parameter]
        public Func<BTreeNode<TItem>, bool> IsLeafExpression { get; set; }

        /// <summary>
        /// 返回子节点的方法
        /// </summary>
        [Parameter]
        public Func<BTreeNode<TItem>, IList<TItem>> ChildrenExpression { get; set; }

        #endregion DataBind

        #region Event

        /// <summary>
        /// 延迟加载
        /// </summary>
        /// <remarks>必须使用async，且返回类型为Task，否则可能会出现载入时差导致显示问题</remarks>
        [Parameter]
        public EventCallback<BTreeEventArgs<TItem>> OnNodeLoadDelayAsync { get; set; }

        /// <summary>
        /// 点击树节点触发
        /// </summary>
        [Parameter]
        public EventCallback<BTreeEventArgs<TItem>> OnClick { get; set; }

        /// <summary>
        /// 双击树节点触发
        /// </summary>
        [Parameter]
        public EventCallback<BTreeEventArgs<TItem>> OnDblClick { get; set; }

        /// <summary>
        /// 右键树节点触发
        /// </summary>
        [Parameter]
        public EventCallback<BTreeEventArgs<TItem>> OnContextMenu { get; set; }

        /// <summary>
        /// 点击树节点 Checkbox 触发
        /// </summary>
        [Parameter]
        public EventCallback<BTreeEventArgs<TItem>> OnCheckBoxChanged { get; set; }

        /// <summary>
        /// 点击展开树节点图标触发
        /// </summary>
        [Parameter]
        public EventCallback<BTreeEventArgs<TItem>> OnExpandChanged { get; set; }

        /// <summary>
        /// 搜索节点时调用(与SearchValue配合使用)
        /// </summary>
        [Parameter]
        public EventCallback<BTreeEventArgs<TItem>> OnSearchValueChanged { get; set; }

        ///// <summary>
        ///// 开始拖拽时调用
        ///// </summary>
        //public EventCallback<BTreeEventArgs> OnDragStart { get; set; }

        ///// <summary>
        ///// dragenter 触发时调用
        ///// </summary>
        //public EventCallback<BTreeEventArgs> OnDragEnter { get; set; }

        ///// <summary>
        ///// dragover 触发时调用
        ///// </summary>
        //public EventCallback<BTreeEventArgs> OnDragOver { get; set; }

        ///// <summary>
        ///// dragleave 触发时调用
        ///// </summary>
        //public EventCallback<BTreeEventArgs> OnDragLeave { get; set; }

        ///// <summary>
        ///// drop 触发时调用
        ///// </summary>
        //public EventCallback<BTreeEventArgs> OnDrop { get; set; }

        ///// <summary>
        ///// dragend 触发时调用
        ///// </summary>
        //public EventCallback<BTreeEventArgs> OnDragEnd { get; set; }

        #endregion Event

        #region Template

        /// <summary>
        /// 缩进模板
        /// </summary>
        [Parameter]
        public RenderFragment<BTreeNode<TItem>> IndentTemplate { get; set; }

        /// <summary>
        /// 标题模板
        /// </summary>
        [Parameter]
        public RenderFragment<BTreeNode<TItem>> TitleTemplate { get; set; }

        /// <summary>
        /// 图标模板
        /// </summary>
        [Parameter]
        public RenderFragment<BTreeNode<TItem>> TitleIconTemplate { get; set; }

        /// <summary>
        /// 切换图标模板
        /// </summary>
        [Parameter]
        public RenderFragment<BTreeNode<TItem>> SwitcherIconTemplate { get; set; }

        #endregion Template

        protected override void OnInitialized()
        {
            SetCssBuilder();
            base.OnInitialized();
        }

        /// <summary>
        /// Find Node
        /// </summary>
        /// <param name="predicate">Predicate</param>
        /// <param name="recursive">Recursive Find</param>
        /// <returns></returns>
        public BTreeNode<TItem> FindFirstOrDefaultNode(Func<BTreeNode<TItem>, bool> predicate, bool recursive = true)
        {
            foreach (var child in ChildNodes)
            {
                if (predicate != null && predicate.Invoke(child))
                {
                    return child;
                }

                if (recursive)
                {
                    var find = child.FindFirstOrDefaultNode(predicate, recursive);
                    if (find != null)
                    {
                        return find;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// from node expand to root
        /// </summary>
        /// <param name="node">Node</param>
        public void ExpandToNode(BTreeNode<TItem> node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }
            var parentNode = node.ParentNode;
            while (parentNode != null)
            {
                parentNode.Expand(true);
                parentNode = parentNode.ParentNode;
            }
        }

        /// <summary>
        /// 展开全部节点
        /// </summary>
        public void ExpandAll()
        {
            this.ChildNodes.ForEach(node => Switch(node, true));
        }

        /// <summary>
        /// 折叠全部节点
        /// </summary>
        public void CollapseAll()
        {
            this.ChildNodes.ForEach(node => Switch(node, false));
        }

        private void Switch(BTreeNode<TItem> node, bool expanded)
        {
            node.Expand(expanded);
            node.ChildNodes.ForEach(n => Switch(n, expanded));
        }
    }
}
