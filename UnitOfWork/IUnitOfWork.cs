using System;
using System.Threading.Tasks;
using EmployeeAPI.Repository;

namespace EmployeeAPI.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        IEmployeeRepository EmployeeRepository { get; }
        Task SaveAsync();
    }
}