using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HexMaster.Core.Infrastructure.Enums;
using HexMaster.Core.Repositories;
using HexMaster.Keesz.Connect.Configuration;
using HexMaster.Keesz.Connect.Contracts.Repositories;
using HexMaster.Keesz.Connect.Data;
using HexMaster.Keesz.Connect.DataTransferObjects;
using HexMaster.Keesz.Connect.DomainModels;
using HexMaster.Keesz.Connect.Mappings;
using MongoDB.Driver;

namespace HexMaster.Keesz.Connect.Repositories
{
    public class FriendsRepository: MongoRepositoryBase<FriendEntity>, IFriendsRepository
    {

        private const string MongoDatabaseName = "KeeszConnect";
        private const string MongoCollectionName = "Friends";

        public async Task<List<Friend>> Get(Guid userId)
        {
            var userCollection = Database.GetCollection<UserEntity>(UsersRepository.MongoCollectionName);

            var friends = await Collection.Find(frnd => frnd.UserId == userId).ToListAsync();

            var friendIds = friends.Select(x => x.FriendUserId).ToList();
            var friendNameFilter = Builders<UserEntity>.Filter.In(nameof(UserEntity.Id), friendIds);
            var friendsNames = await userCollection.Find(friendNameFilter).ToListAsync();

            var friendsQuery = from dtFriends in friends
                join dtNames in friendsNames on dtFriends.FriendUserId equals dtNames.Id
                select new Friend(
                    dtFriends.Id,
                    dtFriends.UserId,
                    dtFriends.FriendUserId,
                    dtNames.Name,
                    dtFriends.IsAccepted,
                    dtFriends.RequestExpiresOn);
            return friendsQuery.ToList();

        }

        public async Task<Friend> GetSingle(Guid friendId)
        {
            var userCollection = Database.GetCollection<UserEntity>(UsersRepository.MongoCollectionName);

            var friendEntity = await Collection.Find(frnd => frnd.Id == friendId).FirstOrDefaultAsync();
            var friendUser =
                await userCollection.Find(usr => usr.Id == friendEntity.FriendUserId).FirstOrDefaultAsync();

        return new Friend(
            friendEntity.Id,
            friendEntity.UserId,
            friendEntity.FriendUserId,
            friendUser.Name,
            friendEntity.IsAccepted,
            friendEntity.RequestExpiresOn);
        }

        public async Task<List<InviteDto>> GetInvites(Guid userId)
        {
            var userCollection = Database.GetCollection<UserEntity>(UsersRepository.MongoCollectionName);

            var friendsCursor = await Collection.FindAsync(frnd => frnd.FriendUserId == userId && !frnd.IsAccepted && frnd.RequestExpiresOn.HasValue);
            var friends = await friendsCursor.ToListAsync();

            var inviteeIds = friends.Select(x => x.UserId).ToList();
            var inviteeNameFilter = Builders<UserEntity>.Filter.In(nameof(UserEntity.Id), inviteeIds);
            var inviteeNameCursor = await userCollection.FindAsync(inviteeNameFilter);
            var inviteeNames = await inviteeNameCursor.ToListAsync();

            var inviteesQuery = from dtFriends in friends
                join dtNames in inviteeNames on dtFriends.UserId equals dtNames.Id
                select new InviteDto
                {
                    Id = dtFriends.Id,
                    ExpiresOn = dtFriends.RequestExpiresOn,
                    Name = dtNames.Name
                }
                    ;
            return inviteesQuery.ToList();
        }
        public async Task<bool> Create(Friend friend)
        {
            if (friend.State == TrackingState.Added)
            {
                var entity = friend.ToEntity();
                await Collection.InsertOneAsync(entity);
                return true;
            }

            return false;
        }
        public async Task<bool> Update(Friend friend)
        {
            bool updated = false;
            if (friend.State == TrackingState.Modified)
            {
                var entity = friend.ToEntity();
                var filter = Builders<FriendEntity>.Filter.Eq(nameof(FriendEntity.Id), friend.Id);
                var result = await Collection.ReplaceOneAsync(filter, entity);
                updated = result.IsAcknowledged;
            }
            if (friend.State == TrackingState.Touched)
            {
                updated = true;
            }
            return updated;
        }

        public async Task<bool> Exists(Guid userId, Guid friendUserId)
        {
            var friends = await Collection
                .Find(frnd => frnd.UserId == userId && frnd.FriendUserId == friendUserId)
                .CountDocumentsAsync();
            return friends > 0;
        }
        public async Task<Friend> FindByUsers(Guid userId, Guid friendUserId)
        {
            var friend = await Collection
                .Find(frnd => frnd.UserId == userId && frnd.FriendUserId == friendUserId)
                .FirstOrDefaultAsync();

            if (friend != null)
            {
                return new Friend(friend.Id, friend.UserId, friend.FriendUserId, null, friend.IsAccepted, friend.RequestExpiresOn);
            }

            return null;
        }

        public FriendsRepository(AppSettingsConfiguration settings) : base(settings.MongoDbConnectionString, MongoDatabaseName, MongoCollectionName)
        {
        }

    }
}
