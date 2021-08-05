using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public class MenuContext
    {
        public Func<MouseEventArgs,Task> OnClick{ get; set; }
    }
}
