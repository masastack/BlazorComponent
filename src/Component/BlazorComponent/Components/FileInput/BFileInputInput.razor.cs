using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BlazorComponent
{
    public partial class BFileInputInput<TValue, TInput> where TInput : IFileInput<TValue>
    {
        public InputFile InputFile
        {
            set
            {
                Component.InputFile = value;
            }
        }

        public EventCallback<InputFileChangeEventArgs> HandleOnFileChange => EventCallback.Factory.Create<InputFileChangeEventArgs>(Component, Component.HandleOnFileChangeAsync);

        public bool Multiple => Component.Multiple;
    }
}
