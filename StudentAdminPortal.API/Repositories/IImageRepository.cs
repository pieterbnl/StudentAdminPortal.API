namespace StudentAdminPortal.API.Repositories
{
    public interface IImageRepository
    {
        Task<String> UploadProfileImage(IFormFile file, string fileName);
    }
}