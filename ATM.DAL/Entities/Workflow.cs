using System;
using System.ComponentModel.DataAnnotations;

namespace ATM.DAL.Model
{
    public class Workflow
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Time { get; set; } = DateTime.Now.ToShortTimeString();

        public string Date { get; set; } = DateTime.Now.ToLongDateString();

        public string? WorkTimeLine { get; set; }

    }
}

