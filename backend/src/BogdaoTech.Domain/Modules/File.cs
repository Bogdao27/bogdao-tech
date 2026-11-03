using BogdaoTech.Domain.Shared;

public record File
{
    private File(string pathToStorage)
    {
        PathToStorage = pathToStorage;
    }
    
    public string PathToStorage { get; } //Путь к файлу, не храним его в базе

    public static Result<File> Create(string pathToStorage)
    {
        if(string.IsNullOrWhiteSpace(pathToStorage))
        {
            return "PathToStorage cannot be empty";
        }

        return pathToStorage;
    }
}
