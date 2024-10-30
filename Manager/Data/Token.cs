using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.Net.Http;

namespace Manager.Data
{
    public class Token
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public Token(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string gettoken()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
            {
                throw new Exception("HttpContext không hợp lệ.");
            }
            var token = httpContext.Session.GetString("jwt");
            if (string.IsNullOrEmpty(token))
            {
                throw new UnauthorizedAccessException("Token không hợp lệ.");
            }
            return token;
        }
    }
}
