using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    public abstract partial class BTreeNode<TItem> : BDomComponentBase
    {
        #region Node

        /// <summary>
        /// 树控件本身
        /// </summary>
        [CascadingParameter(Name = "BTree")]
        public BTree<TItem> BTreeComponent { get; set; }

        /// <summary>
        /// 上一级节点
        /// </summary>
        [CascadingParameter(Name = "Node")]
        public BTreeNode<TItem> ParentNode { get; set; }

        /// <summary>
        /// 子节点
        /// </summary>
        [Parameter]
        public RenderFragment Nodes { get; set; }

        public List<BTreeNode<TItem>> ChildNodes { get; set; } = new List<BTreeNode<TItem>>();

        public bool HasChildNodes => ChildNodes?.Count > 0;

        /// <summary>
        /// 当前节点级别
        /// </summary>
        public int BTreeLevel => (ParentNode?.BTreeLevel ?? -1) + 1;//因为第一层是0，所以默认是-1

        
#pragma warning disable CS1572 // XML 注释中有“”的 param 标记，但是没有该名称的参数
/// <summary>
        /// 添加节点
        /// </summary>
        /// <param name=""></param>
#pragma warning disable CS1573 // 参数“BTreeNode”在“BTreeNode<TItem>.AddNode(BTreeNode<TItem>)”的 XML 注释中没有匹配的 param 标记(但其他参数有)
        internal void AddNode(BTreeNode<TItem> BTreeNode)
#pragma warning restore CS1572 // XML 注释中有“”的 param 标记，但是没有该名称的参数
#pragma warning restore CS1573 // 参数“BTreeNode”在“BTreeNode<TItem>.AddNode(BTreeNode<TItem>)”的 XML 注释中没有匹配的 param 标记(但其他参数有)
        {
            ChildNodes.Add(BTreeNode);
            IsLeaf = false;
        }

        /// <summary>
        /// Find a node
        /// </summary>
        /// <param name="predicate">Predicate</param>
        /// <param name="recursive">Recursive Find</param>
        /// <returns></returns>
        public BTreeNode<TItem> FindFirstOrDefaultNode(Func<BTreeNode<TItem>, bool> predicate, bool recursive = true)
        {
            foreach (var child in ChildNodes)
            {
                if (predicate.Invoke(child))
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
        /// 获得上级数据集合
        /// </summary>
        /// <returns></returns>
        public List<BTreeNode<TItem>> GetParentNodes()
        {
            if (this.ParentNode != null)
                return this.ParentNode.ChildNodes;
            else
                return this.BTreeComponent.ChildNodes;
        }

        public BTreeNode<TItem> GetPreviousNode()
        {
            var parentNodes = GetParentNodes();
            var index = parentNodes.IndexOf(this);
            if (index == 0) return null;
            else return parentNodes[index - 1];
        }

        public BTreeNode<TItem> GetNextNode()
        {
            var parentNodes = GetParentNodes();
            var index = parentNodes.IndexOf(this);
            if (index == parentNodes.Count - 1) return null;
            else return parentNodes[index + 1];
        }

        #endregion Node

        #region BTreeNode

        private static long _nextNodeId;

        internal long NodeId { get; private set; }

        public BTreeNode()
        {
            NodeId = Interlocked.Increment(ref _nextNodeId);
        }

        private string _key;

        /// <summary>
        /// 指定当前节点的唯一标识符名称。
        /// </summary>
        [Parameter]
        public string Key
        {
            get
            {
                if (BTreeComponent.KeyExpression != null)
                    return BTreeComponent.KeyExpression(this);
                else
                    return _key;
            }
            set
            {
                _key = value;
            }
        }

        private bool _disabled;

        /// <summary>
        /// 是否禁用
        /// </summary>
        [Parameter]
        public bool Disabled
        {
            get { return _disabled || (ParentNode?.Disabled ?? false); }//禁用状态受制于父节点
            set { _disabled = value; }
        }

        private bool _selected;

        /// <summary>
        /// 是否已选中
        /// </summary>
        [Parameter]
        public bool Selected
        {
            get => _selected;
            set
            {
                if (_selected == value) return;
                SetSelected(value);
            }
        }

        public void SetSelected(bool value)
        {
            if (_selected == value) return;
            if (Disabled == true) return;
            _selected = value;
            if (value == true)
            {
                if (BTreeComponent.Multiple == false) BTreeComponent.DeselectAll();
                BTreeComponent.SelectedNodeAdd(this);
            }
            else
            {
                BTreeComponent.SelectedNodeRemove(this);
            }
            StateHasChanged();
        }

        /// <summary>
        /// 是否异步加载状态(影响展开图标展示)
        /// </summary>
        [Parameter]
        public bool Loading { get; set; }

        private void SetBTreeNodeCssBuilder()
        {
            CssBuilder.Clear().Add("ant-BTree-BTreenode")
                .AddIf("ant-BTree-BTreenode-disabled", () => Disabled)
                .AddIf("ant-BTree-BTreenode-switcher-open", () => SwitcherOpen)
                .AddIf("ant-BTree-BTreenode-switcher-close", () => SwitcherClose)
                .AddIf("ant-BTree-BTreenode-checkbox-checked", () => Checked)
                .AddIf("ant-BTree-BTreenode-checkbox-indeterminate", () => Indeterminate)
                .AddIf("ant-BTree-BTreenode-selected", () => Selected)
                .AddIf("ant-BTree-BTreenode-loading", () => Loading);
        }

        #endregion BTreeNode

        #region Switcher

        private bool _isLeaf = true;

        /// <summary>
        /// 是否为叶子节点
        /// </summary>
        [Parameter]
        public bool IsLeaf
        {
            get
            {
                if (BTreeComponent.IsLeafExpression != null)
                    return BTreeComponent.IsLeafExpression(this);
                else
                    return _isLeaf;
            }
            set
            {
                if (_isLeaf == value) return;
                _isLeaf = value;
                StateHasChanged();
            }
        }

        /// <summary>
        /// 是否已展开
        /// </summary>
        [Parameter]
        public bool Expanded { get; set; }

        /// <summary>
        /// 折叠节点
        /// </summary>
        /// <param name="expanded"></param>
        public void Expand(bool expanded)
        {
            Expanded = expanded;
        }

        /// <summary>
        /// 真实的展开状态，路径上只要存在折叠，那么下面的全部折叠
        /// </summary>
        internal bool RealDisplay
        {
            get
            {
                if (string.IsNullOrEmpty(BTreeComponent.SearchValue))
                {//普通模式下节点显示规则
                    if (ParentNode == null) return true;//第一级节点默认显示
                    if (ParentNode.Expanded == false) return false;//上级节点如果是折叠的，必定折叠
                    return ParentNode.RealDisplay; //否则查找路径三的级节点显示情况
                }
                else
                {//筛选模式下不考虑节点是否展开，只要节点符合条件，或者存在符合条件的子节点是就展开显示
                    return Matched || HasChildMatched;
                }
            }
        }

        private async Task OnSwitcherClick(MouseEventArgs args)
        {
            this.Expanded = !this.Expanded;
            if (BTreeComponent.OnNodeLoadDelayAsync.HasDelegate && this.Expanded == true)
            {
                //自有节点被展开时才需要延迟加载
                //如果支持异步载入，那么在展开时是调用异步载入代码
                this.Loading = true;
                await BTreeComponent.OnNodeLoadDelayAsync.InvokeAsync(new BTreeEventArgs<TItem>(BTreeComponent, this, args));
                this.Loading = false;
            }
            if (BTreeComponent.OnExpandChanged.HasDelegate)
                await BTreeComponent.OnExpandChanged.InvokeAsync(new BTreeEventArgs<TItem>(BTreeComponent, this, args));
        }

        private bool SwitcherOpen => Expanded && !IsLeaf;

        private bool SwitcherClose => !Expanded && !IsLeaf;

        #endregion Switcher

        #region Checkbox

        [Parameter]
        public bool Checked { get; set; }

        [Parameter]
        public bool Indeterminate { get; set; }

        [Parameter]
        public bool DisableCheckbox { get; set; }//是否可以选择不受父节点控制

        /// <summary>
        /// 当点击选择框是触发
        /// </summary>
        private async void OnCheckBoxClick(MouseEventArgs args)
        {
            SetChecked(!Checked);
            if (BTreeComponent.OnCheckBoxChanged.HasDelegate)
                await BTreeComponent.OnCheckBoxChanged.InvokeAsync(new BTreeEventArgs<TItem>(BTreeComponent, this, args));
        }

        /// <summary>
        /// 设置选中状态
        /// </summary>
        /// <param name="check"></param>
        public void SetChecked(bool check)
        {
            if (Disabled) return;
            this.Checked = check;
            this.Indeterminate = false;
            if (HasChildNodes)
            {
                foreach (var subnode in ChildNodes)
                    subnode?.SetChecked(check);
            }
            if (ParentNode != null)
                ParentNode.UpdateCheckState();
        }

        /// <summary>
        /// 更新选中状态
        /// </summary>
        /// <param name="halfChecked"></param>
        public void UpdateCheckState(bool? halfChecked = null)
        {
            if (halfChecked.HasValue && halfChecked.Value == true)
            {//如果子元素存在不确定状态，父元素必定存在不确定状态
                this.Checked = false;
                this.Indeterminate = true;
            }
            else if (HasChildNodes == true)
            {//判断当前节点的选择状态
                bool hasChecked = false;
                bool hasUnchecked = false;

                foreach (var item in ChildNodes)
                {
                    if (item.Indeterminate == true) break;
                    if (item.Checked == true) hasChecked = true;
                    if (item.Checked == false) hasUnchecked = true;
                }

                if (hasChecked && !hasUnchecked)
                {
                    this.Checked = true;
                    this.Indeterminate = false;
                }
                else if (!hasChecked && hasUnchecked)
                {
                    this.Checked = false;
                    this.Indeterminate = false;
                }
                else if (hasChecked && hasUnchecked)
                {
                    this.Checked = false;
                    this.Indeterminate = true;
                }
            }

            if (ParentNode != null)
                ParentNode.UpdateCheckState(this.Indeterminate);

            //当达到最顶级后进行刷新状态，避免每一级刷新的性能问题
            if (ParentNode == null)
                StateHasChanged();
        }

        #endregion Checkbox

        #region Title

        [Parameter]
        public bool Draggable { get; set; }

        private string _icon;

        /// <summary>
        /// 节点前的图标，与 `ShowIcon` 组合使用
        /// </summary>
        [Parameter]
        public string Icon
        {
            get
            {
                if (BTreeComponent.IconExpression != null)
                    return BTreeComponent.IconExpression(this);
                else
                    return _icon;
            }
            set
            {
                _icon = value;
            }
        }

        private string _title;

        /// <summary>
        /// 文本
        /// </summary>
        [Parameter]
        public string Title
        {
            get
            {
                if (BTreeComponent.TitleExpression != null)
                    return BTreeComponent.TitleExpression(this);
                else
                    return _title;
            }
            set
            {
                _title = value;
            }
        }

        /// <summary>
        /// title是否包含SearchValue(搜索使用)
        /// </summary>
        public bool Matched { get; set; }

        /// <summary>
        /// 子节点存在满足搜索条件，所以夫节点也需要显示
        /// </summary>
        internal bool HasChildMatched { get; set; }

        #endregion Title

        #region 数据绑定

        [Parameter]
        public TItem DataItem { get; set; }

        private IList<TItem> ChildDataItems
        {
            get
            {
                if (BTreeComponent.ChildrenExpression != null)
                    return BTreeComponent.ChildrenExpression(this) ?? new List<TItem>();
                else
                    return new List<TItem>();
            }
        }

        /// <summary>
        /// 获得上级数据集合
        /// </summary>
        /// <returns></returns>
        public IList<TItem> GetParentChildDataItems()
        {
            if (this.ParentNode != null)
                return this.ParentNode.ChildDataItems;
            else
                return this.BTreeComponent.DataSource;
        }

        #endregion 数据绑定

        #region 节点数据操作

        /// <summary>
        /// 添加子节点
        /// </summary>
        /// <param name="dataItem"></param>
        public void AddChildNode(TItem dataItem)
        {
            ChildDataItems.Add(dataItem);
        }

        /// <summary>
        /// 节点后面添加节点
        /// </summary>
        /// <param name="dataItem"></param>
        public void AddNextNode(TItem dataItem)
        {
            var parentChildDataItems = GetParentChildDataItems();
            var index = parentChildDataItems.IndexOf(this.DataItem);
            parentChildDataItems.Insert(index + 1, dataItem);

            AddNodeAndSelect(dataItem);
        }

        /// <summary>
        /// 节点前面添加节点
        /// </summary>
        /// <param name="dataItem"></param>
        public void AddPreviousNode(TItem dataItem)
        {
            var parentChildDataItems = GetParentChildDataItems();
            var index = parentChildDataItems.IndexOf(this.DataItem);
            parentChildDataItems.Insert(index, dataItem);

            AddNodeAndSelect(dataItem);
        }

        /// <summary>
        /// 删除节点
        /// </summary>
        public void Remove()
        {
            var parentChildDataItems = GetParentChildDataItems();
            parentChildDataItems.Remove(this.DataItem);
        }

        public void MoveInto(BTreeNode<TItem> BTreeNode)
        {
            if (BTreeNode == this || this.DataItem.Equals(BTreeNode.DataItem)) return;
            var parentChildDataItems = GetParentChildDataItems();
            parentChildDataItems.Remove(this.DataItem);
            BTreeNode.AddChildNode(this.DataItem);
        }

        /// <summary>
        /// 上移节点
        /// </summary>
        public void MoveUp()
        {
            var parentChildDataItems = GetParentChildDataItems();
            var index = parentChildDataItems.IndexOf(this.DataItem);
            if (index == 0) return;
            parentChildDataItems.RemoveAt(index);
            parentChildDataItems.Insert(index - 1, this.DataItem);
        }

        /// <summary>
        /// 下移节点
        /// </summary>
        public void MoveDown()
        {
            var parentChildDataItems = GetParentChildDataItems();
            var index = parentChildDataItems.IndexOf(this.DataItem);
            if (index == parentChildDataItems.Count - 1) return;
            parentChildDataItems.RemoveAt(index);
            parentChildDataItems.Insert(index + 1, this.DataItem);
        }

        /// <summary>
        /// 降级节点
        /// </summary>
        public void Downgrade()
        {
            var previousNode = GetPreviousNode();
            if (previousNode == null) return;
            var parentChildDataItems = GetParentChildDataItems();
            parentChildDataItems.Remove(this.DataItem);
            previousNode.AddChildNode(this.DataItem);
        }

        /// <summary>
        /// 升级节点
        /// </summary>
        public void Upgrade()
        {
            if (this.ParentNode == null) return;
            var parentChildDataItems = this.ParentNode.GetParentChildDataItems();
            var index = parentChildDataItems.IndexOf(this.ParentNode.DataItem);
            Remove();
            parentChildDataItems.Insert(index + 1, this.DataItem);
        }

        #endregion 节点数据操作

        protected override void OnInitialized()

        {
            SetBTreeNodeCssBuilder();
            if (ParentNode != null)
                ParentNode.AddNode(this);
            else
                BTreeComponent.AddNode(this);
            base.OnInitialized();
        }

        protected override void OnParametersSet()
        {
            SetBTreeNodeCssBuilder();
            base.OnParametersSet();
        }

        private void AddNodeAndSelect(TItem dataItem)
        {
            var tn = ChildNodes.FirstOrDefault(BTreeNode => BTreeNode.DataItem.Equals(dataItem));
            if (tn != null)
            {
                this.Expand(true);
                tn.SetSelected(true);
            }
        }
    }
}
