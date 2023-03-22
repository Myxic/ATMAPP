using System;
using System.ComponentModel.DataAnnotations;

namespace ATM.BLL.Models
{
    public class CustomerLoginModel
    {
        [Required]
        public string? CardNumber { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}

