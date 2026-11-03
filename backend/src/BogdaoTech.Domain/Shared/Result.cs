using System;
using System.Collections.Generic;
using System.Text;

namespace BogdaoTech.Domain.Shared
{
    //Делаем свой класс Result без value
    public class Result
    {
        public Result(bool isSuccess, string? error)
        {
            //если все верно, но при этом есть ошибка, то такого не может быть
            if(isSuccess && error is not null)
            {
                throw new InvalidOperationException();
            }

            //если чет неверно, но и ошибки нет, то тоже бред
            if(isSuccess == false && error == null)
            {
                throw new InvalidOperationException();
            }

            //если все ок, то делаем вот так
            IsSuccess = isSuccess;
            Error = error;
        }
        
        //Свойство ошибка
        public string? Error { get; set; }
        //булевое свойство хорошо
        public bool IsSuccess { get; }
        //булевое свойство плохо, возвращает негативную версию хорошего свойства
        public bool IsFailure => !IsSuccess;

        //статический метод при успешности выполнения операции
        public static Result Success() => new(true, null);

        //статический метод при неудачном выполнении операции
        public static Result Failure(string error) => new Result(false, error);

        public static implicit operator Result(string error) => new(false, error);
    }

    //Делаем свой класс Result c value
    public class Result<TValue> : Result
    {
        //дополняем новый конструктор value
        public Result(TValue value, bool isSuccess, string? error) : base(isSuccess, error)
        {
            _value = value;
        }
        
        //какое-то значение
        private readonly TValue _value;

        //Значение при негативном результате недоступно или доступно если все ок
        public TValue Value => IsSuccess
            ?
            _value
            : throw new InvalidOperationException("The value of a failure result can not be accessed");

        public static Result<TValue> Success(TValue value) => new(value, true, null);

        public new static Result<TValue> Failure(string error) => new(default!, false, error);

        //когда мы захотим привести что-то к Value это автоматически приведется к result
        public static implicit operator Result<TValue>(TValue value) => new(value, true, null);
        //а это когда мы возвращаем ошибку
        public static implicit operator Result<TValue>(string error) => new(default!, false, error);
    }
}
