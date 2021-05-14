using Microsoft.AspNetCore.Components.Forms;
using System;

namespace BlazorComponent
{
    public class UploadFile
    {
        public string Id { get; set; }

        public string FileName { get; set; }

        public bool Uploaded { get; set; }

        public string Error { get; set; }

        public string Url { get; set; }

        public IBrowserFile BrowserFile { get; private set; }

        public string FormatedFileName => FileName ?? BrowserFile?.Name;

        public bool IsImage { get; set; }

        public UploadFile()
        {
            Id = Guid.NewGuid().ToString();
            if (BrowserFile != null)
                IsImage = BrowserFile.ContentType.StartsWith("image/");
        }

        public UploadFile(IBrowserFile file)
        {
            Id = Guid.NewGuid().ToString();
            BrowserFile = file;

            if (BrowserFile != null)
                IsImage = BrowserFile.ContentType.StartsWith("image/");
        }
    }
}
