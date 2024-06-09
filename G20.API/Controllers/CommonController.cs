using G20.API.Infrastructure.Mapper.Extensions;
using G20.API.Models.Common;
using G20.Framework.Models;
using G20.Service.Common;
using Microsoft.AspNetCore.Mvc;

namespace G20.API.Controllers
{
    public class CommonController : BaseController
    {
        protected readonly IPrimaryService _primaryService;
        public CommonController(IPrimaryService primaryService)
        {
            _primaryService = primaryService;
        }

        [HttpPost]
        public virtual IActionResult GetPrimaryData()
        {
            PrimaryDataModel model = new PrimaryDataModel();
            model.AddressTypes = _primaryService.GetAddressTypes().Select(x => x.ToModel<EnumModel>()).ToList();
            model.OccupationTypes = _primaryService.GetOccupationTypes().Select(x => x.ToModel<EnumModel>()).ToList();
            model.RelationTypes = _primaryService.GetRelationTypes().Select(x => x.ToModel<EnumModel>()).ToList();
            model.Roles = _primaryService.GetRoles().Select(x => x.ToModel<EnumModel>()).ToList();
            model.GenderTypes = _primaryService.GetGenderTypes().Select(x => x.ToModel<EnumModel>()).ToList();
            model.FileTypes = _primaryService.GetFileTypes().Select(x => x.ToModel<EnumModel>()).ToList();
            model.CouponCalculateTypes = _primaryService.GetCouponCalculateTypes().Select(x => x.ToModel<EnumModel>()).ToList();
            return Success(model);
        }
    }
}
