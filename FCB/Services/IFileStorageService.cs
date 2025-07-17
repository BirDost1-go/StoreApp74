using Microsoft.AspNetCore.Http;

namespace FCB.Services
{

    public interface IFileStorageService
    {
        /// <summary>
        /// </summary>
        /// <param name="file">The file to be uploaded (IFormFile).</param>"
        /// <returns></returns>
        Task<string> UploadFileAsync(IFormFile file);

        /// <summary>
        /// </summary>
        /// <param name="filePath">The field to be removed</param>
        void RemoveFile(string? filePath);
    }
}
