using G20.API.Models.Cities;

namespace G20.API.Factories.Cities
{
    public interface ICityFactoryModel
    {
        Task<CityListModel> PrepareCityListModelAsync(CitySearchModel searchModel);
    }
}
