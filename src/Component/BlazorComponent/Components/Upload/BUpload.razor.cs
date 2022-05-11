namespace BlazorComponent;

public partial class BUpload
{
    [Parameter]
    public bool Multiple { get; set; }

    [Parameter]
    public string Accept { get; set; }

    [Parameter]
    public string Capture { get; set; }

    [Parameter]
    public int MaximumFileCount { get; set; } = 10;

    [Parameter]
    public List<string> Value { get; set; } = new();

    [Parameter]
    public EventCallback<List<string>> ValueChanged { get; set; }

    [Parameter]
    public FileChangeCallBack OnInputFileChanged { get; set; }

    [Parameter]
    public FileUploadCallBack OnInputFileUpload { get; set; }

    [Parameter]
    public bool WhenFileChangeUpload { get; set; }

    public InputFile InputFileRef { get; set; }

    public IReadOnlyList<IBrowserFile> Files { get; set; } = new List<IBrowserFile>();

    IJSObjectReference UploadJs { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            UploadJs = await Js.InvokeAsync<IJSObjectReference>("import", "./_content/BlazorComponent/js/upload.js");
        }
    }

    protected virtual async Task OnInputFileChange(InputFileChangeEventArgs e)
    {
        Files = e.GetMultipleFiles(MaximumFileCount);
        if (OnInputFileChanged is null) return;
        if (OnInputFileChanged.IsJsCallback)
        {
            OnInputFileChanged.JsCallbackValue = await UploadJs.InvokeAsync<JsonElement>("InputFileChanged", InputFileRef.Element, OnInputFileChanged.JsCallback);
        }
        else if (OnInputFileChanged.IsDelegateCallback)
        {
            await OnInputFileChanged.DelegateCallback(Files);
        }
        else if (OnInputFileUpload.IsEventCallback)
        {
            await OnInputFileChanged.EventCallback.InvokeAsync(Files);
        }

        if (WhenFileChangeUpload) await Upload();
    }

    public virtual async Task Upload()
    {
        if (OnInputFileUpload is null) return;
        if (OnInputFileUpload.IsJsCallback)
        {
            Value = await UploadJs.InvokeAsync<List<string>>("InputFileUpload", InputFileRef.Element, OnInputFileUpload.JsCallback);
        }
        else if (OnInputFileUpload.IsDelegateCallback)
        {
            Value = await OnInputFileUpload.DelegateCallback(Files);
        }
        else if (OnInputFileUpload.IsEventCallback)
        {
            await OnInputFileUpload.EventCallback.InvokeAsync((Files, value => Value = value));
        }

        if (ValueChanged.HasDelegate) await ValueChanged.InvokeAsync(Value);
    }
}

