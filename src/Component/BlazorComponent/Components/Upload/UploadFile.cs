using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Text.Json.Serialization;

namespace BlazorComponent
{
    public class UploadFile
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("fileName")]
        public string FileName { get; set; }

        [JsonPropertyName("uploaded")]
        public bool Uploaded { get; set; }

        [JsonPropertyName("error")]
        public string Error { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }

        [JsonIgnore]
        public IBrowserFile BrowserFile { get; set; }

        [JsonIgnore]
        public bool IsImage { get; set; }

        internal bool Hover { get; set; }

        public UploadFile()
        {
            Id = Guid.NewGuid().ToString();

            if (BrowserFile != null)
            {
                if (FileName == null)
                {
                    FileName = BrowserFile.Name;
                }

                IsImage = BrowserFile.ContentType.StartsWith("image/");
            }
        }

        public UploadFile(IBrowserFile file)
        {
            if (file == null)
            {
                throw new ArgumentException(nameof(file));
            }

            Id = Guid.NewGuid().ToString();
            BrowserFile = file;

            if (BrowserFile != null)
            {
                if (FileName == null)
                {
                    FileName = BrowserFile.Name;
                }

                IsImage = BrowserFile.ContentType.StartsWith("image/");
            }
        }
    }
}
