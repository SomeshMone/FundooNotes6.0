using BusinessLayer.Interfaces;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Services;

namespace FundooNoteApplication6._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        public ICustomerBusiness _cb;
        public CustomerController(ICustomerBusiness _cb)
        {
            this._cb = _cb;

        }

        [HttpPost("Insert")]

        public IActionResult Insert(CustomerModel model)
        {
            var res=_cb.Insert(model);
            if(res!=null)
            {
                return Ok(res);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("NoOfCustomers")]
        public IActionResult Count() 
        {
            var res=_cb.CustomersCount();
            if (res != null)
            {
                return Ok(new {success=true,message="No of Customers",data=res});
            }
            else
            {
                return BadRequest("Customers not exist");
            }
        }

    }
}
