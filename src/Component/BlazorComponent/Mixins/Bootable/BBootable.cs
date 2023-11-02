namespace BlazorComponent;

public class BBootable : BActivatable
{
    protected bool FirstBoot { get; set; }
    
    protected override async Task<bool> ShowLazyContent()
    {
        if (!IsBooted)
        {
            //Set IsBooted to true and show content
            IsBooted = true;
            await Task.Delay(16);
            StateHasChanged();

            FirstBoot = true;

            return true;
        }

        FirstBoot = false;

        return false;
    }
}