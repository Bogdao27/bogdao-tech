namespace BogdaoTech.Domain.Modules
{
    public record ModuleId
    {
        private ModuleId(Guid value)
        {
            Value = value;
        }

        public Guid Value { get; }

        //Метод создает новый айдишник у модуля
        public static ModuleId NewModuleId() => new(Guid.NewGuid());

        //Метод создает пустой айдишник, для другого сценария
        public static ModuleId Empty() => new(Guid.Empty);

        //метод, который позволит перенести вот эту сущность в БД
        //в качестве Id
        public static ModuleId Create(Guid id) => new(id);

        public static implicit operator ModuleId(Guid id) => new(id);
        
        //для БД конекста
        public static implicit operator Guid(ModuleId moduleId)
        {
            if (moduleId is null)
            {
                throw new ArgumentNullException();
            }

            return moduleId.Value;
        }
    }
}
