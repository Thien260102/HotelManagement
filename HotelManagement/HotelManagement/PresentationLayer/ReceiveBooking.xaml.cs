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
    /// Interaction logic for ReceiveBooking.xaml
    /// </summary>
    public partial class ReceiveBooking : Window
    {
		#region Fields & Properties
		List<RoomTypeDTO> roomTypes;
        int currentType = 0;

        CustomerDTO customer;

        decimal total = -1;

        public Action ReloadBooking;
		#endregion

		public ReceiveBooking()
        {
            InitializeComponent();

            LoadData();
        }

        private void LoadData()
        {
            // room type
            roomTypes = new RoomTypeBLL().GetAllRoomTypes();
            foreach (var roomType in roomTypes)
            {
                cb_RoomType.Items.Add(roomType.Name);
            }
            cb_RoomType.SelectedIndex = 0;
            cb_RoomType.SelectionChanged += SelectRoomType;

            txt_DateCheckin.LostFocus += CalculateTotal;
            txt_Totalday.LostFocus += CalculateTotal;
            txt_Totalday.PreviewTextInput += InputOnlyNumber;

            txt_DateBooking.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txt_DateBooking.IsReadOnly = true;

            txt_EmployeeName.Text = new AccountBLL().GetCurrentEmployeeName();
            txt_EmployeeName.IsReadOnly = true;

            txt_Total.IsReadOnly = true;
            txt_Total.Text = "0";

            txt_CCCD.PreviewTextInput += InputOnlyNumber;
            txt_Phone.PreviewTextInput += InputOnlyNumber;

            txt_Phone.LostFocus += CheckCustomer;
            txt_CCCD.LostFocus += CheckCustomer;

            customer = new CustomerDTO();
        }

		private void CheckCustomer(object sender, RoutedEventArgs e)
		{
            var textBox = sender as TextBox;
            if (textBox.Name == "txt_Phone") 
			{
                customer = new CustomerBLL().GetCustomer("", textBox.Text.Trim());
			}
            else
			{
                customer = new CustomerBLL().GetCustomer(textBox.Text.Trim(), "");
            }

            if (customer.Id != -1)
			{
                txt_Name.Text = customer.FullName;
                txt_Phone.Text = customer.PhoneNumber;
                txt_CCCD.Text = customer.CitizenId;
                //Checkbox_Male.IsChecked = customer.Sex;
			}
		}

		private void InputOnlyNumber(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
        }

        private void SelectRoomType(object sender, SelectionChangedEventArgs e)
        {
            int previous = currentType;

            currentType = cb_RoomType.SelectedIndex;

            if (total == -1)
			{
                return;
			}

            int totalDay = (int)(total / roomTypes[previous].Price);
            total = totalDay * roomTypes[currentType].Price;
            txt_Total.Text = new MoneyConverter().Convert(total, null, null, null).ToString().Trim();
		}

		private void CalculateTotal(object sender, RoutedEventArgs e)
		{
            txt_Total.Text = "0";

            if (!Utilities.Validate_DateTime(txt_DateCheckin.Text.Trim()))
            {
                return;
            }

            int totalDay;
            if (!int.TryParse(txt_Totalday.Text.Trim(), out totalDay))
			{
                return;
			}

            if (totalDay <= 0)
			{
                return;
			}

            total = totalDay * roomTypes[currentType].Price;
            txt_Total.Text = new MoneyConverter().Convert(total, null, null, null).ToString().Trim();
        
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
                int roomType = roomTypes[currentType].Id;
                string checkIn = txt_DateCheckin.Text.Trim();
                //bool sex = (bool)Checkbox_Male.IsChecked;

                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(citizenId) ||
                   string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(checkIn))
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

                if (!Utilities.Validate_DateTime(checkIn))
                {
                    throw new Exception("Please fill date with correct format");
                }

                if (DateTime.Parse(checkIn).Date < DateTime.Now.Date)
                {
                    throw new Exception("Check in date must be equal or larger than today.");
                }

                int totalDay;
                if (!int.TryParse(txt_Totalday.Text.Trim(), out totalDay))
                {
                    throw new Exception("Please fill correct Total day.");
                }

                BookingBLL bookingBLL = new BookingBLL();
                CustomerBLL customerBLL = new CustomerBLL();

                if (customer.Id == -1)
				{
                    customer.CitizenId = citizenId;
                    customer.FullName = name;
                    customer.PhoneNumber = phone;

                    if (!customerBLL.InsertCustomer(customer))
					{
                        throw new Exception("Insert customer infor fail.");
					}

				}

                BookingDTO booking = new BookingDTO(
                    customerBLL.GetCustomerId(customer.CitizenId),
                    AccountBLL.Account.UserName, roomType,
                    txt_DateBooking.Text.Trim(), checkIn,
                    totalDay, total);

                if (!bookingBLL.InsertBooking(booking))
                {
                    throw new Exception("Insert booking infor fail.");
                }

                ReloadBooking?.Invoke();
                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
