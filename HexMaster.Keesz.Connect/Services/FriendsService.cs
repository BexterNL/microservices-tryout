using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HexMaster.Keesz.Connect.Contracts.Repositories;
using HexMaster.Keesz.Connect.Contracts.Services;
using HexMaster.Keesz.Connect.DataTransferObjects;
using HexMaster.Keesz.Connect.DomainModels;
using HexMaster.Keesz.Connect.IntegrationEvents;
using HexMaster.Keesz.Connect.IntegrationEvents.Events;
using HexMaster.Keesz.Connect.Mappings;

namespace HexMaster.Keesz.Connect.Services
{
    public class FriendsService : IFriendsService
    {
        private readonly IFriendsRepository _repository;
        private readonly IUsersRepository _usersRepository;
        private readonly IKeeszConnectIntegrationEventService _eventBus;

        public async Task<List<FriendDto>> Get(Guid userId)
        {
                var friends = await _repository.Get(userId);
                return friends?.ToDto();
        }

        public async Task<List<InviteDto>> GetInvites(Guid userId)
        {
            return  await _repository.GetInvites(userId);
        }

        public async Task<FriendDto> Create(Guid userId, Guid friendUserId)
        {
            if (Equals(userId, friendUserId))
            {
                throw new Exception("Failed to create friend request, cannot invite yourself");
            }
            if (await _repository.Exists(userId, friendUserId))
            {
                throw new Exception("A friends connection or request already exists between these users");
            }
            var friend = Friend.Create(userId, friendUserId);
            if (await _repository.Create(friend))
            {
                friend = await _repository.GetSingle(friend.Id);
                var invitee = await _usersRepository.Get(userId);
                PublicFriendInvitationReceivedEvent(friendUserId, invitee.Name);
                return friend?.ToDto();
            }

            return null;
        }

        public async Task<FriendDto> Accept(Guid inviteId, Guid userId)
        {
            var friend = await _repository.GetSingle(inviteId);
            if (friend.FriendUserId.Equals(userId))
            {
                friend.Accept();
                if (await _repository.Update(friend))
                {

                    // Also make the other users friends...
                    var reverseFriends = await _repository.FindByUsers(friend.FriendUserId, friend.UserGuid);
                    if (reverseFriends == null)
                    {
                        reverseFriends = Friend.Create(friend.FriendUserId, friend.UserGuid);
                    }
                    reverseFriends.Accept();
                    if (await _repository.Create(reverseFriends))
                    {
                        var friendUser = await _usersRepository.Get(friend.FriendUserId);
                        var currentUser = await _usersRepository.Get(friend.UserGuid);
                        // Signal the originator a friend request was accepted
                        PublishFriendInvitationAcceptedEvent(friend.FriendUserId, friend.UserGuid, currentUser?.Name);
                        // Signal the receiver user they became friends..
                        PublishFriendInvitationAcceptedEvent(friend.UserGuid, friend.FriendUserId, friendUser?.Name);
                    }

                    return friend.ToDto();
                }
            }

            return null;
        }

        public async Task<FriendDto> Decline(Guid inviteId, Guid userId)
        {
            var friend = await _repository.GetSingle(inviteId);
            if (friend.FriendUserId.Equals(userId))
            {
                friend.Decline();
                if (await _repository.Update(friend))
                {
                    return friend.ToDto();
                }
            }

            return null;
        }

        private void PublishFriendInvitationAcceptedEvent(Guid userId, Guid friendId, string friendName)
        {
            var eventInfo = new FriendInvitationAcceptedEvent(userId, friendId, friendName);
            _eventBus.PublishThroughEventBusAsync(eventInfo);
        }
        private void PublicFriendInvitationReceivedEvent(Guid userId, string friendName)
        {
            var eventInfo = new FriendInvitationReceivedEvent(userId,friendName);
            _eventBus.PublishThroughEventBusAsync(eventInfo);
        }
        

        

        public FriendsService(IFriendsRepository repository, IUsersRepository usersRepository, IKeeszConnectIntegrationEventService eventBus)

        {
            _repository = repository;
            _usersRepository = usersRepository;
            _eventBus = eventBus;
        }


    }
}
