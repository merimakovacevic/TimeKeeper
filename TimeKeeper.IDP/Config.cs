﻿using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TimeKeeper.DAL;

namespace TimeKeeper.IDP
{
    public static class Config
    {
        public static List<TestUser> GetUsers()
        {
            List<TestUser> users = new List<TestUser>();
            using (UnitOfWork unit = new UnitOfWork(new TimeKeeperContext()))
            {
                foreach (var user in unit.Users.Get())
                {
                    users.Add(new TestUser
                    {
                        SubjectId = user.Id.ToString(),
                        Username = user.Username,
                        Password = user.Password,
                        Claims = new List<Claim>
                    {
                        new Claim("given_name", user.Name),
                        new Claim("family_name", user.Name),
                        new Claim("role", user.Role)
                    }
                    });
                }
            }
            return users;
        }

        public static IEnumerable<IdentityResource> GetResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Address(),
                new IdentityResource("roles", "Your roles", new List<string> { "role" }),
                new IdentityResource("teams", "Your engagement(s)", new List<string> { "team" })
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("timekeeper", "Time Keeper API", new List<string> { "role" })
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
                        "roles", //"roles" were added manually, that's why it isn't found in the StandardScopes,
                        "timekeeper",
                        "teams"
                    },
                    ClientSecrets = { new Secret("mistral_talents".Sha256()) }
                }
            };
        }
    }
}
