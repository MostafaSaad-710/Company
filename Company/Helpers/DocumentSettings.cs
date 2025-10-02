namespace Company.Helpers
{
    public static class DocumentSettings
    {
        // 1. Upload
        //string == ImageName
        public static string UplaodFile(IFormFile file , string folderName)
        {
            // File Path
            // Form:
            // 1. Get Folder Location
            // string folderPath = "C:\\Users\\bahgat\\Desktop\\MVC\\Company\\Company\\wwwroot\\files\\" + folderName;
            //                    -          Directory.GetCurrentDirectory()          -

            // var folderPath = Directory.GetCurrentDirectory() + "\\wwwroot\\files\\" + folderName;

            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\files", folderName);

            // 2. Get File Name And make it unique

            var fileName = $"{Guid.NewGuid()}{file.Name}";

            // File Path

            var filepath = Path.Combine(folderPath, fileName);

            using var fileStream = new FileStream(filepath, FileMode.Create);

            file.CopyTo(fileStream);

            return fileName;
        }

        // 2. Delete

        public static void DeleteFile(string fileName ,  string folderName)
        {
            var filePAth = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\files", folderName, fileName);

            if(File.Exists(filePAth) ) 
                File.Delete(filePAth); 

        }

    }
}
