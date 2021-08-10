using System;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public class ItemContext
    {
        public bool IsActive { get; set; }

        public Func<Task> Toggle { get; set; }
    }
}
