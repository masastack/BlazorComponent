using Microsoft.AspNetCore.Components;

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

    // TODO: move to a single file.
    public class BToggleable : BDelayable
    {
        [Parameter]
        public bool Value
        {
            get => GetValue<bool>();
            set => SetValue(value);
        }

        [Parameter]
        public EventCallback<bool> ValueChanged { get; set; }

        protected override void OnWatcherInitialized()
        {
            // TODO: 不使用Watch
            Watcher.Watch<bool>(nameof(Value), OnValueChanged);
        }

        protected virtual async void OnValueChanged(bool val)
        {
            // TODO: ...
        }

        protected override async Task WhenIsActiveUpdating(bool val)
        {
            // TODO: works like 'watch'
            
            // if (val != Value && ValueChanged.HasDelegate)
            // {
            //     await ValueChanged.InvokeAsync(val);
            // }
        }
    }
}