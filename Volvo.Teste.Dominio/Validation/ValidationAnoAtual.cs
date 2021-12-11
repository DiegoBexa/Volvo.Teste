using System;
using System.ComponentModel.DataAnnotations;

namespace Volvo.Teste.Dominio.Validation
{
    public class ValidationAnoAtual : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            if (value.ToString() == DateTime.Now.Year.ToString())
                return ValidationResult.Success;

            return new ValidationResult("Ano inválido, ano tem que ser o atual!");
        }
    }
}
