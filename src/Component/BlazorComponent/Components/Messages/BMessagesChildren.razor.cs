using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BMessagesChildren<TMessages> where TMessages : IMessages
    {
        public List<string> Value => Component.Value;
    }
}
