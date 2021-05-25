using System.ComponentModel.DataAnnotations;
using System;

namespace OEMS.Data.Models
{
    public class Employee : BaseEntity
    {          
		[StringLength(255)]
        [Required]
        public string FirstName { get; set; }
        [StringLength(255)]
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        
    }
}
