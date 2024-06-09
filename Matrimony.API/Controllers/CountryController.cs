using G20.API.Factories.Countries;
using G20.API.Infrastructure.Mapper.Extensions;
using G20.API.Models.Countries;
using G20.Core;
using G20.Core.Domain;
using G20.Service;
using G20.Service.Countries;
using Microsoft.AspNetCore.Mvc;
using Nop.Web.Framework.Models.Extensions;

namespace G20.API.Controllers
{
    public class CountryController : BaseController
    {
        protected readonly IWorkContext _workContext;
        protected readonly ICountryFactoryModel _countryFactoryModel;
        protected readonly ICountryService _countryService;

        public CountryController(IWorkContext workContext,
            ICountryFactoryModel countryFactoryModel,
ICountryService countryService)
        {
            _workContext = workContext;
            _countryFactoryModel = countryFactoryModel;
            _countryService = countryService;

        }

        [HttpPost]
        public virtual async Task<IActionResult> List(CountrySearchModel searchModel)
        {
            //prepare model
            var model = await _countryFactoryModel.PrepareCountryListModelAsync(searchModel);
            return Success(model);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Get(int id)
        {
            var country = await _countryService.GetByIdAsync(id);
            if (country == null)
                return Error("not found");
            return Success(country.ToModel<CountryModel>());
        }

        [HttpPost]
        public virtual async Task<IActionResult> Create(CountryModel model)
        {
            var country = model.ToEntity<Country>();
            await _countryService.InsertAsync(country);
            return Success(country.ToModel<CountryModel>());
        }

        [HttpPost]
        public virtual async Task<IActionResult> Update(CountryModel model)
        {
            var country = await _countryService.GetByIdAsync(model.Id);
            if (country == null)
                return Error("not found");

            country = model.ToEntity(country);
            await _countryService.UpdateAsync(country);
            return Success(country.ToModel<CountryModel>());
        }

        [HttpPost]
        public virtual async Task<IActionResult> Delete(int id)
        {
            var country = await _countryService.GetByIdAsync(id);
            if (country == null)
                return Error("not found");
            await _countryService.DeleteAsync(country);
            return Success(id);
        }
    }
}
