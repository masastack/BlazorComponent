using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public interface IValidatable
    {
        Task<bool> ValidateAsync();

        Task ResetAsync();

        Task ResetValidationAsync();
    }
}
