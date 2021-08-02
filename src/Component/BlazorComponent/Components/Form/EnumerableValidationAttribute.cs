using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    [AttributeUsage(AttributeTargets.Property)]
    public class EnumerableValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var result = new EnumerableValidationResult();
            if (value is IEnumerable enumerable)
            {
                foreach (var item in enumerable)
                {
                    var context = new ValidationContext(item);
                    var validationResults = new List<ValidationResult>();
                    Validator.TryValidateObject(item, context, validationResults, true);

                    var descriptor = new ValidationResultDescriptor(item, validationResults);
                    result.Descriptors.Add(descriptor);
                }

                return result;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
    }
}
