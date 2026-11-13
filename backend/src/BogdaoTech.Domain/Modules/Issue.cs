using BogdaoTech.Domain.Modules;
using BogdaoTech.Domain.Shared;
using CSharpFunctionalExtensions;

public class Issue : BogdaoTech.Domain.Shared.Entity<IssueId>, ISoftDeletable
{
    private bool _isDeleted = false;

    private readonly List<Issue> _subIssues = [];

    //Ef core
    private Issue(IssueId id) : base(id)
    {
    }
    
    private Issue (
        IssueId id,
        LessonId lessonId,
        Title title,
        Description description,
        Issue? parentIssue,
        IssueDetails? details) : base(id)
    {
        LessonId = lessonId;
        Title = title;
        Description = description;
        ParentIssue = parentIssue;
        Details = details;
    }

    public void AddSubIssue(Issue issue)
    {
        //logic and validation
        _subIssues.Add(issue);
    }

    public static Result<Issue> Create(
        IssueId id,
        LessonId lessonId,
        Title title,
        Description description,
        Issue? parentIssue,
        IssueDetails? details)
    {
        return new Issue(id, lessonId, title, description, parentIssue, details);
    }

    public void Delete()
    {
        if (_isDeleted == false)
        {
            _isDeleted = true;
        }
    }

    public void Restore()
    {
        if (_isDeleted)
        {
            _isDeleted = false;
        }
    }

    public LessonId LessonId { get; private set; } //Айдишник урока

    public Title Title { get; private set; } = default!; //Название задачи

    public Description Description { get; private set; } = default!; //Описание задачи

    public Issue? ParentIssue { get; private set; }

    public IReadOnlyList<Issue> SubIssues => _subIssues; //Подзадачи

    public IssueDetails? Details { get; private set; } //У задач есть много файлов
}
