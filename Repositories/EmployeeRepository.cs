using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeManagement.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets all employees.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Employee> GetAllEmployees() => _context.Employees.ToList();

        /// <summary>
        /// Gets the employee details.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <returns></returns>
        public Employee GetEmployeeDetails(int Id) => _context.Employees.SingleOrDefault(e => e.Id == Id);

        /// <summary>
        /// Saves the employee details.
        /// </summary>
        /// <param name="employee">The employee.</param>
        /// <returns></returns>
        public Employee SaveEmployeeDetails(Employee employee)
        {
            if (employee != null)
            {
                if (employee.Id == 0)
                    _context.Employees.Add(employee);
                else
                    _context.Entry(employee).State = EntityState.Modified;
                _context.SaveChanges();
            }
            return _context.Employees.Where(e => e.Name == employee.Name).FirstOrDefault();
        }

        /// <summary>
        /// Deletes the employee.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <returns></returns>
        public Employee DeleteEmployee(int Id)
        {
            Employee employee = _context.Employees.Find(Id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                _context.SaveChanges();
            }
            return employee;
        }
    }
}
