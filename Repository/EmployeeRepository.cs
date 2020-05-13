using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeAPI.GenericRepository;
using EmployeeAPI.Data;
using EmployeeAPI.UnitOfWorks;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAPI.Repository
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ProductsContext context) : base(context) { }

        public async Task<IEnumerable<Employee>> GetAllWithDetails()
        {
            return await _context.Employee.Include(s => s.Detail).ToListAsync();
        }

        public async Task<Employee> GetByIdWithDetails(object id)
        {
            return await _context.Employee.Where(s => s.EmpId == Convert.ToInt32(id)).Include(s => s.Detail).SingleOrDefaultAsync();
        }

    }
}