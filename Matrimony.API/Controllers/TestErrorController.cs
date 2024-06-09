using G20.API.Factories.Countries;
using G20.API.Infrastructure.Mapper.Extensions;
using G20.API.Models;
using G20.API.Models.Countries;
using G20.Core;
using G20.Core.Domain;
using G20.Service;
using G20.Service.Countries;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Web.Framework.Models.Extensions;

namespace G20.API.Controllers
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
