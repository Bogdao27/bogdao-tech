using BogdaoTech.Domain.Shared;
using CSharpFunctionalExtensions;

public record IssueFile
{
    public IssueFile(FilePath pathToStorage)
    {
        PathToStorage = pathToStorage;
    }
    
    public FilePath PathToStorage { get; }
    public int FileSize { get; }
}
