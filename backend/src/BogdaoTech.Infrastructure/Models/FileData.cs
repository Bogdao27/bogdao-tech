using System;

namespace BogdaoTech.Infrastructure.Models;

public record FileData(Stream Stream, string BucketName, string ObjectName);