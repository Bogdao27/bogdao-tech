using System;

namespace BogdaoTech.Application.Modules.AddIssue;

public record AddIssueCommand(
    Guid ModuleId, 
    string Title,
    string Description,
    IEnumerable<CreateFileDto> Files
    );

public record CreateFileDto(Stream Content, string FIleName);