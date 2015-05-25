using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeInfoApp.DAL.DAO;
namespace EmployeeInfoApp.DAL.GETWAY
{
    class EmployeeGateway
    {
        DesignationGateway designationGateway=new DesignationGateway();

        private SqlConnection SqlConnectionObj;
        private SqlCommand SqlCommandObj;
        public EmployeeGateway()
        {
            SqlConnectionObj = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeDBConn"].ConnectionString);
            SqlCommandObj = new SqlCommand();
            SqlCommandObj.Connection = SqlConnectionObj;
        }
        public string Save(Employee anEmployee)
        {
            SqlConnectionObj.Open();
            string query = String.Format("Insert Into dbo.Employee Values('{0}','{1}','{2}','{3}')", anEmployee.EmployeeName,anEmployee.Email, anEmployee.Address, anEmployee.Designation.Id);
            SqlCommandObj.CommandText = query;
            SqlCommandObj.ExecuteNonQuery();
            SqlConnectionObj.Close();
            string message = "Employee : " + anEmployee.EmployeeName + " has been saved";
            return message;
        }
        public string Update(Employee anEmployee)
        {
            SqlConnectionObj.Open();
            string query =String.Format("Update dbo.Employee Set name='{0}',email='{1}',[address]='{2}',designationId = {3} Where id = {4}",anEmployee.EmployeeName, anEmployee.Email, anEmployee.Address, anEmployee.Designation.Id,anEmployee.Id);
            SqlCommandObj.CommandText = query;
            SqlCommandObj.ExecuteNonQuery();
            SqlConnectionObj.Close();
            string message = "Employee information has been updated";
            return message;
        }
        public string Delete(Employee anEmployee)
        {
            SqlConnectionObj.Open();
            string query = String.Format("Delete From dbo.Employee Where Id={0}", anEmployee.Id);
            SqlCommandObj.CommandText = query;
            SqlCommandObj.ExecuteNonQuery();
            SqlConnectionObj.Close();
            string message = "Employee: " + anEmployee.EmployeeName+ " has been deleted.";
            return message;
        }
        public List<Employee> GetAllEmployee()
        {
            string nameOfName = "";
            return GetAllEmployee(nameOfName);
        }
        public List<Employee> GetAllEmployee(string name)
        {
            List<Employee> employees = new List<Employee>();
            SqlConnectionObj.Open();
            string query = String.Format("Select * From dbo.Employee");
            if (name != "")
            {
                query += String.Format(" Where name Like '%{0}%'", name);
            }
            query += " ORDER BY name";
            SqlCommandObj.CommandText = query;
            SqlDataReader reader = SqlCommandObj.ExecuteReader();
            while (reader.Read())
            {
                Employee anEmployee = new Employee();
                anEmployee.Id = Convert.ToInt32(reader["id"]);
                anEmployee.EmployeeName = reader["name"].ToString();
                anEmployee.Email = reader["email"].ToString();
                anEmployee.Address = reader["address"].ToString();
                anEmployee.Designation = designationGateway.GetDesignationId(Convert.ToInt32(reader["designationId"]));
                employees.Add(anEmployee);
            }
            reader.Close();
            SqlConnectionObj.Close();
            return employees;
        }
        public Employee SearchEmployee(string email)
        {
            Employee anEmployee = null;
            SqlConnectionObj.Open();
            string query = String.Format("Select * From dbo.Employee Where email='{0}'", email);
            SqlCommandObj.CommandText = query;
            SqlDataReader reader = SqlCommandObj.ExecuteReader();
            if (reader != null)
            {
                while (reader.Read())
                {
                    anEmployee = new Employee();
                    anEmployee.Id = Convert.ToInt32(reader["id"]);
                    anEmployee.EmployeeName = reader["name"].ToString();
                    anEmployee.Email = reader["email"].ToString();
                    anEmployee.Address = reader["address"].ToString();
                    anEmployee.Designation = designationGateway.GetDesignationId(Convert.ToInt32(reader["designationId"]));
                    SqlConnectionObj.Close();
                    return anEmployee;
                }
            }
            reader.Close();
            SqlConnectionObj.Close();
            return null;
        }
        public bool IsEmailExist(string email)
        {
            if (SearchEmployee(email) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
