namespace shop.Services.Upload
{
    public interface IUploadServise
    {
        String SaveFormFile(IFormFile formFile);
        String SaveFormFile(IFormFile formFile, String path);
        String SaveFormFile(IFormFile formFile, String path, IEnumerable<String> extensionsAllowed);
    }
}
