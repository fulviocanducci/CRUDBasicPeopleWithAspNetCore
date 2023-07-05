using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.ComponentModel.DataAnnotations;
namespace Web.Models.Validations
{
   public sealed class RequiredDateTimeAttribute : ValidationAttribute, IClientModelValidator
   {
      internal const string message = "DateTime is inválid";
      protected override ValidationResult IsValid(object value, ValidationContext validationContext)
      {
         if (value == null)
         {
            return new ValidationResult(ErrorMessage ?? message);
         }
         string text = value.ToString();
         if (text.Length < 10)
         {
            return new ValidationResult(ErrorMessage ?? message);
         }
         if (!DateTime.TryParse(text, out _))
         {
            return new ValidationResult(ErrorMessage ?? message);
         }
         return ValidationResult.Success;
      }

      public void AddValidation(ClientModelValidationContext context)
      {
         if (context == null)
         {
            throw new ArgumentNullException(nameof(context));
         }
         context.Attributes.Add("data-val-required-date-time", ErrorMessage ?? message);
      }
   }
}
