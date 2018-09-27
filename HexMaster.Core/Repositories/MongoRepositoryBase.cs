using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace HexMaster.Core.Repositories
{
    public abstract class MongoRepositoryBase<TEntity>
    {

        private MongoClient _client;
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<TEntity> _collection;

        protected IMongoCollection<TEntity> Collection => _collection;
        protected IMongoDatabase Database => _database;

        protected MongoRepositoryBase(string connectionString, string databaseName, string collectionName)
        {
            _client = new MongoClient(connectionString);
            _database = _client.GetDatabase(databaseName);
            _collection = _database.GetCollection<TEntity>(collectionName);
        }

        public IMongoQueryable<TEntity> GetFiltered(Expression<Func<TEntity, bool>> predicate)
        {
            return Collection.AsQueryable().Where(predicate);
        }

    }
}
