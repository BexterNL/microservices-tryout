using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HexMaster.Keesz.Connect.DataTransferObjects;
using HexMaster.Keesz.Connect.DomainModels;

namespace HexMaster.Keesz.Connect.Contracts.Repositories
{
    public interface IFriendsRepository
    {
        Task<List<Friend>> Get(Guid userId);
        Task<Friend> GetSingle(Guid friendId);
        Task<List<InviteDto>> GetInvites(Guid userId);
        Task<bool> Create(Friend friend);
        Task<bool> Update(Friend friend);

        Task<bool> Exists(Guid userId, Guid friendUserId);
        Task<Friend> FindByUsers(Guid userId, Guid friendUserId);
    }
}