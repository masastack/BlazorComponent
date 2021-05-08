using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public abstract partial class BTree<TItem> : BDomComponentBase
    {
        #region Tree

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
        /// 是否展示 BTreeItem title 前的图标
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

        [Parameter]
        public bool Expanded { get; set; }

        #endregion Tree

        #region Node

        [Parameter]
        public RenderFragment Nodes { get; set; }

        [Parameter]
        public List<BTreeItem<TItem>> ChildNodes { get; set; } = new List<BTreeItem<TItem>>();

        /// <summary>
        /// 添加节点
        /// </summary>
        /// <param name="BTreeItem"></param>
        /// <param name=""></param>
        internal void AddNode(BTreeItem<TItem> BTreeItem)
        {
            ChildNodes.Add(BTreeItem);
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
        internal Dictionary<long, BTreeItem<TItem>> SelectedNodesDictionary { get; set; } = new Dictionary<long, BTreeItem<TItem>>();

        public List<string> SelectedTitles => SelectedNodesDictionary.Select(x => x.Value.Title).ToList();

        internal void SelectedNodeAdd(BTreeItem<TItem> BTreeItem)
        {
            if (SelectedNodesDictionary.ContainsKey(BTreeItem.NodeId) == false)
                SelectedNodesDictionary.Add(BTreeItem.NodeId, BTreeItem);

            UpdateBindData();
        }

        internal void SelectedNodeRemove(BTreeItem<TItem> BTreeItem)
        {
            if (SelectedNodesDictionary.ContainsKey(BTreeItem.NodeId) == true)
                SelectedNodesDictionary.Remove(BTreeItem.NodeId);

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
        public BTreeItem<TItem> SelectedNode { get; set; }

        [Parameter]
        public EventCallback<BTreeItem<TItem>> SelectedNodeChanged { get; set; }

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
        public BTreeItem<TItem>[] SelectedNodes { get; set; }

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
                SelectedNodes = Array.Empty<BTreeItem<TItem>>();
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

        public List<BTreeItem<TItem>> CheckedNodes => GetCheckedNodes(ChildNodes);

        public List<string> CheckedKeys => GetCheckedNodes(ChildNodes).Select(x => x.Key).ToList();

        public List<string> CheckedTitles => GetCheckedNodes(ChildNodes).Select(x => x.Title).ToList();

        private List<BTreeItem<TItem>> GetCheckedNodes(List<BTreeItem<TItem>> childs)
        {
            List<BTreeItem<TItem>> checkeds = new List<BTreeItem<TItem>>();
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

        [Parameter]
        public Func<TItem, bool> DefaultCheckedExpression { get; set; }
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
        public Func<BTreeItem<TItem>, bool> SearchExpression { get; set; }

        /// <summary>
        /// 查询节点
        /// </summary>
        /// <param name="BTreeItem"></param>
        /// <returns></returns>
        private bool SearchNode(BTreeItem<TItem> BTreeItem)
        {
            if (SearchExpression != null)
                BTreeItem.Matched = SearchExpression(BTreeItem);
            else
                BTreeItem.Matched = BTreeItem.Title.Contains(SearchValue);

            var hasChildMatched = BTreeItem.Matched;
            foreach (var item in BTreeItem.ChildNodes)
            {
                var itemMatched = SearchNode(item);
                hasChildMatched = hasChildMatched || itemMatched;
            }
            BTreeItem.HasChildMatched = hasChildMatched;

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
        public Func<BTreeItem<TItem>, string> TitleExpression { get; set; }

        /// <summary>
        /// 指定一个返回节点名称的方法。
        /// </summary>
        [Parameter]
        public Func<BTreeItem<TItem>, string> KeyExpression { get; set; }

        /// <summary>
        /// 指定一个返回节点名称的方法。
        /// </summary>
        [Parameter]
        public Func<BTreeItem<TItem>, string> IconExpression { get; set; }

        /// <summary>
        /// 返回一个值是否是页节点
        /// </summary>
        [Parameter]
        public Func<BTreeItem<TItem>, bool> IsLeafExpression { get; set; }

        /// <summary>
        /// 返回子节点的方法
        /// </summary>
        [Parameter]
        public Func<BTreeItem<TItem>, IList<TItem>> ChildrenExpression { get; set; }

        [Parameter]
        public EventCallback<TItem> HandleItemClick { get; set; }

        #endregion DataBind

        #region Event

        /// <summary>
        /// 延迟加载
        /// </summary>
        /// <remarks>必须使用async，且返回类型为Task，否则可能会出现载入时差导致显示问题</remarks>
        [Parameter]
        public EventCallback<TreeEventArgs<TItem>> OnNodeLoadDelayAsync { get; set; }

        /// <summary>
        /// 点击树节点触发
        /// </summary>
        [Parameter]
        public EventCallback<TreeEventArgs<TItem>> OnClick { get; set; }

        /// <summary>
        /// 双击树节点触发
        /// </summary>
        [Parameter]
        public EventCallback<TreeEventArgs<TItem>> OnDblClick { get; set; }

        /// <summary>
        /// 右键树节点触发
        /// </summary>
        [Parameter]
        public EventCallback<TreeEventArgs<TItem>> OnContextMenu { get; set; }

        /// <summary>
        /// 点击树节点 Checkbox 触发
        /// </summary>
        [Parameter]
        public EventCallback<TreeEventArgs<TItem>> OnCheckBoxChanged { get; set; }

        /// <summary>
        /// 点击展开树节点图标触发
        /// </summary>
        [Parameter]
        public EventCallback<TreeEventArgs<TItem>> OnExpandChanged { get; set; }

        /// <summary>
        /// 搜索节点时调用(与SearchValue配合使用)
        /// </summary>
        [Parameter]
        public EventCallback<TreeEventArgs<TItem>> OnSearchValueChanged { get; set; }

        ///// <summary>
        ///// 开始拖拽时调用
        ///// </summary>
        //public EventCallback<TreeEventArgs> OnDragStart { get; set; }

        ///// <summary>
        ///// dragenter 触发时调用
        ///// </summary>
        //public EventCallback<TreeEventArgs> OnDragEnter { get; set; }

        ///// <summary>
        ///// dragover 触发时调用
        ///// </summary>
        //public EventCallback<TreeEventArgs> OnDragOver { get; set; }

        ///// <summary>
        ///// dragleave 触发时调用
        ///// </summary>
        //public EventCallback<TreeEventArgs> OnDragLeave { get; set; }

        ///// <summary>
        ///// drop 触发时调用
        ///// </summary>
        //public EventCallback<TreeEventArgs> OnDrop { get; set; }

        ///// <summary>
        ///// dragend 触发时调用
        ///// </summary>
        //public EventCallback<TreeEventArgs> OnDragEnd { get; set; }

        [Parameter]
        public EventCallback<TItem> HandleCheckboxClick { get; set; }
        #endregion Event

        #region Template

        /// <summary>
        /// 缩进模板
        /// </summary>
        [Parameter]
        public RenderFragment<BTreeItem<TItem>> IndentTemplate { get; set; }

        /// <summary>
        /// 标题模板
        /// </summary>
        [Parameter]
        public RenderFragment<BTreeItem<TItem>> TitleTemplate { get; set; }

        /// <summary>
        /// 图标模板
        /// </summary>
        [Parameter]
        public RenderFragment<BTreeItem<TItem>> TitleIconTemplate { get; set; }

        /// <summary>
        /// 切换图标模板
        /// </summary>
        [Parameter]
        public RenderFragment<BTreeItem<TItem>> SwitcherIconTemplate { get; set; }

        #endregion Template

        protected override void OnInitialized()
        {
            //SetClassMapper();
            base.OnInitialized();
        }

        /// <summary>
        /// Find Node
        /// </summary>
        /// <param name="predicate">Predicate</param>
        /// <param name="recursive">Recursive Find</param>
        /// <returns></returns>
        public BTreeItem<TItem> FindFirstOrDefaultNode(Func<BTreeItem<TItem>, bool> predicate, bool recursive = true)
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
        public void ExpandToNode(BTreeItem<TItem> node)
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

        private void Switch(BTreeItem<TItem> node, bool expanded)
        {
            node.Expand(expanded);
            node.ChildNodes.ForEach(n => Switch(n, expanded));
        }
    }
}
