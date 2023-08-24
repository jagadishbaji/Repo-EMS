using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.Models
{
    public class Employee
    {
        public string? EmployeeId { get; set; }

        [Required]
        [RegularExpression(@"^[^0-9]+$",ErrorMessage =("numbers not allowed"))]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        [RegularExpression(@"[a-zA-Z0-9._%+-]+@[a-zA-z0-9._]+\.[a-zA-Z]{2,}$", ErrorMessage ="Invalid email format")]
        public string Email { get; set; }

        [Required]
      
        public  string DOB { get; set; }
        [Required]
        [MaxLength(50)]
        public string Dept { get; set; }
       

    }
    
}
