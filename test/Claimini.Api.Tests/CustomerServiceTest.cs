using Claimini.Api.Data;
using Claimini.Api.Repository;
using Claimini.Api.Services;
using Claimini.Api.Tests.Builder;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Claimini.Api.Tests
{
    public class CustomerServiceTest
    {
        private readonly MockRepository mockRepository = new MockRepository(MockBehavior.Strict);
        private readonly UnitOfWorkBuilder unitOfWorkMockBuilder;
        private readonly CustomerRepositoryBuilder customerRepositoryBuilder;

        public CustomerServiceTest()
        {
            this.unitOfWorkMockBuilder = new UnitOfWorkBuilder(this.mockRepository);
            this.customerRepositoryBuilder = new CustomerRepositoryBuilder(this.mockRepository);
            //customerRepositoryMock = mockRepository.Create<IRepository<Customer>>();
        }

        [Fact]
        public void Create_WithCustomer_ReturnsSavedCustomerWithId()
        {
            // Arrange
            var customer = new Customer();

            var customerService = new CustomerService(
                this.unitOfWorkMockBuilder
                    .WithCommitAffectingRows(1)
                        .BuildObject(),
                this.customerRepositoryBuilder
                    .WithAddReturningEntityStateAdded(customer)
                        .BuildObject());

            // Act
            Customer returnedCustomer = customerService.Create(customer);

            // Assert
            returnedCustomer.Should()
                .BeOfType<Customer>(
                    "The same customer is returned. Usually with it's ID updated, but that is not in the scope of the test");
        }
    }
}
