using CustomerAPI.Models;
using CustomerAPI.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerAPI.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("MyPolicy")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _service;

        public CustomerController(CustomerService service)
        {
            _service = service;
        }
        [HttpGet]
        public IEnumerable<Customer> Get()
        {
            if(_service.GetAll()!=null)
            {
                return _service.GetAll();
            }
            return null;
        }
        [HttpGet("{id}")]

        public Customer Get(string id)
        {
            if(_service.GetCustomer(id)!=null)
            {
                return _service.GetCustomer(id);
            }
            return null;
        }
        [HttpPost]
        public async Task<ActionResult<CustomerDTO>> Post([FromBody] CustomerDTO customer)
        {
            var CustomerDTO = _service.Register(customer);
            if (CustomerDTO != null)
                return CustomerDTO;
            return BadRequest("Not able to Register");
        }

        [Route("Login")]
        [HttpPost]

        public async Task<ActionResult<CustomerDTO>> Put([FromBody] CustomerDTO customer)
        {
            var CustomerDTO = _service.Login(customer);
            if (CustomerDTO != null)
                return Ok(CustomerDTO);
            return BadRequest("Not Working");

        }


    }
}
