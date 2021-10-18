using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BDataTableFooters<TItem, TDataTable> where TDataTable : IDataTable<TItem>
    {
        public RenderFragment FooterContent => Component.FooterContent;

        public bool HideDefaultFooter => Component.HideDefaultFooter;
    }
}
