using G20.API.Models.Teams;
using G20.API.Factories.Teams;
using G20.Core;
using G20.Core.Domain;
using G20.Service.Teams;
using Microsoft.AspNetCore.Mvc;
using Nop.Web.Framework.Models.Extensions;
using System.Threading.Tasks;
using G20.API.Infrastructure.Mapper.Extensions;

namespace G20.API.Controllers
{
    public class TeamController : BaseController
    {
        protected readonly IWorkContext _workContext;
        protected readonly ITeamFactoryModel _teamFactoryModel;
        protected readonly ITeamService _teamService;

        public TeamController(IWorkContext workContext,
            ITeamFactoryModel teamFactoryModel,
            ITeamService teamService)
        {
            _workContext = workContext;
            _teamFactoryModel = teamFactoryModel;
            _teamService = teamService;
        }

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
            var team = model.ToEntity<Team>();
            await _teamService.InsertAsync(team);
            return Success(team.ToModel<TeamModel>());
        }

        [HttpPost]
        public virtual async Task<IActionResult> Update(TeamModel model)
        {
            var team = await _teamService.GetByIdAsync(model.Id);
            if (team == null)
                return Error("not found");

            team = model.ToEntity(team);
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
