using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorComponent.Components.Core.CssProcess;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.RenderTree;

namespace BlazorComponent
{
    public abstract class BDomComponentBase : BComponentBase
    {
        private string _class;
        private string _style;

        [Inject]
        private IComponentIdGenerator ComponentIdGenerator { get; set; }

        [Parameter]
        public string Id { get; set; }

        private ElementReference _ref;

        /// <summary>
        /// Returned ElementRef reference for DOM element.
        /// </summary>
        public virtual ElementReference Ref
        {
            get => _ref;
            set
            {
                _ref = value;
                RefBack?.Set(value);
            }
        }

        protected CssBuilder CssBuilder { get; } = new CssBuilder();

        protected StyleBuilder StyleBuilder { get; } = new StyleBuilder();

        public BDomComponentBase()
        {
            CssBuilder.Add(() => Class);
            StyleBuilder.Add(() => Style);
        }

        protected override void OnInitialized()
        {
            Id ??= ComponentIdGenerator.Generate(this);
            base.OnInitialized();
        }

        /// <summary>
        /// Specifies one or more class names for an DOM element.
        /// </summary>
        [Parameter]
        public string Class
        {
            get => _class;
            set
            {
                _class = value;
                CssBuilder.OriginalClass = value;
            }
        }

        /// <summary>
        /// Specifies an inline style for an DOM element.
        /// </summary>
        [Parameter]
        public string Style
        {
            get => _style;
            set
            {
                _style = value;
                StyleBuilder.OriginalStyle = value;
                StateHasChanged(); // TODO: need this?
            }
        }

        /// <summary>
        /// Custom attributes
        /// </summary>
        [Parameter(CaptureUnmatchedValues = true)]
        public IDictionary<string, object> Attributes { get; set; }

        protected virtual Dictionary<string, object> StaticAttributes { get; } = new();

        public abstract void SetComponentClass();

        protected override Task OnInitializedAsync()
        {
            SetComponentClass();

            return base.OnInitializedAsync();
        }

        protected virtual string GenerateStyle()
        {
            return Style;
        }

        /// <summary>
        /// Gets text in ChildContent.
        /// </summary>
        /// <param name="childContent">The child content of <see cref="RenderFragment"/></param>
        /// <returns>The text.</returns>
        protected string GetChildContentText(RenderFragment childContent)
        {
            var builder = new RenderTreeBuilder();
            childContent(builder);
            BuildRenderTree(builder);

            // TODO: will be changed next release version!

            var frame = builder.GetFrames().Array.FirstOrDefault(u => u.FrameType == RenderTreeFrameType.Text);

            return frame.TextContent;
        }
    }
}
