using BogdaoTech.Application.Database;
using BogdaoTech.Application.FileProvider;
using BogdaoTech.Application.Modules.CreateModules;
using BogdaoTech.Application.Providers;
using BogdaoTech.Domain.Modules;
using BogdaoTech.Domain.Shared;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;

namespace BogdaoTech.Application.Modules.AddIssue
{
    public class AddIssueHandler
    { 
        private const string BUCKET_NAME = "";
        
        public AddIssueHandler(
            IFileProvider fileProvider, 
            IModulesRepository modulesRepository,
            IUnitOfWork unitOfWork,
            ILogger<AddIssueHandler> logger)
        {
            _fileProvider = fileProvider;
            _modulesRepository = modulesRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        private readonly IFileProvider _fileProvider;
        private readonly IModulesRepository _modulesRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AddIssueHandler> _logger;

        public async Task<Result<Guid, Error>> HandlerAsync(
            AddIssueCommand command,
            CancellationToken cancellationToken = default)
        {
            var transaction = await _unitOfWork.BeginTransaction(cancellationToken);

            try
            {
                var moduleResult = await _modulesRepository
                    .GetById(ModuleId.Create(command.ModuleId), cancellationToken);

                if (moduleResult.IsFailure)
                {
                    return moduleResult.Error;
                }

                var issueId = IssueId.NewIssueId();
                var title = Title.Create(command.Title).Value;
                var description = Description.Create(command.Description).Value;
                var lessondId = LessonId.Empty();

                List<FileData> filesData = [];
                foreach (var file in command.Files)
                {
                    var extension = Path.GetExtension(file.FIleName);

                    var filePath = FilePath.Create(Guid.NewGuid(), extension);
                    if (filePath.IsFailure)
                    {
                        return filePath.Error;
                    }

                    var fileContent = new FileData(file.Content, filePath.Value, BUCKET_NAME);

                    filesData.Add(fileContent);
                }

                var issueFiles = filesData
                    .Select(f => f.FilePath)
                    .Select(f => new IssueFile(f))
                    .ToList();

                var issue = new Issue(issueId, lessondId, title, description, null, issueFiles);

                moduleResult.Value.AddIssue(issue);

                await _unitOfWork.SaveChanges(cancellationToken);

                var uploadResult = await _fileProvider.UploadFiles(filesData, cancellationToken);
                if (uploadResult.IsFailure)
                {
                    return uploadResult.Error;
                }

                transaction.Commit();

                return issue.Id.Value;    
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                "Can not add issue to module - {id} in transaction", command.ModuleId);

                transaction.Rollback();

                return Error.Failure("module.issue.failure", "Can not add issue to module - {id}");
            }
        }
    }
}