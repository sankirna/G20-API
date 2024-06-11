using G20.API.Models.Media;
using G20.Framework.Models;

namespace G20.API.Models.Teams
{
    public partial record TeamModel : BaseNopEntityModel
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
        public int CountryId { get; set; }
        public int? StateId { get; set; }
        public int? CityId { get; set; }
        public int? logoId { get; set; }
        public FileUploadRequestModel? Logo { get; set; }
    }
}
