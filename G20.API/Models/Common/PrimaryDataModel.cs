using G20.Framework.Models;

namespace G20.API.Models.Common
{
    public class PrimaryDataModel
    {
        public List<EnumModel> AddressTypes { get;set; }
        public List<EnumModel> OccupationTypes { get;set; }
        public List<EnumModel> RelationTypes { get;set; }
        public List<EnumModel> Roles { get;set; }
        public List<EnumModel> GenderTypes { get;set; }
        public List<EnumModel> FileTypes { get;set; }
    }
}
