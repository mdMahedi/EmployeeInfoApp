using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using EmployeeInfoApp.DAL.DAO;
using EmployeeInfoApp.BLL;

namespace EmployeeInfoApp.UI
{
    public partial class FindEmployeeUI : Form
    {
        public FindEmployeeUI()
        {
            InitializeComponent();
        }
        private EmployeeManager employeeManager=new EmployeeManager();
        private string name;
        private void searchButton_Click(object sender, EventArgs e)
        {
            name = searchTextBox.Text;
            LoadListView(name);
        }
        public void LoadListView(string partialName)
        {
            resultListView.Items.Clear();
            List<Employee> employees;
            employees = employeeManager.GetAllEmploye(partialName);
            if (employees.Count > 0)
            {
                int serialNo = 1;
                foreach (Employee employee in employees)
                {
                    ListViewItem item = new ListViewItem(serialNo.ToString());
                    item.Tag = (Employee)employee;
                    item.SubItems.Add(employee.EmployeeName);
                    item.SubItems.Add(employee.Email);
                    resultListView.Items.Add(item);
                    serialNo++;
                }
            }
            else
            {
                MessageBox.Show(@"Employee with given searching criteria is not found in your system", @"Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private Employee GetSelectedEmployee()
        {
            int index = resultListView.SelectedIndices[0];
            ListViewItem item = resultListView.Items[index];
            return (Employee)item.Tag;
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            editToolStripMenuItem.Enabled = (resultListView.Items.Count > 0);
            deleteToolStripMenuItem.Enabled = (resultListView.Items.Count > 0);
        }

        private void editToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            //Employee selectedEmployee = GetSelectedEmployee();
            //if (selectedEmployee != null)
            //{
            //    EmployeeInformationUI employeeInformationUi = new EmployeeInformationUI(selectedEmployee);
            //    employeeInformationUi.ShowDialog();
            //    LoadListView(name);
            //    resultListView.HideSelection = false;
            //}
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Employee selectedEmployee = GetSelectedEmployee();
            //int selectedIndex = resultListView.SelectedIndices[0];
            //DialogResult result =
            //    MessageBox.Show("You are about to delete " + selectedEmployee.EmployeeName + " \nIs that alright?",
            //        "Delete Employee", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //if (result == DialogResult.Yes)
            //{
            //    employeeManager.DeleteEmployee(selectedEmployee);
            //    resultListView.Items.RemoveAt(selectedIndex);
            //}
        }

        private void editToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //Employee selectedEmployee = GetSelectedEmployee();
            //if (selectedEmployee != null)
            //{
            //    EmployeeInformationUI employeeInformationUi = new EmployeeInformationUI(selectedEmployee);
            //    employeeInformationUi.ShowDialog();
            //    LoadListView(name);
            //    resultListView.HideSelection = false;
            //}
        }

        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //Employee selectedEmployee = GetSelectedEmployee();
            //int selectedIndex = resultListView.SelectedIndices[0];
            //DialogResult result =
            //    MessageBox.Show("You are about to delete " + selectedEmployee.EmployeeName + " \nIs that alright?",
            //        "Delete Employee", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //if (result == DialogResult.Yes)
            //{
            //    employeeManager.DeleteEmployee(selectedEmployee);
            //    resultListView.Items.RemoveAt(selectedIndex);
            //}
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Employee selectedEmployee = GetSelectedEmployee();
            if (selectedEmployee != null)
            {
                EmployeeInformationUI employeeInformationUi = new EmployeeInformationUI(selectedEmployee);
                employeeInformationUi.ShowDialog();
                LoadListView(name);
                resultListView.HideSelection = false;
            }
        }

        private void deleteToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Employee selectedEmployee = GetSelectedEmployee();
            int selectedIndex = resultListView.SelectedIndices[0];
            DialogResult result =
                MessageBox.Show("You are about to delete " + selectedEmployee.EmployeeName + " \nIs that alright?",
                    "Delete Employee", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                employeeManager.DeleteEmployee(selectedEmployee);
                resultListView.Items.RemoveAt(selectedIndex);
            }
            searchTextBox.Clear();
        }
    }
}
