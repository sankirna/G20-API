using Microsoft.AspNetCore.Mvc.Filters;
using Nop.Core;
using System.Net;
using System.Linq;
using Matrimony.API.Infrastructure;
using Microsoft.AspNetCore.Mvc;


namespace Matrimony.Framework.Filters
{
    public class APIExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception != null)
            {
                HttpStatusCode code = HttpStatusCode.InternalServerError;
                List<string> errors = new List<string>();
                List<string> innerErrors = new List<string>();
                if (context.Exception is NopException)
                {
                    var customExeption = (NopException)context.Exception;
                    if (customExeption != null)
                    {
                        errors.Add(customExeption.Message);
                        if (customExeption.InnerException != null
                            && !string.IsNullOrEmpty(customExeption.InnerException.Message))
                        {
                            innerErrors.Add(customExeption.InnerException.Message);
                        }
                    }
                }

                else if (context.Exception is Exception)
                {
                    var exeption = (Exception)context.Exception;
                    if (exeption != null)
                    {
                        errors.Add(exeption.Message);
                        if (exeption.InnerException != null
                            && !string.IsNullOrEmpty(exeption.InnerException.Message))
                        {
                            innerErrors.Add(exeption.InnerException.Message);
                        }
                    }
                }
                if (errors.Any() || innerErrors.Any())
                {
                    context.Result = new JsonResult(code.ToErrorApiResponse(errors.ToAppErrorResponse(innerErrors))) { StatusCode = (int)code };
                }
            }



            //context.Result = new JsonResult(error);
        }
    }
}
