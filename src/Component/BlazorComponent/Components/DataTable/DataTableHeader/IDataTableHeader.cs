using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public interface IDataTableHeader : IHasProviderComponent
    {
        bool SingleSelect { get; }

        RenderFragment DataTableSelectContent { get; }

        bool DisableSort { get; }
        
        bool MultiSort { get; }

        string SortIcon { get; }

        bool ShowGroupBy { get; }

        RenderFragment<DataTableHeader> HeaderColContent { get; }

        Task HandleOnGroup(string group);

        Task HandleOnHeaderColClick(string value);

        DataOptions Options { get; }
    }
}

