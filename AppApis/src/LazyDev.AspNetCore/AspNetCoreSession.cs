using LazyDev.Core.Runtime;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;

namespace LazyDev.AspNetCore
{
    public class AspNetCoreSession : ILazyDevSession
    {
        private readonly ClaimsPrincipal _principal;


        public AspNetCoreSession(IHttpContextAccessor httpContextAccessor)
        {
            _principal = httpContextAccessor.HttpContext?.User;
        }

        public int UId {
            get
            {
                var claim = _principal?.Claims?.FirstOrDefault(x => x.Type == LazyDevSessionDefaults.UId);
                if (claim == null)
                {
                    return default;
                }
                if (string.IsNullOrEmpty(claim.Value))
                {

                    return default;
                }
                int.TryParse(claim.Value, out var v);
                return v;
            }
        }
        public int TenantId {
            get
            {
                var claim = _principal?.Claims?.FirstOrDefault(x => x.Type == LazyDevSessionDefaults.TenantId);
                if (claim == null)
                {
                    return default;
                }
                if (string.IsNullOrEmpty(claim.Value))
                {

                    return default;
                }
                int.TryParse(claim.Value, out var v);
                return v;
            }
        }
    }
}
