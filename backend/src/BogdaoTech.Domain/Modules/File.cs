using BogdaoTech.Domain.Shared;
using CSharpFunctionalExtensions;

public record File
{
    private File(string pathToStorage)
    {
        PathToStorage = pathToStorage;
    }
    
    public string PathToStorage { get; } //Путь к файлу, не храним его в базе

    public static Result<File, Error> Create(string pathToStorage)
    {
        if(string.IsNullOrWhiteSpace(pathToStorage))
        {
            return Errors.General.ValueIsInvalid("pathToStorage");
        }

        return new File(pathToStorage);
    }
}
