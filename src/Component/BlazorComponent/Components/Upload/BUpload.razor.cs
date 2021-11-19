using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BUpload : BDomComponentBase
    {
        protected InputFile _inputFileReference;

        [Parameter]
        public List<UploadFile> Files { get; set; } = new List<UploadFile>();

        [Parameter]
        public EventCallback<IList<UploadFile>> FilesChanged { get; set; }

        [Parameter]
        public bool Multiple { get; set; }

        [Parameter]
        public bool ShowUploadList { get; set; } = true;

        [Parameter]
        public bool Counter { get; set; }

        [Parameter]
        public bool ShowSize { get; set; }

        [Parameter]
        public bool Chips { get; set; }

        [Parameter]
        public bool Card { get; set; }

        [Parameter]
        public StringNumber CardSize { get; set; } = 86;

        [Parameter]
        public string Accept { get; set; }

        [Parameter]
        public EventCallback<List<UploadFile>> OnUpload { get; set; }

        [Parameter]
        public RenderFragment ActivatorContent { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        public ElementReference? Element
        {
            get => _inputFileReference.Element;
        }

        protected virtual async Task HandleOnChange(InputFileChangeEventArgs args)
        {
            var uploadFiles = Multiple
                ? args.GetMultipleFiles().Select(file => new UploadFile(file)).ToList()
                : new List<UploadFile>() { new UploadFile(args.File) };

            if (OnUpload.HasDelegate)
            {
                await OnUpload.InvokeAsync(uploadFiles);
            }
            else
            {
                Files.AddRange(uploadFiles);
            }

            foreach (var file in uploadFiles)
            {
                if (file.IsImage)
                {
                    await SetDefaultImageUrl(file);
                }
            }
        }

        protected virtual async Task HandleOnClick(MouseEventArgs args)
        {
            var uploadRef = _inputFileReference.Element.Value;
            await JsInvokeAsync(JsInteropConstants.TriggerEvent, uploadRef, "MouseEvents", "click");
        }

        protected virtual void RemoveFile(UploadFile file)
        {
            Files.Remove(file);
        }

        protected virtual string FormatSize(long size)
        {
            if (size < 1024)
            {
                return $"{size}B";
            }
            else if (size < 1024 * 1024)
            {
                return $"{size / 1024}KB";
            }
            else
            {
                return $"{size / 1024 / 1024}M";
            }
        }

        private async Task SetDefaultImageUrl(UploadFile file)
        {
            if (file.IsImage && string.IsNullOrEmpty(file.Url))
            {
                var buffers = new byte[file.BrowserFile.Size];
                await file.BrowserFile.OpenReadStream().ReadAsync(buffers);
                file.Url = $"data:{file.BrowserFile.ContentType};base64,{Convert.ToBase64String(buffers)}";
            }
        }

        protected virtual string GetColorCss(bool uploaded) => "";

        protected virtual string GetListItemStyle(bool uploaded) => "";
    }
}
