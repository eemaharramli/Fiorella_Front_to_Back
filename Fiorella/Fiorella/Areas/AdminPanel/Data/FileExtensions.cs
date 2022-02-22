using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Fiorella.Areas.AdminPanel.Data
{
    public static class FileExtensions
    {
        public static bool IsImage(this IFormFile file)
        {
            return file.ContentType.Contains("image");
        }

        public static bool IsAllowedSize(this IFormFile file, int mb)
        {
            return file.Length < mb * 1024 * 1000;
        }

        public static async Task<string> GenerateFile(this IFormFile file, string folderPath)
        {
            var fileName = $"{Guid.NewGuid()}_{file.FileName}";
            var path = Path.Combine(folderPath, fileName);

            await using var fileStream = new FileStream(path, FileMode.CreateNew);
            await file.CopyToAsync(fileStream);

            return fileName;
        }
    }
}