using System.Net;
using CSharpFunctionalExtensions;

namespace BogdaoTech.Domain.Modules
{
    public class Module
    {
        private readonly List<Issue> _issues = [];
        
        //Ef Core
        private Module()
        {

        }
        
        private Module(string title, string description)
        {
            Title = title;
            Description = description;
        }
        
        public Guid Id { get; private set; } //Айдишник модуля в базе данных

        public string Title { get; private set; } = default!; //Название сущности

        public string Description { get; private set; } = default!; //Описание сущности

        public IReadOnlyList<Issue> Issues => _issues; //Коллекция сущностей Issue в модуле

        public int NumberOfIssues => Issues.Count; //Возвращает количество задач

        //Мы возвращаем модуль если все ок, а если не ок, то возвращаем исключение
        public static Result<Module> Create(string title, string description)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return Result.Failure<Module>("Title can not be empty");
            }

            if (string.IsNullOrWhiteSpace(description))
            {
                return Result.Failure<Module>("Description can not be empty");
            }

            var module = new Module(title, description);

            return Result.Success(module);
        }
    }
}
