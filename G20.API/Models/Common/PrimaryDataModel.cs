using G20.Framework.Models;

namespace G20.API.Models.Common
{
    public class PrimaryDataModel
    {
        public List<EnumModel> Roles { get;set; }
        public List<EnumModel> FileTypes { get;set; }
        public List<EnumModel> CouponCalculateTypes { get;set; }
    }
}
