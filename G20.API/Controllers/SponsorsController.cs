using G20.API.Factories.Sponsors;
using G20.API.Infrastructure.Mapper.Extensions;
using G20.API.Models.Sponsors;
using G20.Core;
using G20.Core.Domain;
using G20.Service.Sponsors;
using Microsoft.AspNetCore.Mvc;

namespace G20.API.Controllers
{
    public class SponsorsController : BaseController
    {
        protected readonly IWorkContext _workContext;
        protected readonly ISponsorsFactoryModel _sponsorFactoryModel;
        protected readonly ISponsorService _sponsorService;

        public SponsorsController(IWorkContext workContext, ISponsorsFactoryModel sponsorFactoryModel, ISponsorService sponsorService)
        {
            _workContext = workContext;
            _sponsorFactoryModel = sponsorFactoryModel;
            _sponsorService = sponsorService;
        }

        [HttpPost]
        public virtual async Task<IActionResult> List(SponsorSearchModel searchModel)
        {
            var model = await _sponsorFactoryModel.PrepareSponsorListModelAsync(searchModel);
            return Success(model);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Get(int id)
        {
            var sponsor = await _sponsorService.GetByIdAsync(id);
            if (sponsor == null)
                return Error("not found");
            return Success(sponsor.ToModel<SponsorModel>());
        }

        [HttpPost]
        public virtual async Task<IActionResult> Create(SponsorModel model)
        {
            var sponsor = model.ToEntity<Sponsor>();
            await _sponsorService.InsertAsync(sponsor);
            return Success(sponsor.ToModel<SponsorModel>());
        }

        [HttpPost]
        public virtual async Task<IActionResult> Update(SponsorModel model)
        {
            var sponsor = await _sponsorService.GetByIdAsync(model.Id);
            if (sponsor == null)
                return Error("not found");

            sponsor = model.ToEntity(sponsor);
            await _sponsorService.UpdateAsync(sponsor);
            return Success(sponsor.ToModel<SponsorModel>());
        }

        [HttpPost]
        public virtual async Task<IActionResult> Delete(int id)
        {
            var sponsor = await _sponsorService.GetByIdAsync(id);
            if (sponsor == null)
                return Error("not found");
            await _sponsorService.DeleteAsync(sponsor);
            return Success(id);
        }
    }
}
