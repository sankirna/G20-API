using G20.API.Models.Teams;
using G20.API.Factories.Teams;
using G20.Core;
using G20.Core.Domain;
using G20.Service.Teams;
using Microsoft.AspNetCore.Mvc;
using Nop.Web.Framework.Models.Extensions;
using System.Threading.Tasks;
using G20.API.Infrastructure.Mapper.Extensions;
using G20.API.Models.Media;
using G20.API.Factories.Media;

namespace G20.API.Controllers
{
    public class TeamController : BaseController
    {
        protected readonly IWorkContext _workContext;
        protected readonly ITeamFactoryModel _teamFactoryModel;
        protected readonly IMediaFactoryModel _mediaFactoryModel;
        protected readonly ITeamService _teamService;

        public TeamController(IWorkContext workContext,
            ITeamFactoryModel teamFactoryModel,
            IMediaFactoryModel mediaFactoryModel,
            ITeamService teamService)
        {
            _workContext = workContext;
            _teamFactoryModel = teamFactoryModel;
            _mediaFactoryModel = mediaFactoryModel;
            _teamService = teamService;
        }

        #region Private Methods

        private  async Task<int?> AddUpdateLogo(FileUploadRequestModel fileUploadRequestModel)
        {
            if (fileUploadRequestModel != null
               && !string.IsNullOrEmpty(fileUploadRequestModel.FileName)
               && !string.IsNullOrEmpty(fileUploadRequestModel.FileAsBase64))
            {
                fileUploadRequestModel = await _mediaFactoryModel.UploadRequestModelAsync(fileUploadRequestModel);
                return fileUploadRequestModel.Id;
            }
            else
            {
                return null;
            }
        }

        #endregion

        [HttpPost]
        public virtual async Task<IActionResult> List(TeamSearchModel searchModel)
        {
            var model = await _teamFactoryModel.PrepareTeamListModelAsync(searchModel);
            return Success(model);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Get(int id)
        {
            var team = await _teamService.GetByIdAsync(id);
            if (team == null)
                return Error("not found");
            return Success(team.ToModel<TeamModel>());
        }

        [HttpPost]
        public virtual async Task<IActionResult> Create(TeamModel model)
        {
            var fileId = await AddUpdateLogo(model.Logo);

            var team = model.ToEntity<Team>();
            team.LogoId= fileId;
            await _teamService.InsertAsync(team);

            return Success(team.ToModel<TeamModel>());
        }

        [HttpPost]
        public virtual async Task<IActionResult> Update(TeamModel model)
        {
            var team = await _teamService.GetByIdAsync(model.Id);
            if (team == null)
                return Error("not found");
            var fileId = await AddUpdateLogo(model.Logo);
            team = model.ToEntity(team);
            team.LogoId= fileId;
            await _teamService.UpdateAsync(team);
            return Success(team.ToModel<TeamModel>());
        }

        [HttpPost]
        public virtual async Task<IActionResult> Delete(int id)
        {
            var team = await _teamService.GetByIdAsync(id);
            if (team == null)
                return Error("not found");
            await _teamService.DeleteAsync(team);
            return Success(id);
        }
    }
}
