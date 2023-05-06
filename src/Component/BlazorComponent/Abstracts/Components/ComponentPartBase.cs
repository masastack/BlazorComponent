using BlazorComponent.Abstracts;
using Microsoft.AspNetCore.Components.Rendering;
using System.Runtime.CompilerServices;

namespace BlazorComponent
{
    public class ComponentPartBase<TComponent> : IComponentPart where TComponent : IHasProviderComponent
    {
        [NotNull]
        public TComponent? Component { get; set; }

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

        void IComponentPart.SetParameters(ParameterView parameterView)
        {
            parameterView.SetParameterProperties(this);
        }

        protected RenderFragment RenderText(object text)
        {
            return builder => builder.AddContent(0, text);
        }

        protected RenderFragment? RenderPart(Type keyType)
        {
            return AbstractProvider.GetPartContent(keyType, Component);
        }

        protected RenderFragment? RenderPart(Type keyType, string name)
        {
            return AbstractProvider.GetPartContent(keyType, name, Component);
        }

        protected RenderFragment? RenderPart(Type keyType, Dictionary<string, object?> attributes)
        {
            return AbstractProvider.GetPartContent(keyType, Component, builder =>
            {
                builder
                    .SetAttributes(attributes);
            });
        }

        protected RenderFragment? RenderPart(Type keyType, string name, object arg0, [CallerArgumentExpression("arg0")] string arg0Name = "")
        {
            return AbstractProvider.GetPartContent(keyType, name, Component, builder =>
            {
                builder
                    .Add(arg0Name, arg0);
            });
        }

        protected RenderFragment? RenderPart(Type keyType, object arg0, [CallerArgumentExpression("arg0")] string arg0Name = "")
        {
            return AbstractProvider.GetPartContent(keyType, Component, builder =>
            {
                builder
                    .Add(arg0Name, arg0);
            });
        }

        protected RenderFragment? RenderPart(Type keyType, object arg0, object arg1, [CallerArgumentExpression("arg0")] string arg0Name = "", [CallerArgumentExpression("arg1")] string arg1Name = "")
        {
            return AbstractProvider.GetPartContent(keyType, Component, builder =>
            {
                builder
                    .Add(arg0Name, arg0)
                    .Add(arg1Name, arg1);
            });
        }

        protected RenderFragment? RenderPart(Type keyType, object arg0, object arg1, object arg2, [CallerArgumentExpression("arg0")] string arg0Name = "", [CallerArgumentExpression("arg1")] string arg1Name = "", [CallerArgumentExpression("arg2")] string arg2Name = "")
        {
            return AbstractProvider.GetPartContent(keyType, Component, builder =>
            {
                builder
                    .Add(arg0Name, arg0)
                    .Add(arg1Name, arg1)
                    .Add(arg2Name, arg2);
            });
        }

        protected RenderFragment? RenderPart(Type keyType, object arg0, object arg1, object arg2, object arg3, [CallerArgumentExpression("arg0")] string arg0Name = "", [CallerArgumentExpression("arg1")] string arg1Name = "", [CallerArgumentExpression("arg2")] string arg2Name = "", [CallerArgumentExpression("arg3")] string arg3Name = "")
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

        protected EventCallback<TValue> CreateEventCallback<TValue>(Func<TValue, Task> callback)
        {
            return EventCallback.Factory.Create(Component, callback);
        }

        protected Dictionary<string, object?> GetAttributes(Type type, int index)
        {
            return AbstractProvider.GetMetadata(type, index).Attributes;
        }

        protected Dictionary<string, object?> GetAttributes(Type type, object? data = null)
        {
            return AbstractProvider.GetMetadata(type, data).Attributes;
        }

        protected Dictionary<string, object?> GetAttributes(Type type, string name, object? data = null)
        {
            return AbstractProvider.GetMetadata(type, name, data).Attributes;
        }

        protected virtual void BuildRenderTree(RenderTreeBuilder builder)
        {
        }
    }
}
