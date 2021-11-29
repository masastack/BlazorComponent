using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public interface IDatePickerTitle : IHasProviderComponent
    {
        string Year { get; }

        string YearIcon { get; }

        string ComputedTransition { get; }

        string Date { get; }

        DateOnly Value { get; }

        bool SelectingYear { get; }

        Task HandleOnTitleDateBtnClickAsync(MouseEventArgs arg);

        Task HandleOnYearBtnClickAsync(MouseEventArgs arg);
    }
}
