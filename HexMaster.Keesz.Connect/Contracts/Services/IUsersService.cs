using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HexMaster.Keesz.Connect.DataTransferObjects;

namespace HexMaster.Keesz.Connect.Contracts.Services
{
    public interface IUsersService
    {
        Task<UserDto> Get(string subject);
        Task<UserDto> Put(Guid id, UserDto model);
        Task<List<UserSearchResultDto>> Search(string query, List<Guid> exclude, Guid userId);
    }
}