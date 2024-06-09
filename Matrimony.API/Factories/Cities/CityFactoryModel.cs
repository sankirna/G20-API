using Matrimony.API.Infrastructure.Mapper.Extensions;
using Matrimony.API.Models.Cities;
using Matrimony.Service.Cities;
using Nop.Web.Framework.Models.Extensions;

namespace Matrimony.API.Factories.Cities
{
    public class CityFactoryModel : ICityFactoryModel
    {
        protected readonly ICityService _cityService;

        public CityFactoryModel(ICityService cityService)
        {
            _cityService = cityService;
        }

        /// <summary>
        /// Prepare paged city list model
        /// </summary>
        /// <param name="searchModel">City search model</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the city list model
        /// </returns>
        public virtual async Task<CityListModel> PrepareCityListModelAsync(CitySearchModel searchModel)
        {
            ArgumentNullException.ThrowIfNull(searchModel);

            //get cities
            var cities = await _cityService.GetCitiesAsync(name: searchModel.Name,
                stateId: searchModel.StateId,
                pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            //prepare list model
            var model = await new CityListModel().PrepareToGridAsync(searchModel, cities, () =>
            {
                return cities.SelectAwait(async city =>
                {
                    //fill in model values from the entity
                    var cityModel = city.ToModel<CityModel>();
                    return cityModel;
                });
            });

            return model;
        }
    }
}
