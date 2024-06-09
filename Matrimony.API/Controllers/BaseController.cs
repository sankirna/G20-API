using Matrimony.API.Infrastructure;
using Matrimony.Framework.Filters;
using Matrimony.Framework.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Matrimony.API.Controllers
{
    //[Authorize]
    [Route("api/[controller]/[Action]")]
    [ApiController]
    [APIValidationFilter]
    public class BaseController : ControllerBase
    {
        protected JsonResult Success<T>(T data)
        {
            HttpStatusCode code = HttpStatusCode.OK;
            return new JsonResult(code.ToSuccessApiResponse(data)) { StatusCode = (int)code };
        }

        protected JsonResult Error<T>(T data)
        {
            HttpStatusCode code = HttpStatusCode.InternalServerError;
            return new JsonResult(code.ToErrorApiResponse(data)) { StatusCode = (int)code };
        }

        protected JsonResult BadRequest<T>(T data)
        {
            HttpStatusCode code = HttpStatusCode.BadRequest;
            return new JsonResult(code.ToErrorApiResponse(data)) { StatusCode = (int)code };
        }
    }
}
