using MongoDB.Bson.Serialization.Attributes;

namespace FSLInvesting.Models.Interfaces;

public interface IDocument
{
    [BsonId] public string Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int Version { get; set; }
    public bool Deleted { get; set; }
}