using FluentValidation;

namespace BogdaoTech.Application.Modules.Delete
{
    public class DeleteModuleRequestValidator : AbstractValidator<DeleteModuleRequest>
    {
        public DeleteModuleRequestValidator()
        {
            RuleFor(d => d.ModuleId).NotEmpty();
        }
    }
}
