using Matrimony.API.Models.Cities;

namespace Matrimony.API.Factories.Cities
{
    public interface ICityFactoryModel
    {
        Task<CityListModel> PrepareCityListModelAsync(CitySearchModel searchModel);
    }
}
