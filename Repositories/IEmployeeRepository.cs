using EmployeeManagement.Models;
using System.Collections.Generic;

namespace EmployeeManagement.Repositories
{
    public interface IEmployeeRepository
    {
        Employee GetEmployeeDetails(int Id);
        IEnumerable<Employee> GetAllEmployees();
        Employee SaveEmployeeDetails(Employee employee);

        Employee DeleteEmployee(int Id);
    }
}
