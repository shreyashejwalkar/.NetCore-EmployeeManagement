using EmployeeManagement.Models;
using EmployeeManagement.Repositories;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IEmployeeRepository _employeeRepository;

        public HomeController(AppDbContext context, IEmployeeRepository employeeRepository)
        {
            _context = context;
            _employeeRepository = employeeRepository;
        }

        [Route("")]
        [Route("[action]")]
        [Route("~/")]
        public ViewResult Index()
        {
            return View(_employeeRepository.GetAllEmployees());
        }

        [Route("[action]/{id?}")]
        public ViewResult Details(int id)
        {
            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Employee = _employeeRepository.GetEmployeeDetails(id),
                Pagetitle = "Employee Details"
            };
            
            return View(homeDetailsViewModel);
        }

        [Route("[action]/{id?}")]
        [HttpGet]
        public ViewResult SaveEmployeeDetails(int id)
        {
            var model=_employeeRepository.GetEmployeeDetails(id);
            if (model == null)
                model = new Employee();
            return View(model);
        }

        [Route("[action]/{id?}")]
        [HttpPost]
        public IActionResult SaveEmployeeDetails(Employee employee)
        {
            if(ModelState.IsValid)
            {
                 _employeeRepository.SaveEmployeeDetails(employee);
                return RedirectToAction("Index");
            }
            return View();
        }

        [Route("[action]/{id?}")]
        [HttpGet]
        public ViewResult Delete(int? id) => View(_employeeRepository.GetEmployeeDetails(id ?? 1));

        [Route("[action]/{id?}")]
        [HttpPost]
        public RedirectToActionResult Delete(int id)
        {
            _employeeRepository.DeleteEmployee(id);
            return RedirectToAction("Index");
        }
    }
}
