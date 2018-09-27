using System.Collections.Generic;
using System.Linq;
using HexMaster.Keesz.Connect.Data;
using HexMaster.Keesz.Connect.DataTransferObjects;
using HexMaster.Keesz.Connect.DomainModels;

namespace HexMaster.Keesz.Connect.Mappings
{
    public static class UserMappings
    {

        public static UserDto ToDto(this User model)
        {
            return new UserDto
            {
                Id = model.Id,
                Name = model.Name,
                Email = model.Email,
                IsVerified = model.IsVerified,
                FirstLogin =  model.FirstLogin,
                LastLogin =  model.LastLogin
            };
        }


        public static User ToDomainModel(this UserEntity entity, List<Credential> credentials)
        {
            return new User(
                entity.Id, 
                entity.Name, 
                entity.Email,
                entity.IsActive,
                entity.IsVerified,
                entity.VerificationCode,
                credentials , 
                entity.FirstLogin, 
                entity.LastLogin);
        }
        public static List<User> ToDomainModel(this List<UserEntity> entities)
        {
            return entities.Select(x => x.ToDomainModel(null)).ToList();
        }

        public static UserEntity ToEntity(this User model, UserEntity existingEntity = null)
        {
            var entity = existingEntity ?? new UserEntity();
            entity.Id = model.Id;
            entity.Name = model.Name;
            entity.Email = model.Email;
            entity.IsVerified = model.IsVerified;
            entity.IsActive = model.IsActive;
            entity.VerificationCode = model.VerificationCode;
            entity.FirstLogin = model.FirstLogin;
            entity.LastLogin = model.LastLogin;
            entity.Credentials = model.Credentials.ToEntity();
            return entity;
        }

    }
}
