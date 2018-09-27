using System;
using MongoDB.Bson.Serialization.Attributes;

namespace HexMaster.Keesz.Connect.Data
{
    public class FriendEntity
    {
        [BsonId] public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid FriendUserId { get; set; }
        public DateTimeOffset? RequestExpiresOn { get; set; }
        public bool IsAccepted { get; set; }
    }
}