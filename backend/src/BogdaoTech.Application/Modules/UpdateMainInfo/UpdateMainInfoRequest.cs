using BogdaoTech.Domain.Modules;

namespace BogdaoTech.Application.Modules.UpdateMainInfo
{
    public record UpdateMainInfoRequest(Guid ModuleId, UpdateMainInfoDto dto);
    public record UpdateMainInfoDto(string Title, string Description);
}
