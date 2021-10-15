using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public interface IDataTableHeader : IHasProviderComponent
    {
        bool SingleSelect { get; }

        RenderFragment DataTableSelectContent { get; }

        bool DisableSort { get; }

        string SortIcon { get; }

        bool ShowGroupBy { get; }

        RenderFragment<DataTableHeader> HeaderColContent { get; }

        Task HandleOnGroup(string group);

        Dictionary<string, object> GetHeaderAttrs(DataTableHeader header);

        DataOptions Options { get; }
    }
}

