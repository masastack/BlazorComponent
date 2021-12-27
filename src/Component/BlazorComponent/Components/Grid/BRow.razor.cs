using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorComponent.Web;
using Microsoft.AspNetCore.Components;
using OneOf;

namespace BlazorComponent
{
    /*
     * Possible values and meaning
     * int                                                  - horizontal gutter
     * Dictionary<string, int>                              - horizontal gutters for different screen sizes
     * (int, int)                                           - horizontal gutter, vertical gutter
     * (Dictionary<string, int>, int)                       - horizontal gutters for different screen sizes, vertical gutter
     * (int, Dictionary<string, int>)                       - horizontal gutter, vertical gutter for different screen sizes
     * (Dictionary<string, int>, Dictionary<string, int>)   - horizontal gutters for different screen sizes, vertical gutter for different screen sizes
     */

    using GutterType =
        OneOf<int, Dictionary<string, int>, (int horizontalGutter, int verticalGutter), (Dictionary<string, int>
            horizontalGutterSizes, int verticalGutter), (int horizontalGutter, Dictionary<string, int>
            verticalGutterSizes),
            (Dictionary<string, int> horizontalGutterSizes, Dictionary<string, int> verticalGutterSizes)>;

    public abstract partial class BRow : BDomComponentBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public string Type { get; set; }

        [Parameter]
        public virtual string Tag { get; set; } = "div";

        /// <summary>
        /// 'start' | 'center' | 'end' | 'baseline ' | 'stretch '
        /// </summary>
        [Parameter]
        public StringEnum<AlignTypes> Align { get; set; }

        /// <summary>
        /// 'start', 'end', 'center','space-between', 'space-around', 'stretch'
        /// </summary>
        [Parameter]
        public StringEnum<AlignContentTypes> AlignContent { get; set; }

        /// <summary>
        /// 'start' | 'end' | 'center' | 'space-around' | 'space-between'
        /// </summary>
        [Parameter]
        public StringEnum<JustifyTypes> Justify { get; set; }

        [Parameter]
        public bool Wrap { get; set; } = true;

        [Parameter]
        public GutterType Gutter { get; set; }

        [Parameter]
        public EventCallback<BreakpointType> OnBreakpoint { get; set; }

        /// <summary>
        /// Used to set gutter during pre-rendering
        /// </summary>
        [Parameter]
        public BreakpointType DefaultBreakpoint { get; set; }

        [Inject]
        public DomEventJsInterop DomEventJsInterop { get; set; }

        private string GutterStyle { get; set; }

        public IList<BCol> Cols { get; } = new List<BCol>();

        private static BreakpointType[] _breakpoints = new[]
        {
            BreakpointTypes.Xs,
            BreakpointTypes.Sm,
            BreakpointTypes.Md,
            BreakpointTypes.Lg,
            BreakpointTypes.Xl,
            BreakpointTypes.Xxl
        };

        protected override async Task OnInitializedAsync()
        {
            if (DefaultBreakpoint != null)
            {
                SetGutterStyle(DefaultBreakpoint.Name);
            }

            await base.OnInitializedAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                var dimensions = await JsInvokeAsync<Window>(JsInteropConstants.GetWindow);
                DomEventJsInterop.AddEventListener<Window>("window", "resize", OnResize, false);
                OptimizeSize(dimensions.InnerWidth);
            }

            await base.OnAfterRenderAsync(firstRender);
        }

        private void OnResize(Window window)
        {
            _ = Task.Run(() => OptimizeSize(window.InnerWidth));
        }

        private void OptimizeSize(double windowWidth)
        {
            BreakpointType actualBreakpoint = _breakpoints[_breakpoints.Length - 1];
            for (int i = 0; i < _breakpoints.Length; i++)
            {
                if (windowWidth <= _breakpoints[i].Width && (windowWidth >= (i > 0 ? _breakpoints[i - 1].Width : 0)))
                {
                    actualBreakpoint = _breakpoints[i];
                }
            }

            SetGutterStyle(actualBreakpoint.Name);

            if (OnBreakpoint.HasDelegate)
            {
                OnBreakpoint.InvokeAsync(actualBreakpoint);
            }

            InvokeStateHasChanged();
        }

        private void SetGutterStyle(string breakPoint)
        {
            var gutter = GetGutter(breakPoint);

            Cols.ForEach(x => x.RowGutterChanged(gutter));

            GutterStyle = "";
            if (gutter.horizontalGutter > 0)
            {
                GutterStyle =
                    $"margin-left: -{gutter.horizontalGutter / 2}px; margin-right: -{gutter.horizontalGutter / 2}px; ";
            }

            GutterStyle += $"row-gap: {gutter.verticalGutter}px; ";

            InvokeStateHasChanged();
        }

        private (int horizontalGutter, int verticalGutter) GetGutter(string breakPoint)
        {
            GutterType gutter = 0;
            if (Gutter.Value != null)
                gutter = Gutter;

            return gutter.Match(
                num => (num, 0),
                dic => breakPoint != null && dic.ContainsKey(breakPoint) ? (dic[breakPoint], 0) : (0, 0),
                tuple => tuple,
                tupleDicInt => (tupleDicInt.horizontalGutterSizes.ContainsKey(breakPoint) ? tupleDicInt.horizontalGutterSizes[breakPoint] : 0,
                    tupleDicInt.verticalGutter),
                tupleIntDic => (tupleIntDic.horizontalGutter,
                    tupleIntDic.verticalGutterSizes.ContainsKey(breakPoint) ? tupleIntDic.verticalGutterSizes[breakPoint] : 0),
                tupleDicDic => (tupleDicDic.horizontalGutterSizes.ContainsKey(breakPoint) ? tupleDicDic.horizontalGutterSizes[breakPoint] : 0,
                    tupleDicDic.verticalGutterSizes.ContainsKey(breakPoint) ? tupleDicDic.verticalGutterSizes[breakPoint] : 0)
            );
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            DomEventJsInterop.RemoveEventListener<Window>("window", "resize", OnResize);
        }
    }
}