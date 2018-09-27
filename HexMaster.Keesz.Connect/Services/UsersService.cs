using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HexMaster.Core.Contracts;
using HexMaster.Core.Exceptions;
using HexMaster.Keesz.Connect.Contracts.Repositories;
using HexMaster.Keesz.Connect.Contracts.Services;
using HexMaster.Keesz.Connect.DataTransferObjects;
using HexMaster.Keesz.Connect.DomainModels;
using HexMaster.Keesz.Connect.IntegrationEvents;
using HexMaster.Keesz.Connect.IntegrationEvents.Events;
using HexMaster.Keesz.Connect.Mappings;

namespace HexMaster.Keesz.Connect.Services
{
    public class UsersService : IUsersService
    {
        private readonly IRandomizer _randomService;
        private readonly IKeeszConnectIntegrationEventService _eventBus;
        public IUsersRepository Repository { get; }

        public async Task<UserDto> Get(string subject)
        {
            var user = await Repository.Get(subject);
            if (user == null)
            {
                var now = DateTime.UtcNow;
                user = new User();
                user.SetName($"User{now.Hour}{now.Minute}{now.Second}{now.Millisecond}");
                user.AddCredential(subject);
                user.SetLastLogin();
                user.SetVerificationCode(_randomService.GenerateVerificationCode());
                if (await Repository.Create(user))
                {
                    PublishUserInfoChangedIntegrationEvent(user);
                }
            }
            else
            {
                user.SetLastLogin();
                if (!await Repository.Update(user))
                {
                    throw new Exception("Failed to update user");
                }
            }

            return user.ToDto();
        }

        public async Task<UserDto> Put(Guid id, UserDto dto)
        {
            var user = await Repository.Get(id);
            if (user == null)
            {
                throw new NotFoundException("User", id.ToString());
            }
            user.SetEmail(dto.Email);
            user.SetName(dto.Name);
            if (await Repository.Update(user))
            {
                PublishUserInfoChangedIntegrationEvent(user);
                return user.ToDto();
            }

            return null;
        }

        public async Task<List<UserSearchResultDto>> Search(string query, List<Guid> exclude, Guid userId)
        {
            var excludeList = exclude ?? new List<Guid>();
            if (!excludeList.Contains(userId)) { excludeList.Add(userId);}

            var result = await Repository.Find(query, excludeList);
            return result.Select(usr => new UserSearchResultDto
            {
                Id = usr.Id,
                Name = usr.Name
            }).ToList();
        }

        private void PublishUserInfoChangedIntegrationEvent(User user)
        {
            var userInfoChanged = new UserInfoChangedIntegrationEvent(user.Id, user.Name, user.Email);
            _eventBus.PublishThroughEventBusAsync(userInfoChanged);
        }


        public UsersService(
            IUsersRepository repository, 
            IRandomizer randomService,
            IKeeszConnectIntegrationEventService eventBus)
        {
            _randomService = randomService;
            _eventBus = eventBus;
            Repository = repository;
        }

    }
}
