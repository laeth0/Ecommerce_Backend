namespace Ecommerce.PL
{
    public static class FileManagement
    {
        public static string? UploadFile(IFormFile file, string FolderName)// => IFormFile is the type of the file that we want to upload
        {

            if (file == null)
            {
                return null;
            }

            // 1 ) Folder Path
            string FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", FolderName);


            // 2 ) File Name
            string FileName = $"{Guid.NewGuid()}{file.FileName}";


            // 3 ) File Path
            string FilePath = Path.Combine(FolderPath, FileName);


            // 4 ) save file as stream   => stream is a sequence of bytes mean upload data per time
            using var FileStream = new FileStream(FilePath, FileMode.Create);


            file.CopyTo(FileStream);
            return FolderName;
        }   
    }
}
