using System;

namespace BogdaoTech.API.Contracts;

public record AddIssueRequest(
    string Title,
    string Description,
    IFormFileCollection Files
);