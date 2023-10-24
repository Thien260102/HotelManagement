using HotelManagement.BusinessLogicLayer;
using HotelManagement.DataAccessLayer;
using HotelManagement.DataTransferObject;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace HotelManagement.PresentationLayer
{
    /// <summary>
    /// Interaction logic for Employeeinfo.xaml
    /// </summary>
    public partial class Employeeinfo : Window
    {
        EmployeeDTO employee;

        public Action ReloadEmployee;

        public Employeeinfo()
        {
            InitializeComponent();

            cbb_Position.SelectionChanged += SelectPosition;
            cbb_Position.Items.Add(Rule.ROLE.ADMIN);
            cbb_Position.Items.Add(Rule.ROLE.MANAGER);
            cbb_Position.Items.Add(Rule.ROLE.EMPLOYEE);

            txt_Salary.IsReadOnly = true;

            employee = new EmployeeDTO();
        }

		private void SelectPosition(object sender, SelectionChangedEventArgs e)
		{
			switch((Rule.ROLE)cbb_Position.SelectedItem)
			{
                case Rule.ROLE.ADMIN:
                    txt_Salary.Text = Rule.SALARY.ADMIN_SALARY.ToString();
                    break;
                case Rule.ROLE.MANAGER:
                    txt_Salary.Text = Rule.SALARY.MANAGER_SALARY.ToString();
                    break;
                case Rule.ROLE.EMPLOYEE:
                    txt_Salary.Text = Rule.SALARY.EMPLOYEE_SALARY.ToString();
                    break;
            }
		}

		public void SetData(EmployeeDTO employee)
		{
            EmployeeBLL employeeBLL = new EmployeeBLL();
            this.employee = employee;

            txt_Name.Text = employee.FullName;
            txt_CCCD.Text = employee.CitizenId;
            txt_Phone.Text = employee.PhoneNumber;
            txt_Birthday.Text = employee.BirthDay;
            txt_DayStart.Text = employee.StartDay;
            cbb_Position.Text = ((Rule.ROLE)employeeBLL.GetRoleId(employee.Id)).ToString();
            txt_Salary.Text = employee.Salary.ToString();
            Checkbox_Male.IsChecked = employee.Sex;

            txt_CCCD.IsReadOnly = true;
		}

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = txt_Name.Text.Trim();
                string citizenId = txt_CCCD.Text.Trim();
                string phone = txt_Phone.Text.Trim();
                string birth = txt_Birthday.Text.Trim();
                string start = txt_DayStart.Text.Trim();
                Rule.ROLE position = (Rule.ROLE)cbb_Position.SelectedItem;
                decimal salary = Decimal.Parse(txt_Salary.Text.Trim());

                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(citizenId) ||
                   string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(birth) ||
                   string.IsNullOrEmpty(start))
                {
                    throw new Exception("Please fill all information");
                }

                DateTime birthDay;
                DateTime startDay;
                if (!(DateTime.TryParse(birth, out birthDay) && DateTime.TryParse(start, out startDay)))
				{
                    throw new Exception("Please fill date with correct format");
                }

                employee.FullName = name;
                employee.CitizenId = citizenId;
                employee.PhoneNumber = phone;
                employee.BirthDay = birth;
                employee.StartDay = start;
                employee.Salary = salary;
                employee.Sex = (bool)Checkbox_Male.IsChecked;

                EmployeeBLL employeeBLL = new EmployeeBLL();

                if (employee.Id == -1) // add new employeee
                {
                    if (employeeBLL.InsertEmployee(employee))
                    {
                        int employeeId = employeeBLL.GetEmployeeId(citizenId);
                        AccountBLL accountBLL = new AccountBLL();
                        //if (accountBLL.InsertAccount(new DataTransferObject.AccountDTO(userName, passWord, "",
                        //                            false, -1, employeeId), role))
                        //{
                        //    MessageBox.Show("Add new account successful");
                        //    this.Close();
                        //}
                    }
                }

                else                    // update
                {
                    if (employeeBLL.UpdateEmployee(employee))
                    {
                        MessageBox.Show("Update employee successful");
                        AccountBLL accountBLL = new AccountBLL();
                        accountBLL.UpdateRole(employee.Id, (int)position);

                        ReloadEmployee?.Invoke();
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
