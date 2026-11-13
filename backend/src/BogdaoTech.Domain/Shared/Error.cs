using System;
using System.Collections.Generic;
using System.Text;

namespace BogdaoTech.Domain.Shared
{
    //вспомогательный класс для описания типа ошибки
    public record Error
    {
        //инкапсулирование создания ошибки
        private Error(string code, string message, ErrorType type)
        {
            Code = code;
            Message = message;
            Type = type;
        }

        //разделитель
        public const string SEPARATOR = "||";
        public string Code { get; }
        public string Message { get; }
        public ErrorType Type { get; }

        public static Error Validation(string code, string message) =>
            new Error(code, message, ErrorType.Validation);

        public static Error NotFound(string code, string message) =>
            new Error(code, message, ErrorType.NotFound);

        public static Error Failure(string code, string message) =>
            new Error(code, message, ErrorType.Failure);

        public static Error Conflict(string code, string message) =>
            new Error(code, message, ErrorType.Conflict);

        //хочу сделать сериализацию "value.is.invalid||message not found"
        //сериализация - это когда из объекта получаем строку
        public string Serialize()
        {
            return string.Join(SEPARATOR, Code, Message, Type);
        }

        public static Error Deserialize(string serialized)
        {
            var parts = serialized.Split(SEPARATOR);

            if(parts.Length < 3)
            {
                throw new ArgumentException("Invalid serialized format");
            }

            if (Enum.TryParse<ErrorType>(parts[2], out var type) == false)
            {
                throw new ArgumentException("Invalid serialized format");
            }

            return new Error(parts[0], parts[1], type);
        }
    }

    public enum ErrorType
    {
        Validation,
        NotFound,
        Failure,
        Conflict
    }
}
