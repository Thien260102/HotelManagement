using HotelManagement.BusinessLogicLayer;
using HotelManagement.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for Profile.xaml
    /// </summary>
    public partial class Profile : Window
    {
        AccountDTO account;
        string originPhoneNumber = "";

        EmployeeDTO employee;

        public Action IsChangePassword;

        public Profile()
        {
            InitializeComponent();

            LoadData();
        }

        private void LoadData()
		{
            account = AccountBLL.Account;

            EmployeeBLL employeeBLL = new EmployeeBLL();
            employee = employeeBLL.GetEmployee(account.EmployeeID);

            txt_Name.Text = employee.FullName;
            txt_Birthday.Text = employee.BirthDay;

            txt_CCCD.Text = employee.CitizenId;
            txt_CCCD.IsReadOnly = true;

            txt_Phone.Text = employee.PhoneNumber;
            txt_Phone.PreviewTextInput += InputOnlyNumber;

            Checkbox_Male.IsChecked = employee.Sex;

            txt_UserName.Text = account.UserName;
            txt_UserName.IsReadOnly = true;

            txt_Password.Text = account.Password;

            originPhoneNumber = employee.PhoneNumber;
        }

        private void InputOnlyNumber(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
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
                string phone = txt_Phone.Text.Trim();
                string birth = txt_Birthday.Text.Trim();

                string pass = txt_Password.Text.Trim();

                if (string.IsNullOrEmpty(name) ||
                    string.IsNullOrEmpty(phone) ||
                    string.IsNullOrEmpty(birth))
                {
                    throw new Exception("Please fill all information");
                }

                if (!Utilities.Validate_Phone(phone))
                {
                    throw new Exception("Please fill correct phone number");
                }
                
                if (!Utilities.Validate_DateTime(birth))
                {
                    throw new Exception("Please fill birthday with correct format");
                }

                if (!Utilities.Validate_Password(pass))
                {
                    throw new Exception("Please fill password length more or equal 6 characters");
                }

                employee.FullName = name;

                bool isCheck = false;
                if (originPhoneNumber != phone)
				{
                    isCheck = true;
				}
                employee.PhoneNumber = phone;
                employee.BirthDay = birth;
                employee.Sex = (bool)Checkbox_Male.IsChecked;

                EmployeeBLL employeeBLL = new EmployeeBLL();
                AccountBLL accountBLL = new AccountBLL();

                if (employeeBLL.UpdateEmployee(employee, isCheck))
                {
                    MessageBox.Show("Update employee successful!");

                    if (pass != account.Password)
                    {
                        account.Password = pass;
                        accountBLL.UpdateAccount(account);

                        IsChangePassword?.Invoke();
                    }

                    this.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
