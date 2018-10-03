using System.Collections.Generic;
using BlazorRedux;
using Claimini.Shared;

namespace Claimini.BlazorClient.ApplicationState
{
    public class Actions
    {
        // Custoemrs
        public class ReceiveCustomersAction : IAction
        {
            public List<Customer> Customers { get; set; }
        }

        public class UpdateCustomerAction : IAction
        {
            public Customer Customer { get; set; }
        }

        public class SelectCustomerAction : IAction
        {
            public int CustomerId;
        }

        // Articles
        public class ReceiveArticlesAction : IAction
        {
            public List<Article> Articles { get; }

            public ReceiveArticlesAction(List<Article> articles)
            {
                Articles = articles;
            }
        }

        public class SelectArticleAction : IAction
        {
            public int ArticleId;
        }

        public class UpdateArticleAction : IAction
        {
            public Article Article { get; set; }
        }

        // Invoices
        public class ReceiveInvoicesAction : IAction
        {
            public List<InvoiceFullDto> Invoices { get; set; }
        }

        public class SelectInvoiceAction : IAction
        {
            public InvoiceFullDto SelectedInvoice { get; set; }

            public string InvoiceId { get; set; }
        }
    }
}
