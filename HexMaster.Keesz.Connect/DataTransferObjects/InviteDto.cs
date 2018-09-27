using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HexMaster.Keesz.Connect.DataTransferObjects
{
    public class InviteDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset? ExpiresOn { get; set; }
    }
}
