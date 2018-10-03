using System.Collections.Generic;
using System.Threading.Tasks;
using Claimini.Shared;

namespace Claimini.BlazorClient
{
    public interface IApiClient
    {
        // Customers
        Task<List<Customer>> GetCustomers();

        Task<Customer> GetCustomer(int id);

        Task<Customer> PostCustomer(Customer customer);

        Task<Customer> PutCustomer(Customer customer);

        
        // Articles
        Task<List<Article>> GetArticles();
        
        Task<Article> GetArticle(int id);

        Task<Article> PutArticle(Article article);

        Task<Article> PostArticle(Article article);

        
        // Invoices
        Task<List<InvoiceFullDto>> GetInvoices();

        Task<InvoiceFullDto> GetInvoice(string invoiceId);

        Task<InvoiceFullDto> PostInvoice(InvoiceDto invoice);
    }
}
