namespace FCB.Services
{
    public class FileStorageService : IFileStorageService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileStorageService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public void RemoveFile(string? filePath)
        {
            if (string.IsNullOrEmpty(filePath) || filePath == "/images/default-product.jpg")
            {
                return;
            }

            var wwwRootPath = _webHostEnvironment.WebRootPath;

            var fullPath = Path.Combine(wwwRootPath, filePath.TrimStart('/'));
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }

        public async Task<string> UploadFileAsync(IFormFile file)
        {
            var wwwRootPath = _webHostEnvironment.WebRootPath;
            var uploadPath = Path.Combine(wwwRootPath, "Uploads");

            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadPath, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return $"/Uploads/{uniqueFileName}";
        }
    }
}
