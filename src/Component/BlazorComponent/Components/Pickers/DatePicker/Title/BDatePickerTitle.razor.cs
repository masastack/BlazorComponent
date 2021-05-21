using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BDatePickerTitle:BDomComponentBase,IPickerTitle
    {
        protected const string YEAR_BTN = "year-btn";
        protected const string TITLE_DATE = "title-date";

        [Parameter]
        public StringNumber Year { get; set; }

        [Parameter]
        public string YearIcon { get; set; }

        [Parameter]
        public string Date { get; set; }
    }
}
