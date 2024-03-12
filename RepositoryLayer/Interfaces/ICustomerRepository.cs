using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface ICustomerRepository
    {
        public CustomerEntity Insert(CustomerModel model);
        public int CustomersCount();
    }
}
