namespace BlazorComponent;

public class FileUploadCallBack
{
    string JsCallback { get; set; }

    EventCallback<(IReadOnlyList<IBrowserFile>, List<string>)> EventCallback { get; set; }

    Func<IReadOnlyList<IBrowserFile>, Task<List<string>>> DelegateCallback { get; set; }

    public bool IsJsCallback => JsCallback is not null;

    public bool IsEventCallback => EventCallback.Equals(default);

    public bool IsDelegateCallback => DelegateCallback is not null;

    public static implicit operator FileUploadCallBack(string callback)
    {
        return new FileUploadCallBack { JsCallback = callback };
    }

    public static implicit operator string(FileUploadCallBack onUpload)
    {
        return onUpload.JsCallback;
    }

    public static implicit operator FileUploadCallBack(EventCallback<(IReadOnlyList<IBrowserFile>, List<string>)> callback)
    {
        return new FileUploadCallBack { EventCallback = callback };
    }

    public static implicit operator EventCallback<(IReadOnlyList<IBrowserFile>, List<string>)>(FileUploadCallBack onUpload)
    {
        return onUpload.EventCallback;
    }

    public static implicit operator FileUploadCallBack(Func<IReadOnlyList<IBrowserFile>, Task<List<string>>> callback)
    {
        return new FileUploadCallBack { DelegateCallback = callback };
    }

    public static implicit operator Func<IReadOnlyList<IBrowserFile>, Task<List<string>>>(FileUploadCallBack onUpload)
    {
        return onUpload.DelegateCallback;
    }
}

