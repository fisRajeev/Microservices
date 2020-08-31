using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;

namespace SecuringAngularApps.STS
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                //new ApiResource("projects-api", "Projects API")
                 new ApiResource {
                    Name = "books-api",
                    DisplayName = "Books API",
                    Description = "Book API Access",

                    UserClaims = new List<string> {"role"},
                    ApiSecrets = new List<Secret> {new Secret("mySecret".Sha256())},
                    Scopes = new List<Scope> {
                        new Scope("books-api")
                    }
                }
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "spa-client",
                    ClientName = "Projects SPA",
                    AccessTokenType = AccessTokenType.Jwt,
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = false,

                    RedirectUris =           { "http://localhost:4200/assets/oidc-login-redirect.html","http://localhost:4200/assets/silent-redirect.html" },
                    PostLogoutRedirectUris = { "http://localhost:4200/?postLogout=true" },
                    AllowedCorsOrigins =     { "http://localhost:4200/" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "books-api"
                    },
                    IdentityTokenLifetime=120,
                    AccessTokenLifetime=120

                },
                new Client
                {
                    ClientId = "mvc",
                    ClientName = "MVC Client",
                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    RedirectUris           = { "https://localhost:4200/signin-oidc" },
                    PostLogoutRedirectUris = { "https://localhost:4200/signout-callback-oidc" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    },
                    AllowOfflineAccess = true

                }
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
            {
                return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
            }
        }
    }