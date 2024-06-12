using G20.API.Models.Media;

namespace G20.API.Factories.Media
{
    public interface IMediaFactoryModel
    {
        Task<FileUploadRequestModel> GetRequestModelAsync(int? fileId);
        Task<FileUploadRequestModel> UploadRequestModelAsync(FileUploadRequestModel model);

        Task<int?> AddUpdateFile(FileUploadRequestModel fileUploadRequestModel);
    }
}
