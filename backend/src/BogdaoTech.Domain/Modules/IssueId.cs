using BogdaoTech.Domain.Modules;

public record IssueId
{
    private IssueId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    //Метод создает новый айдишник у задачи
    public static IssueId NewIssueId() => new(Guid.NewGuid());

    //Метод создает пустой айдишник, для другого сценария
    public static IssueId Empty() => new(Guid.Empty);

    //метод, который позволит перенести вот эту сущность в БД
    //в качестве Id
    public static IssueId Create(Guid id) => new(id);
}
