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
                        new Claim("role", "admina")
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
                        new Claim("role", "admina")
                    }
                }
            };
        }

        public static IEnumerable<IdentityResource> GetResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
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
                    RedirectUris = { "https://locahost:44350/signin-oidc" },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    },                    
                    ClientSecrets = {new Secret("mistral_talents".Sha256())}
                }
            };
        }
    }
}
