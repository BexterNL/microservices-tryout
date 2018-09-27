using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HexMaster.Keesz.Api.Configuration;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace HexMaster.Keesz.Api
{
    public class Startup
    {


        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            var settingsSection = Configuration.GetSection("ApplicationSettings");
            var appSettings = settingsSection.Get<AppSettingsConfiguration>();
            services.Configure<AppSettingsConfiguration>(settingsSection);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = appSettings.IdentityServerUrl;
                    options.Audience = "angularclient";
                    options.RequireHttpsMetadata = false;
                });
            //var defaultAuthName = "BearerAuthentication";
            //services.AddAuthentication("Bearer")
            //    .AddIdentityServerAuthentication(defaultAuthName, options =>
            //   {
            //       options.Authority = appSettings.IdentityServerUrl;
            //       options.ApiName = "api-gateway";
            //       options.ApiSecret = "f8a852cd-12d3-4696-94f5-6c765c19bd98";
            //       options.SupportedTokens = SupportedTokens.Jwt;
            //       options.RequireHttpsMetadata = false;
            //   });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });
            services.AddOcelot();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
               // app.UseHsts();
            }

            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            //app.UseHttpsRedirection();
            app.UseMvc();
            await app.UseOcelot();
        }
    }
}
