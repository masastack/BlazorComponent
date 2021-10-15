using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public class ComponentAbstractProvider
    {
        private readonly Dictionary<ComponentKey, Type> _typeConfig = new();
        private readonly Dictionary<ComponentKey, Action<Dictionary<string, object>>> _propertiesConfig = new();

        public ComponentAbstractProvider Apply(Type type, Type implementType, Action<Dictionary<string, object>> propertiesAction = null)
        {
            var key = new ComponentKey(type);
            if (_typeConfig.TryAdd(key, implementType))
            {
                _propertiesConfig[key] = propertiesAction;
            }

            return this;
        }

        public ComponentAbstractProvider Apply(Type type, Type implementType, string name, Action<Dictionary<string, object>> propertiesAction = null)
        {
            var key = new ComponentKey(type, name);
            if (_typeConfig.TryAdd(key, implementType))
            {
                _propertiesConfig[key] = propertiesAction;
            }

            return this;
        }

        public ComponentAbstractProvider Apply<TComponent>(Action<Dictionary<string, object>> propertiesAction = null)
        {
            var key = ComponentKey.Get<TComponent>();
            return Apply<TComponent, TComponent>(key, propertiesAction);
        }

        public ComponentAbstractProvider Apply<TComponent, TImplementComponent>(Action<Dictionary<string, object>> propertiesAction = null)
            where TImplementComponent : TComponent
        {
            var key = ComponentKey.Get<TComponent>();
            return Apply<TComponent, TImplementComponent>(key, propertiesAction);
        }

        private ComponentAbstractProvider Apply<TComponent, TImplementComponent>(ComponentKey key, Action<Dictionary<string, object>> propertiesAction) where TImplementComponent : TComponent
        {
            if (_typeConfig.TryAdd(key, typeof(TImplementComponent)))
            {
                _propertiesConfig[key] = propertiesAction;
            }

            return this;
        }

        public ComponentAbstractProvider Apply<TComponent, TImplementComponent>(string name, Action<Dictionary<string, object>> propertiesAction = null)
            where TImplementComponent : TComponent
        {
            var key = ComponentKey.Get<TComponent>(name);
            return Apply<TComponent, TImplementComponent>(key, propertiesAction);
        }

        /// <summary>
        /// Use this with <see cref="GetMetadata(string)"/>
        /// </summary>
        /// <typeparam name="TComponent"></typeparam>
        /// <param name="name"></param>
        /// <param name="propertiesAction"></param>
        /// <returns></returns>
        public ComponentAbstractProvider Apply<TComponent>(string name, Action<Dictionary<string, object>> propertiesAction = null)
            where TComponent : IComponent
        {
            return Apply<IComponent, TComponent>(name, propertiesAction);
        }

        public ComponentAbstractProvider Merge<TComponent>(Action<Dictionary<string, object>> mergePropertiesAction = null)
        {
            var key = ComponentKey.Get<TComponent>();
            Merge(key, mergePropertiesAction);

            return this;
        }

        public ComponentAbstractProvider Merge<TComponent, TImplementComponent>(Action<Dictionary<string, object>> mergePropertiesAction = null)
            where TImplementComponent : TComponent
        {
            var key = ComponentKey.Get<TComponent>();

            _typeConfig[key] = typeof(TImplementComponent);
            Merge(key, mergePropertiesAction);

            return this;
        }

        public ComponentAbstractProvider Merge(Type type, Type implementType, Action<Dictionary<string, object>> mergePropertiesAction = null)
        {
            var key = new ComponentKey(type);

            _typeConfig[key] = implementType;
            Merge(key, mergePropertiesAction);

            return this;
        }

        private void Merge(ComponentKey key, Action<Dictionary<string, object>> mergePropertiesAction = null)
        {
            if (mergePropertiesAction != null)
            {
                var propertiesAction = _propertiesConfig.GetValueOrDefault(key);
                _propertiesConfig[key] = properties =>
                {
                    propertiesAction?.Invoke(properties);
                    mergePropertiesAction?.Invoke(properties);
                };
            }
        }

        public ComponentAbstractProvider Merge<TComponent>(string name, Action<Dictionary<string, object>> mergePropertiesAction = null)
        {
            var key = ComponentKey.Get<TComponent>(name);
            Merge(key, mergePropertiesAction);

            return this;
        }

        /// <summary>
        /// Use this with <see cref="Apply{TComponent}(string, Action{Dictionary{string, object}})"/> and <see cref="GetMetadata(string)"/>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="mergePropertiesAction"></param>
        /// <returns></returns>
        public ComponentAbstractProvider Merge(string name, Action<Dictionary<string, object>> mergePropertiesAction = null)
        {
            var key = ComponentKey.Get<IComponent>(name);
            Merge(key, mergePropertiesAction);

            return this;
        }

        public AbstractMetadata GetMetadata(Type type)
        {
            var key = new ComponentKey(type);
            return GetMetadata(key);
        }

        private AbstractMetadata GetMetadata(ComponentKey key, Dictionary<string, object> dic = null)
        {
            var implementType = _typeConfig.GetValueOrDefault(key, typeof(EmptyComponent));

            var properties = dic ?? new Dictionary<string, object>();
            var action = _propertiesConfig.GetValueOrDefault(key);
            action?.Invoke(properties);

            return new AbstractMetadata(implementType, properties);
        }

        public AbstractMetadata GetMetadata<TComponent>()
        {
            return GetMetadata(typeof(TComponent));
        }

        public AbstractMetadata GetMetadata<TComponent>(string name)
        {
            var key = ComponentKey.Get<TComponent>(name);
            return GetMetadata(key);
        }

        /// <summary>
        /// Use this with <see cref="Apply{TComponent}(string, Action{Dictionary{string, object}})"/>
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public AbstractMetadata GetMetadata(string name)
        {
            return GetMetadata<IComponent>(name);
        }

        public AbstractMetadata GetMetadata<TComponent>(int index)
        {
            var key = new ComponentKey(typeof(TComponent));

            return GetMetadata(key, new Dictionary<string, object>
            {
                //TODO:rename this
                { "ItemIndex", index }
            });
        }

        public AbstractMetadata GetMetadata(Type type, object data = null)
        {
            var key = new ComponentKey(type);

            return GetMetadata(key, new Dictionary<string, object>
            {
                { "_data",data }
            });
        }

        public AbstractMetadata GetMetadata(Type type, string name, object data = null)
        {
            var key = new ComponentKey(type, name);

            //TODO:change Dictionary to something like builder?
            return GetMetadata(key, new Dictionary<string, object>
            {
                { "_data",data}
            });
        }
    }
}
