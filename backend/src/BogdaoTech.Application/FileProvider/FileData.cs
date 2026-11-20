using System;
using System.Collections.Generic;
using System.Text;

namespace BogdaoTech.Application.FileProvider
{
    public record FileData(IEnumerable<FileContent> Files, string BucketName);

    public record FileContent(Stream Stream, string ObjectName);
}
