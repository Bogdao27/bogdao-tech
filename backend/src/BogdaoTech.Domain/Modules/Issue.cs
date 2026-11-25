using BogdaoTech.Domain.Modules;
using BogdaoTech.Domain.Shared;
using BogdaoTech.Domain.Shared.ValueObjects;
using CSharpFunctionalExtensions;

public class Issue : BogdaoTech.Domain.Shared.Entity<IssueId>, ISoftDeletable
{
    private bool _isDeleted = false;

    private readonly List<Issue> _subIssues = [];

    private Issue(IssueId id) : base(id)
    {
    }
    
    public Issue (
        IssueId id,
        LessonId lessonId,
        Title title,
        Description description,
        Issue? parentIssue,
        ValueObjectList<IssueFile> files) : base(id)
    {
        LessonId = lessonId;
        Title = title;
        Description = description;
        ParentIssue = parentIssue;
        Files = files;
    }

    public void AddSubIssue(Issue issue)
    {
        //logic and validation
        _subIssues.Add(issue);
    }

    public void UpdateFilesList(ValueObjectList<IssueFile> files) => Files = files;

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

    public LessonId LessonId { get; private set; }

    public Title Title { get; private set; } = default!;

    public Description Description { get; private set; } = default!;

    public Issue? ParentIssue { get; private set; }

    public IReadOnlyList<Issue> SubIssues => _subIssues;

    public ValueObjectList<IssueFile> Files {get; private set; }
}
