using MongoDB.Driver;

namespace Claimini.Api.Data
{
    public interface IMongoDbContext
    {
        /// <inheritdoc />
        IMongoCollection<Invoice> Invoices { get; }
    }
}