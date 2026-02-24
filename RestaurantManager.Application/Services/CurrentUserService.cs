using Microsoft.AspNetCore.Http;
using RestaurantManager.Application.Services.Interfaces;
using System.Security.Claims;

namespace RestaurantManager.Application.Services
{
    public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
    {
        private readonly IHttpContextAccessor httpContextAccessor = httpContextAccessor;
        public string UserId => httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}
