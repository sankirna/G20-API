using Matrimony.API.Models.Media;
using Matrimony.Core.Enums;
using Matrimony.Framework.Models;

namespace Matrimony.API.Models.Profiles
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
