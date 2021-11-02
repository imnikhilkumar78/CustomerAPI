using CustomerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAPI.Services
{
    public class CustomerService
    {
        private readonly CustomerContext _context;
        private readonly ITokenService _tokenService;

        public CustomerService(CustomerContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        public CustomerDTO Register(CustomerDTO customerDTO)
        {
            
            try
            {
                using var hmac = new HMACSHA512();
                var customer = new Customer()
                {
                    CustomerId = customerDTO.CustomerId,
                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(customerDTO.Password)),
                    PasswordSalt = hmac.Key,
                    Name = customerDTO.Name,
                    Address = customerDTO.Address,
                    Phone = customerDTO.Phone,
                    Email_Id = customerDTO.Email_Id,
                    PAN = customerDTO.PAN,
                    Aadhar = customerDTO.Aadhar,
                    DOB = customerDTO.DOB
                };
                
                _context.Customers.Add(customer);
                _context.SaveChanges();
                customerDTO.jwtToken = _tokenService.CreateToken(customerDTO);
                customerDTO.Password = "";
                var creationstatus = new CustomerCreationStatus()
                {
                    CustomerId = customerDTO.CustomerId,
                   AccountCreationStatus = "Customer Created"
                };
                _context.CustomerCreationStatuses.Add(creationstatus);
                _context.SaveChanges();
                

                return customerDTO;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }

        public CustomerDTO Update(CustomerDTO customer)
        {
            try
            {
                using var hmac = new HMACSHA512();
                foreach (var item in _context.Customers)
                {
                    if (item.CustomerId==customer.CustomerId)
                    {

                        item.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(customer.Password));
                        item.PasswordSalt = hmac.Key;
                        item.Name = customer.Name;
                        item.Address = customer.Address;
                        item.Phone = customer.Phone;
                        item.Email_Id = customer.Email_Id;
                        item.PAN = customer.PAN;
                        item.Aadhar = customer.Aadhar;
                        item.DOB = customer.DOB;
                    }
                }
                _context.SaveChanges();
                return customer;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }

        public CustomerDTO Login(CustomerDTO customerDTO)
        {
            try
            {
                var myCustomer = _context.Customers.SingleOrDefault(c => c.CustomerId == customerDTO.CustomerId);
                if(myCustomer!=null)
                {
                    using var hmac = new HMACSHA512(myCustomer.PasswordSalt);
                    var custPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes(customerDTO.Password));

                    for (int i = 0; i < custPassword.Length; i++)
                    {
                        if (custPassword[i] != myCustomer.PasswordHash[i])
                            return null;
                    }
                    customerDTO.jwtToken = _tokenService.CreateToken(customerDTO);
                    customerDTO.Password = "";
                    return customerDTO;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }
        public List<Customer> GetAll()
        {
            List<Customer> customers = _context.Customers.ToList();
            return customers;
        }

        public Customer GetCustomer(string id)
        {
            Customer FoundCustomer = null;
            foreach (var item in _context.Customers)
            {
                if (item.CustomerId.Equals(id))
                {
                    FoundCustomer = item;
                }
            }
            return FoundCustomer;
        }

    }
}
