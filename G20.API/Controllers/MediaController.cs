using G20.API.Factories.Media;
using G20.API.Models.Countries;
using G20.API.Models.Media;
using Microsoft.AspNetCore.Mvc;
using Nop.Core.Infrastructure;
using System.Text;

namespace G20.API.Controllers
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
