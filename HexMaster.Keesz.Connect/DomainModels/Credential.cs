using System;
using HexMaster.Core.DomainModels;
using HexMaster.Core.Infrastructure.Enums;

namespace HexMaster.Keesz.Connect.DomainModels
{
    public class Credential : DomainModelBase<Guid>
    {
        public string Subject { get; }

        public void Delete()
        {
            SetState(TrackingState.Deleted);
        }


        public Credential(string subject) : base(Guid.NewGuid(), TrackingState.Added)
        {
            Subject = subject;
        }

        public Credential(Guid id, string subject) : base(id)
        {
            Subject = subject;
        }
    }
}
