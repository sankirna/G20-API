using AutoMapper;
using Matrimony.API.Auth;
using Matrimony.API.Factories.Countries;
using Matrimony.API.Factories.Profiles;
using Matrimony.API.Infrastructure.Mapper.Extensions;
using Matrimony.API.Models.Achivements;
using Matrimony.API.Models.Addresss;
using Matrimony.API.Models.Countries;
using Matrimony.API.Models.Educations;
using Matrimony.API.Models.Families;
using Matrimony.API.Models.Occupations;
using Matrimony.API.Models.Profiles;
using Matrimony.Core;
using Matrimony.Core.Domain;
using Matrimony.Core.IndentityModels;
using Matrimony.Service.Account;
using Matrimony.Service.Achivements;
using Matrimony.Service.Addresss;
using Matrimony.Service.Countries;
using Matrimony.Service.Educations;
using Matrimony.Service.Families;
using Matrimony.Service.Occupations;
using Matrimony.Service.Profiles;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Profile = Matrimony.Core.Domain.Profile;

namespace Matrimony.API.Controllers
{
    public class ProfileController : BaseController
    {
        protected readonly IWorkContext _workContext;
        private readonly IAuthenticateService _authenticateService;
        protected readonly IProfileFactoryModel _profileFactoryModel;
        protected readonly IProfileService _profileService;
        protected readonly IAddressService _addressService;
        protected readonly IFamilyService _familyService;
        protected readonly IEducationService _educationService;
        protected readonly IOccupationService _occupationService;
        protected readonly IAchivementService _achivementService;

        public ProfileController(IWorkContext workContext
                               , IAuthenticateService authenticateService
                               , IProfileFactoryModel profileFactoryModel
                               , IProfileService profileService
                               , IAddressService addressService
                               , IFamilyService familyService
                               , IEducationService educationService
                               , IOccupationService occupationService
                               , IAchivementService achivementService)
        {
            _workContext = workContext;
            _authenticateService = authenticateService;
            _profileFactoryModel = profileFactoryModel;
            _profileService = profileService;
            _addressService = addressService;
            _familyService = familyService;
            _educationService = educationService;
            _occupationService = occupationService;
            _achivementService = achivementService;
        }

        #region Private Methods

        private async Task UpdateAddresses(int profileId, List<AddressModel> requestAddresses)
        {
            if (requestAddresses != null)
            {
                var addresses = await _addressService.GetByProfileIdAsync(profileId);
                var existingIds = addresses.Select(x => x.Id);
                var requestIds = requestAddresses.Select(x => x.Id);
                var updateIds = requestIds.Intersect(existingIds);
                var deleteIds = existingIds.Except(requestIds);
                var addedIds = requestIds.Except(existingIds);

                var deleteAddresses = addresses.Where(x => deleteIds.Contains(x.Id));
                foreach (var address in deleteAddresses)
                {
                    await _addressService.DeleteAsync(address);
                }

                var updateAddresses = requestAddresses.Where(x => updateIds.Contains(x.Id));
                foreach (var addressRequest in updateAddresses)
                {
                    var address = addresses.FirstOrDefault(x => x.Id == addressRequest.Id);
                    if (address == null)
                        throw new NopException("address not found");
                    addressRequest.ProfileId = profileId;
                    address = addressRequest.ToEntity(address);
                    await _addressService.UpdateAsync(address);
                }

                var addAddresses = requestAddresses.Where(x => addedIds.Contains(x.Id));
                foreach (var addressRequest in addAddresses)
                {
                    addressRequest.ProfileId = profileId;
                    var address = addressRequest.ToEntity<Address>();
                    await _addressService.InsertAsync(address);
                }
            }
        }

        private async Task UpdateFamilies(int profileId, List<FamilyModel> requestFamilies)
        {
            if (requestFamilies != null)
            {
                var families = await _familyService.GetByProfileIdAsync(profileId);
                var existingIds = families.Select(x => x.Id);
                var requestIds = requestFamilies.Select(x => x.Id);
                var updateIds = requestIds.Intersect(existingIds);
                var deleteIds = existingIds.Except(requestIds);
                var addedIds = requestIds.Except(existingIds);

                var deleteFamilies = families.Where(x => deleteIds.Contains(x.Id));
                foreach (var family in deleteFamilies)
                {
                    await _familyService.DeleteAsync(family);
                }

                var updateFamilies = requestFamilies.Where(x => updateIds.Contains(x.Id));
                foreach (var familyRequest in updateFamilies)
                {
                    var family = families.FirstOrDefault(x => x.Id == familyRequest.Id);
                    if (family == null)
                        throw new NopException("address not found");
                    familyRequest.ProfileId = profileId;
                    family = familyRequest.ToEntity(family);
                    await _familyService.UpdateAsync(family);
                }

                var addFamilies = requestFamilies.Where(x => addedIds.Contains(x.Id));
                foreach (var familyRequest in addFamilies)
                {
                    familyRequest.ProfileId = profileId;
                    var family = familyRequest.ToEntity<Family>();
                    await _familyService.InsertAsync(family);
                }
            }
        }

        private async Task UpdateEducations(int profileId, List<EducationModel> requestEducations)
        {
            if (requestEducations != null)
            {
                var Educations = await _educationService.GetByProfileIdAsync(profileId);
                var existingIds = Educations.Select(x => x.Id);
                var requestIds = requestEducations.Select(x => x.Id);
                var updateIds = requestIds.Intersect(existingIds);
                var deleteIds = existingIds.Except(requestIds);
                var addedIds = requestIds.Except(existingIds);

                var deleteEducations = Educations.Where(x => deleteIds.Contains(x.Id));
                foreach (var education in deleteEducations)
                {
                    await _educationService.DeleteAsync(education);
                }

                var updateEducations = requestEducations.Where(x => updateIds.Contains(x.Id));
                foreach (var educationRequest in updateEducations)
                {
                    var education = Educations.FirstOrDefault(x => x.Id == educationRequest.Id);
                    if (education == null)
                        throw new NopException("education not found");
                    educationRequest.ProfileId = profileId;
                    education = educationRequest.ToEntity(education);
                    await _educationService.UpdateAsync(education);
                }

                var addEducations = requestEducations.Where(x => addedIds.Contains(x.Id));
                foreach (var educationRequest in addEducations)
                {
                    educationRequest.ProfileId = profileId;
                    var education = educationRequest.ToEntity<Education>();
                    await _educationService.InsertAsync(education);
                }
            }
        }

        private async Task UpdateOccupations(int profileId, List<OccupationModel> requestOccupations)
        {
            if (requestOccupations != null)
            {
                var Occupations = await _occupationService.GetByProfileIdAsync(profileId);
                var existingIds = Occupations.Select(x => x.Id);
                var requestIds = requestOccupations.Select(x => x.Id);
                var updateIds = requestIds.Intersect(existingIds);
                var deleteIds = existingIds.Except(requestIds);
                var addedIds = requestIds.Except(existingIds);

                var deleteOccupations = Occupations.Where(x => deleteIds.Contains(x.Id));
                foreach (var occupation in deleteOccupations)
                {
                    await _occupationService.DeleteAsync(occupation);
                }

                var updateOccupations = requestOccupations.Where(x => updateIds.Contains(x.Id));
                foreach (var occupationRequest in updateOccupations)
                {
                    var occupation = Occupations.FirstOrDefault(x => x.Id == occupationRequest.Id);
                    if (occupation == null)
                        throw new NopException("occupation not found");
                    occupationRequest.ProfileId = profileId;
                    occupation = occupationRequest.ToEntity(occupation);
                    await _occupationService.UpdateAsync(occupation);
                }

                var addOccupations = requestOccupations.Where(x => addedIds.Contains(x.Id));
                foreach (var occupationRequest in addOccupations)
                {
                    occupationRequest.ProfileId = profileId;
                    var occupation = occupationRequest.ToEntity<Occupation>();
                    await _occupationService.InsertAsync(occupation);
                }
            }
        }

        private async Task UpdateAchivements(int profileId, List<AchivementModel> requestAchivements)
        {
            if (requestAchivements != null)
            {
                var Achivements = await _achivementService.GetByProfileIdAsync(profileId);
                var existingIds = Achivements.Select(x => x.Id);
                var requestIds = requestAchivements.Select(x => x.Id);
                var updateIds = requestIds.Intersect(existingIds);
                var deleteIds = existingIds.Except(requestIds);
                var addedIds = requestIds.Except(existingIds);

                var deleteAchivements = Achivements.Where(x => deleteIds.Contains(x.Id));
                foreach (var achivement in deleteAchivements)
                {
                    await _achivementService.DeleteAsync(achivement);
                }

                var updateAchivements = requestAchivements.Where(x => updateIds.Contains(x.Id));
                foreach (var achivementRequest in updateAchivements)
                {
                    var achivement = Achivements.FirstOrDefault(x => x.Id == achivementRequest.Id);
                    if (achivement == null)
                        throw new NopException("achivement not found");
                    achivementRequest.ProfileId = profileId;
                    achivement = achivementRequest.ToEntity(achivement);
                    await _achivementService.UpdateAsync(achivement);
                }

                var addAchivements = requestAchivements.Where(x => addedIds.Contains(x.Id));
                foreach (var achivementRequest in addAchivements)
                {
                    achivementRequest.ProfileId = profileId;
                    var achivement = achivementRequest.ToEntity<Achivement>();
                    await _achivementService.InsertAsync(achivement);
                }
            }
        }

        #endregion

        [HttpPost]
        public virtual async Task<IActionResult> Get(int id)
        {
            var model = await _profileFactoryModel.PrepareProfileEditModelAsync(id);
            return Success(model);
        }

        [HttpPost]
        public virtual async Task<IActionResult> List(ProfileSearchModel searchModel)
        {
            //prepare model
            var model = await _profileFactoryModel.PrepareProfileListModelAsync(searchModel);
            return Success(model);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Create(ProfileCreateRequestModel model)
        {
            // check existing profile exist or not
            var isExists = await _profileService.CheckDuplicateAsync(0, model.Email);
            if (isExists)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "User Profile is already exist." });
            }

            // check existing
            ApplicationUser user = await _authenticateService.FindByNameAsync(model.Email);

            // create user use profile id
            if (user == null)
            {
                user = new()
                {
                    Email = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = model.Email
                };
                var result = await _authenticateService.CreateAsync(user, "Test#123");//TODO need to create randomaly
                if (!result)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });
                }
                if (!await _authenticateService.RoleExistsAsync(UserRoles.User))
                    await _authenticateService.CreateAsync(UserRoles.User);

                user = await _authenticateService.FindByNameAsync(model.Email);
            }

            //Assing user role
            if (await _authenticateService.RoleExistsAsync(UserRoles.User))
            {
                await _authenticateService.AddToRoleAsync(user, UserRoles.User);
            }

            // creare profile
            var profile = model.ToEntity<Profile>();
            profile.UserId = user.Id;
            await _profileService.InsertAsync(profile);
            return Success(profile.ToModel<ProfileCreateRequestModel>());
        }


        [HttpPost]
        public virtual async Task<IActionResult> Update(ProfileEditRequestModel model)
        {
            int profileId = model.Id;

            var profile = await _profileService.GetByIdAsync(model.Id);
            if (profile == null)
                throw new NopException("profile not found");

            string email = profile.Email;
            int userId = profile.UserId;
            profile = model.Profile.ToEntity(profile);
            profile.Email = email;
            profile.UserId = userId;
            await _profileService.UpdateAsync(profile);

            await UpdateAddresses(profileId, model.Addresses);
            await UpdateFamilies(profileId, model.Families);
            await UpdateEducations(profileId, model.Educations);
            await UpdateOccupations(profileId, model.Occupations);
            await UpdateAchivements(profileId, model.Achivements);

            return Success(model);

        }
    }
}
