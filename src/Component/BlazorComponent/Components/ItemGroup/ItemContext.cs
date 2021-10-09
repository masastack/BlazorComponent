using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public class ItemContext
    {
        public bool IsActive { get; init; }

        public Func<Task> Toggle { get; init; }

        public ForwardRef Ref { get; set; }
        
        public StringNumber Value { get; set; }
    }
}