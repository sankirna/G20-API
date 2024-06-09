using Matrimony.API.Models.Media;

namespace Matrimony.API.Factories.Media
{
    public interface IMediaFactoryModel
    {
        Task<FileUploadRequestModel> UploadRequestModelAsync(FileUploadRequestModel model);
    }
}
