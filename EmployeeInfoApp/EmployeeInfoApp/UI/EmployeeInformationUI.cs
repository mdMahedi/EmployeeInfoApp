using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EmployeeInfoApp.BLL;
using EmployeeInfoApp.DAL.DAO;

namespace EmployeeInfoApp.UI
{
    public partial class EmployeeInformationUI : Form
    {
        
        public EmployeeInformationUI()
        {
            InitializeComponent();
            LoadDesignations();
        }

        private DesignationManager designationManager=new DesignationManager();
        private EmployeeManager employeeManager=new EmployeeManager();

        private int employeeId;

        private void addDesignationButton_Click(object sender, EventArgs e)
        {
            DesignationUI designationUi=new DesignationUI();
            designationUi.ShowDialog();
            LoadDesignations();
            Designation lastAddeDesignation = designationUi.GetLastAddedDesignation();
            if (lastAddeDesignation != null)
            {
                designationComboBox.Text = lastAddeDesignation.Title;
            }
        }

        public void LoadDesignations()
        {
            designationComboBox.DataSource = designationManager.GetAll();
            designationComboBox.DisplayMember = "Title";
            designationComboBox.ValueMember = "Id";
        }
        private void ClearEmployee()
        {
            nameTextBox.Text = "";
            emailTextBox.Text = "";
            addressTextBox.Text = "";
            designationComboBox.Text = "";
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            Employee anEmployee=new Employee();
            anEmployee.Id = employeeId;
            anEmployee.EmployeeName = nameTextBox.Text;
            anEmployee.Email = emailTextBox.Text;
            anEmployee.Address = addressTextBox.Text;
            anEmployee.Designation = (Designation) designationComboBox.SelectedItem;

            if (nameTextBox.Text != "" && emailTextBox.Text != "" && addressTextBox.Text != "" &&
                designationComboBox.Text != "")
            {
                if (saveButton.Text != @"Update")
                {
                    string message = employeeManager.Save(anEmployee);
                    MessageBox.Show(message, @"Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearEmployee();
                }
                else
                {
                    string message = employeeManager.Update(anEmployee);
                    MessageBox.Show(message, @"Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearEmployee();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show(@"Please fill-up the employee's information properly", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public EmployeeInformationUI(Employee employee)
            : this()
        {
            saveButton.Text = @"Update";
            FillFieldsWith(employee);
            employeeId = employee.Id;
        }

        private void FillFieldsWith(Employee employee)
        {
            nameTextBox.Text = employee.EmployeeName;
            addressTextBox.Text = employee.Address;
            emailTextBox.Text = employee.Email;
            designationComboBox.SelectedValue = employee.Designation.Id;
        }
    }
}
