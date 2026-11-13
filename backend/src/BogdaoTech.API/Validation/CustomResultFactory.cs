using BogdaoTech.API.Extensions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Results;

namespace BogdaoTech.API.Validation
{
    public class CustomResultFactory : IFluentValidationAutoValidationResultFactory
    {
        public Task<IActionResult?> CreateActionResult(
            ActionExecutingContext context, 
            ValidationProblemDetails validationProblemDetails, 
            IDictionary<IValidationContext, ValidationResult> validationResults)
        {
            var errors = validationResults.Values
                .SelectMany(result => result.Errors)
                .ToList();

            if (errors.Count == 0)
            {
                return Task.FromResult<IActionResult?>(
                    new BadRequestObjectResult(validationProblemDetails));
            }

            var response = new ValidationResult(errors).ToValidationErrorResponse();

            return Task.FromResult<IActionResult?>(response);
        }
    }
}
