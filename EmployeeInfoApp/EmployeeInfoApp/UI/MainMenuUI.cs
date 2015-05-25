using System.Windows.Forms;

namespace EmployeeInfoApp.UI
{
    public partial class MainMenuUI : Form
    {
        public MainMenuUI()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            EmployeeInformationUI employeeInformationUi=new EmployeeInformationUI();
            employeeInformationUi.Show();
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            FindEmployeeUI findEmployeeUi=new FindEmployeeUI();
            findEmployeeUi.Show();
        }
    }
}
