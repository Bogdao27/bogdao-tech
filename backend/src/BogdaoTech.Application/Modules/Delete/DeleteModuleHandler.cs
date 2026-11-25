using BogdaoTech.Application.Database;
using BogdaoTech.Application.Modules.CreateModules;
using BogdaoTech.Application.Modules.UpdateMainInfo;
using BogdaoTech.Domain.Shared;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace BogdaoTech.Application.Modules.Delete
{
    public class DeleteModuleHandler
    {
        private readonly IModulesRepository _modulesRepository;
        private readonly ILogger<DeleteModuleHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteModuleHandler(
            IModulesRepository modulesRepository,
            ILogger<DeleteModuleHandler> logger,
            IUnitOfWork unitOfWork)
        {
            _modulesRepository = modulesRepository;
            _logger = logger;
            _unitOfWork = unitOfWork;
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

            moduleResult.Value.Delete();

            await _unitOfWork.SaveChanges();

            _logger.LogInformation("Updated deleted with id {moduleId}", request.ModuleId);

            return moduleResult.Value.Id.Value;
        }
    }
}
