// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System;
using System.IO;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using HexMaster.Keesz.Identity.Models;
using IdentityServer4;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace HexMaster.Keesz.Identity
{
    public class Startup
    {

        public IHostingEnvironment HostingEnvironment { get; private set; }
        public IConfiguration Configuration { get; private set; }

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            this.HostingEnvironment = env;
            this.Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowMyOrigin",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                );
            });

            services.AddMvc();

            var settingsSection = Configuration.GetSection("ApplicationSettings");
            var appSettings = settingsSection.Get<AppSettings>();

            // Inject AppIdentitySettings so that others can use too
            services.Configure<AppSettings>(settingsSection);

            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            // configure identity server with in-memory stores, keys, clients and scopes
            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddTestUsers(Config.GetUsers())
                // this adds the config data from DB (clients, resources)
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                        builder.UseSqlServer(connectionString,
                            sql => sql.MigrationsAssembly(migrationsAssembly));
                })
                // this adds the operational data from DB (codes, tokens, consents)
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                        builder.UseSqlServer(connectionString,
                            sql => sql.MigrationsAssembly(migrationsAssembly));

                    // this enables automatic token cleanup. this is optional.
                    options.EnableTokenCleanup = true;
                    options.TokenCleanupInterval = 30;
                });

            services.AddAuthentication()
                .AddGoogle("Google", options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                    Console.WriteLine(appSettings.IdentityProviders.Google.ClientId);
                    options.ClientId = appSettings.IdentityProviders.Google.ClientId;
                    options.ClientSecret = appSettings.IdentityProviders.Google.ClientSecret;
                })
                .AddOpenIdConnect("oidc", "OpenID Connect", options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                    options.SignOutScheme = IdentityServerConstants.SignoutScheme;

                    options.Authority = "https://demo.identityserver.io/";
                    options.ClientId = "implicit";

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        NameClaimType = "name",
                        RoleClaimType = "role"
                    };
                });

            SeedData.EnsureSeedData(services.BuildServiceProvider());
        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("AllowMyOrigin");

            app.UseIdentityServer();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}