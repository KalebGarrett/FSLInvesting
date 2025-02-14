using System.Linq.Expressions;
using FSLInvesting.Models.Interfaces;

namespace FSLInvesting.Api.Repositories.Interfaces;

public interface IMongoRepository<TDocument> where TDocument : IDocument
{
    IQueryable<TDocument> AsQueryable();
    Task InsertOneAsync(TDocument document);
    Task ReplaceOneAsync(string id, TDocument document);
    Task DeleteOneAsync(string id);
}