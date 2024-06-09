using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using G20.API.Infrastructure;
using Nop.Core.Infrastructure;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using G20.Core;
using G20.API.Infrastructure;

namespace G20.Framework.Filters
{
    public class APIValidationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var test = EngineContext.Current.Resolve<IHttpContextAccessor>();
            var workContext = EngineContext.Current.Resolve<IWorkContext>();
            if (test.HttpContext.User != null)
            {
                var userId = test.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                if(int.TryParse(userId, out var id))
                {
                    workContext.SetCurrentUserId(id);
                }
            }
            if (!context.ModelState.IsValid)
            {
                //the only output i want are the error descriptions, nothing else
                var data = context.ModelState
                    .Values
                    .SelectMany(v => v.Errors.Select(b => b.ErrorMessage))
                    .ToList();
                HttpStatusCode code = HttpStatusCode.BadRequest;
                context.Result = new JsonResult(code.ToErrorApiResponse(data)) {  StatusCode = (int)code };
            }
        }
    }
}
