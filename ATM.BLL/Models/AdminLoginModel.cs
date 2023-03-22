using System;
using System.ComponentModel.DataAnnotations;

namespace ATM.BLL.Models
{
    public class AdminLoginModel
    {
        [Required]
        public string? CompanyName { get; set; }

        [Required]
        public string? Password { get; set; }

    }
}

