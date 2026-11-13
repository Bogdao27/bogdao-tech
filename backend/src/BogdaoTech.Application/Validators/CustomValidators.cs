using BogdaoTech.Domain.Shared;
using CSharpFunctionalExtensions;
using FluentValidation;

namespace BogdaoTech.Application.Validators
{
    public static class CustomValidators
    {
        //метод расширения для моего валидатора, Которое говорит о том, что некий элемент должен быть
        //Value Object. По сути я расширяю некий билдер, который строит валидацию и чтобы произвести
        //расширение я принимаю фабричный метод с помощью которого я буду делать валидацию
        public static IRuleBuilderOptionsConditions<T, TElement> MustBeValueObject<
            T, 
            TElement, 
            TValueObject>(
            this IRuleBuilder<T, TElement> ruleBuilder, 
            Func<TElement, Result<TValueObject, Error>> factoryMethod)
        {
            return ruleBuilder.Custom((value, context) =>
            {
                Result<TValueObject, Error> result = factoryMethod(value);

                if (result.IsSuccess)
                    return;

                context.AddFailure(result.Error.Serialize());
            });
        }

        //для объектов, которые не являются Value object
        public static IRuleBuilderOptions<T, TProperty> WithError<T, TProperty>(
            this IRuleBuilderOptions<T, TProperty> rule, Error error)
        {
            return rule.WithMessage(error.Serialize());
        }
    }
}
