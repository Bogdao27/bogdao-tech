using BogdaoTech.Application.Validators;
using FluentValidation;

namespace BogdaoTech.Application.Modules.CreateModules
{
    public class CreateModuleRequestValidator : AbstractValidator<CreateModuleRequest>
    {
        public CreateModuleRequestValidator()
        {
            RuleFor(c => c.Title).MustBeValueObject(Title.Create);
            RuleFor(c => c.Description).MustBeValueObject(Description.Create);
        }
    }
}
