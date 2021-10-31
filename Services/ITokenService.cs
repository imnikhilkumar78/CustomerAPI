using CustomerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerAPI.Services
{
    public interface ITokenService
    {
        public string CreateToken(CustomerDTO customerDTO);
       
    }
}
