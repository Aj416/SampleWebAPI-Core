using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeeAPI.Model
{
    public class EmployeeEditDto
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(60, ErrorMessage = "Name can't be longer than 60 characters")]
        public string EmpName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public DateTime? JoiningDate { get; set; }
        public string Mobile { get; set; }
        public string Qualification { get; set; }
        public string Email { get; set; }
        [Required]
        public int? DetailId { get; set; }

    }
}