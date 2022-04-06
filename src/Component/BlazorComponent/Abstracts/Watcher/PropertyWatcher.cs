using System.Collections.Concurrent;
using System.Linq.Expressions;

namespace BlazorComponent
{
    /// <summary>
    /// This may has bugs,do not use now
    /// </summary>
    public sealed class PropertyWatcher
    {
        private ConcurrentDictionary<string, IObservableProperty> _props = new();
        private Type _objectType;

        public PropertyWatcher(Type objectType)
        {
            _objectType = objectType;
        }

        public TValue GetValue<TValue>(TValue @default = default, string name = null)
        {
            var property = GetProperty(@default, name);
            if (!property.HasValue && property != default)
            {
                property.Value = @default;
            }

            return property.Value;
        }

        private ObservableProperty<TValue> GetProperty<TValue>(TValue @default, string name)
        {
            var prop = _props.GetOrAdd(name, key => new ObservableProperty<TValue>(name, @default));
            if (prop.GetType() == typeof(ObservableProperty))
            {
                //Internal watch may before `ObservableProperty<TValue>` be created 
                prop = new ObservableProperty<TValue>(prop, @default);
                _props[name] = prop;
            }

            var property = (ObservableProperty<TValue>)prop;
            return property;
        }

        public TValue GetComputedValue<TValue>(Expression<Func<TValue>> valueExpression, string name)
        {
            var property = GetProperty<TValue>(default, name);
            if (!property.HasValue)
            {
                var valueFactory = valueExpression.Compile();
                property.ValueFactory = valueFactory;
                property.Value = valueFactory();

                //Analysis the dependency property and watch them,so when they have changes,we will re-compute the value
                var visitor = new MemberAccessVisitor();
                visitor.Visit(valueExpression);

                var propertyInfos = visitor.PropertyInfos.Where(r => _objectType.IsSubclassOf(r.DeclaringType));
                foreach (var propertyInfo in propertyInfos)
                {
                    Watch(propertyInfo.Name, () =>
                    {
                        var value = valueFactory();
                        SetValue(value, name);
                    });
                }
            }

            return property.Value;
        }

        public TValue GetComputedValue<TValue>(Func<TValue> valueFactory, string[] dependencyProperties, string name)
        {
            var property = GetProperty<TValue>(default, name);
            if (!property.HasValue)
            {
                property.ValueFactory = valueFactory;
                property.Value = valueFactory();

                foreach (var dependencyProperty in dependencyProperties)
                {
                    Watch(dependencyProperty, () =>
                    {
                        var value = valueFactory();
                        SetValue(value, name);
                    });
                }
            }

            return property.Value;
        }

        public void SetValue<TValue>(TValue value, string name)
        {
            var property = GetProperty<TValue>(default, name);
            property.Value = value;
        }

        public PropertyWatcher Watch<TValue>(string name, Action changeCallback)
        {
            return Watch<TValue>(name, (newValue, oldValue) => changeCallback?.Invoke());
        }

        public PropertyWatcher Watch<TValue>(string name, Action<TValue> changeCallback)
        {
            return Watch<TValue>(name, (newValue, oldValue) => changeCallback?.Invoke(newValue));
        }

        public PropertyWatcher Watch<TValue>(string name, Action<TValue, TValue> changeCallback)
        {
            var property = GetProperty<TValue>(default, name);
            property.OnValueChange += changeCallback;

            return this;
        }

        private void Watch(string name, Action changeCallback)
        {
            //Internal watch can'not infer the TValue,can we get a better solution?
            var prop = _props.GetOrAdd(name, key => new ObservableProperty(name));
            prop.OnChange += _ => changeCallback?.Invoke();
        }
    }
}
