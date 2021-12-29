using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using BlazorComponent.Web;
using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BWindow : BItemGroup
    {
        public BWindow() : base(GroupType.Window)
        {
            Mandatory = true;
        }

        [Parameter]
        public bool Continuous { get; set; }

        [Parameter]
        public string NextIcon { get; set; }

        [Parameter]
        public RenderFragment<Action> NextContent { get; set; }

        [Parameter]
        public string PrevIcon { get; set; }

        [Parameter]
        public RenderFragment<Action> PrevContent { get; set; }

        // TODO: need transition
        [Parameter]
        public bool Reverse { get; set; }

        [Parameter]
        public bool ShowArrows { get; set; }

        [Parameter]
        public bool ShowArrowsOnHover { get; set; }

        // TODO: need transition
        [Parameter]
        public bool Vertical { get; set; }

        [Inject]
        public Document Document { get; set; }

        public bool ArrowsVisible => ShowArrowsOnHover || ShowArrows;

        public int TransitionCount { get; set; }

        public StringNumber TransitionHeight { get; set; }

        public bool IsActive => TransitionCount > 0;

        public int InternalIndex
        {
            get => GetValue<int>();
            set => SetValue(value);
        }

        public bool HasActiveItems => Items.Any(item => !item.Disabled);

        public bool HasNext => Continuous || InternalIndex < Items.Count - 1;

        public bool HasPrev => Continuous || InternalIndex > 0;

        protected override void OnInitialized()
        {
            base.OnInitialized();

            Watcher.Watch<int>(nameof(InternalIndex),
                (newVal, oldVal) => Reverse = UpdateReverse(newVal, oldVal));
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            InternalIndex = Items.FindIndex(item => item.Value == Value);
        }

        protected void Next()
        {
            if (!HasActiveItems || !HasNext) return;

            var nextIndex = GetNextIndex(InternalIndex);
            var nextItem = Items[nextIndex];

            if (ValueChanged.HasDelegate)
            {
                // TODO: whether need await
                ValueChanged.InvokeAsync(nextItem.Value);
            }
            else
            {
                Value = nextItem.Value;
            }
        }

        protected void Prev()
        {
            if (!HasActiveItems || !HasPrev) return;

            var prevIndex = GetPrevIndex(InternalIndex);
            var pervItem = Items[prevIndex];

            if (ValueChanged.HasDelegate)
            {
                // TODO: whether need await
                ValueChanged.InvokeAsync(pervItem.Value);
            }
            else
            {
                Value = pervItem.Value;
            }
        }

        protected int GetNextIndex(int currentIndex)
        {
            var nextIndex = (currentIndex + 1) % Items.Count;
            var nextItem = Items[nextIndex];

            if (nextItem.Disabled) return GetNextIndex(nextIndex);

            return nextIndex;
        }

        protected int GetPrevIndex(int currentIndex)
        {
            var prevIndex = (currentIndex + Items.Count - 1) % Items.Count;
            var prevItem = Items[prevIndex];

            if (prevItem.Disabled) return GetPrevIndex(prevIndex);

            return prevIndex;
        }

        private bool UpdateReverse(int val, int oldVal)
        {
            var itemsLength = Items.Count;
            var lastIndex = itemsLength - 1;

            if (itemsLength <= 2) return val < oldVal;

            if (val == lastIndex && oldVal == 0)
            {
                return true;
            }
            else if (val == 0 && oldVal == lastIndex)
            {
                return false;
            }
            else
            {
                return val < oldVal;
            }
        }
    }
}