using Matrimony.API.Infrastructure.Mapper.Extensions;
using Matrimony.API.Models.Achivements;
using Matrimony.API.Models.Addresss;
using Matrimony.API.Models.Educations;
using Matrimony.API.Models.Families;
using Matrimony.API.Models.Occupations;
using Matrimony.API.Models.Profiles;
using Matrimony.Service.Achivements;
using Matrimony.Service.Addresss;
using Matrimony.Service.Educations;
using Matrimony.Service.Families;
using Matrimony.Service.Occupations;
using Matrimony.Service.Profiles;
using Nop.Core;
using Nop.Web.Framework.Models.Extensions;

namespace Matrimony.API.Factories.Profiles
{
    public class ProfileFactoryModel : IProfileFactoryModel
    {
        protected readonly IProfileService _profileService;
        protected readonly IAddressService _addressService;
        protected readonly IFamilyService _familyService;
        protected readonly IEducationService _educationService;
        protected readonly IOccupationService _occupationService;
        protected readonly IAchivementService _achivementService;

        public ProfileFactoryModel(IProfileService profileService
                                , IAddressService addressService
                                , IFamilyService familyService
                                , IEducationService educationService
                                , IOccupationService occupationService
                                , IAchivementService achivementService)
        {
            _profileService = profileService;
            _addressService = addressService;
            _familyService = familyService;
            _educationService = educationService;
            _occupationService = occupationService;
            _achivementService = achivementService;
        }

        public virtual async Task<ProfileEditRequestModel> PrepareProfileEditModelAsync(int id)
        {
            var profile = await _profileService.GetByIdAsync(id);
            if (profile == null)
                throw new NopException("profile not found");

            ProfileEditRequestModel model = new ProfileEditRequestModel();
            model.Id = profile.Id;
            model.Profile = profile.ToModel<ProfileModel>();

            var addresses = await _addressService.GetByProfileIdAsync(id);
            model.Addresses = addresses.Select(x => x.ToModel<AddressModel>()).ToList();

            var families = await _familyService.GetByProfileIdAsync(id);
            model.Families = families.Select(x => x.ToModel<FamilyModel>()).ToList();

            var educations = await _educationService.GetByProfileIdAsync(id);
            model.Educations = educations.Select(x => x.ToModel<EducationModel>()).ToList();

            var occupations = await _occupationService.GetByProfileIdAsync(id);
            model.Occupations = occupations.Select(x => x.ToModel<OccupationModel>()).ToList();

            var achivements = await _achivementService.GetByProfileIdAsync(id);
            model.Achivements = achivements.Select(x => x.ToModel<AchivementModel>()).ToList();

            return model;
        }

        /// <summary>
        /// Prepare paged profile list model
        /// </summary>
        /// <param name="searchModel">Profile search model</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the profile list model
        /// </returns>
        public virtual async Task<ProfileListModel> PrepareProfileListModelAsync(ProfileSearchModel searchModel)
        {
            ArgumentNullException.ThrowIfNull(searchModel);

            //get profiles
            var profiles = await _profileService.GetProfilesAsync(name: searchModel.Name,
                pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            //prepare list model
            var model = await new ProfileListModel().PrepareToGridAsync(searchModel, profiles, () =>
            {
                return profiles.SelectAwait(async profile =>
                {
                    //fill in model values from the entity
                    var profileModel = profile.ToModel<ProfileModel>();
                    return profileModel;
                });
            });

            return model;
        }
    }
}
