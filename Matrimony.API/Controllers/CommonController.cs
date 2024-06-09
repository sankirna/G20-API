using Matrimony.API.Infrastructure.Mapper.Extensions;
using Matrimony.API.Models.Common;
using Matrimony.Framework.Models;
using Matrimony.Service.Common;
using Microsoft.AspNetCore.Mvc;

namespace Matrimony.API.Controllers
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
            return Success(model);
        }
    }
}
