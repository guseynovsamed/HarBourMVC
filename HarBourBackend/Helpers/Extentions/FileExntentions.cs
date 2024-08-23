using System;
namespace HarBourBackEnd.Helpers.Extentions
{
	public static class FileExntentions
	{
        public static bool CheckFileType(this IFormFile file, string type)
        {
            return file.ContentType.Contains(type);
        }

        public static bool CheckFileSize(this IFormFile file, int fileSize)
        {
            return file.Length / 1024 / 1024 < fileSize;
        }

        public static async Task SaveFileToLocalAsync(this IFormFile file, string filePath)
        {
            using FileStream stream = new(filePath, FileMode.Create);
            await file.CopyToAsync(stream);
        }

        public static void DeleteFileFromLocal(this string path)
        {
            if (File.Exists(path))
                File.Delete(path);
        }
    }
}

