using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public class ItemValue<TItem>
    {
        private Func<TItem, object> _factory;

        public ItemValue(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public string Name { get; }

        public Func<TItem, object> Factory
        {
            get
            {
                if (_factory == null)
                {
                    try
                    {

                        var parameterExpression = Expression.Parameter(typeof(TItem), "item");
                        var propertyExpression = Expression.Property(parameterExpression, Name);
                        var valueExpression = Expression.Convert(propertyExpression, typeof(object));

                        var lambdaExpression = Expression.Lambda<Func<TItem, object>>(valueExpression, parameterExpression);
                        _factory = lambdaExpression.Compile();
                    }
                    catch (Exception ex)
                    {
                        //REVIEW:is this ok?
                        throw new InvalidOperationException($"Can not access property {Name} of {typeof(TItem).Name}", ex);
                    }
                }

                return _factory;
            }
        }

        public object Invoke(TItem item)
        {
            return Factory.Invoke(item);
        }

        public static implicit operator string(ItemValue<TItem> itemValue) => itemValue.Name;

        public static implicit operator ItemValue<TItem>(string name) => new(name);
    }
}
