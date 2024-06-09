using Matrimony.API.Models.Countries;

namespace Matrimony.API.Factories.Countries
{
    public interface ICountryFactoryModel
    {
        Task<CountryListModel> PrepareCountryListModelAsync(CountrySearchModel searchModel);
    }
}
