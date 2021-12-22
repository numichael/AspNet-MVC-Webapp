using System;
using System.ComponentModel.DataAnnotations;

namespace webapp.Models
{
    public class TodoItem
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime DueDate { get; set; }
    }
}