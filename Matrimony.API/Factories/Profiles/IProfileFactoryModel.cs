using G20.API.Models.Countries;
using G20.API.Models.Profiles;

namespace G20.API.Factories.Profiles
{
    public interface IProfileFactoryModel
    {
        Task<ProfileEditRequestModel> PrepareProfileEditModelAsync(int id);
        Task<ProfileListModel> PrepareProfileListModelAsync(ProfileSearchModel searchModel);
    }
}
