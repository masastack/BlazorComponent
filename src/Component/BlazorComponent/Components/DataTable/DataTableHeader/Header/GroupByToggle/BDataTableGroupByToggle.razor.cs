using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BDataTableGroupByToggle<TDataTableHeader> where TDataTableHeader : IDataTableHeader
    {
        [Parameter]
        public DataTableHeader Header { get; set; }

        public EventCallback<MouseEventArgs> HandleOnGroup => CreateEventCallback<MouseEventArgs>(async args=> await Component.HandleOnGroup(Header.Value));
    }
}
