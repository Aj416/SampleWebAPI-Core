using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeAPI.GenericRepository;
using EmployeeAPI.Data;

namespace EmployeeAPI.Repository
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        Task<IEnumerable<Employee>> GetAllWithDetails();
        Task<Employee> GetByIdWithDetails(object id);

    }
}