using BlazorComponent.Abstracts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public class ComponentPartBase<TComponent> : IComponentPart where TComponent : IHasProviderComponent
    {
        public TComponent Component { get; set; }

        protected ComponentCssProvider CssProvider => Component.CssProvider;

        protected ComponentAbstractProvider AbstractProvider => Component.AbstractProvider;

        RenderFragment IComponentPart.Content => BuildRenderTree;

        void IComponentPart.Attach(IHasProviderComponent hasProviderComponent)
        {
            if (hasProviderComponent is TComponent component)
            {
                Component = component;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        void IComponentPart.SetParametersView(ParameterView parameterView)
        {
            parameterView.SetParameterProperties(this);
        }

        protected RenderFragment Render(Type type, Action<AttributesBuilder> parametersBuilderAction = null, object key = null, object data = null, Action<object> referenceCapture = null)
        {
            var metadata = AbstractProvider.GetMetadata(type, data);
            return builder =>
            {
                var sequence = 0;
                builder.OpenComponent(sequence++, metadata.Type);

                builder.AddMultipleAttributes(sequence++, metadata.Attributes);

                if (parametersBuilderAction != null)
                {
                    var parametersBuilder = new AttributesBuilder();
                    parametersBuilderAction.Invoke(parametersBuilder);

                    builder.AddMultipleAttributes(sequence++, parametersBuilder.Attributes);
                }

                if (key != null)
                {
                    builder.SetKey(key);
                }

                if (referenceCapture != null)
                {
                    builder.AddComponentReferenceCapture(sequence++, referenceCapture);
                }

                builder.CloseComponent();
            };
        }

        protected RenderFragment Render(Type type, string name, Action<AttributesBuilder> parametersBuilderAction = null, object key = null, object data = null, string textContent = null)
        {
            var metadata = AbstractProvider.GetMetadata(type, name, data);
            return builder =>
            {
                var sequence = 0;
                builder.OpenComponent(sequence++, metadata.Type);

                builder.AddMultipleAttributes(sequence++, metadata.Attributes);

                if (parametersBuilderAction != null)
                {
                    var propsBuilder = new AttributesBuilder();
                    parametersBuilderAction.Invoke(propsBuilder);

                    builder.AddMultipleAttributes(sequence++, propsBuilder.Attributes);
                }

                if (key != null)
                {
                    builder.SetKey(key);
                }

                if (textContent != null)
                {
                    builder.AddAttribute(sequence++, "ChildContent", RenderText(textContent));
                }

                builder.CloseComponent();
            };
        }

        protected RenderFragment RenderText(object text)
        {
            return builder => builder.AddContent(0, text);
        }

        protected RenderFragment RenderPart(Type keyType)
        {
            return AbstractProvider.GetPartContent(keyType, Component);
        }

        protected RenderFragment RenderPart(Type keyType, Action<AttributesBuilder> builderAction)
        {
            return AbstractProvider.GetPartContent(keyType, Component, builderAction);
        }

        protected RenderFragment RenderPart(Type keyType, Dictionary<string, object> parameters)
        {
            return AbstractProvider.GetPartContent(keyType, Component, builder =>
            {
                builder
                    .SetAttributes(parameters);
            });
        }

        protected RenderFragment RenderPart(Type keyType, object arg0, [CallerArgumentExpression("arg0")] string arg0Name = null)
        {
            return AbstractProvider.GetPartContent(keyType, Component, builder =>
            {
                builder
                    .Add(arg0Name, arg0);
            });
        }

        protected RenderFragment RenderPart(Type keyType, object arg0, object arg1, [CallerArgumentExpression("arg0")] string arg0Name = null, [CallerArgumentExpression("arg1")] string arg1Name = null)
        {
            return AbstractProvider.GetPartContent(keyType, Component, builder =>
            {
                builder
                    .Add(arg0Name, arg0)
                    .Add(arg1Name, arg1);
            });
        }

        protected RenderFragment RenderPart(Type keyType, object arg0, object arg1, object arg2, [CallerArgumentExpression("arg0")] string arg0Name = null, [CallerArgumentExpression("arg1")] string arg1Name = null, [CallerArgumentExpression("arg2")] string arg2Name = null)
        {
            return AbstractProvider.GetPartContent(keyType, Component, builder =>
            {
                builder
                    .Add(arg0Name, arg0)
                    .Add(arg1Name, arg1)
                    .Add(arg2Name, arg2);
            });
        }

        protected RenderFragment RenderPart(Type keyType, object arg0, object arg1, object arg2, object arg3, [CallerArgumentExpression("arg0")] string arg0Name = null, [CallerArgumentExpression("arg1")] string arg1Name = null, [CallerArgumentExpression("arg2")] string arg2Name = null, [CallerArgumentExpression("arg2")] string arg3Name = null)
        {
            return AbstractProvider.GetPartContent(keyType, Component, builder =>
            {
                builder
                    .Add(arg0Name, arg0)
                    .Add(arg1Name, arg1)
                    .Add(arg2Name, arg2)
                    .Add(arg3Name, arg3);
            });
        }

        protected RenderFragment RenderPart(Type keyType, object name, Action<AttributesBuilder> builderAction)
        {
            return AbstractProvider.GetPartContent(keyType, name, Component, builderAction);
        }

        protected EventCallback<TValue> CreateEventCallback<TValue>(Func<TValue, Task> callback)
        {
            return EventCallback.Factory.Create(Component, callback);
        }

        protected Dictionary<string, object> GetAttributes(Type type, int index)
        {
            return AbstractProvider.GetMetadata(type, index).Attributes;
        }

        protected Dictionary<string, object> GetAttributes(Type type, object data = null)
        {
            return AbstractProvider.GetMetadata(type, data).Attributes;
        }

        protected Dictionary<string, object> GetAttributes(Type type, string name, object data = null)
        {
            return AbstractProvider.GetMetadata(type, name, data).Attributes;
        }

        protected virtual void BuildRenderTree(RenderTreeBuilder builder)
        {
        }
    }
}