using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public class InputContext<TValue>
    {
        public ElementReference InputRef { get; set; }

        public event Func<TValue, Task> OnValueChanged;

        public async Task NotifyValueChanged(TValue value)
        {
            await OnValueChanged?.Invoke(value);
        }
    }
}
