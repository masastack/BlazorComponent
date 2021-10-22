using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BDialogContent<TDialog> where TDialog : IDialog
    {
        public ElementReference ContentRef
        {
            set { Component.ContentRef = value; }
        }

        public Dictionary<string, object> ContentAttrs => Component.ContentAttrs;
    }
}
