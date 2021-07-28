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

        public event Func<NullableValue<TValue>, Task> OnValueChanged;

        public async Task NotifyValueChanged(NullableValue<TValue> value)
        {
            await OnValueChanged?.Invoke(value);
        }
    }
}
