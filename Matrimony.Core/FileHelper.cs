using Matrimony.Core.Enums;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Formats.Tar;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Core
{
    public static class FileHelper
    {
        public static string SaticFilesPath = "staticfiles";
        public static string GetRootPath(this IWebHostEnvironment webHostEnvironment)
        {
            string webRootPath = File.Exists(webHostEnvironment.WebRootPath)
            ? Path.GetDirectoryName(webHostEnvironment.WebRootPath)
            : webHostEnvironment.WebRootPath;

            return webRootPath;
        }

        public static string ToGetFolderPath(this FileTypeEnum fileType)
        {
            return string.Format("{0}/{1}/", SaticFilesPath,  fileType.ToString());
        }

        public static string ToGetFileName(this string fileName)
        {
            var filePathName = Path.GetFileNameWithoutExtension(fileName) + "-" +
                               DateTime.Now.ToString().Replace("/", "").Replace(":", "").Replace(" ", "") +
                               Path.GetExtension(fileName);
            return filePathName;
        }
    }
}
