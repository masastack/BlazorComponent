using Microsoft.JSInterop;

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
    public List<string> Value { get; set; }

    [Parameter]
    public EventCallback<List<string>> ValueChanged { get; set; }

    [Parameter]
    public FileChangeCallBack OnInputFileChanged { get; set; }

    [Parameter]
    public FileUploadCallBack OnInputFileUpload { get; set; }

    public InputFile InputFileRef { get; set; }

    public IReadOnlyList<IBrowserFile> Files { get; set; }

    async Task OnInputFileChange(InputFileChangeEventArgs e)
    {
        Files = e.GetMultipleFiles(MaximumFileCount);
        if (OnInputFileChanged is null) return;
        if (OnInputFileChanged.IsJsCallback)
        {
            string callback = OnInputFileChanged;
            var upload = await Js.InvokeAsync<IJSObjectReference>("import", "./_content/BlazorComponent/js/upload.js");
            await upload.InvokeVoidAsync("InputFileChanged", callback);
        }
        else if (OnInputFileChanged.IsDelegateCallback)
        {
            Func<IReadOnlyList<IBrowserFile>, Task> callback = OnInputFileChanged;
            await callback(Files);
        }
        else if (OnInputFileUpload.IsEventCallback)
        {
            EventCallback<IReadOnlyList<IBrowserFile>> callback = OnInputFileChanged;
            await callback.InvokeAsync(Files);
        }
    }

    protected async Task InputFileUpload()
    {
        if (OnInputFileUpload is null) return;
        if (OnInputFileUpload.IsJsCallback)
        {
            string callback = OnInputFileUpload;
            var upload = await Js.InvokeAsync<IJSObjectReference>("import", "./_content/BlazorComponent/js/upload.js");
            Value = await upload.InvokeAsync<List<string>>("InputFileUpload", callback);
        }
        else if (OnInputFileUpload.IsDelegateCallback)
        {
            Func<IReadOnlyList<IBrowserFile>, Task<List<string>>> callback = OnInputFileUpload;
            Value = await callback(Files);
        }
        else if (OnInputFileUpload.IsEventCallback)
        {
            EventCallback<(IReadOnlyList<IBrowserFile>, List<string>)> callback = OnInputFileUpload;
            await callback.InvokeAsync((Files, Value));
        }

        if (ValueChanged.HasDelegate) await ValueChanged.InvokeAsync(Value);
    }
}

