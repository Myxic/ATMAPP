using System;
using System.ComponentModel.DataAnnotations;

namespace ATM.DAL.Model
{
    public class Complains
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"^([0-9]{15,16})$", ErrorMessage = "Incorrect CardNo Format")]
        public string CardNo { get; set; } = null!;

        [Required]
        public string Subject { get; set; } = null!;

        [Required]
        public string Reports { get; set; } = null!;
    }
}

