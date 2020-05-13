using System.Threading.Tasks;
using EmployeeAPI.Data;
using EmployeeAPI.Repository;

namespace EmployeeAPI.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool disposedValue = false;

        private readonly ProductsContext _context;

        private IEmployeeRepository _employeeRepository;

        public IEmployeeRepository EmployeeRepository
        {
            get
            {
                return _employeeRepository = _employeeRepository ?? new EmployeeRepository(_context);
            }
        }

        public UnitOfWork(ProductsContext context)
        {
            _context = context;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                    _context.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~UnitOfWork()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            System.GC.SuppressFinalize(this);
        }
    }
}