using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerAPI.Models
{
    public class CustomerDTO
    {
        public string CustomerId { get; set; }
        public string Password { get; set; }
        
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email_Id { get; set; }
        public string PAN { get; set; }
        public string Aadhar { get; set; }
        public DateTime DOB { get; set; }
        public string jwtToken { get; set; }
    }
}
