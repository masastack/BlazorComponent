using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public class ComponentKey
    {
        public ComponentKey(Type type)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
        }

        public ComponentKey(Type type, string name)
            : this(type)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException(null, nameof(name));
            }

            Name = name;
        }

        public Type Type { get; }
        public string Name { get; }

        public override int GetHashCode()
        {
            return Name != null ? Type.GetHashCode() ^ Name.GetHashCode() : Type.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return obj != null && obj is ComponentKey key && key.Type == Type && key.Name == Name;
        }

        public static ComponentKey Get<TComponent>()
        {
            return new ComponentKey(typeof(TComponent));
        }

        public static ComponentKey Get<TComponent>(string name)
        {
            return new ComponentKey(typeof(TComponent), name);
        }
    }
}
