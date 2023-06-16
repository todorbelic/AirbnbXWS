namespace NotificationService.Settings
{
    public class MongoDbSettings : IMongoDbSettings
    {
        public string DatabaseName { get ; set ; }
        public string ConnectionString { get ; set ; }
    }
}
