using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using BlazorComponent.Abstracts;
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
            Watcher = new PropertyWatcher(GetType());
            OnWatcherInitialized();
        }

        [Parameter]
        public string Id { get; set; }

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
        public virtual IDictionary<string, object> Attributes { get; set; } = new Dictionary<string, object>();

        [Inject]
        public IComponentIdGenerator ComponentIdGenerator { get; set; }

        public ComponentCssProvider CssProvider { get; } = new();

        public ComponentAbstractProvider AbstractProvider { get; } = new();

        public PropertyWatcher Watcher { get; }

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

        protected virtual void OnWatcherInitialized()
        {
        }

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

        protected TValue GetValue<TValue>(TValue @default = default, [CallerMemberName] string name = null)
        {
            return Watcher.GetValue(@default, name);
        }

        protected TValue GetComputedValue<TValue>(Expression<Func<TValue>> valueExpression, [CallerMemberName] string name = null)
        {
            return Watcher.GetComputedValue(valueExpression, name);
        }

        protected TValue GetComputedValue<TValue>(Func<TValue> valueFactory, string[] dependencyProperties, [CallerMemberName] string name = null)
        {
            return Watcher.GetComputedValue(valueFactory, dependencyProperties, name);
        }

        protected void SetValue<TValue>(TValue value, [CallerMemberName] string name = null)
        {
            Watcher.SetValue(value, name);
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

        protected RenderFragment RenderPart(Type keyType)
        {
            return AbstractProvider.GetPartContent(keyType, this);
        }

        protected RenderFragment RenderPart(Type keyType, Action<AttributesBuilder> builderAction)
        {
            return AbstractProvider.GetPartContent(keyType, this, builderAction);
        }

        protected RenderFragment RenderPart(Type keyType, object arg0, [CallerArgumentExpression("arg0")] string arg0Name = null)
        {
            return AbstractProvider.GetPartContent(keyType, this, builder =>
            {
                builder
                    .Add(arg0Name, arg0);
            });
        }

        protected RenderFragment RenderPart(Type keyType, object arg0, object arg1, [CallerArgumentExpression("arg0")] string arg0Name = null, [CallerArgumentExpression("arg1")] string arg1Name = null)
        {
            return AbstractProvider.GetPartContent(keyType, this, builder =>
            {
                builder
                    .Add(arg0Name, arg0)
                    .Add(arg1Name, arg1);
            });
        }

        protected RenderFragment RenderPart(Type keyType, object arg0, object arg1, object arg2, [CallerArgumentExpression("arg0")] string arg0Name = null, [CallerArgumentExpression("arg1")] string arg1Name = null, [CallerArgumentExpression("arg2")] string arg2Name = null)
        {
            return AbstractProvider.GetPartContent(keyType, this, builder =>
            {
                builder
                    .Add(arg0Name, arg0)
                    .Add(arg1Name, arg1)
                    .Add(arg2Name, arg2);
            });
        }

        protected Dictionary<string, object> GetAttributes(Type type, object data = null)
        {
            return AbstractProvider.GetMetadata(type, data).Attributes;
        }

        protected Dictionary<string, object> GetAttributes(Type type, string name, object data = null)
        {
            return AbstractProvider.GetMetadata(type, name, data).Attributes;
        }

        public EventCallback<TValue> CreateEventCallback<TValue>(Func<TValue, Task> callback)
        {
            return EventCallback.Factory.Create(this, callback);
        }

        public EventCallback CreateEventCallback(Func<Task> callback)
        {
            return EventCallback.Factory.Create(this, callback);
        }

        public EventCallback<TValue> CreateEventCallback<TValue>(Action<TValue> callback)
        {
            return EventCallback.Factory.Create(this, callback);
        }

        public EventCallback CreateEventCallback(Action callback)
        {
            return EventCallback.Factory.Create(this, callback);
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