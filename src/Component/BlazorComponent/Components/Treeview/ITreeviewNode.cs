using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public interface ITreeviewNode<TItem, TKey> : IHasProviderComponent
    {
        RenderFragment<TItem> PrependContent { get; }

        RenderFragment LabelContent { get; }

        string Text { get; }

        RenderFragment AppendContent { get; }

        bool Selectable { get; }

        string ComputedIcon { get; }

        bool HasChildren { get; }

        bool IsLoading { get; }

        string LoadingIcon { get; }

        string ExpandIcon { get; }

        int Level { get; }

        bool IsOpen { get; }

        List<TItem> ComputedChildren { get; }

        bool Disabled { get; }

        Task HandleOnClick(MouseEventArgs args);

        TKey Key { get; }

        bool IsSelected { get; }

        bool IsIndeterminate { get; }

        TItem Item { get; }
    }
}
