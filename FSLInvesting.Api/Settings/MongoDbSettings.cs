using FSLInvesting.Api.Settings.Interfaces;

namespace FSLInvesting.Api.Settings;

public class MongoDbSettings : IMongoDbSettings
{
    public string DatabaseName { get; set; }
    public string ConnectionString { get; set; }
}