using Matrimony.Core.Enums;

namespace Matrimony.API.Models.Media
{
    public class FileUploadRequestModel
    {
        //public byte[] FileData { get; set; }
        public string FileName { get; set; }
        public string FileSize { get; set; }
        public string FileAsBase64 { get; set; }
        public FileTypeEnum FileType { get; set; }
    }
}
