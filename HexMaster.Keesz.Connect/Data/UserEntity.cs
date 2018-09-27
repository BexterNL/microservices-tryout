using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace HexMaster.Keesz.Connect.Data
{
    public class UserEntity
    {

        public UserEntity()
        {
            Credentials = new List<CredentialEntity>();
        }

        [BsonId] public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsActive { get;  set; }
        public bool IsVerified { get;  set; }
        public string VerificationCode { get;  set; }
        public List<CredentialEntity> Credentials { get; set; }
        public DateTimeOffset FirstLogin { get; set; }
        public DateTimeOffset LastLogin { get; set; }
    }
}
