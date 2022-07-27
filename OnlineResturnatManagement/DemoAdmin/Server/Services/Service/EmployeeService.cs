
using OnlineResturnatManagement.Server.Data;
using OnlineResturnatManagement.Server.Models;
using OnlineResturnatManagement.Server.Services.IService;
using Microsoft.EntityFrameworkCore;

namespace OnlineResturnatManagement.Server.Services.Service
{
    public class EmployeeService : IEmployeeService
    {
        public ApplicationDbContext _context;
        public EmployeeService(ApplicationDbContext context)
        {
            _context=context;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeeAsync()
        {
            return await _context.Employees
               .OrderBy(e => e.Name)
               .ToListAsync();
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            var result= await _context.Employees.Where(e => e.Id.Equals(id))
                .FirstOrDefaultAsync();
            return result;
        }

        
        public bool CreateEmployee(Employee employee)
        {
            _context.AddAsync(employee);
            return _context.SaveChangesAsync().Status>0;
        }

        public bool UpdateEmployee(Employee employee)
        {
            _context.Update(employee);
            return _context.SaveChangesAsync().Status > 0;
        }

        public bool DeleteEmployee(Employee employee)
        {
            _context.Remove(employee);
            return _context.SaveChangesAsync().Status > 0;
        }
    }
   
}
