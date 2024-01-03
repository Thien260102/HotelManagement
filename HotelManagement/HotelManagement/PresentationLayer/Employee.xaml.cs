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
        private ObservableCollection<EmployeeDTO> _employees;
        private int _currentIndex = -1;
        List<string> _searchTypes;
        int _currentSearchType = 0;
        #endregion

        public Employee()
        {
            InitializeComponent();

            LoadEmployee();

            DataGridEmployee.SelectionChanged += SelectEmployee;

            Combobox_TypeSearch.SelectionChanged += SelectTypeSearch;
            Combobox_TypeSearch.SelectedIndex = _currentSearchType;

            txt_Search.KeyDown += Finding;
        }

        private void LoadEmployee()
		{
            EmployeeBLL employee = new EmployeeBLL();

            _employees = new ObservableCollection<EmployeeDTO>(employee.GetAllEmployees());

            DataGridEmployee.ItemsSource = _employees;

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
        }

        #region Events
        private void SelectTypeSearch(object sender, SelectionChangedEventArgs e)
        {
            _currentSearchType = Combobox_TypeSearch.SelectedIndex;
        }

        private void Finding(object sender, KeyEventArgs e)
        {
            TextBox text = sender as TextBox;
            if (e.Key == Key.Return)
            {
                var filter = new List<EmployeeDTO>();

                switch (_currentSearchType)
                {
                    case 0: // Name
                        filter = (from employee in _employees
                                  where employee.FullName.ToLower().Contains(text.Text.ToLower())
                                  select employee).ToList();
                        break;

                    case 1: // phone
                        filter = (from employee in _employees
                                  where employee.PhoneNumber.Contains(text.Text.Trim())
                                  select employee).ToList();
                        break;
                    case 2: // Citizen id
                        filter = (from employee in _employees
                                  where employee.CitizenId.ToLower().Contains(text.Text)
                                  select employee).ToList();
                        break;
                }
                DataGridEmployee.ItemsSource = filter;
            }
        }

        private void SelectEmployee(object sender, SelectionChangedEventArgs e)
        {
            _currentIndex = DataGridEmployee.SelectedIndex;
        }

        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {
            Employeeinfo employeeinfo = new Employeeinfo();
            employeeinfo.ReloadEmployee += LoadEmployee;
            employeeinfo.Show();
        }

        private void btn_Update_Click(object sender, RoutedEventArgs e)
        {
            if(_currentIndex == -1)
			{
                new MessageBoxCustom("Please choose employee you want to change", MessageType.Info, MessageButtons.Ok).ShowDialog();
                return;
			}

            Employeeinfo employeeinfo = new Employeeinfo();
            employeeinfo.ReloadEmployee += LoadEmployee;
            employeeinfo.Show();
            employeeinfo.SetData(_employees[_currentIndex]);
        }

        private void btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            if (_currentIndex == -1)
            {
                new MessageBoxCustom("Please choose employee you want to delete", MessageType.Info, MessageButtons.Ok).ShowDialog();
                return;
            }

            EmployeeBLL employeeBLL = new EmployeeBLL();
            EmployeeDTO employee = _employees[_currentIndex];

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
