using HotelManagement.BusinessLogicLayer;
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
	/// Interaction logic for GuestInfor.xaml
	/// </summary>
	public partial class GuestInfor : Window
	{
        CustomerDTO customer;
		string originPhoneNumber = "";

        public Action ReloadGuest;

		public GuestInfor()
		{
			InitializeComponent();
            customer = new CustomerDTO();
		}

        public void SetData(CustomerDTO customer)
        {
            this.customer = customer;

            txt_CitizenID.Text = customer.CitizenId;
            txt_CitizenID.IsReadOnly = true;

            txt_FullName.Text = customer.FullName;
            txt_Phone.Text = customer.PhoneNumber;
            txt_Birthday.Text = customer.BirthDay;
            txt_Nationality.Text = customer.Nationality;
            Checkbox_Male.IsChecked = customer.Sex;

			originPhoneNumber = customer.PhoneNumber;
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_Save_Click(object sender, RoutedEventArgs e)
        {
			try
			{
				string name = txt_FullName.Text.Trim();
				string citizenId = txt_CitizenID.Text.Trim();
				string phone = txt_Phone.Text.Trim();
				string birth = txt_Birthday.Text.Trim();
				string nationality = txt_Nationality.Text.Trim();

				if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(citizenId)	||
				   string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(birth)		||
				   string.IsNullOrEmpty(nationality))
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

				if (!Utilities.Validate_DateTime(birth))
				{
					throw new Exception("Please fill date with correct format");
				}


				customer.FullName = name;
				customer.CitizenId = citizenId;

				bool isCheck = false;
				if (originPhoneNumber != phone)
				{
					isCheck = true;
				}
				customer.PhoneNumber = phone;

				customer.BirthDay = birth;
				customer.Sex = (bool)Checkbox_Male.IsChecked;
				customer.Nationality = nationality;

				CustomerBLL customerBLL = new CustomerBLL();

				if (customer.Id == -1) // add new customere
				{
					if (customerBLL.InsertCustomer(customer))
					{
						MessageBox.Show("Add new customer infor successfully");

						ReloadGuest?.Invoke();
						this.Close();
					}
				}
				else                    // update
				{
					if (customerBLL.UpdateCustomer(customer, isCheck))
					{
						MessageBox.Show("Update customer successful");

						ReloadGuest?.Invoke();
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
