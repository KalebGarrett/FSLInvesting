using MongoDB.Bson.Serialization.Attributes;

namespace FSLInvesting.Models.Documents.Interfaces;

public interface IDocument
{
    [BsonId] public string Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int Version { get; set; }
}