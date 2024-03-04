using System.Text.Json;

namespace BlazorComponent
{
    public partial class BOtpInput : BDomComponentBase, IOtpInput, IAsyncDisposable
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

        private int _prevLength;

        private int _prevFocusIndex;

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

        private DotNetObjectReference<Invoker<OtpJsResult>> _handle;
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _handle = DotNetObjectReference.Create(new Invoker<OtpJsResult>(GetResultFromJs));
                await JsInvokeAsync(JsInteropConstants.RegisterOTPInputOnInputEvent, InputRefs, _handle);
            }

            await base.OnAfterRenderAsync(firstRender);
        }

        public async Task GetResultFromJs(OtpJsResult result)
        {
            switch (result.type)
            {
                case "Backspace":
                case "Input":
                    await ApplyValues(result.index, result.value);
                    if (ValueChanged.HasDelegate)
                    {
                        await ValueChanged.InvokeAsync(Value);
                    }

                    if (result.type == "input" && OnInput.HasDelegate)
                    {
                        await OnInput.InvokeAsync(result.value);
                    }

                    if (result.index >= Length - 1 && !Values.Any(p => string.IsNullOrEmpty(p)) && OnFinish.HasDelegate)
                    {
                        await OnFinish.InvokeAsync(Value);
                    }
                    break;
                default:
                    break;
            }
        }

        private async Task FocusAsync(int index)
        {
            if (_prevFocusIndex != index)
            {
                _prevFocusIndex = index;

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
                    var containsActiveElement = await JsInvokeAsync<bool>(JsInteropConstants.ContainsActiveElement, Ref);

                    if (containsActiveElement)
                    {
                        var item = InputRefs[index];
                        await JsInvokeAsync(JsInteropConstants.Focus, item);
                    }
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
            var temp = new List<string>(Values.ToArray());
            temp[index] = value;
            temp.RemoveAll(p => string.IsNullOrEmpty(p));

            Values[index] = value;

            await Task.Yield();
            await InvokeAsync(StateHasChanged);

            Values[index] = String.Empty;

            Values = temp;

            var count = temp.Count;

            for (int i = 0; i < this.Length - count; i++)
            {
                Values.Add(String.Empty);
            }

            await InvokeAsync(StateHasChanged);
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

        async ValueTask IAsyncDisposable.DisposeAsync()
        {
            try
            {
                _handle.Dispose();
                await JsInvokeAsync(JsInteropConstants.UnregisterOTPInputOnInputEvent, InputRefs);
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}
