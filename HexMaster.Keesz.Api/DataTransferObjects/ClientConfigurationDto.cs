using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HexMaster.Keesz.Api.Models
{
    public class ClientConfigurationDto
    {
        [JsonProperty("stsServer")]
        public string StsServer { get; set; }
        [JsonProperty("Redirect_url")]
        public string RedirectUrl { get; set; }
        [JsonProperty("client_id")]
        public string ClientID { get; set; }
        [JsonProperty("response_type")]
        public string ResponseType { get; set; }
        [JsonProperty("scope")]
        public string Scope { get; set; }
        [JsonProperty("post_logout_redirect_uri")]
        public string PostLogoutUrl { get; set; }
        [JsonProperty("silent_renew")]
        public bool EnableSilentRenew { get; set; }
        [JsonProperty("forbidden_route")]
        public string ForbiddenRoute { get; set; }
        [JsonProperty("unauthorized_route")]
        public string UnauthorizedRoute { get; set; }
        [JsonProperty("apiServer")]
        public string ApiGatewayUrl { get; set; }

    }
}
