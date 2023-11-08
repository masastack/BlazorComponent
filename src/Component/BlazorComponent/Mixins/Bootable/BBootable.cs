namespace BlazorComponent;

public class BBootable : BActivatable
{
    protected override async Task<bool> ShowLazyContent()
    {
        if (!IsBooted)
        {
            //Set IsBooted to true and show content
            IsBooted = true;
            await Task.Delay(16);
            StateHasChanged();

            return true;
        }

        return false;
    }
}