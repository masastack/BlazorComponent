using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace BlazorComponent;

public class BDelayable : BDomComponentBase, IDelayable
{
    private Delayer _delayer;

    [Parameter]
    public virtual int OpenDelay { get; set; }

    [Parameter]
    public virtual int CloseDelay { get; set; }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        _delayer = new Delayer()
        {
            OpenDelay = OpenDelay,
            CloseDelay = CloseDelay
        };
    }

    public Task RunOpenDelay(Func<Task> cb = null) => _delayer.RunOpenDelay(cb);

    public Task RunCloseDelay(Func<Task> cb = null) => _delayer.RunCloseDelay(cb);
}