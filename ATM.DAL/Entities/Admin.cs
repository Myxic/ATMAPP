using System;
using System.ComponentModel.DataAnnotations;
using Azure;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ATM.DAL.Model
{
    public class Admin
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"[A-Za-z]{1,}",ErrorMessage = "This Value cant be empty")]
        public string AdminName { get; set; } = null!;

        [Required]
        [RegularExpression(@"^([0-9]{4})$", ErrorMessage = "Incorrect AdminPin format")]
        public string AdminPin { get; set; } = null!;
    }
}

