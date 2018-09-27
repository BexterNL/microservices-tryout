using System;

namespace HexMaster.Keesz.Connect.DataTransferObjects
{
    public class FriendDto
    {
        public Guid Id { get; set; }
        public Guid FriendUserId { get; set; }
        public string Name { get; set; }
        public DateTimeOffset? RequestExpiresOn { get; set; }
        public bool IsAccepted { get; set; }
    }
}