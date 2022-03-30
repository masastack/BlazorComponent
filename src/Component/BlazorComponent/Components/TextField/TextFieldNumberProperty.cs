using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public class TextFieldNumberProperty
    {
        public decimal? Min { get; set; }

        public decimal? Max { get; set; }

        public decimal Step { get; set; } = 1;

        public bool ShowControl { get; set; } = true;
    }
}
