using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Hospital_reservation_system.validations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class FutureDateAttribute: ValidationAttribute, IClientModelValidator
    {
            public override bool IsValid(object value)
            {
                DateTime date = (DateTime)value;
                return date >= DateTime.Now;
            }

            public void AddValidation(ClientModelValidationContext context)
            {
                context.Attributes.Add("data-val", "true");
                context.Attributes.Add("data-val-futuredate", "Please select a future date.");
            }
        

    }
}
