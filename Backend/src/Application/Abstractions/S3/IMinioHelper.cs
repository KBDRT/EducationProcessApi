namespace Application.Abstractions.S3
{
    public interface IMinioHelper
    {
        public Task<MemoryStream> GetFileToStreamAsync(string fileNameInMinio);

        public Task PutFileFromStreamAsync(MemoryStream fileStream, string fileName);

        public Task GetFileAsync(string fileNameInMinio, string newFileName);
        public Task PutFileAsync(string fileNameInMinio, string filePath);
        public void SetBucketName(string bucketName);
    }
}
