using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent.Doc.CLI.Models
{
    public class DemoComponentModel
    {
        public string Title { get; set; }

        public string SubTitle { get; set; }

        public string Type { get; set; }

        public string Desc { get; set; }

        public string ApiDoc { get; set; }

        public int? Cols { get; set; }

        public string Cover { get; set; }

        public List<DemoItemModel> DemoList { get; set; }
        public int Order { get; set; }
    }

    public class DemoItemModel
    {
        public decimal Order { get; set; }

        public string Name { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Code { get; set; }

        public string Type { get; set; }

        public string Style { get; set; }

        public int? Iframe { get; set; }

        public bool? Docs { get; set; }

        public bool Debug { get; set; }
    }
}
