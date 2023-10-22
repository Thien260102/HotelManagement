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
        internal ObservableCollection<EmployeeDTO> employees;

        public Employee()
        {
            InitializeComponent();

            LoadEmployee();
        }

        private void LoadEmployee()
		{
            EmployeeBLL employee = new EmployeeBLL();

            employees = new ObservableCollection<EmployeeDTO>(employee.GetAllEmployees());

            dataGrid_Employees.DataContext = employees;

            MessageBox.Show("Hello" + employees[0].CitizenId);
		}

		#region Events
		private void btn_Add_Click(object sender, RoutedEventArgs e)
        {
            Employeeinfo employeeinfo = new Employeeinfo();
            employeeinfo.Show();
        }

        private void btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            dataGrid_Employees.DataContext = employees;
            MessageBox.Show("Hello" + employees[0].FullName);
        }
		#endregion
	}
}
