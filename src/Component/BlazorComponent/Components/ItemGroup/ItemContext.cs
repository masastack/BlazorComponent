using System;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public class ItemContext
    {
        public bool Active { get; init; }

        public string ActiveClass { get; set; }

        public Func<Task> Toggle { get; init; }

        public ForwardRef Ref { get; set; }

        public StringNumber Value { get; set; }
    }
}