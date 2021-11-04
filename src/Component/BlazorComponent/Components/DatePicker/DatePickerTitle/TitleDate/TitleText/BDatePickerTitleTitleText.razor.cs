using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BDatePickerTitleTitleText<TDatePickerTitle> where TDatePickerTitle : IDatePickerTitle
    {
        public string ComputedTransition => Component.ComputedTransition;

        public string Date=>Component.Date;

        public DateOnly Value =>Component.Value;
    }
}
