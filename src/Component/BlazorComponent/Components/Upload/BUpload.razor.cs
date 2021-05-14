using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BUpload : BDomComponentBase
    {
        protected InputFile _uploadElementReference;

        [Parameter]
        public IList<UploadFile> Files { get; set; } = new List<UploadFile>();

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
        public string Accept { get; set; }

        [Parameter]
        public EventCallback<UploadFile> Upload { get; set; }

        [Parameter]
        public RenderFragment Activator { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        protected virtual async Task HandleOnChange(InputFileChangeEventArgs args)
        {
            if (Multiple)
            {
                foreach (var item in args.GetMultipleFiles())
                {
                    var uploadFile = new UploadFile(item);

                    if (Upload.HasDelegate)
                    {
                        await Upload.InvokeAsync(uploadFile);

                        if (string.IsNullOrEmpty(uploadFile.Url))
                            await SetDefaultImageUrl(uploadFile);
                    }
                    else
                    {
                        await SetDefaultImageUrl(uploadFile);
                    }

                    Files.Add(uploadFile);
                }
            }
            else
            {
                Files.Clear();

                var uploadFile = new UploadFile(args.File);


                if (Upload.HasDelegate)
                {
                    await Upload.InvokeAsync(uploadFile);

                    if (string.IsNullOrEmpty(uploadFile.Url))
                        await SetDefaultImageUrl(uploadFile);
                }
                else
                {
                    await SetDefaultImageUrl(uploadFile);
                }

                Files.Add(uploadFile);
            }

            await InvokeStateHasChangedAsync();
        }

        protected virtual async Task HandleOnClick(MouseEventArgs args)
        {
            var uploadRef = _uploadElementReference.Element.Value;
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
    }
}
