using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Message
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime Date { get; set; }

        [Required]
        public string User { get; set; }
        [Required]
        public string Room { get; set; }
    }
}
