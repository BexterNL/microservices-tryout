namespace HexMaster.Keesz.Connect.Configuration
{
    public class AppSettingsConfiguration
    {
        public string MongoDbConnectionString { get; set; }
        public string IdentityServerUrl { get; set; }
        public EventBusSettings EventBus { get; set; }
    }

    public class EventBusSettings
    {
        public bool AzureServiceBusEnabled { get; set; }
        public string SubscriptionClientName { get; set; }
        public string EventBusConnection { get; set; }
        public string EventBusUserName { get; set; }
        public string EventBusPassword { get; set; }
        public int EventBusRetryCount { get; set; }

    }

}
