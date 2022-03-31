using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;

namespace BlazorComponent
{
    public partial class BUploadImage
    {
        string? _defaultImageUrl;

        [Parameter]
        public RenderFragment<List<string>>? ChildContent { get; set; }

        [Parameter]
        public int MaximumFileCount { get; set; } = 10;

        [Parameter]
        public string DefaultImageUrl
        {
            get => _defaultImageUrl ?? throw new Exception("Please set parameter DefaultImageUrl value");
            set
            {
                if ((ImageUrls.FirstOrDefault() ?? _defaultImageUrl) == _defaultImageUrl)
                {
                    ImageUrls.Clear();
                    ImageUrls.Add(value);
                }
                _defaultImageUrl = value;
            }
        }

        [Parameter]
        public int PreviewImageWith { get; set; } = 100;

        [Parameter]
        public int PreviewImageHeight { get; set; } = 100;

        [Parameter]
        public bool Multiple { get; set; }

        [Parameter]
        public EventCallback<IReadOnlyList<IBrowserFile>> OnChange { get; set; }

        Func<List<DotNetStreamReference>, Task<List<string>>> GetImageUsingStreaming { get; set; }

        List<string> ImageUrls { get; set; } = new();

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                var upload = await Js.InvokeAsync<IJSObjectReference>("import", "./_content/BlazorComponent/js/upload.js");
                GetImageUsingStreaming = async streams => await upload.InvokeAsync<List<string>>("GetImageUsingStreaming", streams);
            }
        }

        async Task OnInputFileChange(InputFileChangeEventArgs e)
        {
            var images = e.GetMultipleFiles(MaximumFileCount);
            if (OnChange.HasDelegate)
            {
                await OnChange.InvokeAsync(images);
            }
            if (images.Count > 0)
            {
                var imageStreams = new List<DotNetStreamReference>();
                foreach (var image in images)
                {
                    var resizedImage = await image.RequestImageFileAsync(image.ContentType, PreviewImageWith, PreviewImageHeight);
                    var imageStream = new DotNetStreamReference(resizedImage.OpenReadStream());
                    imageStreams.Add(imageStream);
                }
                ImageUrls.Clear();
                var imageUrls = await GetImageUsingStreaming(imageStreams);
                ImageUrls.AddRange(imageUrls);
            }
        }
    }
}
