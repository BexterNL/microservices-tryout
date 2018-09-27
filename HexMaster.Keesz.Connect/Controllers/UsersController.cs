using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using HexMaster.Keesz.Connect.Contracts.Services;
using HexMaster.Keesz.Connect.Controllers.Base;
using HexMaster.Keesz.Connect.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace HexMaster.Keesz.Connect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : KeeszControllerBase
    {
        private readonly IUsersService _service;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var subject = GetSubjectClaim();
                if (!string.IsNullOrEmpty(subject))
                {
                    var userInfo = await _service.Get(subject);
                    if (userInfo == null)
                    {
                        return BadRequest("User could not be created or updated");
                    }

                    return Ok(userInfo);
                }
                return BadRequest("Either the subject or hosted domain claim not found");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut("api/[controller]/{id:guid}")]
        public async Task<IActionResult> Put([FromQuery] Guid id, [FromBody] UserDto model)
        {
            try
            {
                var result = await _service.Put(id, model);
                if (result == null)
                {
                    return BadRequest("Failed to update user");
                }
                return Ok(result);
            }        
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery]string q = null, [FromQuery]List<Guid> exclude = null)
        {
            try
            {
                var userId = await GetUserId();
                if (!Guid.Empty.Equals(userId)) 
                {
                    var userInfo = await _service.Search(q, exclude, userId);
                    if (userInfo == null)
                    {
                        return BadRequest("User could not be created or updated");
                    }

                    return Ok(userInfo);
                }
                return BadRequest("Either the subject or hosted domain claim not found");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public UsersController(IUsersService service, IMemoryCache cache):base (service, cache)
        {
            _service = service;
        }

    }
}