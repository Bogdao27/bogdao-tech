using System.Net;
using BogdaoTech.Domain.Shared;

namespace BogdaoTech.Domain.Modules
{
    
    public sealed class Module : Shared.Entity<ModuleId>
    {
        private readonly List<Issue> _issues = [];
        
        //Ef Core
        private Module(ModuleId id) : base(id)
        {
        }

        private Module(ModuleId moduleId, string title, string description)
            : base(moduleId)
        {
            Title = title;
            Description = description;
        }
                
        public string Title { get; private set; } = default!; //Название сущности

        public string Description { get; private set; } = default!; //Описание сущности

        public IReadOnlyList<Issue> Issues => _issues; //Коллекция сущностей Issue в модуле

        public int GetNumberOfIssues() => _issues.Count(); //Возвращает количество задач

        //Мы возвращаем модуль если все ок, а если не ок, то возвращаем исключение
        public static Result<Module> Create(ModuleId moduleId, string title, string description)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return "Title can not be empty";
            }

            if (string.IsNullOrWhiteSpace(description))
            {
                return "Description can not be empty";
            }

            var module = new Module(moduleId, title, description);

            return module;
        }
    }
}
