using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent.JSInterop
{
    public class DocumentElement
    {
        public double ScrollTop { get; set; }
        public double ScrollLeft { get; set; }
        public double ClientHeight { get; set; }
        public double ClientWidth { get; set; }
    }
}
