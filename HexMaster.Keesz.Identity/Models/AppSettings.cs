using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HexMaster.Keesz.Identity.Models
{
    public class AppSettings
    {
        public IdentityProviders IdentityProviders { get; set; }
    }

    public class IdentityProviders
    {
        public GoogleIdentityProvider Google { get; set; }
    }

    public class GoogleIdentityProvider
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}
