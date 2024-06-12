using G20.API.Models.Media;
using G20.Core;
using G20.Core.Enums;
using G20.Service.Files;
using Microsoft.AspNetCore.Hosting;
using Nop.Core.Infrastructure;
using File = G20.Core.Domain.File;

namespace G20.API.Factories.Media
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

        public virtual async Task<FileUploadRequestModel> GetRequestModelAsync(int? fileId)
        {
            FileUploadRequestModel model = new FileUploadRequestModel();
            if (fileId.HasValue && fileId.Value > 0)
            {
                var entity = await _fileService.GetByIdAsync(fileId.Value);
                var fileType = (FileTypeEnum)entity.TypeId;
                model.FileName = entity.OriginalName;
                model.FileType = fileType;
                model.Url = string.Format("{0}{1}", fileType.ToGetUrlFolderPath(), entity.Name);
            }
            return model;

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
            model.Id = file.Id;
            return model;
        }

        public virtual async Task<int?> AddUpdateFile(FileUploadRequestModel fileUploadRequestModel)
        {
            if (fileUploadRequestModel != null
               && !string.IsNullOrEmpty(fileUploadRequestModel.FileName)
               && !string.IsNullOrEmpty(fileUploadRequestModel.FileAsBase64)
               && fileUploadRequestModel.Id <= 0)
            {
                fileUploadRequestModel = await UploadRequestModelAsync(fileUploadRequestModel);
                return fileUploadRequestModel.Id;
            }
            else if (fileUploadRequestModel != null && fileUploadRequestModel.Id > 0)
            {
                return fileUploadRequestModel.Id;
            }
            else
            {
                return null;
            }
        }
    }
}
