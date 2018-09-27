// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System.Collections.Generic;
using System.Security.Claims;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace HexMaster.Keesz.Identity
{
    public class Config
    {
        // scopes define the resources in your system
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            var gatewayApi = new ApiResource("angularclient", "Gateway API - Connecting the microservices together");
            gatewayApi.ApiSecrets.Add(new Secret("f8a852cd-12d3-4696-94f5-6c765c19bd98"));
            gatewayApi.Scopes.Add(new Scope("api"));
            
            var connectApi = new ApiResource("api-connect", "Connect API - To create a network of friends");
            gatewayApi.ApiSecrets.Add(new Secret("bfa787ac-f672-4b82-8051-2a0f2abfaf2d"));
            gatewayApi.Scopes.Add(new Scope("connect"));

            var gameApi = new ApiResource("api-game", "Game API - To maintain game states");
            gatewayApi.ApiSecrets.Add(new Secret("9a7d8006-d4ff-4a9a-90c4-eb7481297db9"));
            gatewayApi.Scopes.Add(new Scope("game"));

            return new List<ApiResource>
            {
                gatewayApi,
                connectApi,
                gameApi
            };
        }

        // clients want to access resources (aka scopes)
        public static IEnumerable<Client> GetClients()
        {
            // client credentials client
            return new List<Client>
            {
                new Client
                {
                    ClientId = "angularclient",
                    ClientName = "Angular SPA Client application",
                    AccessTokenType = AccessTokenType.Jwt,
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RedirectUris = new List<string>
                    {
                        "https://www.keesz.int:53700/callback",
                        "http://www.keesz.int:53600/callback",
                        "https://localhost:53700/callback",
                        "http://localhost:53600/callback",
                        "http://localhost:4200/callback"
                     },
                    PostLogoutRedirectUris = new List<string>
                    {
                        "https://www.keesz.int:53700/",
                        "http://www.keesz.int:53600/",
                        "https://localhost:53700/",
                        "http://localhost:53600/",
                        "http://localhost:4200/"
                    },
                    AllowedCorsOrigins = new List<string>
                    {
                        "https://www.keesz.int:53700/",
                        "http://www.keesz.int:53600/",
                        "https://localhost:53700",
                        "https://localhost:53600/",
                        "http://localhost:4200/"
                    },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "connect",
                        "game",
                        "api",
                        "angularclient"
                    },
                    
                }
                //, new Client
                //{
                //    ClientId = "front-end-spa",
                //    ClientName = "Front-end SPA",
                //    AllowedGrantTypes = GrantTypes.Implicit,
                //    RedirectUris = { "https://localhost:53700/dashboard" },
                //    PostLogoutRedirectUris = { "https://localhost:53700/" },

                //    AllowedScopes = 
                //    {
                //        IdentityServerConstants.StandardScopes.OpenId,
                //        IdentityServerConstants.StandardScopes.Profile,
                //        "connect",
                //        "game",
                //        "api"
                //    },
                //    AllowOfflineAccess = true
                //}

            };
        }

        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "alice",
                    Password = "password",

                    Claims = new List<Claim>
                    {
                        new Claim("name", "Alice"),
                        new Claim("website", "https://alice.com")
                    }
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "bob",
                    Password = "password",

                    Claims = new List<Claim>
                    {
                        new Claim("name", "Bob"),
                        new Claim("website", "https://bob.com")
                    }
                }
            };
        }
    }
}