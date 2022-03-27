namespace BlazorComponent
{
    public class BBootable : BActivatable
    {
        protected override Task OnActiveUpdating(bool value)
        {
            if (value && !IsBooted)
            {
                //Set IsBooted to true and show content
                IsBooted = true;
                StateHasChanged();
            }

            return Task.CompletedTask;
        }
    }
}