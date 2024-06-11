using G20.Core.Enums;

namespace G20.API.Models.Media
{
    public class FileUploadRequestModel
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FileSize { get; set; }
        public string FileAsBase64 { get; set; }
        public string Url { get; set; }
        public FileTypeEnum FileType { get; set; }
    }
}
