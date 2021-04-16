using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent.Doc.CLI.Models
{
    public class DemoMenuItemModel
    {
        public int Order { get; set; }

        public string Title { get; set; }

        public string SubTitle { get; set; }

        public string Type { get; set; }

        public string Url { get; set; }

        public string Cover { get; set; }

        public DemoMenuItemModel[] Children { get; set; }
    }
}
