using CommonLayer.Model;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class CustomerRespository:ICustomerRepository

    {
        private readonly FundooContext _fundooContext;
        public CustomerRespository (FundooContext fundooContext)
        {
            _fundooContext = fundooContext;
        }
        public CustomerEntity Insert(CustomerModel model)
        {
            try
            {


                CustomerEntity cu = new CustomerEntity();
                cu.customer_id = model.id;
                cu.customer_name = model.name;
                cu.customer_phonenumber = model.phone;
                cu.customer_address = model.address;
                cu.customer_email = model.email;




                _fundooContext.Customer1.Add(cu);

                _fundooContext.SaveChanges();
                return cu;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public int CustomersCount()
        {
            var count=_fundooContext.Customer1.Count();
            return count;
        }
    }
}
