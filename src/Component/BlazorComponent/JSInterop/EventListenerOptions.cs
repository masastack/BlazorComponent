namespace BlazorComponent;

public class EventListenerOptions
{
    public bool Capture { get; set; }

    public bool Once { get; set; }

    public bool Passive { get; set; }
}

public class EventListenerActions
{
    public bool StopPropagation { get; set; }

    public string RelatedTarget { get; set; }

    public bool PreventDefault { get; set; }

    public EventListenerActions()
    {
    }

    public EventListenerActions(bool stopPropagation, bool preventDefault)
    {
        StopPropagation = stopPropagation;
        PreventDefault = preventDefault;
    }

    public EventListenerActions(string relatedTarget)
    {
        RelatedTarget = relatedTarget;
    }
}