using BogdaoTech.Domain.Modules;
using BogdaoTech.Domain.Shared;
using CSharpFunctionalExtensions;

namespace BogdaoTech.Application.Modules
{
    public interface IModulesRepository
    {
        Task<Guid> Add(Module module, CancellationToken cancellationToken = default);
        Task<Result<Module, Error>> GetById(
            ModuleId moduleId, 
            CancellationToken cancellationToken = default);
        Task<Result<Module, Error>> GetByTitle(
            Title title, 
            CancellationToken cancellationToken = default);
        Task<Guid> Save(Module module, CancellationToken cancellationToken = default);
        Task<Guid> Delete(Module module, CancellationToken cancellationToken = default);
    }
}
