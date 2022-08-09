namespace BlazorComponent;

public partial class BMobilePicker<TColumnItem, TColumnItemValue> : BDomComponentBase
{
    [Parameter] public string Title { get; set; }

    [Parameter] public string CancelText { get; set; }

    [Parameter] public string OkText { get; set; }

    [Parameter] public EventCallback<(List<TColumnItemValue> value, List<int> index)> OnOk { get; set; }

    [Parameter] public EventCallback<(List<TColumnItemValue> value, List<int> index)> OnChange { get; set; }

    [Parameter] public EventCallback OnCancel { get; set; }


    // protected async Task HandleOnOk()
    // {
    //     foreach (var child in Children)
    //     {
    //         await child.StopMomentum();
    //     }
    //
    //     if (OnOk.HasDelegate)
    //     {
    //         var items = GetItems();
    //         var values = items.Select(ColumnItemValue).ToList();
    //         var indexes = GetIndexes();
    //
    //         await OnChange.InvokeAsync((values, indexes));
    //     }
    // }
    //
    // protected async Task HandleOnCancel()
    // {
    //     if (OnCancel.HasDelegate)
    //     {
    //         await OnCancel.InvokeAsync();
    //     }
    // }
    //
    // protected async Task HandleOnChange(int index)
    // {
    //     if (OnChange.HasDelegate)
    //     {
    //         var items = GetItems();
    //         var values = items.Select(ColumnItemValue).ToList();
    //         var indexes = GetIndexes();
    //
    //         await OnChange.InvokeAsync((values, indexes));
    //     }
    // }
    //
    // private List<TColumnItem> GetItems()
    // {
    //     return Children.Select(child => child.GetItem()).ToList();
    // }
    //
    // private List<int> GetIndexes()
    // {
    //     return Children.Select(child => child.CurrentIndex).ToList();
    // }

}
