using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
namespace Web.Models.Validations
{
   public sealed class RequiredMoneyAttribute : ValidationAttribute, IClientModelValidator
   {
      public int? Minimum { get; set; }
      public int? Maximum { get; set; }
      public RequiredMoneyAttribute()
      {
         Minimum = null;
         Maximum = null;
      }
      public RequiredMoneyAttribute(int minimum)
      {
         Minimum = minimum;
         Maximum = null;
      }
      public RequiredMoneyAttribute(int minimum, int maximum)
      {
         Minimum = minimum;
         Maximum = maximum;
      }

      protected override ValidationResult IsValid(object value, ValidationContext validationContext)
      {
         if (value is not null)
         {
            if (!decimal.TryParse(value.ToString(), out decimal decimalValue))
            {
               return new ValidationResult(ErrorMessage ?? "Money inválid");
            }
            if (Minimum is not null && decimalValue < Minimum)
            {
               return new ValidationResult(ErrorMessage ?? "Money inválid");
            }
            if (Maximum is not null && Maximum < decimalValue)
            {
               return new ValidationResult(ErrorMessage ?? "Money inválid");
            }
            return ValidationResult.Success;
         }
         return new ValidationResult(ErrorMessage ?? "Money inválid");
      }

      public void AddValidation(ClientModelValidationContext context)
      {
         context.Attributes.Add("data-val-required-money", ErrorMessage ?? "Money inválid");
         if (Minimum.HasValue)
         {
            context.Attributes.Add("data-val-required-money-minimum", Minimum.ToString());
         }
         if (Maximum.HasValue)
         {
            context.Attributes.Add("data-val-required-money-maximum", Maximum.ToString());
         }
      }
   }
}
