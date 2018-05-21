using System;
using System.Collections.Generic;
using Claimini.Api.Data;
using Claimini.Api.Repository;
using Claimini.Shared;

namespace Claimini.Api.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<Customer> customerRepository;

        public CustomerService(IUnitOfWork unitOfWork, IRepository<Customer> customerRepository)
        {
            this.unitOfWork = unitOfWork;
            this.customerRepository = customerRepository;
        }

        public Customer Create(Customer customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }

            customerRepository.Add(customer);
            unitOfWork.Commit();

            return customer;
        }

        public Customer Get(int id)
        {
            if (id < 1)
            {
                throw new ArgumentException("The Customer Id must be positive", nameof(id));
            }

            return this.customerRepository.Get(id);
        }

        public IEnumerable<Customer> GetAll()
        {
            IEnumerable<Customer> customers = this.customerRepository.GetAll();
            return customers;
        }

        public bool Delete(int id)
        {
            bool exists = this.customerRepository.Exists(customer => customer.Id == id);

            if (!exists)
            {
                return false;
            }

            this.customerRepository.Delete(new Customer {Id = id});
            this.unitOfWork.Commit();

            return true;
        }

        public Customer Update(Customer customer)
        {
            this.customerRepository.Update(customer);
            this.unitOfWork.Commit();

            return customer;
        }
    }
}
