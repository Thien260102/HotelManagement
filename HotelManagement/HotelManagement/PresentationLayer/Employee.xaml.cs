using HotelManagement.BusinessLogicLayer;
using HotelManagement.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HotelManagement.PresentationLayer
{
    /// <summary>
    /// Interaction logic for Employee.xaml
    /// </summary>
    public partial class Employee : UserControl
    {
        #region Fields & Properties
        private ObservableCollection<EmployeeDTO> employees;
        private int currentIndex = -1;
		#endregion

		public Employee()
        {
            InitializeComponent();

            LoadEmployee();

            membersDataGrid.SelectionChanged += SelectEmployee;
        }

		private void LoadEmployee()
		{
            EmployeeBLL employee = new EmployeeBLL();

            employees = new ObservableCollection<EmployeeDTO>(employee.GetAllEmployees());

            membersDataGrid.ItemsSource = employees;
		}

        #region Events
        private void SelectEmployee(object sender, SelectionChangedEventArgs e)
        {
            currentIndex = membersDataGrid.SelectedIndex;
        }

        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {
            Employeeinfo employeeinfo = new Employeeinfo();
            employeeinfo.Show();
        }

        private void btn_Update_Click(object sender, RoutedEventArgs e)
        {
            if(currentIndex == -1)
			{
                MessageBox.Show("Please choose employee you want to change.");
                return;
			}

            Employeeinfo employeeinfo = new Employeeinfo();
            employeeinfo.ReloadEmployee += LoadEmployee;
            employeeinfo.Show();
            employeeinfo.SetData(employees[currentIndex]);
        }

        private void btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            
        }
		#endregion
	}
}
