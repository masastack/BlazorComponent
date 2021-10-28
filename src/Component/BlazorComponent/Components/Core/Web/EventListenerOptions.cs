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

    public EventListenerActions()
    {
    }

    public EventListenerActions(bool stopPropagation)
    {
        StopPropagation = stopPropagation;
    }

    public EventListenerActions(string relatedTarget)
    {
        RelatedTarget = relatedTarget;
    }
}