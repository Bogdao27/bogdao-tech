using Microsoft.AspNetCore.Mvc;
using Minio;

namespace BogdaoTech.API.Controllers;

public class FileController : ApplicationController
{
    private readonly IMinioClient _minioClient;
    
    public FileController(IMinioClient minioClient)
    {
        _minioClient = minioClient;
    }

    [HttpPost]
    public async Task<IActionResult> CreateFile()
    {
        
    }
}
