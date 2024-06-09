using Matrimony.API.Factories.Countries;
using Matrimony.API.Infrastructure.Mapper.Extensions;
using Matrimony.API.Models;
using Matrimony.API.Models.Countries;
using Matrimony.Core;
using Matrimony.Core.Domain;
using Matrimony.Service;
using Matrimony.Service.Countries;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Web.Framework.Models.Extensions;

namespace Matrimony.API.Controllers
{
    public class TestErrorController : BaseController
    {

        [HttpPost]
        public virtual async Task<IActionResult> CustomError()
        {
            throw new NopException("Custom Error");
        }

        [HttpPost]
        public virtual async Task<IActionResult> ApplicationError()
        {
            int k = 0;
            int i = 1 / k;
            return Success(i);
        }

        [HttpPost]
        public virtual async Task<IActionResult> ModelError(TestRequestModel model)
        {
     
            return Success(1);
        }
    }
}
