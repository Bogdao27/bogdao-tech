public class File
{
    public Guid Id { get; set; } //Айдишник файла

    public string Path { get; set; } = default!; //Путь к файлу, не храним его в базе
}
