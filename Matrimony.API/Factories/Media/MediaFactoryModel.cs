using Matrimony.API.Models.Media;
using Matrimony.API.Models.Profiles;
using Matrimony.Core;
using Matrimony.Service.Files;
using Microsoft.AspNetCore.Hosting;
using Nop.Core.Infrastructure;
using File = Matrimony.Core.Domain.File;

namespace Matrimony.API.Factories.Media
{
    public class MediaFactoryModel : IMediaFactoryModel
    {
        protected readonly INopFileProvider _fileProvider;
        protected readonly IWebHostEnvironment _webHostEnvironment;
        protected readonly IFileService _fileService;


        public MediaFactoryModel(IWebHostEnvironment webHostEnvironment
                               , INopFileProvider fileProvider
                               , IFileService fileService)
        {
            _webHostEnvironment = webHostEnvironment;
            _fileProvider = fileProvider;
            _fileService = fileService;
        }

        public virtual async Task<FileUploadRequestModel> UploadRequestModelAsync(FileUploadRequestModel model)
        {
            if (model.FileAsBase64.Contains(","))
            {
                model.FileAsBase64 = model.FileAsBase64.Substring(model.FileAsBase64.IndexOf(",") + 1);
            }
            string originalFileName = model.FileName;
            string newFileName = originalFileName.ToGetFileName();
            string path = string.Format("{0}{1}", model.FileType.ToGetFolderPath(), newFileName);
            byte[] bytes = Convert.FromBase64String(model.FileAsBase64);
            _fileProvider.CreateFile(path);
            await _fileProvider.WriteAllBytesAsync(path, bytes);

            File file = new File()
            {
                OriginalName = originalFileName,
                Name = newFileName,
                TypeId = (int)model.FileType
            };

            await _fileService.InsertAsync(file);
            return model;
        }
    }
}
