using System;

namespace HexMaster.Keesz.Connect.DataTransferObjects
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsVerified { get; set; }
        public DateTimeOffset FirstLogin { get; set; }
        public DateTimeOffset LastLogin { get; set; }
    }
}
