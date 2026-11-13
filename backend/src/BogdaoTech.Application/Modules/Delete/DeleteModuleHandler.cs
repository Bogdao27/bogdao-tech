using BogdaoTech.Application.Modules.CreateModules;
using BogdaoTech.Application.Modules.UpdateMainInfo;
using BogdaoTech.Domain.Shared;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace BogdaoTech.Application.Modules.Delete
{
    public record DeleteModuleRequest(Guid ModuleId);
    
    public class DeleteModuleRequestValidator : AbstractValidator<DeleteModuleRequest>
    {
        public DeleteModuleRequestValidator()
        {
            RuleFor(d => d.ModuleId).NotEmpty();
        }
    }
    
    public class DeleteModuleHandler
    {
        private readonly IModulesRepository _modulesRepository;
        private readonly ILogger<DeleteModuleHandler> _logger;

        public DeleteModuleHandler(
            IModulesRepository modulesRepository,
            ILogger<DeleteModuleHandler> logger)
        {
            _modulesRepository = modulesRepository;
            _logger = logger;
        }

        public async Task<Result<Guid, Error>> HandlerAsync(
            DeleteModuleRequest request,
            CancellationToken cancellationToken = default)
        {
            var moduleResult = await _modulesRepository
                .GetById(request.ModuleId, cancellationToken);
            if (moduleResult.IsFailure)
            {
                return moduleResult.Error;
            }

            var result = await _modulesRepository.Delete(moduleResult.Value, cancellationToken);

            _logger.LogInformation("Module deleted with id {moduleId}", request.ModuleId);

            return result;
        }
    }
}
