using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EmployeeManagementSystem.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly IEmployeeService _employeeService;

        public EmployeeController(ILogger<EmployeeController> logger, IEmployeeService employeeService)
        {
            _logger = logger;
             _employeeService = employeeService;
        }

        public IActionResult Index()
        {
           var empList = _employeeService.GetAll();
            return View(empList);
        }

        public IActionResult AddNewEmployee()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddNewEmployee(Employee employee) 
        {
            if (employee.Dept == null)
            {
                ViewData["Error"] = "please select department";
                return View();
            }
            var emp = _employeeService.AddEmployee(employee);
            
            if (emp.EmployeeId != null)
            { 
                return RedirectToAction("Index");
            }
            ViewData["Error"] = "Email " + employee.Email + " already existed";
            return View();
        }
        
        public IActionResult UpdateEmployee(string id)
        {
            var emp = _employeeService.Get(id);
            if (emp != null)
            { 
                return View(emp);
            }
            return View();
        }
        [HttpPost]
        public IActionResult UpdateEmployee(Employee employee)
        {
            var emp = _employeeService.UpdateEmployee(employee);
            return RedirectToAction("Index");
        }

       
        public IActionResult DeleteEmployee(string id)
        {
            if (_employeeService.DeleteEmployee(id))
            { 
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}