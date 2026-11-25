using System;
using System.Collections.Generic;
using System.Text;

namespace BogdaoTech.Application.FileProvider
{
    public record FileData(Stream Stream, FilePath FilePath, string BucketName);
}
