using Matrimony.API.Models.Countries;
using Matrimony.API.Models.Profiles;

namespace Matrimony.API.Factories.Profiles
{
    public interface IProfileFactoryModel
    {
        Task<ProfileEditRequestModel> PrepareProfileEditModelAsync(int id);
        Task<ProfileListModel> PrepareProfileListModelAsync(ProfileSearchModel searchModel);
    }
}
