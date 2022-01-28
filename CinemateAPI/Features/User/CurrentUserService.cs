namespace CinemateAPI.Features.User
{
    using System.Security.Claims;
    using Microsoft.AspNetCore.Http;
    using CinemateAPI.Infrastructure.Extensions;

    public class CurrentUserService : ICurrentUserService
    {
        private readonly ClaimsPrincipal user;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
            => this.user = httpContextAccessor.HttpContext?.User;

        public string GetUesername()
            => this.user?.Identity.Name;

        public string GetId()
            => this.user?.GetId();
    }
}
