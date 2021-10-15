using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public class DataPagination
    {
        public int Page { get; set; }

        public int ItemsPerPage { get; set; }

        public int PageStart { get; set; }

        public int PageStop { get; set; }

        public int PageCount { get; set; }

        public int ItemsLength { get; set; }
    }
}
