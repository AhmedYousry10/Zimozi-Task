using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementAPI.Models
{
    public class Employee
    {
        [Required]
        public int EmployeeID { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 100 characters.")]
        public string Name { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Department must be between 3 and 100 characters.")]
        public string Department { get; set; }
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Salary must be a positive number and bigger than 0.")]
        public decimal Salary { get; set; }
    }
}