using BogdaoTech.Domain.Shared;
using CSharpFunctionalExtensions;

namespace BogdaoTech.Domain.Modules
{
    public sealed class Module : Shared.Entity<ModuleId>, ISoftDeletable
    {
        private bool _isDeleted = false;
        
        private readonly List<Issue> _issues = [];
        
        //Ef Core
        private Module(ModuleId id) : base(id)
        {
        }

        public Module(ModuleId moduleId, Title title, Description description)
            : base(moduleId)
        {
            Title = title;
            Description = description;
        }
                
        public Title Title { get; private set; } = default!; //Название сущности

        public Description Description { get; private set; } = default!; //Описание сущности

        public IReadOnlyList<Issue> Issues => _issues; //Коллекция сущностей Issue в модуле

        public int GetNumberOfIssues() => _issues.Count(); //Возвращает количество задач

        public void UpdateMainInfo(Title title, Description description)
        {
            Title = title;
            Description = description;
        }

        public void Delete()
        {
            if(_isDeleted == false)
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

        public UnitResult<Error> AddIssue(Issue issue)
        {
            //валидация
            _issues.Add(issue);

            return Result.Success<Error>();
        }
    }
}
