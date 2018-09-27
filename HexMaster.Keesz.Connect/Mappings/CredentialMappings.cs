using System.Collections.Generic;
using System.Linq;
using HexMaster.Keesz.Connect.Data;
using HexMaster.Keesz.Connect.DomainModels;

namespace HexMaster.Keesz.Connect.Mappings
{
    public static class CredentialMappings
    {

        public static Credential ToDomainModel(this CredentialEntity entity)
        {
            return new Credential(entity.Id, entity.Subject);
        }
        public static List<Credential> ToDomainModel(this List<CredentialEntity> entity)
        {
            return entity.Select(x => x.ToDomainModel()).ToList();
        }

        public static CredentialEntity ToEntity(this Credential model)
        {
            return new CredentialEntity
            {
                Id = model.Id,
                Subject = model.Subject
            };
        }
        public static List<CredentialEntity> ToEntity(this ICollection<Credential> models)
        {
            return models.Select(x => x.ToEntity()).ToList();
        }

    }
}
