using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public class InputContext
    {
        public ElementReference InputRef { get; set; }

        public event Action<string> OnValueChanged;

        public void NotifyValueChanged(string value)
        {
            OnValueChanged?.Invoke(value);
        }
    }
}
