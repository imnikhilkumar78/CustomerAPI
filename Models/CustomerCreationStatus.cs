using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerAPI.Models
{
    public class CustomerCreationStatus
    {
        [Key]
        [ForeignKey("Customer")]
        public string CustomerId { get; set; }
        public string AccountCreationStatus { get; set; }
    }
}
