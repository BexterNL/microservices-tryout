using System;
using HexMaster.Core.DomainModels;
using HexMaster.Core.Infrastructure.Enums;

namespace HexMaster.Keesz.Connect.DomainModels
{
    public class Friend : DomainModelBase<Guid>
    {
        public Guid UserGuid { get; private set; }
        public Guid FriendUserId { get; private set; }
        public string Name { get; private set; }
        public bool IsAccepted { get; private set; }
        public DateTimeOffset? ExpiresOn { get; private set; }

        public void SetName(string value)
        {
            SetState(TrackingState.Touched);
            if (!Equals(Name, value))
            {
                Name = value;
                SetState(TrackingState.Modified);
            }
        }
        public void Accept()
        {
            SetState(TrackingState.Touched);
            if (!IsAccepted)
            {
                IsAccepted = true;
                ExpiresOn = null;
                SetState(TrackingState.Modified);
            }
        }
        public void Decline()
        {
            ExpiresOn = DateTime.UtcNow;
            SetState(TrackingState.Modified);
        }

        #region [ Constructors && Factory ]

        public Friend(Guid id, Guid userId, Guid friendUserId, string name, bool accepted, DateTimeOffset? expiresOn) : base(id)
        {
            UserGuid = userId;
            FriendUserId = friendUserId;
            Name = name;
            IsAccepted = accepted;
            ExpiresOn = expiresOn;
        }
        private Friend() : base(Guid.NewGuid(), TrackingState.Added)
        {
            ExpiresOn = DateTimeOffset.UtcNow.AddDays(14);
        }
        public static Friend Create(Guid userGuid, Guid friendUserGuid)
        {
            var friend = new Friend {UserGuid = userGuid, FriendUserId = friendUserGuid};
            return friend;
        }

        #endregion

    }
}
