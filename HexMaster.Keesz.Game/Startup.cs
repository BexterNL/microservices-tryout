using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using HexMaster.Core.Contracts;
using HexMaster.Core.Services;
using HexMaster.Keesz.Game.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.ServiceBus;
using HexMaster.Keesz.BuildingBlocks.EventBus;
using HexMaster.Keesz.BuildingBlocks.EventBus.Abstractions;
using HexMaster.Keesz.BuildingBlocks.EventBusRabbitMQ;
using HexMaster.Keesz.BuildingBlocks.EventBusServiceBus;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using RabbitMQ.Client;

namespace HexMaster.Keesz.Game
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            IdentityModelEventSource.ShowPII = true;
            var settingsSection = Configuration.GetSection("ApplicationSettings");
            var appSettings = settingsSection.Get<AppSettingsConfiguration>();
            services.Configure<AppSettingsConfiguration>(settingsSection);

            services.AddMvcCore()
                .AddAuthorization()
                .AddJsonFormatters();


            services.AddSingleton<IRandomizer, RandomizerService>();
            services.AddSingleton(appSettings);

            services.AddAuthentication("Bearer");
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = appSettings.IdentityServerUrl;
                    options.Audience = "angularclient";
                    options.RequireHttpsMetadata = false;
                });
            //.AddIdentityServerAuthentication(options =>
            //{
            //    options.Authority = appSettings.IdentityServerUrl;
            //    options.RequireHttpsMetadata = false;
            //    options.ApiName = "api-game";
            //    options.ApiSecret = "9a7d8006-d4ff-4a9a-90c4-eb7481297db9";
            //    options.SupportedTokens = SupportedTokens.Jwt;
            //});

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });
            ConfigureEventBus(services, appSettings.EventBus);
            RegisterEventBus(services, appSettings.EventBus);

            var container = new ContainerBuilder();
            container.Populate(services);
            return new AutofacServiceProvider(container.Build());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
//                app.UseHsts();
            }

            ConfigureEventBus(app);

            app.UseAuthentication();
            //app.UseHttpsRedirection();
            app.UseCors("CorsPolicy");
            app.UseMvc();
        }

        private void ConfigureEventBus(IServiceCollection services, EventBusSettings settings)
        {
            if (settings.AzureServiceBusEnabled)
            {
                services.AddSingleton<IServiceBusPersisterConnection>(sp =>
                {
                    var logger = sp.GetRequiredService<ILogger<DefaultServiceBusPersisterConnection>>();
                    var serviceBusConnection = new ServiceBusConnectionStringBuilder(settings.EventBusConnection);
                    return new DefaultServiceBusPersisterConnection(serviceBusConnection, logger);
                });
            }
            else
            {
                services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
                {
                    var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();

                    var factory = new ConnectionFactory()
                    {
                        HostName = settings.EventBusConnection
                    };
                    factory.UserName = settings.EventBusUserName;
                    factory.Password = settings.EventBusPassword;
                    var retryCount = settings.EventBusRetryCount;
                    return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);
                });
            }
        }

        private void RegisterEventBus(IServiceCollection services, EventBusSettings settings)
        {
            var subscriptionClientName = settings.SubscriptionClientName;
            if (settings.AzureServiceBusEnabled)
            {
                services.AddSingleton<IEventBus, EventBusServiceBus>(sp =>
                {
                    var serviceBusPersisterConnection = sp.GetRequiredService<IServiceBusPersisterConnection>();
                    var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
                    var logger = sp.GetRequiredService<ILogger<EventBusServiceBus>>();
                    var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                    return new EventBusServiceBus(serviceBusPersisterConnection, logger,
                        eventBusSubcriptionsManager, subscriptionClientName, iLifetimeScope);
                });

            }
            else
            {
                services.AddSingleton<IEventBus, EventBusRabbitMQ>(sp =>
                {
                    var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
                    var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
                    var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ>>();
                    var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();
                    var retryCount = settings.EventBusRetryCount;
                    return new EventBusRabbitMQ(rabbitMQPersistentConnection, logger, iLifetimeScope,
                        eventBusSubcriptionsManager, subscriptionClientName, retryCount);
                });
            }

            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
        }

        protected virtual void ConfigureEventBus(IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
        }

    }
}
