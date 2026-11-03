using BogdaoTech.Domain.Shared;
using System.Net;

public class Issue : Entity<IssueId>//Задачи в модуле
{
    //Ef core
    private Issue(IssueId id) : base(id)
    {
    }

    public Guid? LessonId { get; set; } //Айдишник урока

    public string Title { get; set; } = default!; //Название задачи

    public string Description { get; set; } = default!; //Описание задачи

    public Issue? ParentIssue { get; set; }

    public List<Issue> SubIssues { get; set; } = []; //Подзадачи

    public IssueDetails? Details { get; private set; } //У задач есть много файлов
}
