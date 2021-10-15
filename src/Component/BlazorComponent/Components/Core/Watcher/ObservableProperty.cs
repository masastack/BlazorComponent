using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public class ObservableProperty : IObservableProperty
    {
        public ObservableProperty(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public event Action<string> OnChange;

        void IObservableProperty.NotifyChange()
        {
            OnChange?.Invoke(Name);
        }
    }
}
