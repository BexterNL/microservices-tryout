using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HexMaster.Keesz.Connect.DataTransferObjects;

namespace HexMaster.Keesz.Connect.Contracts.Services
{
    public interface IFriendsService
    {
        Task<List<FriendDto>> Get(Guid userId);
        Task<List<InviteDto>> GetInvites(Guid userId);
        Task<FriendDto> Create(Guid userId, Guid friendUserId);
        Task<FriendDto> Accept(Guid inviteId, Guid userId);
        Task<FriendDto> Decline(Guid inviteId, Guid userId);

    }
}