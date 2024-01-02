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
        List<string> _searchTypes;
        int _currentSearchType = 0;
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

            _searchTypes = new List<string>()
            {
                "Name",
                "Phone",
                "Citizen ID"
            };
            foreach (var file in _searchTypes)
            {
                Combobox_TypeSearch.Items.Add(file);
            }
            Combobox_TypeSearch.SelectedIndex = _currentSearchType;
            //Combobox_TypeSearch.SelectionChanged += SelectSearch;
        }

        #region Events
        private void SelectEmployee(object sender, SelectionChangedEventArgs e)
        {
            currentIndex = membersDataGrid.SelectedIndex;
        }

        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {
            Employeeinfo employeeinfo = new Employeeinfo();
            employeeinfo.ReloadEmployee += LoadEmployee;
            employeeinfo.Show();
        }

        private void btn_Update_Click(object sender, RoutedEventArgs e)
        {
            if(currentIndex == -1)
			{
                new MessageBoxCustom("Please choose employee you want to change", MessageType.Info, MessageButtons.Ok).ShowDialog();
                return;
			}

            Employeeinfo employeeinfo = new Employeeinfo();
            employeeinfo.ReloadEmployee += LoadEmployee;
            employeeinfo.Show();
            employeeinfo.SetData(employees[currentIndex]);
        }

        private void btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            if (currentIndex == -1)
            {
                new MessageBoxCustom("Please choose employee you want to delete", MessageType.Info, MessageButtons.Ok).ShowDialog();
                return;
            }

            EmployeeBLL employeeBLL = new EmployeeBLL();
            EmployeeDTO employee = employees[currentIndex];

            AccountBLL accountBLL = new AccountBLL();
            AccountDTO account = accountBLL.GetAccount(employee.Id);

            try
			{
                employeeBLL.RemoveEmployee(employee.Id);
                accountBLL.RemoveAccount(account.UserName);

                LoadEmployee();
                new MessageBoxCustom("Remove employee successful", MessageType.Info, MessageButtons.Ok).ShowDialog();
			}
            catch (Exception ex)
			{
                new MessageBoxCustom(ex.Message, MessageType.Info, MessageButtons.Ok).ShowDialog();
			}
        }
		#endregion
	}
}
