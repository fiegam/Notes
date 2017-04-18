using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace Notes.IdentityServer
{
    public class Config
    {
        // scopes define the API resources in your system
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("notes.api", "Notes API")
            };
        }

        // client want to access resources (aka scopes)
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                //new Client
                //{
                //    ClientId = "Notes.Web",
                //    ClientName = "Notes Web Application",
                //    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,
                //      RequireConsent = false,
                //    ClientSecrets =
                //    {
                //        new Secret("19e94cce-52d0-4240-b9b5-55afc8c38857".Sha256())
                //    },
                //    RedirectUris           = { "http://localhost:5002/signin-oidc" },
                //    PostLogoutRedirectUris = { "http://localhost:5002" },
                //    AllowedScopes = {
                //        IdentityServerConstants.StandardScopes.OpenId,
                //        IdentityServerConstants.StandardScopes.Profile,
                //        "api1" },
                //    AllowOfflineAccess = true
                //},
                new Client
{
    ClientId = "notes.web",
    ClientName = "Notes Web Application",
    AllowedGrantTypes = GrantTypes.ImplicitAndClientCredentials,
    AllowAccessTokensViaBrowser = true,
                      RequireConsent = false,

    RedirectUris =           { "http://localhost:5003/callback.html", "http://localhost:61679/home" },
    PostLogoutRedirectUris = { "http://localhost:5003/index.html", "http://localhost:61679/home"  },
    AllowedCorsOrigins =     { "http://localhost:5003", "http://localhost:61679" },

    AllowedScopes =
    {
        IdentityServerConstants.StandardScopes.OpenId,
        IdentityServerConstants.StandardScopes.Profile,
        "notes.api"
    }
}
            };
        }

        internal static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]{
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }
    }
}