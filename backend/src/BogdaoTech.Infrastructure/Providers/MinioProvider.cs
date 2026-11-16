using System;
using BogdaoTech.Infrastructure.Models;
using Minio;
using Minio.DataModel.Args;

namespace BogdaoTech.Infrastructure.Providers;

public class MinioProvider
{
    private readonly MinioClient _minioClient;

    public MinioProvider(MinioClient minioClient)
    {
        _minioClient = minioClient;
    }

    public async Task<string> UploadFile(
        FileData fileData, 
        CancellationToken cancellationToken = default)
    {
        var bucketExistArgs = new BucketExistsArgs().WithBucket("photos");

        var bucketExist = await _minioClient.BucketExistsAsync(bucketExistArgs, cancellationToken);
        if (bucketExist == false)
        {
            var makeBucketArgs = new MakeBucketArgs().WithBucket("photos");

            await _minioClient.MakeBucketAsync(makeBucketArgs, cancellationToken);
        }

        var path = Guid.NewGuid();

        var putObjectArgs = new PutObjectArgs()
            .WithBucket("photos")
            .WithStreamData(fileData.Stream)
            .WithObjectSize(fileData.Stream.Length)
            .WithObject(path.ToString());
        
        var result = await _minioClient.PutObjectAsync(putObjectArgs, cancellationToken);

        return result.ObjectName;
    }
}
