using System;
using System.Linq;
using System.Threading.Tasks;
using HexMaster.Keesz.Connect.Contracts.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace HexMaster.Keesz.Connect.Controllers.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class KeeszControllerBase : ControllerBase
    {
        public IUsersService UsersService { get; }
        public IMemoryCache MemoryCache { get; }

        protected KeeszControllerBase(IUsersService usersService, IMemoryCache memoryCache)
        {
            UsersService = usersService;
            MemoryCache = memoryCache;
        }

        protected virtual string GetSubjectClaim()
        {
            var subjectClaim = HttpContext.User.Claims.FirstOrDefault(x =>
                x.Type.Contains("nameidentifier") || x.Type.Contains("sub"));
            return subjectClaim?.Value;
        }

        protected virtual async Task< Guid> GetUserId()
        {
            var claim = GetSubjectClaim();
            Guid userId;
            if (!MemoryCache.TryGetValue(claim, out userId))
            {
                var user = await UsersService.Get(claim);
                if (user != null)
                {
                    userId = user.Id;
                    MemoryCache.Set(claim, userId);
                }
            }

            return userId;
        }


    }
}