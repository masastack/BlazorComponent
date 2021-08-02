using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public class EnumerableValidationResult : ValidationResult
    {
        public EnumerableValidationResult()
            : base("Invalid enumerable.")
        {

        }

        public List<ValidationResultDescriptor> Descriptors { get; } = new();
    }

    public class ValidationResultDescriptor
    {
        public ValidationResultDescriptor(object objectInstance, List<ValidationResult> results)
        {
            ObjectInstance = objectInstance;
            Results = results ?? throw new ArgumentNullException(nameof(results));
        }

        public object ObjectInstance { get; }

        public List<ValidationResult> Results { get; }
    }
}
