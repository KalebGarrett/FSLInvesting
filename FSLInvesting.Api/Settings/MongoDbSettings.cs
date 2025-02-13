using FSLInvesting.Api.Settings.Interfaces;
using MongoDB.Driver;

namespace FSLInvesting.Api.Settings;

public class MongoDbSettings : IMongoDbSettings 
{
    public string DatabaseName { get; set; }
    public string ConnectionString { get; set; }
}