using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorComponent.Components.Core;
using BlazorComponent.Components.Core.CssProcess;

namespace BlazorComponent
{
    public class ComponentConfigProvider
    {
        private readonly Dictionary<ComponentConfigKey, Action<CssBuilder>> _cssConfig  = new();
        private readonly Dictionary<ComponentConfigKey, Action<StyleBuilder>> _styleConfig  = new();
        private readonly Dictionary<ComponentConfigKey,Type> _typeConfig=new();
        private readonly Dictionary<ComponentConfigKey,Action<Dictionary<string,object>>> _propertiesConfig=new();

        public ComponentConfigProvider Add(Type type, Action<CssBuilder> cssAction = null, Action<StyleBuilder> styleAction = null)
        {
            if (cssAction != null)
            {
                _cssConfig.TryAdd(new ComponentConfigKey(type), cssAction);
            }

            if (styleAction != null)
            {
                _styleConfig.TryAdd(new ComponentConfigKey(type), styleAction);
            }

            return this;
        }

        public ComponentConfigProvider Add(Type type, string name, Action<CssBuilder> cssAction = null, Action<StyleBuilder> styleAction = null)
        {
            if (cssAction != null)
            {
                _cssConfig.TryAdd(new ComponentConfigKey(type, name), cssAction);
            }

            if (styleAction != null)
            {
                _styleConfig.TryAdd(new ComponentConfigKey(type, name), styleAction);
            }

            return this;
        }

        public ComponentConfigProvider Add<TComponent>(Action<CssBuilder> cssAction = null, Action<StyleBuilder> styleAction = null)
        {
            return Add(typeof(TComponent), cssAction, styleAction);
        }

        public ComponentConfigProvider Add<TComponent>(string name, Action<CssBuilder> cssAction = null, Action<StyleBuilder> styleAction = null)
        {
            return Add(typeof(TComponent), name, cssAction, styleAction);
        }

        public ComponentConfigProvider Add<TComponent, TImplementComponent>(Action<Dictionary<string, object>> propertiesAction = null)
            where TImplementComponent : TComponent
        {
            var key=GetKey<TComponent>();
            _typeConfig.TryAdd(key, typeof(TImplementComponent));

            _propertiesConfig[key] = propertiesAction;

            return this;
        }

        public ComponentConfigProvider Add<TComponent, TImplementComponent>(Action<TImplementComponent> propertiesAction = null)
            where TImplementComponent : TComponent
        {
            return this;
        }

        public ComponentConfigProvider Add<TComponent, TImplementComponent>(string name, Action<Dictionary<string, object>> propertiesAction = null)
            where TImplementComponent : TComponent
        {
            var key=GetKey<TComponent>(name);
            _typeConfig.TryAdd(key, typeof(TImplementComponent));

            _propertiesConfig[key] = propertiesAction;

            return this;
        }

        private static ComponentConfigKey GetKey<TComponent>()
        {
            return new ComponentConfigKey(typeof(TComponent));
        }

        private static ComponentConfigKey GetKey<TComponent>(string name)
        {
            return new ComponentConfigKey(typeof(TComponent), name);
        }

        public Type Get<TComponent>()
        {
            var type=_typeConfig.GetValueOrDefault(GetKey<TComponent>(), typeof(TComponent));
            return type;
        }

        public Type Get<TComponent>(string name)
        {
            var type=_typeConfig.GetValueOrDefault(GetKey<TComponent>(name), typeof(TComponent));
            return type;
        }

        public Dictionary<string, object> GetProperties<TComponent>()
        {
            var properties=new Dictionary<string,object>();

            var action=_propertiesConfig.GetValueOrDefault(GetKey<TComponent>());
            action?.Invoke(properties);

            return properties;
        }

        public Dictionary<string, object> GetProperties<TComponent>(string name)
        {
            var properties=new Dictionary<string,object>();

            var action=_propertiesConfig.GetValueOrDefault(GetKey<TComponent>(name));
            action?.Invoke(properties);

            return properties;
        }

        public string GetCss(Type type)
        {
            var action= _cssConfig.GetValueOrDefault(new ComponentConfigKey(type), _ => { });

            var builder=new CssBuilder();
            action?.Invoke(builder);

            return builder.Class;
        }

        public string GetStyle(Type type)
        {
            var action= _styleConfig.GetValueOrDefault(new ComponentConfigKey(type), _ => { });

            var builder=new StyleBuilder();
            action?.Invoke(builder);

            return builder.Style;
        }

        public string GetCss(Type type, string name)
        {
            var action= _cssConfig.GetValueOrDefault(new ComponentConfigKey(type, name), _ => { });

            var builder=new CssBuilder();
            action?.Invoke(builder);

            return builder.Class;
        }

        public string GetStyle(Type type, string name)
        {
            var action= _styleConfig.GetValueOrDefault(new ComponentConfigKey(type, name), _ => { });

            var builder=new StyleBuilder();
            action?.Invoke(builder);

            return builder.Style;
        }

        public string GetCss<TComponent>(TComponent component)
        {
            return GetCss(typeof(TComponent));
        }

        public string GetStyle<TComponent>(TComponent component)
        {
            return GetStyle(typeof(TComponent));
        }

        public string GetCss<TComponent>(TComponent component, string name)
        {
            return GetCss(typeof(TComponent), name);
        }

        public string GetStyle<TComponent>(TComponent component, string name)
        {
            return GetStyle(typeof(TComponent), name);
        }
    }
}
