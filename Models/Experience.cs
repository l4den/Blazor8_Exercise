using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CRUD.frontend.Helpers;

namespace CRUD.frontend.Models
{
    public class Experience
    {
        public int Id { get; set; }
        [Required]
        [MinLength(2, ErrorMessage = "Company name must be at least 2 characters")]
        [MaxLength(50, ErrorMessage = "Company name can't be more than 50 characters")]
        public string CompanyName { get; set; } = string.Empty;
        [Required]
        [MinLength(2, ErrorMessage = "Position must be at least 2 characters")]
        [MaxLength(50, ErrorMessage = "Position can't be more than 50 characters")]
        public string Position { get; set; } = string.Empty;
        [Required]
        [FutureDateValidator(ErrorMessage = "Start date cannot be in the future")]
        public DateTime StartDate { get; set; }
        
        [FutureDateValidator(ErrorMessage = "End date cannot be in the future")]
        public DateTime EndDate { get; set; }
        // FK
        public int? PersonId { get; set; }
        // Navigation Property
        public Person? Person { get; set; }
    }
}