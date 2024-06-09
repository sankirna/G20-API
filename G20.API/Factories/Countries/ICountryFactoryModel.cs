using G20.API.Models.Countries;

namespace G20.API.Factories.Countries
{
    public interface ICountryFactoryModel
    {
        Task<CountryListModel> PrepareCountryListModelAsync(CountrySearchModel searchModel);
    }
}
