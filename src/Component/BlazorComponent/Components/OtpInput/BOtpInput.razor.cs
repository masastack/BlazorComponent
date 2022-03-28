using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    public partial class BOtpInput : BDomComponentBase, IOtpInput
    {
        [CascadingParameter(Name = "IsDark")]
        public bool CascadingIsDark { get; set; }

        [Parameter]
        public int Length { get; set; } = 6;

        [Parameter]
        public OtpInputType Type { get; set; } = OtpInputType.Text;

        [Parameter]
        public bool Readonly { get; set; }

        [Parameter]
        public bool Disabled { get; set; }

        [Parameter]
        public string Value
        {
            get
            {
                return string.Join("", Values);
            }
            set
            {
                if (value != null)
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

        [Parameter]
        public EventCallback<string> OnFinish { get; set; }

        [Parameter]
        public EventCallback<string> OnInput { get; set; }

        [Parameter]
        public bool Dark { get; set; }

        [Parameter]
        public bool Light { get; set; }

        public List<ElementReference> InputRefs { get; set; } = new();

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

        protected List<string> Values { get; set; } = new();

        private int _prevLength;
        
        protected override async Task OnParametersSetAsync()
        {
            if (_prevLength != Length)
            {
                _prevLength = Length;

                if (Values.Count > Length)
                {
                    for (int i = Length; i < Values.Count; i++)
                    {
                        Values.RemoveAt(i);
                    }

                    if (ValueChanged.HasDelegate)
                    {
                        await ValueChanged.InvokeAsync(Value);
                    }
                }
                else
                {
                    for (int i = 0; i < Length; i++)
                    {
                        if (Values.Count() < (i + 1))
                            Values.Add(string.Empty);
                        if (InputRefs.Count() < (i + 1))
                            InputRefs.Add(new ElementReference());
                    }
                }
            }
            await base.OnParametersSetAsync();
        }

        private async Task FocusAsync(int index)
        {
            if (index < 0)
            {
                await FocusAsync(0);
            }
            else if (index >= Length)
            {
                await FocusAsync(Length - 1);
            }
            else
            {
                var item = InputRefs[index];
                await item.FocusAsync();
            }
        }

        public async Task OnKeyUpAsync(BOtpInputEventArgs<KeyboardEventArgs> events)
        {
            if (events.Index < Length)
            {
                var eventKey = events.Args.Key;

                if (eventKey == "ArrowLeft" || (eventKey == "Backspace"))
                {
                    if (eventKey == "Backspace")
                    {
                        await ApplyValues(events.Index, "");
                        if (ValueChanged.HasDelegate)
                        {
                            await ValueChanged.InvokeAsync(Value);
                        }
                    }
                    await FocusAsync(events.Index - 1);
                }
                else if (eventKey == "ArrowRight")
                {
                    await FocusAsync(events.Index + 1);
                }
            }
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

        private async Task ApplyValues(int index, string value)
        {
            Values[index] = value;

            var tempStr = string.Join("", this.Values);

            var temp = tempStr.Select(p => p.ToString()).ToList();

            for (int i = 0; i < Values.Count; i++)
            {
                Values[i] = String.Empty;
            }

            await Task.Yield();

            for (int i = 0; i < temp.Count; i++)
            {
                Values[i] = temp[i];
            }

            for (int i = temp.Count; i < this.Length - temp.Count; i++)
            {
                Values[i] = String.Empty;
            }
        }

        public async Task OnInputAsync(BOtpInputEventArgs<ChangeEventArgs> events)
        {
            if (!string.IsNullOrWhiteSpace(events.Args.Value.ToString()))
            {
                var firstEmptyItemIndex = GetFirstEmptyIndex();

                var temp = Values[events.Index];

                var inputValue = events.Args.Value.ToString();

                var writeIndex = Math.Min(firstEmptyItemIndex, events.Index);

                if (writeIndex != events.Index)
                {
                    Values[events.Index] = inputValue;

                    //https://stackoverflow.com/questions/68901935/reset-input-field-value-if-value-is-invalid-in-blazor
                    await Task.Yield();

                    Values[events.Index] = String.Empty;

                    Values[firstEmptyItemIndex] = inputValue;
                }
                else
                {
                    Values[events.Index] = String.Empty;

                    await Task.Yield();

                    if (inputValue.Length > 1)
                    {
                        Values[events.Index] = temp;
                        return;
                    }

                    Values[events.Index] = inputValue;
                }

                if (ValueChanged.HasDelegate)
                {
                    await ValueChanged.InvokeAsync(Value);
                }

                if (OnInput.HasDelegate)
                    await OnInput.InvokeAsync(events.Args.Value.ToString());

                if (writeIndex + 1 < this.Length)
                {
                    await FocusAsync((writeIndex + 1));
                }

                if (!Values.Any(p => string.IsNullOrEmpty(p)))
                {
                    if (OnFinish.HasDelegate)
                    {
                        await OnFinish.InvokeAsync(Value);
                    }
                }

            }
        }

        public async Task OnPasteAsync(BOtpInputEventArgs<PasteWithDataEventArgs> events)
        {
            var clipboardData = events.Args.PastedData;

            if (!string.IsNullOrWhiteSpace(clipboardData))
            {
                var firstEmptyIndex = GetFirstEmptyIndex();

                var startIndex = Math.Min(firstEmptyIndex, events.Index);

                for (int i = 0; i < clipboardData.Length; i++)
                {
                    var changeIndex = startIndex + i;

                    if (changeIndex >= this.Length)
                        break;

                    Values[changeIndex] = clipboardData[i].ToString();
                }

                var newFocusIndex = Math.Min(events.Index + clipboardData.Length - 1, this.Length - 1);
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

        public async Task OnFocusAsync(int index)
        {
            if (index >= 0 && index <= Length)
            {
                var item = InputRefs[index];
                await JsInvokeAsync(JsInteropConstants.Select, item);
            }
        }
    }
}
