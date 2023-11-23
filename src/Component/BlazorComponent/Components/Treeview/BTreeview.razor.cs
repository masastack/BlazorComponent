namespace BlazorComponent
{
    public partial class BTreeview<TItem, TKey> : ITreeview<TItem, TKey> where TKey : notnull
    {
        private List<TItem>? _oldItems;
        private string? _oldItemsKeys;
        private List<TKey> _oldValue = new();
        private List<TKey> _oldActive = new();
        private List<TKey> _oldOpen = new();

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

        [Parameter, MassApiParameter("$loading")]
        public string LoadingIcon { get; set; } = "$loading";

        [Parameter, MassApiParameter("$subgroup")]
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

        public List<TItem> ComputedItems
        {
            get
            {
                if (string.IsNullOrEmpty(Search))
                {
                    return Items;
                }

                return Items.Where(r => !IsExcluded(ItemKey.Invoke(r))).ToList();
            }
        }

        public Dictionary<TKey, NodeState<TItem, TKey>> Nodes { get; private set; } = new();

        public List<TKey> ExcludedItems
        {
            get
            {
                var excluded = new List<TKey>();

                if (string.IsNullOrEmpty(Search))
                {
                    return excluded;
                }

                foreach (var item in Items)
                {
                    FilterTreeItems(item, ref excluded);
                }

                return excluded;
            }
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

        public void UpdateActive(TKey key, bool isActive)
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
        {
            if (!Nodes.TryGetValue(key, out var nodeState)) return;

            nodeState.IsSelected = isSelected;
            nodeState.IsIndeterminate = false;

            if (SelectionType == SelectionType.Standalone)
                UpdateChildrenSelected(nodeState.Children, nodeState.IsSelected);
            else if (SelectionType == SelectionType.Leaf)
            {
                UpdateChildrenSelected(nodeState.Children, nodeState.IsSelected);
                UpdateParentSelected(nodeState.Parent);
            }
        }

        public async Task EmitSelectedAsync()
        {
            var selected = Nodes.Values.Where(r =>
            {
                if (SelectionType == SelectionType.Independent || SelectionType == SelectionType.Standalone)
                {
                    return r.IsSelected;
                }

                return r.IsSelected && !r.Children.Any();
            }).Select(r => r.Item).ToList();

            if (ValueChanged.HasDelegate)
            {
                await ValueChanged.InvokeAsync(selected.Select(ItemKey).ToList());
            }
            else if (OnInput.HasDelegate)
            {
                await OnInput.InvokeAsync(selected);
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
            BuildTree(Items, default);
        }

        private void UpdateParentSelected(TKey? parent, bool isIndeterminate = false)
        {
            if (parent == null) return;

            if (Nodes.TryGetValue(parent, out var nodeState))
            {
                if (isIndeterminate)
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

                UpdateParentSelected(nodeState.Parent, nodeState.IsIndeterminate);
            }
        }

        private void UpdateChildrenSelected(IEnumerable<TKey>? children, bool isSelected)
        {
            if (children == null) return;

            foreach (var child in children)
            {
                if (Nodes.TryGetValue(child, out var nodeState))
                {
                    //control by parent
                    nodeState.IsSelected = isSelected;
                    UpdateChildrenSelected(nodeState.Children, isSelected);
                }
            }
        }

        private bool FilterTreeItems(TItem item, ref List<TKey> excluded)
        {
            if (FilterTreeItem(item, Search, ItemText))
            {
                return true;
            }

            var children = ItemChildren(item);

            if (children != null)
            {
                var match = false;

                foreach (var child in children)
                {
                    if (FilterTreeItems(child, ref excluded))
                    {
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
            return !string.IsNullOrEmpty(Search) && ExcludedItems.Contains(key);
        }

        private string CombineItemKeys(IList<TItem> list)
        {
            string keys = string.Empty;

            if (list == null || list.Count == 0)
            {
                return keys;
            }

            foreach (var item in list)
            {
                var key = ItemKey(item);
                keys += key.ToString();

                var children = ItemChildren(item);
                if (children is not null)
                {
                    keys += CombineItemKeys(children);
                }
            }

            return keys;
        }

        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();

            if (_oldItems != Items)
            {
                _oldItems = Items;
                _oldItemsKeys = CombineItemKeys(Items);

                BuildTree(Items, default);
            }
            else
            {
                var itemKeys = CombineItemKeys(Items);
                if (_oldItemsKeys != itemKeys)
                {
                    _oldItemsKeys = itemKeys;

                    BuildTree(Items, default);
                }
            }

            if (!ListComparer.Equals(_oldValue, Value))
            {
                await HandleUpdate(_oldValue, Value, UpdateSelected, EmitSelectedAsync);
                _oldValue = Value ?? new List<TKey>();
            }

            if (!ListComparer.Equals(_oldActive, Active))
            {
                await HandleUpdate(_oldActive, Active, UpdateActive, EmitActiveAsync);
                _oldActive = Active ?? new List<TKey>();
            }

            if (!ListComparer.Equals(_oldOpen, Open))
            {
                await HandleUpdate(_oldOpen, Open, UpdateOpen, EmitOpenAsync);
                _oldOpen = Open ?? new List<TKey>();
            }
        }

        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            if (firstRender)
            {
                if (OpenAll)
                {
                    UpdateAll(true);
                }

                StateHasChanged();
            }
        }

        public void UpdateAll(bool val)
        {
            Nodes.Values.ForEach(nodeState => { nodeState.IsOpen = val; });
        }

        private void UpdateOpen(TKey key, bool isOpen)
        {
            if (!Nodes.TryGetValue(key, out var nodeState)) return;

            nodeState.IsOpen = isOpen;
        }

        private async Task HandleUpdate(List<TKey> old, List<TKey>? value, Action<TKey, bool> updateFn, Func<Task> emitFn)
        {
            if (value == null) return;

            old.ForEach(k => updateFn(k, false));
            value.ForEach(k => updateFn(k, true));

            await emitFn.Invoke();
        }

        private void BuildTree(List<TItem> items, TKey? parent)
        {
            foreach (var item in items)
            {
                var key = ItemKey(item);
                var children = ItemChildren(item) ?? new List<TItem>();

                Nodes.TryGetValue(key, out var oldNode);

                var newNode = new NodeState<TItem, TKey>(item, children.Select(ItemKey), parent);

                BuildTree(children, key);

                if (SelectionType != SelectionType.Independent && SelectionType != SelectionType.Standalone && parent != null && !Nodes.ContainsKey(key) &&
                    Nodes.TryGetValue(parent, out var node))
                {
                    newNode.IsSelected = node.IsSelected;
                }
                else if (oldNode is not null)
                {
                    newNode.IsSelected = oldNode.IsSelected;
                    newNode.IsIndeterminate = oldNode.IsIndeterminate;
                }

                if (oldNode is not null)
                {
                    newNode.Node = oldNode.Node;
                    newNode.IsActive = oldNode.IsActive;
                    newNode.IsOpen = oldNode.IsOpen;
                }

                Nodes[key] = newNode;

                // TODO: there is still some logic in Vuetify but no implementation here, it's necessarily?

                if (newNode.IsSelected && (SelectionType != SelectionType.Leaf || !newNode.Children.Any()))
                {
                    if (Value == null)
                    {
                        Value = new List<TKey> { key };
                    }
                    else
                    {
                        Value.Add(key);
                    }
                }

                if (newNode.IsActive)
                {
                    if (Active == null)
                    {
                        Active = new List<TKey> { key };
                    }
                    else
                    {
                        Active.Add(key);
                    }
                }

                if (newNode.IsOpen)
                {
                    if (Open == null)
                    {
                        Open = new List<TKey> { key };
                    }
                    else
                    {
                        Open.Add(key);
                    }
                }
            }
        }
    }
}
