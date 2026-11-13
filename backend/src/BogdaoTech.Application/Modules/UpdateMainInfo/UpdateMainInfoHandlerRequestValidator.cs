using BogdaoTech.Application.Validators;
using BogdaoTech.Domain.Shared;
using FluentValidation;

namespace BogdaoTech.Application.Modules.UpdateMainInfo
{
    public class UpdateMainInfoHandlerRequestValidator : AbstractValidator<UpdateMainInfoRequest>
    {
        public UpdateMainInfoHandlerRequestValidator()
        {
            RuleFor(r => r.ModuleId).NotEmpty().WithError(Errors.General.ValueIsRequired());
        }
    }

    public class UpdateMainInfoHandlerDtoValidator : AbstractValidator<UpdateMainInfoDto>
    {
        public UpdateMainInfoHandlerDtoValidator()
        {
            RuleFor(r => r.Title).MustBeValueObject(Title.Create);
            RuleFor(r => r.Description).MustBeValueObject(Description.Create);
        }
    }
}
