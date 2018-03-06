using MongoDB.Bson;

namespace Claimini.Api.Data
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