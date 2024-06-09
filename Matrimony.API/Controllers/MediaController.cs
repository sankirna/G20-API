using Matrimony.API.Factories.Media;
using Matrimony.API.Models.Countries;
using Matrimony.API.Models.Media;
using Matrimony.Service.Profiles;
using Microsoft.AspNetCore.Mvc;
using Nop.Core.Infrastructure;
using System.Text;

namespace Matrimony.API.Controllers
{
    public class MediaController : BaseController
    {
        protected readonly INopFileProvider _fileService;
        protected readonly IMediaFactoryModel _mediaFactoryModel;

        public MediaController(INopFileProvider fileService
                             , IMediaFactoryModel mediaFactoryModel)
        {
            _fileService= fileService;
            _mediaFactoryModel=mediaFactoryModel;
        }

        [HttpPost]
        public virtual async Task<IActionResult> Upload(FileUploadRequestModel model)
        {
            _mediaFactoryModel.UploadRequestModelAsync(model);
            //prepare model
            return Success(model);
        }
    }
}
