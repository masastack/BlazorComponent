using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public class MenuableDimensions
    {
        public MenuablePosition Activator { get; set; } = new MenuablePosition();
        public MenuablePosition Content { get; set; } = new MenuablePosition();
        public double RelativeYOffset { get; set; }
        public double OffsetParentLeft { get; set; }
    }
}
