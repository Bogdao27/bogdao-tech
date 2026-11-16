using BogdaoTech.Application.Modules.CreateModules;
using BogdaoTech.Application.Providers;
using BogdaoTech.Domain.Modules;
using BogdaoTech.Domain.Shared;
using CSharpFunctionalExtensions;

namespace BogdaoTech.Application.Modules.AddIssue
{
    public class AddIssueHandler
    {
        public AddIssueHandler(IFileProvider fileProvider)
        {
            _fileProvider = fileProvider;
        }

        private readonly IFileProvider _fileProvider;

        public async Task<Result<Guid, Error>> HandlerAsync(
            CancellationToken cancellationToken = default)
        {
            _fileProvider.UploadFile();
        }
    }
}