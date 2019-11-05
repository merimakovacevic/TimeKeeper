﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddMvc();

            //Enables anonymous access to our application (IIS security is not used) o. AutomaticAuthentication = false
            services.Configure<IISOptions>(o =>
            {
                o.AutomaticAuthentication = false;
            });

            /*services.AddAuthentication("BasicAuthentication")
                    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);*/

                 
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
            
            services.AddAuthentication(o =>
            {
                o.DefaultScheme = "Cookies";
                o.DefaultChallengeScheme = "oidc";
            }).AddCookie("Cookies")
              .AddOpenIdConnect("oidc", o => {
                  o.SignInScheme = "Cookies";
                  o.Authority = "https://localhost:44300";
                  o.ClientId = "tk2019";
                  o.ClientSecret = "mistral_talents";
                  o.ResponseType = "code id_token";
                  o.Scope.Add("openid"); //request users identity
                  o.Scope.Add("profile"); //request users profile
                  o.SaveTokens = true; //tokens will be saved in cookies in browser
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
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
