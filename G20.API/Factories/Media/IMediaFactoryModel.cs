using G20.API.Models.Media;

namespace G20.API.Factories.Media
{
    public interface IMediaFactoryModel
    {
        Task<FileUploadRequestModel> UploadRequestModelAsync(FileUploadRequestModel model);
    }
}
