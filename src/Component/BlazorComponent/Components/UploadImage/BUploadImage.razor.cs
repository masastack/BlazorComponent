using System.Text.Json;

namespace BlazorComponent
{
    public partial class BUploadImage : BUpload
    {
        [Parameter]
        public RenderFragment<List<string>>? ChildContent { get; set; }

        [Parameter]
        public string DefaultImage
        {
            set
            {
                if (Value.Count == 0)
                {
                    Value.Add(value);
                    PreviewImageUrls.Add(value);
                }
            }
        }

        [Parameter]
        public int PreviewImageWith { get; set; } = 100;

        [Parameter]
        public int PreviewImageHeight { get; set; } = 100;

        [Parameter]
        public string Icon { get; set; } = "./_content/Masa.Blazor/images/upload/upload.svg";

        [Parameter]
        public string ImageClass { get; set; } = "mr-4";

        public List<string> PreviewImageUrls { get; set; } = new();

        protected override void OnInitialized()
        {
            Accept = "image/*";
            OnInputFileChanged = "GetPreviewImageUrls";
        }

        protected override async Task OnInputFileChange(InputFileChangeEventArgs e)
        {
            await base.OnInputFileChange(e);
            if (OnInputFileChanged.JsCallbackValue.Equals(default) is false)
            {
                PreviewImageUrls = OnInputFileChanged.JsCallbackValue.Deserialize<List<string>>();
            }
        }
    }
}
