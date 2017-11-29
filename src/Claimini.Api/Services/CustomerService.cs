using System;
using System.Collections;
using System.Collections.Generic;
using Claimini.Api.Data;
using Claimini.Api.Repository;
using Microsoft.EntityFrameworkCore;

namespace Claimini.Api.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<Customer> contactRepository;

        public CustomerService(IUnitOfWork unitOfWork, IRepository<Customer> contactRepository)
        {
            this.unitOfWork = unitOfWork;
            this.contactRepository = contactRepository;
        }

        public Customer Create(Customer customer)
        {
            contactRepository.Add(customer);
            unitOfWork.Commit();

            return customer;
        }

        public Customer Get(int id)
        {
            return this.contactRepository.Get(id);
        }

        public IEnumerable<Customer> GetAll()
        {
            IEnumerable<Customer> customers = this.contactRepository.GetAll();
            return customers;
        }

        public bool Delete(int id)
        {
            bool exists = this.contactRepository.Exists(customer => customer.Id == id);

            if (!exists)
            {
                return false;
            }

            this.contactRepository.Delete(new Customer {Id = id});
            this.unitOfWork.Commit();

            return true;
        }

        public Customer Update(Customer customer)
        {
            this.contactRepository.Update(customer);
            this.unitOfWork.Commit();

            return customer;
        }
    }
}
