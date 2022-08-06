namespace StudentAdminPortal.API.Repositories
{
    public class LocalStorageProfileImageRepository : IImageRepository
    {
        public async Task<string> UploadProfileImage(IFormFile file, string fileName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"Resources\ProfileImages", fileName);
            using Stream fileStream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(fileStream); // save file

            Console.WriteLine(GetServerRelativePath(fileName));
            return GetServerRelativePath(fileName);
        }

        private string GetServerRelativePath(string fileName)
        {
            return Path.Combine(@"Resources\ProfileImages", fileName);
        }
    }
}
