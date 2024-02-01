using System.Reflection;
using System.Text;

namespace BlazorComponent
{
    public partial class BTreeview<TItem, TKey> : ITreeview<TItem, TKey> where TKey : notnull
    {
        private string? _oldItemsKeys;
        private HashSet<TKey> _oldValue = [];
        private HashSet<TKey> _oldActive = [];
        private HashSet<TKey> _oldOpen = [];
        private bool _oldOpenAll;

        [Parameter, EditorRequired]
        public List<TItem> Items { get; set; } = null!;

        [Parameter, EditorRequired]
        public Func<TItem, string> ItemText { get; set; } = null!;

        [Parameter, EditorRequired]
        public Func<TItem, TKey> ItemKey { get; set; } = null!;

        [Parameter, EditorRequired]
        public Func<TItem, List<TItem>> ItemChildren { get; set; } = null!;

        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        [Parameter]
        public string? Search { get; set; }

        [Parameter]
        public RenderFragment<TreeviewItem<TItem>>? PrependContent { get; set; }

        [Parameter]
        public RenderFragment<TreeviewItem<TItem>>? LabelContent { get; set; }

        [Parameter]
        public RenderFragment<TreeviewItem<TItem>>? AppendContent { get; set; }

        [Parameter]
        public Func<TItem, bool>? ItemDisabled { get; set; }

        [Parameter]
        public bool Selectable { get; set; }

        [Parameter]
        public bool OpenAll { get; set; }

        [Parameter]
        public Func<TItem, string?, Func<TItem, string>, bool>? Filter { get; set; }

        [Parameter]
        public SelectionType SelectionType { get; set; }

        [Parameter]
        public List<TKey>? Value { get; set; }

        [Parameter]
        public EventCallback<List<TKey>> ValueChanged { get; set; }

        [Parameter]
        public bool MultipleActive { get; set; }

        [Parameter]
        public bool MandatoryActive { get; set; }

        [Parameter]
        public List<TKey>? Active { get; set; }

        [Parameter]
        public EventCallback<List<TKey>> ActiveChanged { get; set; }

        [Parameter]
        public List<TKey>? Open { get; set; }

        [Parameter]
        public EventCallback<List<TKey>> OpenChanged { get; set; }

        [Parameter, MasaApiParameter("$loading")]
        public string LoadingIcon { get; set; } = "$loading";

        [Parameter, MasaApiParameter("$subgroup")]
        public string ExpandIcon { get; set; } = "$subgroup";

        [Parameter]
        public EventCallback<List<TItem>> OnInput { get; set; }

        [Parameter]
        public EventCallback<List<TItem>> OnActiveUpdate { get; set; }

        [Parameter]
        public EventCallback<List<TItem>> OnOpenUpdate { get; set; }

        [Parameter]
        public bool Dark { get; set; }

        [Parameter]
        public bool Light { get; set; }

        [CascadingParameter(Name = "IsDark")]
        public bool CascadingIsDark { get; set; }

        public bool IsDark
        {
            get
            {
                if (Dark)
                {
                    return true;
                }

                if (Light)
                {
                    return false;
                }

                return CascadingIsDark;
            }
        }
        public Dictionary<TKey, NodeState<TItem, TKey>> Nodes { get; private set; } = [];
        HashSet<TKey> ExcludedKeys = [];
        List<TItem> ComputedItems = [];

        static bool IsHashSetEqual<TValue>(HashSet<TValue> left, HashSet<TValue> right) =>
            left.Count == right.Count && left.All(right.Contains);

        private HashSet<TKey> GetExcludeKeys()
        {
            var keys = new HashSet<TKey>();
            if (string.IsNullOrEmpty(Search)) return keys;
            foreach (var item in Items)
            {
                FilterTreeItems(item, keys);
            }
            return keys;
        }

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters);

            Items.ThrowIfNull(ComponentName);
            ItemText.ThrowIfNull(ComponentName);
            ItemKey.ThrowIfNull(ComponentName);
            ItemChildren.ThrowIfNull(ComponentName);
        }

        public void AddNode(ITreeviewNode<TItem, TKey> node)
        {
            if (Nodes.TryGetValue(node.Key, out var nodeState))
            {
                nodeState.Node = node;
            }
        }

        public bool IsActive(TKey key)
        {
            if (Nodes.TryGetValue(key, out var nodeState))
            {
                return nodeState.IsActive;
            }

            return false;
        }

        public bool IsIndeterminate(TKey key)
        {
            if (Nodes.TryGetValue(key, out var nodeState))
            {
                return nodeState.IsIndeterminate;
            }

            return false;
        }

        public bool IsOpen(TKey key)
        {
            if (Nodes.TryGetValue(key, out var nodeState))
            {
                return nodeState.IsOpen;
            }

            return false;
        }

        public bool IsSelected(TKey key)
        {
            if (Nodes.TryGetValue(key, out var nodeState))
            {
                return nodeState.IsSelected;
            }

            return false;
        }

        public void UpdateActiveValue(TKey key)
        {
            if (Nodes.TryGetValue(key, out var nodeState))
            {
                if (nodeState.IsActive && Active?.Contains(key) != true)
                {
                    Active ??= [];
                    Active.Add(key);
                }
                else if (!nodeState.IsActive && Active?.Contains(key) == true)
                {
                    Active.Remove(key);
                }
                _oldActive = [.. Active ?? []];
            }
        }


        public void UpdateActiveState(TKey key, bool isActive)
        {
            if (!Nodes.TryGetValue(key, out var nodeState)) return;

            if (MandatoryActive && !isActive)
            {
                return;
            }

            nodeState.IsActive = isActive;

            if (MultipleActive) return;

            foreach (var ns in Nodes.Values.Where(r => r != nodeState && r.IsActive))
            {
                ns.IsActive = false;
            }
        }

        public async Task EmitActiveAsync()
        {
            var active = Nodes.Values.Where(r => r.IsActive).Select(r => r.Item!).ToList();

            if (OnActiveUpdate.HasDelegate)
            {
                _ = OnActiveUpdate.InvokeAsync(active);
            }

            if (ActiveChanged.HasDelegate)
            {
                await ActiveChanged.InvokeAsync(active.Select(ItemKey).ToList());
            }
            else
            {
                StateHasChanged();
            }
        }

        public void UpdateOpen(TKey key)
        {
            if (Nodes.TryGetValue(key, out var nodeState))
            {
                nodeState.IsOpen = !nodeState.IsOpen;
                if (nodeState.IsOpen && Open?.Contains(key) != true)
                {
                    Open ??= [];
                    Open.Add(key);
                }
                else if(!nodeState.IsOpen && Open?.Contains(key) == true)
                {
                    Open.Remove(key);
                }
                _oldOpen = [.. Open ?? []];
            }
        }

        public async Task EmitOpenAsync()
        {
            var open = Nodes.Values.Where(r => r.IsOpen).Select(r => r.Item!).ToList();

            if (OnOpenUpdate.HasDelegate)
            {
                _ = OnOpenUpdate.InvokeAsync(open);
            }

            if (OpenChanged.HasDelegate)
            {
                await OpenChanged.InvokeAsync(open.Select(ItemKey).ToList());
            }
            else
            {
                StateHasChanged();
            }
        }

        public void UpdateSelected(TKey key, bool isSelected)
            => UpdateSelected(key, isSelected, false, []);

        private void UpdateSelectedByValue(TKey key, bool isSelected, HashSet<TKey> visited)
            => UpdateSelected(key, isSelected, true, visited);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="isSelected"></param>
        /// <param name="updateByValue"></param>
        /// <param name="visited">store nodes that have been accessed to prevent them from being accessed again</param>
        private void UpdateSelected(TKey key, bool isSelected, bool updateByValue, HashSet<TKey> visited)
        {
            if (!Nodes.TryGetValue(key, out var nodeState)) return;

            nodeState.IsSelected = isSelected;
            nodeState.IsIndeterminate = false;

            //store affected node for adjust selected value when hand click mode
            var store = new List<NodeState<TItem, TKey>>
            {
                nodeState
            };

            if (updateByValue && SelectionType == SelectionType.LeafButIndependentParent)
            {
                UpdateParentSelected(nodeState.Parent, false, store, visited);
            }
            else if (SelectionType is SelectionType.Leaf or SelectionType.LeafButIndependentParent)
            {
                UpdateChildrenSelected(nodeState.Children, nodeState.IsSelected, store);
                UpdateParentSelected(nodeState.Parent, false, store, visited);
            }

            if (!updateByValue) //set selected value when hand click mode
            {
                Value ??= [];
                //add selected value when node selected and selected value not contain the node
                store.Where(c => c.IsSelected)
                    .Select(c => ItemKey(c.Item))
                    .Where(c => !Value.Contains(c))
                    .ForEach(c => Value.Add(c));

                //remove selected value when node not selected
                store.Where(c => !c.IsSelected)
                    .Select(c => ItemKey(c.Item))
                    .ForEach(c => Value.Remove(c));

                _oldValue = [.. Value ?? []];
            }
        }

        public async Task EmitSelectedAsync()
        {
            if (ValueChanged.HasDelegate)
            {
                await ValueChanged.InvokeAsync(Value);
            }
            else if (OnInput.HasDelegate)
            {
                var hsValue = Value?.ToHashSet() ?? [];
                var selectedItems = Nodes.Values
                    .Where(c => hsValue.Contains(ItemKey(c.Item)))
                    .Select(c => c.Item)
                    .ToList();
                await OnInput.InvokeAsync(selectedItems);
            }
            else
            {
                StateHasChanged();
            }
        }

        /// <summary>
        /// Rebuilds the tree with current Items.
        /// </summary>
        public void RebuildTree()
        {
            var oldState = Nodes;
            Nodes = [];
            BuildTree(Items, default, oldState);
        }

        private void UpdateParentSelected(TKey? parent, bool isIndeterminate,List<NodeState<TItem, TKey>> store,HashSet<TKey>visited)
        {
            if (parent == null || visited.Contains(parent)) return;

            if (Nodes.TryGetValue(parent, out var nodeState))
            {
                if (SelectionType == SelectionType.LeafButIndependentParent)
                {
                    nodeState.IsSelected = true;
                    nodeState.IsIndeterminate = false;
                }
                else if (isIndeterminate)
                {
                    nodeState.IsSelected = false;
                    nodeState.IsIndeterminate = true;
                }
                else
                {
                    var children = Nodes
                                   .Where(r => nodeState.Children.Contains(r.Key))
                                   .Select(r => r.Value)
                                   .ToList();

                    if (children.All(r => r.IsSelected))
                    {
                        nodeState.IsSelected = true;
                        nodeState.IsIndeterminate = false;
                    }
                    else if (children.All(r => !r.IsSelected))
                    {
                        nodeState.IsSelected = false;
                        nodeState.IsIndeterminate = false;
                    }
                    else
                    {
                        nodeState.IsSelected = false;
                        nodeState.IsIndeterminate = true;
                    }
                }
                visited.Add(parent);
                if (SelectionType != SelectionType.Leaf) // value not contain parent when selection type is Leaf
                    store.Add(nodeState);
                UpdateParentSelected(nodeState.Parent, nodeState.IsIndeterminate,store,visited);
            }
        }

        private void UpdateChildrenSelected(IEnumerable<TKey>? children, bool isSelected, List<NodeState<TItem, TKey>> store)
        {
            if (children == null) return;

            foreach (var child in children)
            {
                if (Nodes.TryGetValue(child, out var nodeState))
                {
                    //control by parent
                    nodeState.IsSelected = isSelected;
                    store.Add(nodeState);
                    UpdateChildrenSelected(nodeState.Children, isSelected, store);
                }
            }
        }

        private bool FilterTreeItems(TItem item, HashSet<TKey> excluded)
        {
            if (FilterTreeItem(item, Search, ItemText))
            {
                if (Nodes.TryGetValue(ItemKey(item), out var nodeState) && nodeState.Parent != null)
                {
                    UpdateOpenState(nodeState.Parent, true);
                }

                return true;
            }

            var children = ItemChildren(item);

            if (children != null)
            {
                var match = false;

                foreach (var child in children)
                {
                    if (FilterTreeItems(child, excluded))
                    {
                        if (Nodes.TryGetValue(ItemKey(child), out var nodeState) && nodeState.Parent != null)
                        {
                            UpdateOpenState(nodeState.Parent, true);
                        }

                        match = true;
                    }
                }

                if (match)
                {
                    return true;
                }
            }

            excluded.Add(ItemKey(item));
            return false;
        }

        private bool FilterTreeItem(TItem item, string? search, Func<TItem, string> itemText)
        {
            if (Filter is not null)
            {
                return Filter.Invoke(item, search, itemText);
            }

            if (string.IsNullOrEmpty(search))
            {
                return true;
            }

            var text = itemText(item);
            return text.ToLower().IndexOf(search.ToLower(), StringComparison.InvariantCulture) > -1;
        }

        public bool IsExcluded(TKey key)
        {
            return !string.IsNullOrEmpty(Search) && ExcludedKeys.Contains(key);
        }

        private bool IsItemsChanged() {
            var newKeys = CombineItemKeys(Items);
            if (_oldItemsKeys == newKeys) return false;
            _oldItemsKeys = newKeys;
            return true;
        }
        private string CombineItemKeys(IList<TItem> list)
        {
            List<string> li = [];
            ExploreForKeys(list,li,1);
            return string.Join(",", li);
        }

        private void ExploreForKeys(IList<TItem> list,List<string> store,int level)
        {
            if (list == null || list.Count == 0) return;
            foreach (var item in list)
            {
                store.Add(ItemKey(item).ToString() ?? "");
                var children = ItemChildren(item);
                if (children is not null && children.Count>0)
                {
                    store.Add($"{level}:"); //consider node position
                    ExploreForKeys(children, store,level+1);
                }
            }
        }

        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();

            if (IsItemsChanged())
            {
                RebuildTree();
            }

            if (string.IsNullOrEmpty(Search))
            {
                ExcludedKeys = [];
                ComputedItems = Items;
            }
            else
            {
                ExcludedKeys = GetExcludeKeys();
                ComputedItems = Items.Where(r => !ExcludedKeys.Contains(ItemKey.Invoke(r))).ToList();
            }

            HashSet<TKey> value = [.. Value ?? []];
            if (!IsHashSetEqual(_oldValue, value))
            {
                //set node not select where old select and current not select
                _oldValue.Where(c => !value.Contains(c)).ForEach(c =>
                {
                    if (!Nodes.TryGetValue(c, out var nodeState)) return;
                    nodeState.IsSelected = false;
                });

                //set node select where current select
                value.ForEach(c =>
                {
                    if (!Nodes.TryGetValue(c, out var nodeState)) return;
                    nodeState.IsSelected = true;
                });
                HashSet<TKey> visited = []; //store visited nodes to prevent duplicate execution
                value.ForEach(k => UpdateSelectedByValue(k, true, visited));
                _oldValue = value;
            }

            HashSet<TKey> active = [.. Active ?? []];
            if (!IsHashSetEqual(_oldActive, active))
            {
                HandleUpdate(_oldActive, active, UpdateActiveState);
                _oldActive = active;
            }

            HashSet<TKey> open = [.. Open ?? []];
            if (!IsHashSetEqual(_oldOpen, open))
            {
                HandleUpdate(_oldOpen, open, UpdateOpenState);
                _oldOpen = open;
            }

            if(OpenAll != _oldOpenAll)
            {
                Nodes.Values.ForEach(c => c.IsOpen = OpenAll);
                _oldOpenAll = OpenAll;
            }
        }

        private void UpdateOpenState(TKey key, bool isOpen)
        {
            if (!Nodes.TryGetValue(key, out var nodeState)) return;

            nodeState.IsOpen = isOpen;
        }

        static void HandleUpdate(HashSet<TKey> old, HashSet<TKey> value, Action<TKey, bool> updateFn)
        {
            if (value == null) return;

            old.ForEach(k => updateFn(k, false));
            value.ForEach(k => updateFn(k, true));
        }

        private void BuildTree(List<TItem> items, TKey? parent, Dictionary<TKey, NodeState<TItem, TKey>> oldNodes)
        {
            foreach (var item in items)
            {
                var key = ItemKey(item);
                var children = ItemChildren(item) ?? [];
                var newNode = new NodeState<TItem, TKey>(item, children.Select(ItemKey), parent);

                if (oldNodes.TryGetValue(key, out var oldNode))
                {
                    newNode.IsActive = oldNode.IsActive;
                    newNode.IsOpen = oldNode.IsOpen;
                }
                Nodes[key] = newNode;

                BuildTree(children, key, oldNodes);
            }
        }
    }
}
