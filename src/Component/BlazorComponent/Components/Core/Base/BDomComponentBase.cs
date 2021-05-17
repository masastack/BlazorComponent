using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

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
    }
}
