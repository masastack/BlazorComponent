using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BTextFieldAppendSlot<TValue>
    {
        public string AppendOuterIcon => Input.AppendOuterIcon;

        public RenderFragment AppendOuterContent => Input.AppendOuterContent;
    }
}
