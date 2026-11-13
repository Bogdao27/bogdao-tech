using BogdaoTech.Application.Modules;
using BogdaoTech.Domain.Modules;
using BogdaoTech.Domain.Shared;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace BogdaoTech.Infrastructure.Repositories
{
    public class ModulesRepository : IModulesRepository
    {
        //с помощью этого поля мы получаем доступ к контексту БД, чтобы в дальнейшем
        //с ним как-то взаимодействовать
        private readonly ApplicationDbContext _dbContext;

        public ModulesRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //создаем первый способ взаимодействия с БД, можно создать новый модуль в БД
        public async Task<Guid> Add(
            Module module, 
            CancellationToken cancellationToken = default)
        {
            //получаем доступ к DbSet контекста БД
            await _dbContext.Modules.AddAsync(module, cancellationToken);

            //сохраняем изменения
            await _dbContext.SaveChangesAsync(cancellationToken);

            return module.Id;
        }

        public async Task<Result<Module, Error>> GetById(
            ModuleId moduleId, 
            CancellationToken cancellationToken = default)
        {
            var module = await _dbContext.Modules
                .Include(m => m.Issues)
                .ThenInclude(i => i.SubIssues)
                .FirstOrDefaultAsync(m => m.Id == moduleId, cancellationToken);

            if (module is null)
            {
                return Errors.General.NotFound(moduleId);
            }

            return module;
        }

        public async Task<Result<Module, Error>> GetByTitle(
            Title title, 
            CancellationToken cancellationToken = default)
        {
            var module = await _dbContext.Modules
                .Include(m => m.Issues)
                .ThenInclude(i => i.SubIssues)
                .FirstOrDefaultAsync(m => m.Title == title, cancellationToken);

            if (module is null)
            {
                return Errors.General.NotFound();
            }

            return module;
        }

        public async Task<Guid> Save(
            Module module, 
            CancellationToken cancellationToken = default)
        {
            _dbContext.Modules.Attach(module);
            
            await _dbContext.SaveChangesAsync(cancellationToken);

            return module.Id;
        }

        public async Task<Guid> Delete(
            Module module, 
            CancellationToken cancellationToken = default)
        {
            _dbContext.Remove(module);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return module.Id;
        }
    }
}
