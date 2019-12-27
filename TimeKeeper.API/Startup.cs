using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using TimeKeeper.API.Authorization;
using TimeKeeper.API.Services;
using TimeKeeper.DAL;

namespace TimeKeeper.API
{
    public class Startup
    {
        public static IConfigurationRoot Configuration;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder().SetBasePath(env.ContentRootPath)
                                                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddMvc();

            services.AddAuthorization(o =>
            {
                o.AddPolicy("IsAdmin", builder =>
                {
                    builder.AddRequirements(new IsRoleRequirement());
                });
                o.AddPolicy("AdminOrLeader", builder =>
                {
                    builder.AddRequirements(new AdminOrLeadRequirement())
;
                });
                o.AddPolicy("AdminLeadOrMember", builder =>
                {
                    builder.AddRequirements(new AdminLeadOrMemberRequirement());
                });
                o.AddPolicy("AdminLeadOrOwner", builder =>
                {
                    builder.AddRequirements(new AdminLeadOrOwnerRequirement());
                });
            });
            services.AddScoped<IAuthorizationHandler, RequireAdminRole>();
            services.AddScoped<IAuthorizationHandler, AdminOrLeadHandler>();
            services.AddScoped<IAuthorizationHandler, AdminLeadOrMemberHandler>();
            services.AddScoped<IAuthorizationHandler, AdminLeadOrOwnerHandler>();
            //Enables anonymous access to our application (IIS security is not used) o. AutomaticAuthentication = false
            services.Configure<IISOptions>(o =>
            {
                o.AutomaticAuthentication = false;
            });

            services.AddAuthentication("TokenAuthentication")
                    .AddScheme<AuthenticationSchemeOptions, TokenAuthenticationHandler>("TokenAuthentication", null);

            //services.AddAuthentication(o =>
            //{
            //    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(o =>
            //{
            //    o.Authority = "https://localhost:44300/";
            //    o.Audience = "timekeeper";
            //    o.RequireHttpsMetadata = false;
            //});

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            //AddCookie("Cookies", o =>
            //{
            //    o.AccessDeniedPath = "/AccessDenied";
            //})
            //.AddOpenIdConnect("oidc", o => //will be called in case the client is not authenticated
            //{
            //    o.SignInScheme = "Cookies";
            //    o.Authority = "https://localhost:44300"; //identity guarantor
            //    o.ClientId = "tk2019"; //client application in Config in IDP
            //    o.ClientSecret = "mistral_talents"; //key used to sign tokens, usually given by the identity provider
            //    o.ResponseType = "code id_token";
            //    o.Scope.Add("openid"); //identity of the user that signed in
            //    o.Scope.Add("profile"); //profile of the user (given name, last name)
            //    o.Scope.Add("address"); //address of the user
            //    o.Scope.Add("roles");
            //    o.Scope.Add("timekeeper");
            //    o.Scope.Add("teams");
            //    o.SaveTokens = true; //save the tokens in cookies
            //    o.GetClaimsFromUserInfoEndpoint = true; //get all claims defined for users, by default it's false
            //    o.ClaimActions.MapUniqueJsonKey("address", "address"); //address isn't mapped by default, unlike profile and id
            //    o.ClaimActions.MapUniqueJsonKey("role", "role");
            //    o.ClaimActions.MapUniqueJsonKey("team", "team");
            //    o.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        NameClaimType = JwtClaimTypes.GivenName,
            //        RoleClaimType = JwtClaimTypes.Role               
            //    };
            //});

            string connectionString = Configuration["ConnectionString"];          
            services.AddDbContext<TimeKeeperContext>(o => { o.UseNpgsql(connectionString); });

            //Is this config neccessary?
            services.AddSwaggerDocument(config => 
            {
                config.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "ToDo API";
                    document.Info.Description = "A simple ASP.NET Core web API";
                    document.Info.TermsOfService = "None";
                    document.Info.Contact = new NSwag.OpenApiContact
                    {
                        Name = "Shayne Boyer",
                        Email = string.Empty,
                        Url = "https://twitter.com/spboyer"
                    };
                    document.Info.License = new NSwag.OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = "https://example.com/license"
                    };
                };
            });
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseCors(c => c.AllowAnyOrigin()
                              .AllowAnyMethod()
                              .AllowAnyHeader()
                              .AllowCredentials());
            //app.UseMvcWithDefaultRoute();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
