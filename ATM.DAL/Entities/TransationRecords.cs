using System;
using System.ComponentModel.DataAnnotations;

namespace ATM.DAL.Model
{
    public class TransationRecords
    {
        [Key]
        public int Id { get; set; }

        //public int CustomerID;
        [Required]
        public Customer Customer { get; set; } = null!;
        [Required]
        public string Date { get; set; } = DateTime.Now.ToLongDateString();
        [Required]
        public string Time { get; set; } = DateTime.Now.ToShortTimeString();

        [Required]
        public string Reports { get; set; } = null!;
    }
}

