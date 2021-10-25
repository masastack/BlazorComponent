using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace BlazorComponent
{
    public class Element : ComponentBase
    {
        private bool _firstElement;

        public Element()
        {
            Watcher = new(GetType());
        }

        [Parameter]
        public string Tag { get; set; } = "div";

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        //TODO: remove this
        [Parameter]
        public Action<ElementReference> ReferenceCaptureAction { get; set; }

        [Parameter]
        public string Class { get; set; }

        [Parameter]
        public string Style { get; set; }

        [Parameter]
        public bool Show
        {
            get
            {
                return GetValue<bool>();
            }
            set
            {
                SetValue(value);
            }
        }

        [Parameter]
        public bool If
        {
            get
            {
                return GetValue<bool>();
            }
            set
            {
                SetValue(value);
            }
        }

        [CascadingParameter]
        public Transition Transition { get; set; }

        [Parameter(CaptureUnmatchedValues = true)]
        public IDictionary<string, object> ExtraAttributes { get; set; }

        public ElementReference Reference { get; private set; }

        protected Action<ElementReference> ComputedReferenceCaptureAction
        {
            get
            {
                if (ReferenceCaptureAction == null)
                {
                    return reference => Reference = reference;
                }

                return ReferenceCaptureAction;
            }
        }

        protected CssBuilder CssBuilder { get; } = new();

        protected StyleBuilder StyleBuilder { get; } = new();

        protected PropertyWatcher Watcher { get; }

        protected TValue GetValue<TValue>(TValue @default = default, [CallerMemberName] string name = null)
        {
            return Watcher.GetValue(@default, name);
        }

        protected void SetValue<TValue>(TValue value, [CallerMemberName] string name = null)
        {
            Watcher.SetValue(value, name);
        }

        public async Task UpdateViewAsync()
        {
            await InvokeAsync(StateHasChanged);
        }

        protected override void OnInitialized()
        {
            CssBuilder
                .Add(() => Class)
                .AddIf(() => Transition.Class, () => Transition != null && _firstElement);

            StyleBuilder
                .Add(() => Style)
                .AddIf(() => Transition.Style, () => Transition != null && _firstElement);

            if (Transition != null)
            {
                _firstElement = Transition.Register(this);
                if (_firstElement)
                {
                    Watcher
                        .Watch<bool>(nameof(Show), val =>
                        {
                            Transition.RunTransition(TransitionMode.Show, val);
                        })
                        .Watch<bool>(nameof(If), val =>
                        {
                            Transition.RunTransition(TransitionMode.If, val);
                        });
                }
            }
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            if (Transition == null || Transition.If)
            {
                var sequence = 0;
                builder.OpenElement(sequence++, Tag);

                builder.AddAttribute(sequence++, "class", CssBuilder.Class);
                builder.AddAttribute(sequence++, "style", StyleBuilder.Style);

                builder.AddMultipleAttributes(sequence++, ExtraAttributes);
                builder.AddContent(sequence++, ChildContent);

                builder.AddElementReferenceCapture(sequence++, ComputedReferenceCaptureAction);

                builder.CloseElement();
            }
        }
    }
}
