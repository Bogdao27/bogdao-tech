using BogdaoTech.Application.FileProvider;
using BogdaoTech.Domain.Shared;
using CSharpFunctionalExtensions;

namespace BogdaoTech.Application.Providers
{
    public interface IFileProvider
    {
        Task<Result<IReadOnlyList<FilePath>, Error>> UploadFiles(
            IEnumerable<FileData> filesData,
            CancellationToken cancellationToken = default);
    }
}
