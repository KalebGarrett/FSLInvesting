using FSLInvesting.Api.Repositories.Interfaces;
using FSLInvesting.Models;
using FSLInvesting.Models.Interfaces;
using MongoDB.Driver;

namespace FSLInvesting.Api.Repositories;

public class InquiryRepository<TDocument> : IMongoRepository<TDocument> where TDocument : IDocument
{
    private readonly IMongoCollection<TDocument> _collection;

    public InquiryRepository()
    {
        var database = new MongoClient(Environment.GetEnvironmentVariable("MongoDbConnectionString"))
            .GetDatabase("FSLInvesting");
        _collection = database.GetCollection<TDocument>(GetCollectionName(typeof(TDocument)));
    }

    public IQueryable<TDocument> AsQueryable()
    {
        return _collection.AsQueryable();
    }

    public async Task InsertOneAsync(TDocument document)
    {
        document.Id = Guid.NewGuid().ToString();
        document.CreatedAt = DateTime.UtcNow;
        document.UpdatedAt = DateTime.UtcNow;
        document.Version++;
        await _collection.InsertOneAsync(document);
    }

    public async Task ReplaceOneAsync(string id, TDocument document)
    {
        document.UpdatedAt = DateTime.UtcNow;
        document.Version++;
        await _collection.ReplaceOneAsync(x => x.Id == id, document);
    }

    public async Task DeleteOneAsync(string id)
    {
        await _collection.DeleteOneAsync(x => x.Id == id);
    }

    private string GetCollectionName(Type documentType)
    {
        return ((BsonCollectionAttribute)documentType.GetCustomAttributes(
            typeof(BsonCollectionAttribute),
            true).FirstOrDefault())?.CollectionName;
    }
}