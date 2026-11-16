using BogdaoTech.Application.FileProvider;
using BogdaoTech.Domain.Shared;
using CSharpFunctionalExtensions;

namespace BogdaoTech.Application.Providers
{
    public interface IFileProvider
    {
        Task<Result<string, Error>> UploadFile(
        FileData fileData,
        CancellationToken cancellationToken = default);
    }
}
