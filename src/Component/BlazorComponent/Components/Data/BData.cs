using Microsoft.AspNetCore.Components;
using OneOf;

namespace BlazorComponent
{
    public abstract class BData<TItem> : BDomComponentBase
    {
        [Parameter]
        public IEnumerable<TItem> Items
        {
            get
            {
                return GetValue<IEnumerable<TItem>>(new List<TItem>());
            }
            set
            {
                SetValue(value);
            }
        }

        [Parameter]
        public OneOf<string, IList<string>> SortBy
        {
            get
            {
                return GetValue<OneOf<string, IList<string>>>(new List<string>());
            }
            set
            {
                SetValue(value);
            }
        }

        [Parameter]
        public OneOf<bool, IList<bool>> SortDesc
        {
            get
            {
                return GetValue<OneOf<bool, IList<bool>>>(new List<bool>());
            }
            set
            {
                SetValue(value);
            }
        }

        [Parameter]
        public Func<IEnumerable<TItem>, IEnumerable<ItemValue<TItem>>, IList<string>, List<bool>, string, IEnumerable<TItem>> CustomSort { get; set; } = DefaultSortItems;

        [Parameter]
        public bool MustSort
        {
            get
            {
                return GetValue<bool>();
            }
            set
            {
                SetValue(value);
            }
        }

        [Parameter]
        public bool MultiSort
        {
            get
            {
                return GetValue<bool>();
            }
            set
            {
                SetValue(value);
            }
        }

        [Parameter]
        public int Page
        {
            get
            {
                return GetValue(1);
            }
            set
            {
                SetValue(value);
            }
        }

        [Parameter]
        public int ItemsPerPage
        {
            get
            {
                return GetValue(10);
            }
            set
            {
                SetValue(value);
            }
        }

        [Parameter]
        public OneOf<string, IList<string>> GroupBy { get; set; } = new List<string>();

        [Parameter]
        public IList<bool> GroupDesc { get; set; } = new List<bool>();

        [Parameter]
        public Func<IEnumerable<TItem>, IEnumerable<ItemValue<TItem>>, IList<string>, IList<bool>, IEnumerable<IGrouping<string, TItem>>> CustomGroup { get; set; } = DefaultGroupItems;

        [Parameter]
        public string Locale { get; set; } = "en-US";

        [Parameter]
        public bool DisableSort { get; set; }

        [Parameter]
        public bool DisablePagination
        {
            get
            {
                return GetValue<bool>();
            }
            set
            {
                SetValue(value);
            }
        }

        [Parameter]
        public bool DisableFiltering
        {
            get
            {
                return GetValue<bool>();
            }
            set
            {
                SetValue(value);
            }
        }

        [Parameter]
        public string Search
        {
            get
            {
                return GetValue<string>();
            }
            set
            {
                SetValue(value);
            }
        }

        [Parameter]
        public Func<IEnumerable<TItem>, IEnumerable<ItemValue<TItem>>, string, IEnumerable<TItem>> CustomFilter { get; set; } = DefaultSearchItems;

        [Parameter]
        public int ServerItemsLength
        {
            get
            {
                return GetValue(-1);
            }
            set
            {
                SetValue(value);
            }
        }

        [Parameter]
        public IEnumerable<ItemValue<TItem>> ItemValues { get; set; } = new List<ItemValue<TItem>>();

        [Parameter]
        public EventCallback<int> OnPageCount { get; set; }

        [Parameter]
        public EventCallback<DataOptions> OnOptionsUpdate { get; set; }

        protected DataOptions InternalOptions
        {
            get
            {
                return GetValue(new DataOptions());
            }
            set
            {
                SetValue(value);
            }
        }

        public DataPagination Pagination => new()
        {
            Page = InternalOptions.Page,
            ItemsPerPage = InternalOptions.ItemsPerPage,
            PageStart = PageStart,
            PageStop = PageStop,
            PageCount = PageCount,
            ItemsLength = ItemsLength
        };

        public int PageStart
        {
            get
            {
                if (InternalOptions.ItemsPerPage == -1 || !Items.Any())
                {
                    return 0;
                }

                return (InternalOptions.Page - 1) * InternalOptions.ItemsPerPage;
            }
        }

        public IEnumerable<TItem> FilteredItems
        {
            get
            {
                return GetComputedValue(() =>
                {
                    var items = new List<TItem>(Items);

                    if (!DisableFiltering && ServerItemsLength <= 0 && ItemValues != null)
                    {
                        return CustomFilter(items, ItemValues, Search);
                    }

                    return items;
                }, new string[]
                {
                    nameof(DisableFiltering),
                    nameof(ServerItemsLength),
                    nameof(ItemValues),
                    nameof(Items),
                    nameof(Search)
                });
            }
        }

        public int ItemsLength
        {
            get
            {
                return GetComputedValue(() =>
                     ServerItemsLength >= 0 ? ServerItemsLength : FilteredItems.Count());
            }
        }

        public int PageStop
        {
            get
            {
                if (InternalOptions.ItemsPerPage == -1)
                {
                    return ItemsLength;
                }

                if (!Items.Any())
                {
                    return 0;
                }

                return Math.Min(ItemsLength, InternalOptions.Page * InternalOptions.ItemsPerPage);
            }
        }

        public int PageCount
        {
            get
            {
                return GetComputedValue(() =>
                     InternalOptions.ItemsPerPage <= 0 ? 1 : (int)Math.Ceiling(ItemsLength / (InternalOptions.ItemsPerPage * 1.0)));
            }
        }

        public static IEnumerable<TItem> DefaultSearchItems(IEnumerable<TItem> items, IEnumerable<ItemValue<TItem>> itemValues, string search)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                return items;
            }

            search = search.ToLowerInvariant();
            return items.Where(item => itemValues.Any(itemValue => DefaultFilter(itemValue.Invoke(item), search, item)));
        }

        public static bool DefaultFilter(object value, string search, TItem item)
        {
            return value != null && search != null && value is not bool && value.ToString().ToLowerInvariant().IndexOf(search.ToLowerInvariant()) != -1;
        }

        public static IEnumerable<TItem> DefaultSortItems(IEnumerable<TItem> items, IEnumerable<ItemValue<TItem>> itemValues, IList<string> sortBy, List<bool> sortDesc, string locale)
        {
            var sortedItems = default(IOrderedEnumerable<TItem>);

            for (int i = 0; i < sortBy.Count; i++)
            {
                var itemValue = itemValues.FirstOrDefault(itemValue => itemValue == sortBy[i]);
                if (itemValue == null)
                {
                    continue;
                }

                var selector = itemValue.Factory;
                var desc = sortDesc[i];

                if (i == 0)
                {
                    if (!desc)
                    {
                        sortedItems = items.OrderBy(selector);
                    }
                    else
                    {
                        sortedItems = items.OrderByDescending(selector);
                    }
                }
                else
                {
                    if (!desc)
                    {
                        sortedItems = sortedItems.ThenBy(selector);
                    }
                    else
                    {
                        sortedItems = sortedItems.ThenByDescending(selector);
                    }
                }
            }

            return sortedItems ?? items;
        }

        private static IEnumerable<IGrouping<string, TItem>> DefaultGroupItems(IEnumerable<TItem> items, IEnumerable<ItemValue<TItem>> itemValues, IList<string> groupBy, IList<bool> groupDesc)
        {
            var key = groupBy.FirstOrDefault();
            var itemValue = itemValues.FirstOrDefault(itemValue => itemValue == key);
            if (key == null)
            {
                return new List<IGrouping<string, TItem>>();
            }

            return items.GroupBy(item => itemValue.Invoke(item).ToString());
        }

        public bool IsGrouped => InternalOptions.GroupBy.Count > 0;

        public IEnumerable<IGrouping<string, TItem>> GroupedItems
        {
            get
            {
                return IsGrouped ? GroupItems(ComputedItems) : Enumerable.Empty<IGrouping<string, TItem>>();
            }
        }

        public IEnumerable<TItem> ComputedItems
        {
            get
            {
                return GetComputedValue(() =>
                 {
                     IEnumerable<TItem> items = new List<TItem>(FilteredItems);

                     if ((!DisableSort || InternalOptions.GroupBy.Count > 0) && ServerItemsLength <= 0)
                     {
                         items = SortItems(items);
                     }

                     if (!DisablePagination && ServerItemsLength <= 0)
                     {
                         items = PaginateItems(items);
                     }

                     return items;
                 }, new string[]
                 {
                    nameof(FilteredItems),
                    nameof(DisableSort),
                    nameof(InternalOptions),
                    nameof(ServerItemsLength),
                    nameof(DisablePagination)
                 });
            }
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            Watcher
                .Watch<int>(nameof(Page), value =>
                {
                    InternalOptions.Page = value;
                })
                .Watch<int>(nameof(ItemsPerPage), value =>
                {
                    InternalOptions.ItemsPerPage = value;
                })
                .Watch<bool>(nameof(MultiSort), value =>
                {
                    InternalOptions.MultiSort = value;
                })
                .Watch<bool>(nameof(MustSort), value =>
                {
                    InternalOptions.MustSort = value;
                })
                .Watch<int>(nameof(PageCount), value =>
                {
                    OnPageCount.InvokeAsync(value);
                })
                .Watch<OneOf<string, IList<string>>>(nameof(SortBy), val =>
                {
                    InternalOptions.SortBy = WrapperInArray(val);
                })
                .Watch<OneOf<bool, IList<bool>>>(nameof(SortDesc), val =>
                {
                    InternalOptions.SortDesc = WrapperInArray(val);
                });

            UpdateOptions(options =>
            {
                options.Page = Page;
                options.ItemsPerPage = ItemsPerPage;
                options.SortBy = WrapperInArray(SortBy);
                options.SortDesc = WrapperInArray(SortDesc);
                options.GroupBy = WrapperInArray(GroupBy);
                options.GroupDesc = GroupDesc;
                options.MustSort = MustSort;
                options.MultiSort = MultiSort;

                var sortDiff = InternalOptions.SortBy.Count - InternalOptions.SortDesc.Count;
                var groupDiff = InternalOptions.GroupBy.Count - InternalOptions.GroupDesc.Count;

                if (sortDiff > 0)
                {
                    for (int i = 0; i < sortDiff; i++)
                    {
                        InternalOptions.SortDesc.Add(false);
                    }
                }

                if (groupDiff > 0)
                {
                    for (int i = 0; i < groupDiff; i++)
                    {
                        InternalOptions.GroupDesc.Add(false);
                    }
                }
            }, false);
        }

        protected IList<TValue> WrapperInArray<TValue>(OneOf<TValue, IList<TValue>> val)
        {
            if (val.Value == null)
            {
                return new List<TValue>();
            }

            return val.Match(
                t1 => new List<TValue>
                {
                    t1
                },
                t2 => t2
                );
        }

        public (IList<string> by, IList<bool> desc, int page) Toggle(string key, IList<string> oldBy, IList<bool> oldDesc, int page, bool mustSort, bool multiSort)
        {
            var by = oldBy;
            var desc = oldDesc;
            var byIndex = oldBy.IndexOf(key);

            if (byIndex < 0)
            {
                if (!multiSort)
                {
                    by = new List<string>();
                    desc = new List<bool>();
                }

                by.Add(key);
                desc.Add(false);
            }
            else if (byIndex >= 0 && !desc[byIndex])
            {
                desc[byIndex] = true;
            }
            else if (!mustSort)
            {
                by.RemoveAt(byIndex);
                desc.RemoveAt(byIndex);
            }
            else
            {
                desc[byIndex] = false;
            }

            //TODO:reset page to 1

            return (by, desc, page);
        }

        public void Group(string key)
        {
            var (groupBy, groupDesc, page) = Toggle(key, InternalOptions.GroupBy, InternalOptions.GroupDesc, InternalOptions.Page, true, false);

            UpdateOptions(options =>
            {
                options.GroupDesc = groupDesc;
                options.GroupBy = groupBy;
                options.Page = page;
            });
        }

        public void Sort(string key)
        {
            var (sortBy, sortDesc, page) = Toggle(key, InternalOptions.SortBy, InternalOptions.SortDesc, InternalOptions.Page, InternalOptions.MustSort, InternalOptions.MultiSort);

            UpdateOptions(options =>
            {
                options.SortDesc = sortDesc;
                options.SortBy = sortBy;
                options.Page = page;
            });
        }

        public void SortArray(IList<string> sortBy)
        {
            throw new NotImplementedException();
        }

        public void UpdateOptions(Action<DataOptions> options, bool emit = true)
        {
            options?.Invoke(InternalOptions);

            InternalOptions.Page = ServerItemsLength < 0
                ? Math.Max(1, Math.Min(InternalOptions.Page, PageCount))
                : InternalOptions.Page;

            if (OnOptionsUpdate.HasDelegate && emit)
            {
                OnOptionsUpdate.InvokeAsync(InternalOptions);
            }
        }

        public IEnumerable<IGrouping<string, TItem>> GroupItems(IEnumerable<TItem> items)
        {
            return CustomGroup(items, ItemValues, InternalOptions.GroupBy, InternalOptions.GroupDesc);
        }

        public IEnumerable<TItem> SortItems(IEnumerable<TItem> items)
        {
            var sortBy = new List<string>();
            var sortDesc = new List<bool>();

            if (!DisableSort)
            {
                sortBy = InternalOptions.SortBy.ToList();
                sortDesc = InternalOptions.SortDesc.ToList();
            }

            if (InternalOptions.GroupBy.Count > 0)
            {
                sortBy.InsertRange(0, InternalOptions.GroupBy);
                sortDesc.InsertRange(0, InternalOptions.GroupDesc);
            }

            return CustomSort(items, ItemValues, sortBy, sortDesc, Locale);
        }

        public IEnumerable<TItem> PaginateItems(IEnumerable<TItem> items)
        {
            if (ServerItemsLength == -1 && items.Count() <= PageStart)
            {
                InternalOptions.Page = Math.Max(1, (int)Math.Ceiling(items.Count() / (InternalOptions.ItemsPerPage * 1.0)));
            }

            return items.Skip(PageStart).Take(PageStop - PageStart);
        }
    }
}
