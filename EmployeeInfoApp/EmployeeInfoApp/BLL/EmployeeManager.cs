using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeInfoApp.DAL.DAO;
using EmployeeInfoApp.DAL.GETWAY;

namespace EmployeeInfoApp.BLL
{
    class EmployeeManager
    {
        private EmployeeGateway employeeGateway = new EmployeeGateway();

        public string Save(Employee anEmployee)
        {
            if (employeeGateway.IsEmailExist(anEmployee.Email))
            {
                throw new Exception("Your system already has an employee with this email address. Try again");
            }
            else
            {
                return employeeGateway.Save(anEmployee);
            }
        }
        public List<Employee> GetAllEmploye(string name)
        {
            return employeeGateway.GetAllEmployee(name);
        }

        public string Update(Employee anEmployee)
        {
            Employee selectedEmployee = employeeGateway.SearchEmployee(anEmployee.Email);
            if (selectedEmployee.Id != anEmployee.Id)
            {
                return "Your system already has an employee with this email address. Try again";
            }
            else
            {
                return employeeGateway.Update(anEmployee);
            }
        }
        public string DeleteEmployee(Employee selectedEmployee)
        {
            return employeeGateway.Delete(selectedEmployee);
        }

        public List<Employee> GetAllEmploye()
        {
            return employeeGateway.GetAllEmployee();
        }
    }
}
