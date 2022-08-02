using SchoolAttendance.Application.Common.Interfaces;
using System.Security.Claims;

namespace SchoolAttendance.WebAPI.Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int? UserId
        {
            get
            {
                if (_httpContextAccessor.HttpContext == null)
                    return (int?)null;

                try
                {
                    return int.Parse(_httpContextAccessor.HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier));
                }
                catch (Exception ex)
                {
                    return (int?)null;
                }


            }
        }
    }
}
