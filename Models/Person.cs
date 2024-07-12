using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.frontend.Models
{
    public class Person
    {
        public int Id { get; set;}
        [Required]
        [MinLength(13, ErrorMessage = "EMBG must be 13 characters")]
        [MaxLength(13, ErrorMessage = "EMBG must be 13 characters")]
        public string Embg { get; set; } = string.Empty;
        [Required]
        [MinLength(2, ErrorMessage = "Name must be at least 2 characters")]
        [MaxLength(50, ErrorMessage = "Name can't be more than 50 characters")]
        public string Name { get; set; } = string.Empty;
        [Required]
        [MinLength(2, ErrorMessage = "Last name must be at least 2 characters")]
        [MaxLength(50, ErrorMessage = "Last name can't be more than 50 characters")]
        public string LastName { get; set; } = string.Empty;
        [Required]
        [MinLength(3, ErrorMessage = "Address must be at least 3 characters")]
        [MaxLength(50, ErrorMessage = "Address can't be more than 50 characters")]
        public string Address { get; set; } = string.Empty;

        
        public List<Experience> Experiences { get; set; } = new List<Experience>();
    }
}