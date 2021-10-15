using OneOf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BDataFooterItemsPerPageSelect<TComponent> where TComponent : IDataFooter
    {
        public List<DataItemsPerPageOption> ComputedDataItemsPerPageOptions => Component.ComputedDataItemsPerPageOptions;

        public string ItemsPerPageText => Component.ItemsPerPageText;
    }
}
