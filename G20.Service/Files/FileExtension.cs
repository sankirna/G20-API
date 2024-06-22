using G20.Core.Enums;
using G20.Core;

namespace G20.Service.Files
{
    public static class FileExtension
    {
        public static string ToGetImageUrl(this Core.Domain.File file)
        {
            var fileType = (FileTypeEnum)file.TypeId;
            return string.Format("{0}{1}", fileType.ToGetUrlFolderPath(), file.Name);
        }
    }
}
