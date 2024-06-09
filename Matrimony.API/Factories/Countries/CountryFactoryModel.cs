using Matrimony.API.Infrastructure.Mapper.Extensions;
using Matrimony.API.Models.Countries;
using Matrimony.Service.Countries;
using Nop.Web.Framework.Models.Extensions;

namespace Matrimony.API.Factories.Countries
{
    public class CountryFactoryModel: ICountryFactoryModel
    {
        protected readonly ICountryService _countryService;
        public CountryFactoryModel(ICountryService countryService)
        {
            _countryService = countryService;
        }

        /// <summary>
        /// Prepare paged customer list model
        /// </summary>
        /// <param name="searchModel">Customer search model</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the customer list model
        /// </returns>
        public virtual async Task<CountryListModel> PrepareCountryListModelAsync(CountrySearchModel searchModel)
        {
            ArgumentNullException.ThrowIfNull(searchModel);

            //get customers
            var countries = await _countryService.GetCountriesAsync(name: searchModel.Name,
                pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            //prepare list model
            var model = await new CountryListModel().PrepareToGridAsync(searchModel, countries, () =>
            {
                return countries.SelectAwait(async country =>
                {
                    //fill in model values from the entity
                    var countryModel = country.ToModel<CountryModel>();
                    return countryModel;
                });
            });

            return model;
        }

    }
}
