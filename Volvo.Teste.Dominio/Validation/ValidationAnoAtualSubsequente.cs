using System;
using System.ComponentModel.DataAnnotations;

namespace Volvo.Teste.Dominio.Validation
{
    public class ValidationAnoAtualSubsequente: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            if (int.Parse(value.ToString()) == DateTime.Now.Year || 
                            int.Parse(value.ToString()) == DateTime.Now.AddYears(1).Year)
                return ValidationResult.Success;

            return new ValidationResult("Ano inválido, ano tem que ser o atual ou subsequente!");
        }
    }
}
