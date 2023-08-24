using Microsoft.AspNetCore.Mvc;
using EmployeeManagementSystem.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeManagementSystem.Controllers
{
    [Route("api/employee")]
    [ApiController]
    public class EmployeeAPIController : ControllerBase
    {
        private readonly EmployeeDBContext _dbContext;
        public EmployeeAPIController(EmployeeDBContext dBContext)
        {
                _dbContext = dBContext;
        }
        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<Employee> Get()
        {
            return _dbContext.Employees.ToList();
           
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var emp = _dbContext.Employees.FirstOrDefault(x => x.EmployeeId == id);
            if (emp != null)
            { 
                return Ok(emp);
            }
            return NotFound("Employee not found with id : "+id);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public IActionResult Post(Employee employee)
        {
            var emp = _dbContext.Employees.FirstOrDefault(x => x.Email == employee.Email);
            DateTime dateValue;
            if (employee.Name == null || employee.Name == "")
            {
                return BadRequest("please proide employye name");
            }            
            if (employee.Dept == null)
            {
                return BadRequest("please proide department");
            }
            if (employee.Dept != "IT" && employee.Dept != "HR" && employee.Dept != "Finance" && employee.Dept != "Marketing")
            {
                return BadRequest("Accepts one of [Finance,HR,Marketing,IT]");
            }
            if (employee.DOB == null)
            {
                return BadRequest("please select date of birth");
            }
            if(!DateTime.TryParse(employee.DOB, out dateValue))
            {
                return BadRequest("Invalid date");
            }
            dateValue = Convert.ToDateTime(employee.DOB);
            DateTime currDate = new DateTime(2000, 12, 31);
            if (dateValue > currDate)
            {
                return BadRequest("please select date less than 31-12-2000");
            }

            if (emp == null)
            {
                employee.EmployeeId = Guid.NewGuid().ToString();
                _dbContext.Add(employee);
                _dbContext.SaveChanges();
                return Ok(employee);
            }
            return BadRequest("Employee with " + employee.Email + " already existed");

        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(string id,Employee employee)
        {
            var emp = _dbContext.Employees.FirstOrDefault(x => x.EmployeeId == id);
            DateTime dateValue;
            if (emp == null)
            { 
                return NotFound("Employee not found with id : " + id); 
            }
            if (employee.Name == null|| employee.Name == "")
            { 
                return BadRequest("please proide employye name");
            }
            if (employee.Dept == null)            {
               
                return BadRequest("please proide department");
            }
            if (employee.Dept != "IT" || employee.Dept != "HR" || employee.Dept != "Finance" || employee.Dept != "Marketing")
            {
                return BadRequest("Accepts one of [Finance,HR,Marketing,IT]");
            }
            if (!DateTime.TryParse(employee.DOB, out dateValue))
            {
                return BadRequest("Invalid date");
            }
            dateValue = Convert.ToDateTime(employee.DOB);
            DateTime currDate = new DateTime(2000, 12, 31);
            if (dateValue > currDate)
            {
                return BadRequest("please select date less than 31-12-2000");
            }
            emp.Name = employee.Name;          
            emp.Dept = employee.Dept;
            emp.DOB = employee.DOB;
            _dbContext.Update(emp);
            _dbContext.SaveChanges();   
            return Ok(emp);
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var emp = _dbContext.Employees.FirstOrDefault(x => x.EmployeeId == id);
            if (emp == null)
            {
                return NotFound("Employee not found with id : " + id);
            }
            _dbContext.Remove(emp);
            _dbContext.SaveChanges();
            return Ok("Employee deleted successfully");
        }
    }
}
