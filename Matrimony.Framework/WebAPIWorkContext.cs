using G20.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace G20.Framework
{
    public class WebAPIWorkContext : IWorkContext
    {
        public const string UserCacheKey = "CurrentUserId";
        protected readonly IHttpContextAccessor _httpContextAccessor;
        public WebAPIWorkContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public int GetCurrentUserId()
        {
            StringValues userIdHeader = (StringValues)(_httpContextAccessor.HttpContext?.Request.Headers.FirstOrDefault(x => x.Key == UserCacheKey).Value);// (UserCacheKey, out headerUserIdValue);
            if (!StringValues.IsNullOrEmpty(userIdHeader))
            {
                int userId = 0;
                int.TryParse(userIdHeader, out userId);
                return userId;
            }
            return 0;
        }

        public void SetCurrentUserId(int userId)
        {
            _httpContextAccessor.HttpContext?.Request.Headers.Append(UserCacheKey, userId.ToString());
        }
    }
}
