namespace EmployeeManagementSystem.Models
{
    public class IEmployeeService
    {
        private readonly EmployeeDBContext _dbContext;
        public IEmployeeService(EmployeeDBContext dBContext)
        {
            _dbContext = dBContext;        
        }

        //Add new employee
        public Employee AddEmployee(Employee employee)
        {
            var emp = _dbContext.Employees.FirstOrDefault(x=>x.Email == employee.Email);
            if (emp == null)
            {
                employee.EmployeeId = Guid.NewGuid().ToString();
                _dbContext.Add(employee);
                _dbContext.SaveChanges();
                return employee;
            }
            return employee;
        }

        //List Employees
        public List<Employee> GetAll()
        {
            return _dbContext.Employees.ToList();
        }

        //get emploee
        public Employee Get(string employeeId)
        {
            return _dbContext.Employees.FirstOrDefault(x => x.EmployeeId == employeeId);
        }
        //update employee
        public Employee UpdateEmployee(Employee employee)
        {
            var emp = _dbContext.Employees.FirstOrDefault(x=>x.EmployeeId == employee.EmployeeId);           
            if (emp != null)
            {
                //emp.Email = employee.Email;
                emp.Dept = employee.Dept;
                emp.DOB = employee.DOB;
                emp.Name   = employee.Name;
                _dbContext.Update(emp);
                _dbContext.SaveChanges();
                return emp;
            }
            return emp;
        }

        //delete employee
        public bool DeleteEmployee(string id)
        {
            var emp = _dbContext.Employees.FirstOrDefault(x => x.EmployeeId == id);
            if(emp != null)
            {
                _dbContext.Remove(emp);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
