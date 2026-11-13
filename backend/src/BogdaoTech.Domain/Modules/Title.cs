using BogdaoTech.Domain.Shared;
using CSharpFunctionalExtensions;

public record Title
{
    public const int MAX_LENGTH = 100;

    private Title(string value)
    {
        Value = value;
    }
    
    public string Value { get; }

    public static Result<Title, Error> Create(string value)
    {
        if(string.IsNullOrWhiteSpace(value) || value.Length > MAX_LENGTH)
        {
            return Errors.General.ValueIsInvalid("Title");
        }

        return new Title(value);
    }
}
