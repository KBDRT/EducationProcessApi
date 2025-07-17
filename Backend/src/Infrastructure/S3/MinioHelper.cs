
using Application.Abstractions.S3;
using Application.Settings;
using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel.Args;
using System.Security.AccessControl;

namespace Application.Services.Helpers.Implementation
{

    public class MinioHelper : IMinioHelper
    {
        private readonly IMinioClient _minioClient;
        private readonly MinioSettings _settings;

        private string _bucketName = string.Empty;

        public MinioHelper(IMinioClient minioClient,
                           IOptions<MinioSettings> minioSettings)
        {
            _minioClient = minioClient;
            _settings = minioSettings.Value;
            _bucketName = _settings.DefaultBucketName;
        }

        public void SetBucketName(string bucketName) => _bucketName = bucketName;

        public async Task GetFileAsync(string fileNameInMinio,
                                        string newFileName)
        {
            try
            {
                var args = new GetObjectArgs()
                    .WithBucket(_bucketName)
                    .WithObject(fileNameInMinio)
                    .WithFile(newFileName)
                    .WithServerSideEncryption(null);
                await _minioClient.GetObjectAsync(args).ConfigureAwait(false);
            }
            catch
            {
                
            }
        }

        public async Task PutFileAsync(string fileNameInMinio,
                                       string filePath)
        {
            try
            {
                var args = new PutObjectArgs()
                   .WithBucket(_bucketName)
                   .WithObject(fileNameInMinio)
                   .WithContentType("application/octet-stream")
                   .WithFileName(filePath);
                await _minioClient.PutObjectAsync(args).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                throw new Exception("[MinioProblem]");
            }
        }


        public async Task<MemoryStream> GetFileToStreamAsync(string fileNameInMinio)
        {
            var memoryStream = new MemoryStream();
            try
            {
                var getObjectArgs = new GetObjectArgs()
                    .WithBucket(_bucketName)
                    .WithObject(fileNameInMinio)
                    .WithCallbackStream(stream =>
                    {
                        stream.CopyTo(memoryStream);
                        memoryStream.Position = 0;
                    });

                await _minioClient.GetObjectAsync(getObjectArgs);
            }
            catch
            {

            }
            return memoryStream;
        }

        public async Task PutFileFromStreamAsync(MemoryStream fileStream, string fileName)
        {
            try
            {
                var args = new PutObjectArgs()
               .WithBucket(_bucketName)
               .WithObject(fileName)
               .WithContentType("application/vnd.openxmlformats-officedocument.wordprocessingml.document\"")
               .WithStreamData(fileStream)
               .WithObjectSize(fileStream.Length);

                fileStream.Position = 0;
                await _minioClient.PutObjectAsync(args).ConfigureAwait(false);
            }
            catch
            {
                throw new Exception("[MinioProblem]");
            }
        }
    }
}
