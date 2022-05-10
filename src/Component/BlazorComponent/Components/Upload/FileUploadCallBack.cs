namespace BlazorComponent;

public class FileUploadCallBack
{
    public string JsCallback { get; init; }

    public EventCallback<(IReadOnlyList<IBrowserFile>, Action<List<string>>)> EventCallback { get; init; }

    public Func<IReadOnlyList<IBrowserFile>, Task<List<string>>> DelegateCallback { get; init; }

    public bool IsJsCallback => JsCallback is not null;

    public bool IsEventCallback => EventCallback.Equals(default);

    public bool IsDelegateCallback => DelegateCallback is not null;

    public static implicit operator FileUploadCallBack(string callback)
    {
        return new FileUploadCallBack { JsCallback = callback };
    }

    public static implicit operator FileUploadCallBack(EventCallback<(IReadOnlyList<IBrowserFile>, Action<List<string>>)> callback)
    {
        return new FileUploadCallBack { EventCallback = callback };
    }

    public static FileUploadCallBack CreateCallback(string callback)
    {
        return new FileUploadCallBack { JsCallback = callback };
    }

    public static FileUploadCallBack CreateCallback(ComponentBase receiver, Action<(IReadOnlyList<IBrowserFile>, Action<List<string>>)> callback)
    {
        return new FileUploadCallBack
        {
            EventCallback = Microsoft.AspNetCore.Components.EventCallback.Factory.Create(receiver, callback)
        };
    }

    public static FileUploadCallBack CreateCallback(Func<IReadOnlyList<IBrowserFile>, Task<List<string>>> callback)
    {
        return new FileUploadCallBack { DelegateCallback = callback };
    }
}

