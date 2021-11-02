using System;
using System.Threading.Tasks;

namespace BlazorComponent;

public interface IDelayable
{
    int OpenDelay { get; set; }

    int CloseDelay { get; set; }

    Task RunOpenDelay(Func<Task> cb = null);

    Task RunCloseDelay(Func<Task> cb = null);
}