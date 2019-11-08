using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TimeKeeper.IDP
{
    public static class Config
    {
        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "john",
                    Username = "johndoe",
                    Password = "$ch00l",
                    Claims = new List<Claim>
                    {
                        new Claim("given_name", "John"),
                        new Claim("family_name", "Doe"),
                        new Claim("role", "user"),
                        new Claim("address", "Sarajevo")
                    }
                },
                new TestUser
                {
                    SubjectId = "jane",
                    Username = "janedoe",
                    Password = "$ch00l",
                    Claims = new List<Claim>
                    {
                        new Claim("given_name", "Jane"),
                        new Claim("family_name", "Doe"),
                        new Claim("role", "admin"),
                        new Claim("address", "Mostar")
                    }
                }
            };
        }

        public static IEnumerable<IdentityResource> GetResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Address(),
                new IdentityResource("roles", "Your roles", new List<string> { "role" })
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client> {
                new Client
                {
                    ClientName = "TimeKeeper",
                    ClientId = "tk2019",
                    AllowedGrantTypes = GrantTypes.Hybrid,
                    RedirectUris = { "https://localhost:44350/signin-oidc" },
                    PostLogoutRedirectUris = { "https://localhost:44350/signout-callback-oidc" },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Address,
                        "roles" //"roles" were added manually, that's why it isn't found in the StandardScopes
                    },
                    ClientSecrets = { new Secret("mistral_talents".Sha256()) }
                }
            };
        }
    }
}
