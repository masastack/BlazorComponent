using Microsoft.AspNetCore.Components.Web;

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
