namespace BlazorComponent;

public class BBootable : BActivatable
{
    protected override void ShowLazyContent()
    {
        if (!IsBooted)
        {
            //Set IsBooted to true and show content
            IsBooted = true;
            StateHasChanged();
        }
    }
}