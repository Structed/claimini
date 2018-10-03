using MongoDB.Bson;

namespace Claimini.Shared
{
    public interface IMongoDocument
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        ObjectId Id { get; set; }
    }
}
