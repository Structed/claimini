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

        /// <summary>
        /// Retrieves an Invoice PDF as byte array
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns></returns>
        Task<byte[]> GetInvoicePdf(string invoiceId);

        /// <summary>
        /// Retrieves an Invoice PDF as Base64 encoded string
        /// </summary>
        /// <param name="id">The Invoice ID</param>
        /// <returns>A Base64 encoded string</returns>
        Task<string> GetInvoicePdfBas64(string id);

        Task<InvoiceFullDto> PostInvoice(InvoiceDto invoice);
        
        Task RegisterUser(TokenViewModel viewModel);
        
        Task<string> Login(TokenViewModel viewModel);
    }
}
