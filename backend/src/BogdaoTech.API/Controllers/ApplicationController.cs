using BogdaoTech.API.Response;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace BogdaoTech.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //для взаимодействия с модулями
    public abstract class ApplicationController : ControllerBase
    {
        public override OkObjectResult Ok(object? value)
        {
            var envelope = Envelope.Ok(value);

            return new(envelope);
        }
    }
}
