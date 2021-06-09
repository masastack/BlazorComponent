using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BPagination : BDomComponentBase
    {
        [Parameter]
        public int Value { get; set; }

        [Parameter]
        public EventCallback<int> ValueChanged { get; set; }

        [Parameter]
        public EventCallback<int> OnInput { get; set; }

        [Parameter]
        public EventCallback OnPrevious { get; set; }

        [Parameter]
        public EventCallback OnNext { get; set; }

        [Parameter]
        public int Length { get; set; }

        [Parameter]
        public string NextIcon { get; set; } = "mdi-chevron-right";

        [Parameter]
        public string PrevIcon { get; set; } = "mdi-chevron-left";

        [Parameter]
        public StringNumber TotalVisible { get; set; }

        [Parameter]
        public string Color { get; set; } = "primary";

        protected bool PrevDisabled => Value <= 1;

        protected bool NextDisabled => Value >= Length;

        protected async Task HandlePreviousAsync(MouseEventArgs args)
        {
            if (PrevDisabled)
            {
                return;
            }

            Value--;
            if (ValueChanged.HasDelegate)
            {
                await ValueChanged.InvokeAsync(Value);
            }

            if (OnInput.HasDelegate)
            {
                await OnInput.InvokeAsync(Value);
            }

            if (OnPrevious.HasDelegate)
            {
                await OnPrevious.InvokeAsync();
            }
        }

        protected async Task HandleNextAsync(MouseEventArgs args)
        {
            if (NextDisabled)
            {
                return;
            }

            Value++;
            if (ValueChanged.HasDelegate)
            {
                await ValueChanged.InvokeAsync(Value);
            }

            if (OnInput.HasDelegate)
            {
                await OnInput.InvokeAsync(Value);
            }

            if (OnNext.HasDelegate)
            {
                await OnNext.InvokeAsync();
            }
        }

        protected async Task HandleItemClickAsync(StringNumber item)
        {
            Value = item.AsT1;
            if (ValueChanged.HasDelegate)
            {
                await ValueChanged.InvokeAsync(Value);
            }

            if (OnInput.HasDelegate)
            {
                await OnInput.InvokeAsync(item.AsT1);
            }
        }

        public IEnumerable<StringNumber> GetItems()
        {
            if (TotalVisible != null && TotalVisible.ToInt32() == 0)
            {
                return Enumerable.Empty<StringNumber>();
            }

            var maxLength = Math.Min(Math.Max(0, TotalVisible == null ? Length : TotalVisible.ToInt32()), Length);
            if (Length <= maxLength)
            {
                return Range(1, Length);
            }

            var items = new List<StringNumber>();

            var even = maxLength % 2 == 0 ? 1 : 0;
            var left = Convert.ToInt32(Math.Floor(maxLength / 2M));
            var right = Length - left + 1 + even;

            if (Value > left && Value < right)
            {
                var start = Value - left + 2;
                var end = Value + left - 2 - even;

                items.Add(1);
                items.Add("...");
                items.AddRange(Range(start, end));
                items.Add("...");
                items.Add(Length);

                return items;
            }
            else if (Value == left)
            {
                var end = Value + left - 1 - even;

                items.AddRange(Range(1, end));
                items.Add("...");
                items.Add(Length);

                return items;
            }
            else if (Value == right)
            {
                var start = Value - left + 1;
                items.Add(1);
                items.Add("...");
                items.AddRange(Range(start, Length));

                return items;
            }
            else
            {
                items.AddRange(Range(1, left));
                items.Add("...");
                items.AddRange(Range(right, Length));

                return items;
            }
        }

        public static IEnumerable<StringNumber> Range(int start, int end)
        {
            return Enumerable.Range(start, end - start + 1).Select(r => (StringNumber)r);
        }
    }
}
