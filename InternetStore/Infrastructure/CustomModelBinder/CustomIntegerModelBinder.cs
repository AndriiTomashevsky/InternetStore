using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace InternetStore.Infrastructure.CustomModelBinder
{
    public class CustomIntegerModelBinder : IModelBinder
    {
        SimpleTypeModelBinder simpleTypeModelBinder = new SimpleTypeModelBinder(typeof(Int32));

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            string power = bindingContext.ValueProvider.GetValue("Power").FirstValue;

            if (power == null)
            {
                return simpleTypeModelBinder.BindModelAsync(bindingContext);
            }
            else
            {
                string currencyDecimalSeparator = CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;
                power = currencyDecimalSeparator == "." ? Regex.Replace(power, @",", ".") : Regex.Replace(power, @"\.", ",");

                int result = (int)Math.Ceiling(double.Parse(power));

                bindingContext.Result = ModelBindingResult.Success(result);
                return Task.CompletedTask;
            }
        }
    }
}
