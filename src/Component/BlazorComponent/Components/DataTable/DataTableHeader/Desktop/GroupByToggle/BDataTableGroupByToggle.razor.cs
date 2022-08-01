using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    public partial class BDataTableGroupByToggle<TDataTableHeader> where TDataTableHeader : IDataTableHeader
    {
        [Parameter]
        public DataTableHeader Header { get; set; }

        public EventCallback<MouseEventArgs> HandleOnGroup => CreateEventCallback<MouseEventArgs>(async args => await Component.HandleOnGroup(Header.Value));
    }
}
