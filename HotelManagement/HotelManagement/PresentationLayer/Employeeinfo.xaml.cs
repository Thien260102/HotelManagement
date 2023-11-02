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
        AccountDTO account;

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
            account = new AccountDTO();
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


            // Account
            AccountBLL accountBLL = new AccountBLL();
            account = accountBLL.GetAccount(employee.Id);
            txt_UserName.Text = account.UserName;
            txt_Password.Text = account.Password;
            Checkbox_IsAvailable.IsChecked = account.IsAvailable;

            txt_UserName.IsReadOnly = true;
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

                string userName = txt_UserName.Text.Trim();
                string pass = txt_Password.Text.Trim();

                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(citizenId) ||
                   string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(birth) ||
                   string.IsNullOrEmpty(start) || string.IsNullOrEmpty(userName))
                {
                    throw new Exception("Please fill all information");
                }

                if (!Utilities.Validate_CitizenId(citizenId))
				{
                    throw new Exception("Please fill correct citizen ID.");
                }

                if (!Utilities.Validate_Phone(phone))
				{
                    throw new Exception("Please fill correct phone number");
				}

                if (!Utilities.Validate_DateTime(birth) && Utilities.Validate_DateTime(start))
				{
                    throw new Exception("Please fill date with correct format");
                }

                if (!Utilities.Validate_Password(pass))
				{
                    throw new Exception("Please fill password length more or equal 6 characters");
                }

                employee.FullName = name;
                employee.CitizenId = citizenId;
                employee.PhoneNumber = phone;
                employee.BirthDay = birth;
                employee.StartDay = start;
                employee.Salary = salary;
                employee.Sex = (bool)Checkbox_Male.IsChecked;

                account.UserName = userName;
                account.Password = pass;
                account.IsAvailable = (bool)Checkbox_IsAvailable.IsChecked;
                account.EmployeeID = employee.Id;
                account.RoleID = (int)position;

                EmployeeBLL employeeBLL = new EmployeeBLL();
                AccountBLL accountBLL = new AccountBLL();

                if (employee.Id == -1) // add new employeee
                {
                    if (employeeBLL.InsertEmployee(employee))
                    {
                        int employeeId = employeeBLL.GetEmployeeId(citizenId);
                        account.EmployeeID = employeeId;

						if (accountBLL.InsertAccount(account))
						{
							MessageBox.Show("Add new employee successful");

                            ReloadEmployee?.Invoke();
                            this.Close();
						}
                        else
                        {
                            employeeBLL.RemoveEmployee(employeeId);
                            throw new Exception("Add new employee fail");
                        }
                    }
                }

                else                    // update
                {
                    if (employeeBLL.UpdateEmployee(employee))
                    {
                        MessageBox.Show("Update employee successful");
                        accountBLL.UpdateAccount(account);

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
