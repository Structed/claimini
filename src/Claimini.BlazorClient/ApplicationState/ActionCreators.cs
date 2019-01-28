using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorRedux;
using Claimini.Shared;

namespace Claimini.BlazorClient.ApplicationState
{
    public class ActionCreators
    {
        public static async Task GetbearerToken(Dispatcher<IAction> dispatch, IApiClient apiClient, TokenViewModel tokenViewModel)
        {
            string bearerToken = await apiClient.Login(tokenViewModel);
            dispatch(new Actions.ReceiveBearerTokenAction()
            {
                BearerToken = bearerToken
            });
        }
        
        // Customers
        public static async Task LoadCustomers(Dispatcher<IAction> dispatch, IApiClient apiClient)
        {
            List<Customer> customers = await apiClient.GetCustomers();
            dispatch(new Actions.ReceiveCustomersAction()
            {
                Customers = customers
            });
        }

        public static async Task UpdateCustomer(Dispatcher<IAction> dispatch, IApiClient apiClient, Customer customer)
        {
            customer = await apiClient.PutCustomer(customer);
            dispatch(new Actions.UpdateCustomerAction()
            {
                Customer = customer
            });
        }

        public static void SelectCustomer(Dispatcher<IAction> dispatch, int customerId)
        {
            dispatch(new Actions.SelectCustomerAction()
            {
                CustomerId = customerId
            });
        }
        
        // Articles
        public static async Task LoadArticles(Action<IAction> dispatch, IApiClient apiClient)
        {
            List<Article> articles = await apiClient.GetArticles();
            dispatch(new Actions.ReceiveArticlesAction(articles));
        }

        public static void SelectArticle(Dispatcher<IAction> dispatch, int articleId)
        {
            dispatch(new Actions.SelectArticleAction()
            {
                ArticleId = articleId
            });
        }

        public static async Task UpdateArticle(Dispatcher<IAction> dispatch, IApiClient apiClient, Article article)
        {
            article = await apiClient.PutArticle(article);
            dispatch(new Actions.UpdateArticleAction()
            {
                Article = article
            });
        }

        // Invoices
        public static async Task LoadInvoices(Action<IAction> dispatch, IApiClient apiClient)
        {
            List <InvoiceFullDto> invoices = await apiClient.GetInvoices();
            dispatch(new Actions.ReceiveInvoicesAction()
            {
                Invoices = invoices
            });
        }

        public static async Task SelectInvoice(Action<IAction> dispatch, IApiClient apiClient, string invoiceId)
        {
            var invoice = await apiClient.GetInvoice(invoiceId);
            dispatch(new Actions.SelectInvoiceAction()
            {
                SelectedInvoice = invoice,
                InvoiceId = invoiceId
            });
        }
    }
}
