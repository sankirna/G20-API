using G20.API.Models.Media;
using G20.Core.Enums;
using G20.Framework.Models;

namespace G20.API.Models.Profiles
{
    public partial record ProfileModel : BaseNopEntityModel
    {
        public int UserId { get; set; }

        public string FirstName { get; set; } = null!;

        public string? MiddleName { get; set; }

        public string? LastName { get; set; }

        public GenderTypeEnum Sex { get; set; }

        public string SexValue { get { return Sex.ToString(); } }

        public string Email { get; set; } = null!;

        public string? AlternateEmail { get; set; }

        public string? PhoneNo { get; set; }

        public string? AlternatePhoneNo { get; set; }

        public string? Langauge { get; set; }

        public string? OtherInformation { get; set; }
        public FileUploadRequestModel ResumeFileData { get; set; }

    }
}
