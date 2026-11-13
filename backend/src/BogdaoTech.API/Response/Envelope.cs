namespace BogdaoTech.API.Response
{
    //инвалид филд нужен, чтобы увидеть, какое свойство оказалось неверным. Поэтому и было создано данное 
    //поле -  список ошибок, потому что fluentValidation возвращает именно список
    public record ResponseError(string? ErrorCode, string? ErrorMessage, string? InvalidField);

    //обертка для того, чтобы вернуть результат АПИ
    public record Envelope
    {
        private Envelope(object? result, IEnumerable<ResponseError> errors)
        {
            Result = result;
            Errors = errors.ToList();
            TimeGenerated = DateTime.Now;
        }

        //тут может лежать все, что угодно, поэтому и оbject
        public object? Result { get; }

        //А вот и сам список
        public List<ResponseError> Errors { get; }

        //дата генерации ошибки
        public DateTime TimeGenerated { get; }

        //Если все ок, то вернется некий результат и пустой список
        public static Envelope Ok(object? result = null) =>
            new Envelope(result, []);

        //Если не ок, то вернется список ошибок
        public static Envelope Error(IEnumerable<ResponseError> errors) =>
            new Envelope(null, errors);
    }
}
