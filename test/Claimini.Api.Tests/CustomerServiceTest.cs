using System;
using Claimini.Api.Data;
using Claimini.Api.Repository;
using Claimini.Api.Services;
using Claimini.Api.Tests.Builder;
using Claimini.Shared;
using FluentAssertions;
using Moq;
using Xunit;

namespace Claimini.Api.Tests
{
    public class CustomerServiceTest
    {
        private readonly MockRepository mockRepository = new MockRepository(MockBehavior.Loose);
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
                    "because the same customer is returned. Usually with it's ID updated, but that is not in the scope of the test");
        }

        [Fact]
        public void Create_WithNullCustomer_ThrowsArgumentNullException()
        {
            // Arrange
            var customerService = new CustomerService(
                this.unitOfWorkMockBuilder
                    .WithCommitAffectingRows(1)
                    .BuildObject(),
                this.customerRepositoryBuilder
                    .WithAddReturningEntityStateAdded(It.IsAny<Customer>())
                    .BuildObject());

            // Act
            Action act = () => customerService.Create(null);

            // Assert
            act.ShouldThrowExactly<ArgumentNullException>("passing null as a customer is invalid")
                .And.ParamName.Should().Be("customer");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Get_WithIdLessThan1_ShouldThrowArgumentException(int id)
        {
            // Arrange
            var customerService = new CustomerService(
                It.IsAny<IUnitOfWork>(),
                It.IsAny<IRepository<Customer>>()
            );

            // Act
            Action act = () => customerService.Get(id);

            // Assert
            act.ShouldThrowExactly<ArgumentException>("The Customer Id must be positive")
                .And.ParamName.Should().Be("id");
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void Get_WithIdGreaterThan0_ShouldNotThrowArgumentException(int id)
        {
            // Arrange
            var customerService = new CustomerService(
                It.IsAny<IUnitOfWork>(),
                It.IsAny<IRepository<Customer>>()
            );

            // Act
            Action act = () => customerService.Get(id);

            // Assert
            act.ShouldNotThrow<ArgumentException>();
        }
    }
}
