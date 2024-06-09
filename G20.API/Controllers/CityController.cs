using G20.API.Factories.Cities;
using G20.API.Infrastructure.Mapper.Extensions;
using G20.API.Models.Cities;
using G20.Core;
using G20.Core.Domain;
using G20.Service.Cities;
using Microsoft.AspNetCore.Mvc;
using Nop.Web.Framework.Models.Extensions;
using System.Threading.Tasks;

namespace G20.API.Controllers
{
    public class CityController : BaseController
    {
        protected readonly IWorkContext _workContext;
        protected readonly ICityFactoryModel _cityFactoryModel;
        protected readonly ICityService _cityService;

        public CityController(IWorkContext workContext, ICityFactoryModel cityFactoryModel, ICityService cityService)
        {
            _workContext = workContext;
            _cityFactoryModel = cityFactoryModel;
            _cityService = cityService;
        }

        [HttpPost]
        public virtual async Task<IActionResult> List(CitySearchModel searchModel)
        {
            var model = await _cityFactoryModel.PrepareCityListModelAsync(searchModel);
            return Success(model);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Get(int id)
        {
            var city = await _cityService.GetByIdAsync(id);
            if (city == null)
                return Error("not found");
            return Success(city.ToModel<CityModel>());
        }

        [HttpPost]
        public virtual async Task<IActionResult> Create(CityModel model)
        {
            var city = model.ToEntity<City>();
            await _cityService.InsertAsync(city);
            return Success(city.ToModel<CityModel>());
        }

        [HttpPost]
        public virtual async Task<IActionResult> Update(CityModel model)
        {
            var city = await _cityService.GetByIdAsync(model.Id);
            if (city == null)
                return Error("not found");

            city = model.ToEntity(city);
            await _cityService.UpdateAsync(city);
            return Success(city.ToModel<CityModel>());
        }

        [HttpPost]
        public virtual async Task<IActionResult> Delete(int id)
        {
            var city = await _cityService.GetByIdAsync(id);
            if (city == null)
                return Error("not found");
            await _cityService.DeleteAsync(city);
            return Success(id);
        }
    }
}
