using BusinessLayer.Interfaces;
using CommonLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryLayer.Interfaces;

namespace BusinessLayer.Services
{
    public class CustomerBusiness:ICustomerBusiness
    {
        private readonly ICustomerRepository _cb;
        public CustomerBusiness(ICustomerRepository _cb) 
        { 
            this._cb = _cb;
        }
        public CustomerEntity Insert(CustomerModel model)
        {
            return _cb.Insert(model);
        }

        public int CustomersCount()
        {
            return _cb.CustomersCount();
        }
    }
}
