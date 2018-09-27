using System;
using System.Linq;
using System.Threading.Tasks;
using HexMaster.Keesz.Connect.Contracts.Services;
using HexMaster.Keesz.Connect.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace HexMaster.Keesz.Connect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FriendsController : KeeszControllerBase
    {
        private readonly IFriendsService _service;

        public async Task<IActionResult> Get()
        {
            try
            {
                var userId = await GetUserId();
                if (!Equals(userId, Guid.Empty)) { 

                    var friends = await _service.Get(userId);
                    return Ok(friends);
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("invites")]
        public async Task<IActionResult> GetInvites()
        {
            try
            {
                var userId = await GetUserId();
                if (!Equals(userId, Guid.Empty))
                {

                    var friends = await _service.GetInvites(userId);
                    return Ok(friends);
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("{friendUserId:guid}")]
        public async Task<IActionResult> Create(Guid friendUserId)
        {
            try
            {
                var userId = await GetUserId();
                if (!Equals(userId, Guid.Empty))
                {
                    var friends = await _service.Create(userId, friendUserId);
                    return Ok(friends);
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet("{inviteId:guid}/accept")]
        public async Task<IActionResult> Accept(Guid inviteId)
        {
            try
            {
                var userId = await GetUserId();
                if (!Equals(userId, Guid.Empty))
                {

                    var friends = await _service.Accept(inviteId, userId);
                    return Ok(friends);
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet("{inviteId:guid}/decline")]
        public async Task<IActionResult> Decline(Guid inviteId)
        {
            try
            {
                var userId = await GetUserId();
                if (!Equals(userId, Guid.Empty))
                {

                    var friends = await _service.Decline(inviteId, userId);
                    return Ok(friends);
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public FriendsController(IUsersService usersService, IMemoryCache cache, IFriendsService friendsService)
        :base(usersService, cache)
        {
            _service = friendsService;
        }

    }
}