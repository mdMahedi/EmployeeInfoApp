using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeInfoApp.DAL.DAO;
using EmployeeInfoApp.DAL.GETWAY;

namespace EmployeeInfoApp.BLL
{
    class DesignationManager
    {
        private DesignationGateway designationGateway=new DesignationGateway();
        public List<Designation> GetAll()
        {
            return designationGateway.GetAll();
        }
        public bool Save(Designation aDesignation, out string showMessage)
        {
            if (aDesignation.Title == string.Empty)
            {
                showMessage = "Designation Title missing";
                return false;
            }
            else if (designationGateway.IsDesignationTitleExist(aDesignation.Title))
            {
                showMessage = "Title is already exist.";
                return false;
            }
            else
            {
                showMessage = "Designation has been saved.";
                return designationGateway.Save(aDesignation);
            }
        }
    }
}
