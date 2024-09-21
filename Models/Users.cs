using System;
using System.ComponentModel.DataAnnotations;

namespace LeHuuTam.Models
{
    public class Users
    {
        public int Id { get; set; } 

        [Required]
        [StringLength(100)]
        public string? Name { get; set; } 

        [Required]
        [EmailAddress]
        [StringLength(255)]
        public string? Email { get; set; } 

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now; 
    }
}
