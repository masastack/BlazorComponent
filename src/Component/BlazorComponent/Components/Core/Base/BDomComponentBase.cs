using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorComponent.Components;
using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public abstract class BDomComponentBase : BComponentBase, IHasProviderComponent
    {
        private ElementReference _ref;

        public BDomComponentBase()
        {
            CssProvider.StaticClass = () => Class;
            CssProvider.StaticStyle = () => Style;
        }

        public ComponentCssProvider CssProvider { get; } = new();

        public ComponentAbstractProvider AbstractProvider { get; } = new();

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

        #region Prevent render before invoking ShouldRender

        /*
         * https://github.com/dotnet/aspnetcore/issues/18919
         * Keep until new api(@onmousemove:preventStateHasChanged) releases
         */

        private bool _preventRender = false;

        /// <summary>
        /// Prevent render before invoking ShouldRender
        /// </summary>
        protected void PreventRender() => _preventRender = true;

        protected override bool ShouldRender()
        {
            if (!_preventRender)
                return base.ShouldRender();

            _preventRender = false;
            return false;
        }

        #endregion
    }
}