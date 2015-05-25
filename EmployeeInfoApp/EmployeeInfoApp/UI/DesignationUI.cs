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
    public partial class DesignationUI : Form
    {
        public DesignationUI()
        {
            InitializeComponent();
        }
        private Designation aDesignation=new Designation();
        private void saveButton_Click(object sender, EventArgs e)
        {
            DesignationManager designationManager= new DesignationManager();
            string message;
            if (designationNameTextBox.Text != "")
            {
                aDesignation.Title = designationNameTextBox.Text;
                if (designationManager.Save(aDesignation, out message))
                {
                    this.Close();
                }
                else
                {
                    MessageBox.Show(message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show(@"Please fill all fields properly", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public Designation GetLastAddedDesignation()
        {
            return aDesignation;
        }
    }
}
