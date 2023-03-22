using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ATM.DAL.Model;
using Microsoft.AspNetCore.Identity;

namespace ATM.DAL.Model
{
    public class Customer : IdentityUser  
    {
        [Key]
        public new int Id;
        [Required]
        [RegularExpression(@"[A-Za-z]{1,}", ErrorMessage = "This Value cant be empty")]
        public string CustomerName { get; set; } = null!;

        [Required]
        [RegularExpression(@"^([0-9]{15,16})$", ErrorMessage = "Incorrect CardNo Format")]
        public string CardNo { get; set; } = null!;

        [Required]
        [RegularExpression(@"^([0-9]{4})$", ErrorMessage = "Incorrect Pin format")]
        public string PinNo { get; set; } = null!;

        [Required]
        [Column(TypeName = "decimal(,2)")]
        public decimal Balance { get; set; }

        [Required]
        [RegularExpression(@"^([0-9]{3})-([0-9]{3})-([0-9]{4})$", ErrorMessage = "Incorrect format (Sample: \"608-301-0103\" )")]
        public string? PhoneNumber { get; set; }

        public IEnumerable<TransationRecords>? TransationHistory { get; set; }

    }
}

