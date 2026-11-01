using System.Net;

public class Issue //Задачи в модуле
{
    public Guid Id { get; set; } //Айдишник задачи в базе данных

    public Guid LessonId { get; set; } //Айдишник урока

    public string Title { get; set; } = default!; //Название задачи

    public string Description { get; set; } = default!; //Описание задачи

    public Issue? ParentIssue { get; set; }

    public List<Issue> SubIssues { get; set; } = []; //Подзадачи

    public List<File> Files { get; set; } = []; //У задач есть много файлов
}
