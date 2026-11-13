using BogdaoTech.API.Extensions;
using BogdaoTech.API.Response;
using BogdaoTech.Application.Modules.CreateModules;
using BogdaoTech.Application.Modules.Delete;
using BogdaoTech.Application.Modules.UpdateMainInfo;
using BogdaoTech.Domain.Shared;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BogdaoTech.API.Controllers
{
    //для взаимодействия с модулями
    public class ModulesController : ApplicationController
    {
        //создание модуля
        [HttpPost]
        public async Task<ActionResult> Create(
            [FromServices] CreateModuleHandler handler,
            [FromBody] CreateModuleRequest request,
            CancellationToken cancellationToken = default)
        {
            var result = await handler.HandlerAsync(request, cancellationToken);
            if (result.IsFailure)
            {
                return result.Error.ToResponse();
            }

            return CreatedAtAction("", result.Value);
        }

        //частичное обновление ресурса. В данном случае я пишу то, что
        //я хоче конкретно обновить
        [HttpPut("{id:guid}/main-info")]
        public async Task<ActionResult> UpdateMainInfo(
            [FromRoute] Guid id,
            [FromBody] UpdateMainInfoDto dto,
            [FromServices] UpdateMainInfoHandler handler,
            [FromServices] IValidator<UpdateMainInfoRequest> validator,
            CancellationToken cancellationToken = default)
        {
            var request = new UpdateMainInfoRequest(id, dto);

            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (validationResult.IsValid == false)
            {
                return validationResult.ToValidationErrorResponse();
            }

            var result = await handler.HandlerAsync(request, cancellationToken);
            if (result.IsFailure)
            {
                return result.Error.ToResponse();
            }

            return Ok(result.Value);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(
            [FromRoute] Guid id,
            [FromServices] DeleteModuleHandler handler,
            [FromServices] IValidator<DeleteModuleRequest> validator,
            CancellationToken cancellationToken = default)
        {
            var request = new DeleteModuleRequest(id);

            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (validationResult.IsValid == false)
            {
                return validationResult.ToValidationErrorResponse();
            }

            var result = await handler.HandlerAsync(request, cancellationToken);
            if (result.IsFailure)
            {
                return result.Error.ToResponse();
            }

            return Ok(result.Value);
        }
    }
}
