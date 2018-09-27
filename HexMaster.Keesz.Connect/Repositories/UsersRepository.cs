using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HexMaster.Core.Infrastructure.Enums;
using HexMaster.Core.Repositories;
using HexMaster.Keesz.Connect.Configuration;
using HexMaster.Keesz.Connect.Contracts.Repositories;
using HexMaster.Keesz.Connect.Data;
using HexMaster.Keesz.Connect.DomainModels;
using HexMaster.Keesz.Connect.Mappings;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HexMaster.Keesz.Connect.Repositories
{
    public class UsersRepository : MongoRepositoryBase<UserEntity>, IUsersRepository
    {

        public const string MongoDatabaseName = "KeeszConnect";
        public const string MongoCollectionName = "Users";

        public async Task<User> Get(Guid id)
        {
            var filter = Builders<UserEntity>.Filter.Eq(nameof(UserEntity.Id), id);
            var cursor = await Collection.FindAsync(filter);
            var entity = cursor.FirstOrDefault();

            var credentials = entity?.Credentials.ToDomainModel();
            return entity?.ToDomainModel(credentials);
        }

        public async Task<User> Get(string subject)
        {
            var cursor  = await Collection.FindAsync(usr => usr.Credentials.Any(cred => cred.Subject == subject));
            var entity = cursor.FirstOrDefault();

            var credentials = entity?.Credentials.ToDomainModel();
            return entity?.ToDomainModel(credentials);
        }

        public async Task<bool> Create(User user)
        {
            if (user.State == TrackingState.Added)
            {
                var entity = user.ToEntity();
                await Collection.InsertOneAsync(entity);
                return true;
            }

            return false;
        }

        public async Task<bool> Update(User user)
        {
            bool updated = false;
            if (user.State == TrackingState.Modified)
            {
                var entity = user.ToEntity();
                var filter = Builders<UserEntity>.Filter.Eq(nameof(UserEntity.Id), user.Id);
                var result = await Collection.ReplaceOneAsync(filter, entity);
                updated= result.IsAcknowledged;
            }
            if (user.State == TrackingState.Touched)
            {
                updated= true;
            }
            return updated;
        }

        public async Task<List<User>> Find(string search, List<Guid> exclude)
        {
            var builder = Builders<UserEntity>.Filter;
            var regEx = new BsonRegularExpression(search, "i");// case insensitive
            var filter = builder.Nin(nameof(UserEntity.Id), exclude) & builder.Regex(nameof(UserEntity.Name),regEx);
            var result = await Collection.Find(filter).ToListAsync();
            return result.ToDomainModel();
        }


        public UsersRepository(AppSettingsConfiguration settings) : base(settings.MongoDbConnectionString, MongoDatabaseName, MongoCollectionName)
        {
        }



    }
}
