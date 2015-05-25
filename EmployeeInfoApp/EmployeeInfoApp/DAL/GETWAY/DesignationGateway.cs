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
    class DesignationGateway
    {
        private SqlConnection SqlConnectionObj;
        private SqlCommand SqlCommandObj;
        public DesignationGateway()
        {
            SqlConnectionObj = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeDBConn"].ConnectionString);
            SqlCommandObj = new SqlCommand();
            SqlCommandObj.Connection = SqlConnectionObj;
        }
        public bool Save(Designation aDesignation)
        {
            string query = String.Format("Insert into dbo.Designation Values('{0}')", aDesignation.Title);
            if (SqlConnectionObj.State == System.Data.ConnectionState.Open)
            {
                SqlConnectionObj.Close();
            }
            else
            {
                SqlConnectionObj.Open();
                SqlCommandObj.CommandText = query;
                SqlCommandObj.ExecuteNonQuery();
                SqlConnectionObj.Close();
            }
            return true;
        }
        public List<Designation> GetAll()
        {
            List<Designation> designations = new List<Designation>();
            string query = "Select * from dbo.Designation";
            SqlConnectionObj.Open();
            SqlCommandObj.CommandText = query;
            SqlDataReader readerObj = SqlCommandObj.ExecuteReader();
            while (readerObj.Read())
            {
                Designation aDesignation = new Designation();
                aDesignation.Id = int.Parse(readerObj["id"].ToString());
                aDesignation.Title = readerObj["name"].ToString();

                designations.Add(aDesignation);
            }
            readerObj.Close();
            SqlConnectionObj.Close();
            return designations;
        }

        public Designation GetDesignationId(int designationId)
        {
            SqlConnectionObj.Open();
            string query = String.Format("Select * From dbo.Designation Where id='{0}'", designationId);
            SqlCommandObj.CommandText = query;
            SqlDataReader reader = SqlCommandObj.ExecuteReader();
            Designation aDesignation = new Designation();
            while (reader.Read())
            {
                aDesignation.Id = Convert.ToInt32(reader["id"]);
                aDesignation.Title = reader["name"].ToString();
            }
            reader.Close();
            SqlConnectionObj.Close();
            return aDesignation;
        }
        public bool IsDesignationTitleExist(string name)
        {
            string query = String.Format("Select * From dbo.Designation Where name = '{0}'", name);
            SqlConnectionObj.Open();
            SqlCommandObj.CommandText = query;
            SqlDataReader readerObj = SqlCommandObj.ExecuteReader();
            bool canRead = readerObj.Read();
            readerObj.Close();
            SqlConnectionObj.Close();
            if (readerObj != null)
            {
                return canRead;
            }
            return false;
        }
    }
}
