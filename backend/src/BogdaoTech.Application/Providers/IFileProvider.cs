using BogdaoTech.Application.FileProvider;
using BogdaoTech.Domain.Shared;
using CSharpFunctionalExtensions;

namespace BogdaoTech.Application.Providers
{
    public interface IFileProvider
    {
        Task<UnitResult<Error>> UploadFiles(
            FileData filesData,
            CancellationToken cancellationToken = default);
    }
}
