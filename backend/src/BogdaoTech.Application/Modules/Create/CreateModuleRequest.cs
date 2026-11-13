using System;
using System.Collections.Generic;
using System.Text;

namespace BogdaoTech.Application.Modules.CreateModules
{
    //запрос для создания модулей
    //не указываем вэлью обджекты, максимально плоско с использованием обычных данных
    //си шарпа. Это DTO, никакой логики тут нет
    public record CreateModuleRequest(string Title, string Description);
}
