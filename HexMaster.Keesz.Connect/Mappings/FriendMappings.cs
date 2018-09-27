using System.Collections.Generic;
using System.Linq;
using HexMaster.Keesz.Connect.Data;
using HexMaster.Keesz.Connect.DataTransferObjects;
using HexMaster.Keesz.Connect.DomainModels;

namespace HexMaster.Keesz.Connect.Mappings
{
    public static class FriendMappings
    {

        public static FriendDto ToDto(this Friend model)
        {
            return new FriendDto
            {
                Id = model.Id,
                FriendUserId = model.FriendUserId,
                Name = model.Name,
                IsAccepted = model.IsAccepted,
                RequestExpiresOn = model.ExpiresOn
            };
        }
        public static List<FriendDto> ToDto(this List<Friend> models)
        {
            return models.Select(frnd => frnd.ToDto()).ToList();
        }

        public static FriendEntity ToEntity(this Friend model)
        {
            return new FriendEntity()
            {
                Id = model.Id,
                FriendUserId = model.FriendUserId,
                UserId = model.UserGuid,
                IsAccepted = model.IsAccepted,
                RequestExpiresOn = model.ExpiresOn
            };
        }

    }
}
