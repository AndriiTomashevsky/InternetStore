using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetStore.Infrastructure.CustomModelBinder
{
    public class CustomIntegerModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context.Metadata.PropertyName == "Power")
            {
                return new CustomIntegerModelBinder();
            }
            else
            {
                return null;
            }
        }
    }
}
