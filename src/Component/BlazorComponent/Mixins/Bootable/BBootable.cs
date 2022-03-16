using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public class BBootable : BActivatable
    {
        protected override void OnValueChanged(bool value)
        {
            if (value && !IsBooted)
            {
                IsBooted = true;
            }

            //We cann't call async method here
            NextTick(async () =>
            {
                await SetIsActiveAsync(value);
                StateHasChanged();
            });
        }

        protected override async Task SetIsActiveAsync(bool isActive)
        {
            if (isActive && !IsBooted)
            {
                //Set IsBooted to true and show content
                //Set isActive in nextTick so we can get content element reference
                IsBooted = true;
                NextTick(async () =>
                {
                    await base.SetIsActiveAsync(isActive);
                    StateHasChanged();
                });
            }
            else
            {
                await base.SetIsActiveAsync(isActive);
            }
        }
    }
}
