using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorComponent.Web;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.JSInterop;

namespace BlazorComponent
{
    public abstract partial class BMenu : BMenuable
    {
        private readonly int _defaultOffset = 8;

        private string _initialAbsoluteUri;

        protected string CalculatedLeft
        {
            get
            {
                var menuWidth = Math.Max(Dimensions.content.Width, NumberHelper.ParseDouble(CalculatedMinWidth));

                if (!Auto) return CalcLeft(menuWidth) ?? "0";

                return ((StringNumber)CalcXOverflow(CalcLeftAuto(), menuWidth)).ConvertToUnit() ?? "0";
            }
        }

        protected string CalculatedMaxHeight => Auto ? "200px" : MaxHeight.ConvertToUnit();

        protected string CalculatedMaxWidth => MaxWidth.ConvertToUnit();

        protected string CalculatedMinWidth
        {
            get
            {
                if (MinWidth != null)
                {
                    return MinWidth.ConvertToUnit();
                }

                var nudgeWidth = 0d;
                if (NudgeWidth != null)
                {
                    (_, nudgeWidth) = NudgeWidth.TryGetNumber();
                }

                var minWidth = Math.Min(
                    Dimensions.activator.Width + nudgeWidth + (Auto ? 16 : 0),
                    Math.Max(PageWidth - 24, 0));

                double calculatedMaxWidth;
                if (NumberHelper.TryParseDouble(CalculatedMaxWidth, out var value))
                {
                    calculatedMaxWidth = value;
                }
                else
                {
                    calculatedMaxWidth = minWidth;
                }

                return ((StringNumber)Math.Min(calculatedMaxWidth, minWidth)).ConvertToUnit();
            }
        }

        protected string CalculatedTop => !Auto ? CalcTop() : ((StringNumber)CalcYOverflow(CalcTopAuto())).ConvertToUnit();

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public bool Auto { get; set; }

        [Parameter]
        public bool CloseOnClick { get; set; } = true;

        [Parameter]
        public bool CloseOnContentClick { get; set; } = true;

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public string ContentClass { get; set; }

        [Parameter]
        public string ContentStyle { get; set; }

        [Parameter]
        public bool DisableKeys { get; set; }

        [Parameter]
        public StringNumber MaxHeight { get; set; } = "auto";

        [Parameter]
        public EventCallback<MouseEventArgs> OnOutsideClick { get; set; }

        [Parameter]
        public string Origin { get; set; }

        [Parameter]
        public StringBoolean Rounded { get; set; }

        [Parameter]
        public bool Tile { get; set; }

        [Parameter]
        public string Transition { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            NavigationManager.LocationChanged += OnLocationChanged;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                DomEventJsInterop.ResizeObserver<Dimensions[]>(ActivatorSelector, ObserveSizeChange);

                if (!OpenOnHover)
                {
                    await JsInvokeAsync(JsInteropConstants.AddOutsideClickEventListener,
                        DotNetObjectReference.Create(new Invoker<object>(OutsideClick)),
                        new[] {GetContent().Selector, ActivatorSelector});
                }
            }
        }

        protected virtual Task DelContentFrom()
        {
            return Task.CompletedTask;
        }

        protected override Task Activate(Action lazySetter)
        {
            return UpdateDimensions(lazySetter);
        }

        protected override Dictionary<string, EventCallback<KeyboardEventArgs>> GenActivatorKeyboardListeners()
        {
            var listeners = base.GenActivatorKeyboardListeners();

            if (DisableKeys && listeners.ContainsKey("keydown"))
            {
                listeners.Remove("keydown");
            }

            return listeners;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            NavigationManager.LocationChanged -= OnLocationChanged;
        }

        protected virtual void OnContentClick(MouseEventArgs args)
        {
            // TODO: if clicked element attribute contains 'disabled', return
            if (CloseOnContentClick)
            {
                Value = false;
            }
            else
            {
                PreventRender();
            }
        }

        protected virtual void OnContentMouseenter(MouseEventArgs args)
        {
            if (!Disabled && OpenOnHover)
            {
                if (Value) return;

                Value = true;
            }
            else
            {
                PreventRender();
            }
        }

        protected virtual void OnContentMouseleave(MouseEventArgs args)
        {
            if (OpenOnHover)
            {
                Value = false;
            }
            else
            {
                PreventRender();
            }
        }

        private double CalcTopAuto()
        {
            if (OffsetY)
            {
                return ComputedTop;
            }

            // ignores some code about List

            return ComputedTop - 1;
        }

        private double CalcLeftAuto()
        {
            return Dimensions.activator.Left - _defaultOffset * 2;
        }

        private HtmlElement GetContent() => Document.QuerySelector(ContentRef);

        private void ObserveSizeChange(Dimensions[] _)
        {
            UpdateDimensions();
        }

        private void OnLocationChanged(object sender, LocationChangedEventArgs e)
        {
            if (_initialAbsoluteUri == null)
            {
                _initialAbsoluteUri = new Uri(e.Location).AbsolutePath;
            }
            else
            {
                var absolutePath = new Uri(e.Location).AbsolutePath;

                if (absolutePath == _initialAbsoluteUri) return;

                object selectors = new[]
                {
                    GetContent().Selector,
                    ActivatorSelector
                };

                JsInvokeAsync(JsInteropConstants.RemoveOutsideClickEventListener, selectors);

                DelContentFrom();
            }
        }

        private async Task OutsideClick(object _)
        {
            if (Value)
            {
                await OnOutsideClick.InvokeAsync();
            }

            if (CloseOnClick)
            {
                Value = false;
            }

            await InvokeStateHasChangedAsync();
        }
    }
}