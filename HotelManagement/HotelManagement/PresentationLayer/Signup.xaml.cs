using HotelManagement.BusinessLogicLayer;
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
	/// Interaction logic for Signup.xaml
	/// </summary>
	public partial class Signup : Window
	{

		#region Methods
		public Signup()
		{
			InitializeComponent();
		}

        private void SU_iconClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void buttonSignup_Click(object sender, RoutedEventArgs e)
        {
            string citizenId = txtBox_CitizenID.Text.Trim();
            if(citizenId == "")
			{
                MessageBox.Show("Please fill your Citizen ID");
                return;
            }

            string fullname = txtBox_FullName.Text.Trim();
            if (fullname == "")
            {
                MessageBox.Show("Please fill your name");
                return;
            }

            string phone = txtBox_PhoneNumber.Text.Trim();
            if (phone == "")
            {
                MessageBox.Show("Please fill your Phone Number");
                return;
            }

            bool sex = false; // true is male, false is female
            string sexx = ((ComboBoxItem)cb_Sex.SelectedValue).Content.ToString().Trim();
            if (((ComboBoxItem)cb_Sex.SelectedValue).Content.ToString().Trim() == "Male")
			{
                sex = true;
			}

            DateTime? selectedDate = dp_BirthDay.SelectedDate;
            if(!selectedDate.HasValue)
			{
                MessageBox.Show("Please choose your birthday");
                return;
			}
            string birthDay = selectedDate.Value.ToString("yyyy-MM-dd");

            decimal salary = 0m;
            Rule.ROLE role = Rule.ROLE.EMPLOYEE;
            switch(((ComboBoxItem)cb_Role.SelectedValue).Content.ToString().Trim())
			{
                case "Admin":
                    role = Rule.ROLE.ADMIN;
                    
                    break;

                case "Manager":
                    role = Rule.ROLE.MANAGER;
                    salary = Rule.SALARY.MANAGER_SALARY;
                    break;

                case "Employee":
                    role = Rule.ROLE.EMPLOYEE;
                    salary = Rule.SALARY.EMPLOYEE_SALARY;
                    break;
			}

            string userName = txtBox_UserName.Text.Trim();
            if(userName == "")
			{
                MessageBox.Show("Please fill username");
                return;
			}

            string passWord = txtBox_Password.Password.Trim();
            if(passWord == "")

            {
                MessageBox.Show("Please fill password");
                return;
            }

            string cPassword = txtBox_CPassword.Password.Trim();
            if(cPassword == "")

            {
                MessageBox.Show("Please fill confirm password");
                return;
            }

            try
			{
                EmployeeBLL employeeBLL = new EmployeeBLL();

                if (employeeBLL.InsertEmployee(new DataTransferObject.EmployeeDTO(citizenId, fullname, phone, sex,
                                                       birthDay, DateTime.Now.ToString("yyyy-MM-dd"), salary)))
				{
                    int employeeId = employeeBLL.GetEmployeeId(citizenId);
                    try
					{
                        AccountBLL accountBLL = new AccountBLL();
                        if(accountBLL.InsertAccount(new DataTransferObject.AccountDTO(userName, passWord, "",
                                                    false, -1, employeeId), role))
						{
                            MessageBox.Show("Add new account successful");
						}
					}
                    catch(Exception ex)
					{
                        employeeBLL.RemoveEmployee(employeeId);
                        MessageBox.Show("Exeption add account" + ex.Message);
					}
				}


			}
            catch(Exception ex)
			{
                MessageBox.Show("Exeption add employee" + ex.Message);
			}
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();
        }
        #endregion
    }
}
