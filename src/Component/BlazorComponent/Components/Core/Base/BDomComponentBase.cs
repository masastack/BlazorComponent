using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.RenderTree;

namespace BlazorComponent
{
    public abstract class BDomComponentBase : BComponentBase
    {
        private ElementReference _ref;

        public BDomComponentBase()
        {
            CssProvider.StaticClassProvider = () => Class;
            CssProvider.StaticStyleProvider = () => Style;
        }

        protected ComponentCssProvider CssProvider { get; } = new();

        protected ComponentSlotProvider SlotProvider { get; } = new();

        [Inject]
        public IComponentIdGenerator ComponentIdGenerator { get; set; }

        [Parameter]
        public string Id { get; set; }

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

        /// <summary>
        /// Specifies one or more class names for an DOM element.
        /// </summary>
        [Parameter]
        public string Class { get; set; }

        /// <summary>
        /// Specifies an inline style for an DOM element.
        /// </summary>
        [Parameter]
        public string Style { get; set; }

        /// <summary>
        /// Custom attributes
        /// </summary>
        [Parameter(CaptureUnmatchedValues = true)]
        public IDictionary<string, object> Attributes { get; set; } = new Dictionary<string, object>();

        protected override void OnInitialized()
        {
            Id ??= ComponentIdGenerator.Generate(this);
            base.OnInitialized();
        }

        protected override Task OnInitializedAsync()
        {
            SetComponentClass();
            return base.OnInitializedAsync();
        }

        protected virtual void SetComponentClass()
        {

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
