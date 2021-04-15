using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        protected CssBuilder cssBuilder { get; } = new CssBuilder();

        public BDomComponentBase()
        {
            cssBuilder.Add(() => Class);
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
                cssBuilder.OriginalClass = value;
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
                StateHasChanged();
            }
        }

        /// <summary>
        /// Custom attributes
        /// </summary>
        [Parameter(CaptureUnmatchedValues = true)]
        public IDictionary<string, object> Attributes { get; set; }

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
    }
}
