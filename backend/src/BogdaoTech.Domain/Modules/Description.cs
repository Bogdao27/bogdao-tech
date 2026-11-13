using BogdaoTech.Domain.Shared;
using CSharpFunctionalExtensions;

public record Description
{
    public const int MAX_LENGTH = 2000;

    private Description(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<Description, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length > MAX_LENGTH)
        {
            return Errors.General.ValueIsInvalid("description");
        }

        return new Description(value);
    }
}