using BogdaoTech.Application.FileProvider;
using BogdaoTech.Application.Modules.CreateModules;
using BogdaoTech.Application.Providers;
using BogdaoTech.Domain.Modules;
using BogdaoTech.Domain.Shared;
using CSharpFunctionalExtensions;

namespace BogdaoTech.Application.Modules.AddIssue
{
    public class AddIssueHandler
    {
        private const string BUCKET_NAME = "files";
        
        public AddIssueHandler(IFileProvider fileProvider, IModulesRepository modulesRepository)
        {
            _fileProvider = fileProvider;
            _modulesRepository = modulesRepository;
        }

        private readonly IFileProvider _fileProvider;
        private readonly IModulesRepository _modulesRepository;

        public async Task<Result<Guid, Error>> HandlerAsync(
            AddIssueCommand command,
            CancellationToken cancellationToken = default)
        {
            var moduleResult = await _modulesRepository.GetById(
                ModuleId.Create(command.ModuleId), cancellationToken);

            if (moduleResult.IsFailure)
            {
                return moduleResult.Error;
            }

            var issueId = IssueId.NewIssueId();
            var title = Title.Create(command.Title).Value;
            var description = Description.Create(command.Description).Value;
            var lessonId = LessonId.Empty();

            // загрузка файлов в minio, создание VO файлов

            List<FileContent> fileContents = [];
            foreach (var file in command.Files)
            {
                var extension = Path.GetExtension(file.FIleName); 

                var filePath = FilePath.Create(Guid.NewGuid(), extension);
                if (filePath.IsFailure)
                {
                    return filePath.Error;
                }

                var fileContent = new FileContent(file.Content, filePath.Value.Path);

                fileContents.Add(fileContent);
            }

            var fileData = new FileData(fileContents, BUCKET_NAME);
            
            var uploadResult = await _fileProvider.UploadFiles(fileData, cancellationToken);
            if (uploadResult.IsFailure)
            {
                return uploadResult.Error;
            }

            var filePaths = command.Files
                .Select(f => FilePath.Create(Guid.NewGuid(), f.FIleName).Value);

            var issueFiles = filePaths.Select(f => new IssueFile(f));
            
            var issue = new Issue(
                issueId,
                lessonId,
                title,
                description,
                null,
                new FilesList(issueFiles));

            moduleResult.Value.AddIssue(issue);

            await _modulesRepository.Save(moduleResult.Value, cancellationToken);

            return issue.Id.Value;
        }
    }
}