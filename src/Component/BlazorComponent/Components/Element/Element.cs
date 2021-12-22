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
    public class Element : BComponentBase
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
        public bool? Show
        {
            get
            {
                return GetValue<bool?>();
            }
            set
            {
                SetValue(value);
            }
        }

        [Parameter]
        public bool? If
        {
            get
            {
                return GetValue<bool?>();
            }
            set
            {
                SetValue(value);
            }
        }

        [Parameter]
        public object Key
        {
            get
            {
                return GetValue<object>();
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

        public ElementReference Reference { get; protected set; }

        protected Action<ElementReference> ComputedReferenceCaptureAction
        {
            get
            {
                if (ReferenceCaptureAction == null)
                {
                    return reference => Reference = reference;
                }

                return reference =>
                {
                    ReferenceCaptureAction(reference);
                    Reference = reference;
                };
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

        public virtual async Task UpdateViewAsync()
        {
            await InvokeAsync(StateHasChanged);
        }

        protected bool? InternalIf { get; set; } = true;

        protected override void OnInitialized()
        {
            CssBuilder
                .Add(() => Class)
                .AddIf(() => Transition.Class, () => Transition != null && _firstElement);

            StyleBuilder
                .Add(() => Style)
                .AddIf(() => Transition.Style, () => Transition != null && _firstElement)
                .AddIf("display:none", () => Transition == null && Show == false);

            if (Transition != null)
            {
                _firstElement = Transition.Register(this);
                if (_firstElement)
                {
                    if (If != null && Key != null)
                    {
                        InternalIf = If;
                    }

                    Watcher
                        .Watch<bool?>(nameof(Show), val =>
                        {
                            NextTick(() =>
                            {
                                //Dom may not ready,so we move transition to nextTick
                                Transition.RunTransition(TransitionMode.Show, val.Value);
                                return Task.CompletedTask;
                            });
                        })
                        .Watch<bool?>(nameof(If), val =>
                        {
                            NextTick(() =>
                            {
                                Transition.RunTransition(TransitionMode.If, val.Value);
                                return Task.CompletedTask;
                            });
                        })
                        .Watch<object>(nameof(Key), () =>
                        {
                            NextTick(() =>
                            {
                                InternalIf = !InternalIf;
                                Transition.RunTransition(TransitionMode.If, InternalIf.Value);

                                return Task.CompletedTask;
                            });
                        });
                }
            }
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            if (Transition == null || Transition.If != false)
            {
                var sequence = 0;
                builder.OpenElement(sequence++, Tag);

                builder.AddAttribute(sequence++, "class", CssBuilder.Class);
                builder.AddAttribute(sequence++, "style", StyleBuilder.Style);

                builder.AddMultipleAttributes(sequence++, ExtraAttributes);

                if (Key != null)
                {
                    builder.AddContent(sequence++, elBuilder =>
                    {
                        elBuilder.OpenComponent<ElementWrapper>(0);
                        elBuilder.AddAttribute(1, nameof(ElementWrapper.Value), InternalIf);
                        elBuilder.AddAttribute(2, nameof(ChildContent), ChildContent);
                        elBuilder.CloseComponent();
                    });
                }
                else
                {
                    builder.AddContent(sequence++, ChildContent);
                }

                builder.AddElementReferenceCapture(sequence++, ComputedReferenceCaptureAction);

                builder.CloseElement();
            }
        }
    }
}
