using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HexMaster.Keesz.Connect.DomainModels;

namespace HexMaster.Keesz.Connect.Contracts.Repositories
{
    public interface IUsersRepository
    {
        Task<User> Get(Guid id);
        Task<User> Get( string subject);
        Task<bool> Create(User user);
        Task<bool> Update(User user);

        Task<List<User>> Find(string search, List<Guid> exclude);
    }
}