using G20.API.Models.Achivements;
using G20.API.Models.Addresss;
using G20.API.Models.Educations;
using G20.API.Models.Families;
using G20.API.Models.Occupations;
using G20.Core.Domain;
using G20.Framework.Models;

namespace G20.API.Models.Profiles
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
