using Claimini.Api.Data;
using Claimini.Api.Repository;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Claimini.Api.Tests.Builder
{
    public class CustomerRepositoryBuilder
    {
        private readonly MockRepository mockRepository;
        private readonly Mock<IRepository<Customer>> mock;

        public CustomerRepositoryBuilder(MockRepository mockRepository)
        {
            this.mockRepository = mockRepository;
            this.mock = this.mockRepository.Create<IRepository<Customer>>();
        }

        public Mock<IRepository<Customer>> Build()
        {
            return this.mock;
        }

        public IRepository<Customer> BuildObject()
        {
            return this.mock.Object;
        }

        public CustomerRepositoryBuilder WithAddReturningEntityStateAdded(Customer customer)
        {
            this.mock.Setup(x => x.Add(It.IsAny<Customer>())).Returns(EntityState.Added);

            return this;
        }
    }
}
