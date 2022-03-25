using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    public partial class BOtpInput : BDomComponentBase, IOtpInput
    {
        [Parameter]
        public StringNumber Length { get; set; } = "6";

        [Parameter]
        public string Type { get; set; } = "text";

        [Parameter]
        public bool ReadOnly { get; set; }

        [Parameter]
        public bool Disabled { get; set; }

        [Parameter]
        public bool Dark { get; set; }

        public bool Light { get; set; } = true;

        protected List<string> Values { get; set; } = new();

        [Parameter]
        public string Value
        {
            get
            {
                return string.Join("", Values);
            }
            set
            {
                if(value != null)
                {
                    for (int i = 0; i < value.Length; i++)
                    {
                        if (Values.Count < i + 1)
                            Values.Add(value[i].ToString());
                        else
                            Values[i] = value[i].ToString();
                    }
                }
            }
        }

        [Parameter]
        public EventCallback<string> ValueChanged { get; set; }

        [Parameter]
        public bool Plain { get; set; }

        public List<ElementReference> Elements { get; set; } = new();

        [Parameter]
        public EventCallback<string> OnFinish { get; set; }

        [Parameter]
        public EventCallback<string> OnInput { get; set; }

        //[Parameter]
        //public EventCallback<string> OnChange { get; set; }

        protected override Task OnInitializedAsync()
        {
            for (int i = 0; i < Length.ToInt32(); i++)
            {
                //SlotItems.Add(new BOtpInputSlotItem());
                if (Values.Count() < (i + 1))
                    Values.Add(string.Empty);
                Elements.Add(new ElementReference());
            }

            return base.OnInitializedAsync();
        }

        private async Task FocusAsync(int index)
        {
            if (index < 0)
            {
                await FocusAsync(0);
            }
            else if(index >= Length.ToInt32())
            {
                await FocusAsync(Length.ToInt32() - 1);
            }
            else
            {
                //var item = SlotItems[index];
                //await item.Element.FocusAsync();

                var item = Elements[index];
                await item.FocusAsync();
            }
        }

        public async Task OnKeyUpAsync(BOtpInputKeyboardEventArgs events)
        {   
            var errorInput = new List<string>() { "Tab", "Shift", "Meta", "Control", "Alt", "Delete" };

            if (events.Index < Length.ToInt32())
            {
                var eventKey = events.Args.Key;

                if (errorInput.Any(p => p == eventKey))
                {
                    return;
                }
                else if (eventKey == "ArrowLeft" || (eventKey == "Backspace"))
                {
                    if (eventKey == "Backspace")
                    {
                        await ApplyValues(Values, events.Index, "");
                        if (ValueChanged.HasDelegate)
                        {
                            await ValueChanged.InvokeAsync(Value);
                        }
                    }
                    await FocusAsync(events.Index - 1);
                    return;
                }
                else if (eventKey == "ArrowRight")
                {
                    await FocusAsync(events.Index + 1);
                    return;
                }
            }
            return;
        }

        private int GetFirstEmptyIndex()
        {
            for (int i = 0; i < Values.Count; i++)
            {
                if (string.IsNullOrEmpty(Values[i]))
                {
                    return i;
                }
            }

            return Values.Count - 1;
        }

        private async Task ApplyValues(List<string> values, int index, string value)
        {
            values[index] = value;

            var tempStr = string.Join("", (IEnumerable<string>)this.Values);

            var temp = tempStr.Select(p=> p.ToString()).ToList();

            for (int i = 0; i < values.Count; i++)
            {
                values[i] = String.Empty;
            }

            await Task.Yield();

            for (int i = 0; i < temp.Count; i++)
            {
                values[i] = temp[i];
            }

            for (int i = temp.Count; i < this.Length.ToInt32() - temp.Count; i++)
            {
                values[i] = String.Empty;
            }

            return;
        }

        public async Task OnInputAsync(BOtpInputChangeEventArgs events)
        {
            if (!string.IsNullOrWhiteSpace(events.Args.Value.ToString()))
            {
                var firstEmptyItemIndex = GetFirstEmptyIndex();

                var temp = Values[events.Index];

                //https://stackoverflow.com/questions/68901935/reset-input-field-value-if-value-is-invalid-in-blazor
                var inputValue = events.Args.Value.ToString();

                if (inputValue.Length > 1)
                {
                    Values[events.Index] = String.Empty;

                    await Task.Yield();

                    Values[events.Index] = temp;

                    return;
                }
                
                Values[events.Index] = events.Args.Value.ToString();

                await Task.Yield();

                var writeIndex = Math.Min(firstEmptyItemIndex, events.Index);

                if(writeIndex != events.Index)
                {
                    Values[events.Index] = String.Empty;
                    Values[firstEmptyItemIndex] = events.Args.Value.ToString();
                }

                if (OnInput.HasDelegate)
                    await OnInput.InvokeAsync(events.Args.Value.ToString());

                if (writeIndex + 1 < this.Length.ToInt32())
                {
                    await FocusAsync((writeIndex + 1));

                    //var item = SlotItems[writeIndex];
                    //if (OnChange.HasDelegate && item.Value != events.Args.Value.ToString())
                    //    await OnChange.InvokeAsync(events.Args.Value.ToString());

                }

                if (!Values.Any(p => string.IsNullOrEmpty(p)))
                {
                    if (OnFinish.HasDelegate)
                    {
                        await OnFinish.InvokeAsync(Value);
                    }  
                }

                if (ValueChanged.HasDelegate)
                {
                    await ValueChanged.InvokeAsync(Value);
                }
            }
            
            return;
        }

        public async Task OnPasteAsync(BOtpInputPasteWithDataEventArgs events)
        {
            var clipboardData = events.Args.PastedData;

            if (!string.IsNullOrWhiteSpace(clipboardData))
            {
                var firstEmptyIndex = GetFirstEmptyIndex();

                var startIndex = Math.Min(firstEmptyIndex, events.Index);

                for (int i = 0; i < clipboardData.Length; i++)
                {
                    var changeIndex = startIndex + i;

                    if (changeIndex >= this.Length.ToInt32())
                        break;

                    Values[changeIndex] = clipboardData[i].ToString();
                }

                var newFocusIndex = Math.Min(events.Index + clipboardData.Length - 1, this.Length.ToInt32() - 1);
                await FocusAsync(newFocusIndex);

                var hasEmptyValue = Values.Any(p => string.IsNullOrWhiteSpace(p));

                if (!hasEmptyValue)
                {
                    if (OnFinish.HasDelegate)
                    {
                        await OnFinish.InvokeAsync(Value);
                    }
                        
                }
            }
        }

        public bool IsDark
        {
            get
            {
                if (Dark)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public async Task OnFocusAsync(int index)
        {
            if (index >= 0 && index <= Length.ToInt32())
            {
                //var item = SlotItems[index];
                //await JsInvokeAsync(JsInteropConstants.Select, item.Element);

                var item = Elements[index];
                await JsInvokeAsync(JsInteropConstants.Select, item);
            }
        }
    }
}
