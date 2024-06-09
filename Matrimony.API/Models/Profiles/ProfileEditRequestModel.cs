using Matrimony.API.Models.Achivements;
using Matrimony.API.Models.Addresss;
using Matrimony.API.Models.Educations;
using Matrimony.API.Models.Families;
using Matrimony.API.Models.Occupations;
using Matrimony.Core.Domain;
using Matrimony.Framework.Models;

namespace Matrimony.API.Models.Profiles
{
    public partial record ProfileEditRequestModel : BaseNopEntityModel
    {
        public ProfileEditRequestModel()
        {
            Profile = new ProfileModel();
            Addresses = new List<AddressModel>();
            Families = new List<FamilyModel>();
            Educations = new List<EducationModel>();
            Occupations = new List<OccupationModel>();
            Achivements = new List<AchivementModel>();
        }
        public ProfileModel Profile { get; set; }
        public List<AddressModel> Addresses { get; set; }
        public List<FamilyModel> Families { get; set; }
        public List<EducationModel> Educations { get; set; }
        public List<OccupationModel> Occupations { get; set; }
        public List<AchivementModel> Achivements { get; set; }

    }
}
