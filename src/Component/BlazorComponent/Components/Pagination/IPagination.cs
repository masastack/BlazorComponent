using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public interface IPagination : IHasProviderComponent
    {
        string GetIcon(int index);

        IEnumerable<StringNumber> GetItems();

        bool PrevDisabled => default;

        bool NextDisabled => default;

        Task HandlePreviousAsync(MouseEventArgs args);

        Task HandleNextAsync(MouseEventArgs args);

        Task HandleItemClickAsync(StringNumber item);

        string PrevIcon => default;

        int Value => default;
    }
}
