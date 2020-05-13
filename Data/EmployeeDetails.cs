using System;
using System.Collections.Generic;

namespace EmployeeAPI.Data
{
    public partial class EmployeeDetails
    {
        public EmployeeDetails()
        {
            Employee = new HashSet<Employee>();
        }

        public int DetailId { get; set; }
        public string PresentAddress { get; set; }
        public string Area { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public virtual ICollection<Employee> Employee { get; set; }
    }
}
