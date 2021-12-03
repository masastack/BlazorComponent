using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public interface IObservableProperty
    {
        string Name { get; }

        event Action<string> OnChange;

        void NotifyChange();
    }
}
