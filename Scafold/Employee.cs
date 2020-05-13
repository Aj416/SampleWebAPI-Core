using System;
using System.Collections.Generic;

namespace EmployeeAPI.Scafold
{
    public partial class Employee
    {
        public int EmpId { get; set; }
        public string EmpName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public DateTime? JoiningDate { get; set; }
        public string Mobile { get; set; }
        public string Qualification { get; set; }
        public string Email { get; set; }
        public int DetailId { get; set; }

        public virtual EmployeeDetails Detail { get; set; }
    }
}
