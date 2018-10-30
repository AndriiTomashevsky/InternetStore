using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InternetStore.Infrastructure
{
    public class MustBeMoreThenZeroAttribute : Attribute, IModelValidator
    {
        public IEnumerable<ModelValidationResult> Validate(ModelValidationContext context)
        {
            decimal? value = context.Model as decimal?;

            if (value <= 0)
            {
                return new List<ModelValidationResult> { new ModelValidationResult("", "Value must be more than zero") };
            }
            else
            {
                return Enumerable.Empty<ModelValidationResult>();
            }
        }
    }
}
