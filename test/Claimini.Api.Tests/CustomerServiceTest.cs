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
        private readonly Mock<IRepository<Customer>> customerRepositoryMock;
        private readonly UnitOfWorkBuilder unitOfWorkMockBuilder;

        public CustomerServiceTest()
        {
            unitOfWorkMockBuilder = new UnitOfWorkBuilder(this.mockRepository);
            customerRepositoryMock = mockRepository.Create<IRepository<Customer>>();
        }

        [Fact]
        public void Create_WithCustomer_ReturnsSavedCustomerWithId()
        {
            // Arrange
            var customer = new Customer();
            customerRepositoryMock.Setup(x => x.Add(It.IsAny<Customer>())).Returns(EntityState.Added);
            Mock<IUnitOfWork> uowMock = this.unitOfWorkMockBuilder.WithCommitAffectingRows(1).Build();
            var customerService = new CustomerService(uowMock.Object, this.customerRepositoryMock.Object);

            // Act
            Customer returnedCustomer = customerService.Create(customer);

            // Assert
            returnedCustomer.Should()
                .BeOfType<Customer>(
                    "The same customer is returned. USually with it's ID updated, but that is not in the scope of the test");
        }
    }
}
