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
	/// Interaction logic for RentingRoom.xaml
	/// </summary>
	public partial class RentingRoom : Window
	{
		CustomerDTO _customer;
        RoomDTO _roomInfor;

        List<RoomTypeDTO> _roomTypes;
        int _currentType = 0;

        decimal _total = 0;

		public RentingRoom()
		{
			InitializeComponent();
		}

		public void SetData(RoomDTO roomInfor)
		{
            this._roomInfor = roomInfor;

            // room type
            _roomTypes = new RoomTypeBLL().GetAllRoomTypes();
            foreach (var roomType in _roomTypes)
            {
                cb_RoomType.Items.Add(roomType.Name);
            }
            cb_RoomType.Text = roomInfor.RoomTypeName;
            cb_RoomType.SelectionChanged += SelectRoomType;
            _currentType = _roomTypes.FindIndex(e => e.Id == roomInfor.Id);

            txt_RoomID.Text = roomInfor.Id.ToString();
			txt_RoomID.IsReadOnly = true;

			txt_DateCreate.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
            txt_DateCreate.IsReadOnly = true;

            txt_Employee.Text = new AccountBLL().GetCurrentEmployeeName();
            txt_Employee.IsReadOnly = true;

            txt_NumberPhone.PreviewTextInput += InputOnlyNumber;
			txt_CitizenID.PreviewTextInput += InputOnlyNumber;

			txt_NumberPhone.LostFocus += CheckCustomer;
			txt_CitizenID.LostFocus += CheckCustomer;

            txt_DateCheckin.LostFocus += CalculateTotal;
            txt_Totalday.LostFocus += CalculateTotal;
        }

		private void CheckCustomer(object sender, RoutedEventArgs e)
		{
			var textBox = sender as TextBox;
			if (textBox.Name == "txt_NumberPhone")
			{
				_customer = new CustomerBLL().GetCustomer("", textBox.Text.Trim());
			}
			else
			{
				_customer = new CustomerBLL().GetCustomer(textBox.Text.Trim(), "");
			}

			if (_customer.Id != -1)
			{
				txt_CustomerName.Text = _customer.FullName;
				txt_NumberPhone.Text = _customer.PhoneNumber;
				txt_CitizenID.Text = _customer.CitizenId;
				Checkbox_Male.IsChecked = _customer.Sex;
			}
		}

		private void InputOnlyNumber(object sender, TextCompositionEventArgs e)
		{
			var textBox = sender as TextBox;
			e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
		}

        private void SelectRoomType(object sender, SelectionChangedEventArgs e)
        {
            int previous = _currentType;

            _currentType = cb_RoomType.SelectedIndex;

            if (_total == -1)
            {
                return;
            }

            int totalDay = (int)(_total / _roomTypes[previous].Price);
            _total = totalDay * _roomTypes[_currentType].Price;
            txt_Total.Text = new MoneyConverter().Convert(_total, null, null, null).ToString().Trim();
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

            _total = totalDay * _roomTypes[_currentType].Price;
            txt_Total.Text = new MoneyConverter().Convert(_total, null, null, null).ToString().Trim();

        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = txt_CustomerName.Text.Trim();
                string citizenId = txt_CitizenID.Text.Trim();
                string phone = txt_NumberPhone.Text.Trim();
                int roomType = int.Parse(txt_RoomID.Text.Trim());
                string checkIn = txt_DateCheckin.Text.Trim();
                string checkOut = txt_DateCheckout.Text.Trim();
                bool sex = (bool)Checkbox_Male.IsChecked;

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

                if (!Utilities.Validate_DateTime(checkIn) || !Utilities.Validate_DateTime(checkOut))
                {
                    throw new Exception("Please fill date with correct format");
                }

                if (DateTime.Parse(checkIn).Date < DateTime.Now.Date)
                {
                    throw new Exception("Check in date must be equal or larger than today.");
                }

                if (DateTime.Parse(checkIn).Date > DateTime.Parse(checkOut))
				{
                    throw new Exception("Check in date must be smaller than check out date.");
                }

                int totalDay;
                if (!int.TryParse(txt_Totalday.Text.Trim(), out totalDay))
                {
                    throw new Exception("Please fill correct Total day.");
                }

                RentingBLL rentingBLL = new RentingBLL();
                CustomerBLL customerBLL = new CustomerBLL();

                if (_customer.Id == -1)
                {
                    _customer.CitizenId = citizenId;
                    _customer.FullName = name;
                    _customer.PhoneNumber = phone;
                    _customer.Nationality = "";
                    _customer.Sex = true;
                    _customer.BirthDay = "";

                    if (!customerBLL.InsertCustomer(_customer))
                    {
                        throw new Exception("Insert _customer infor fail.");
                    }

                }

                RentingDTO renting = new RentingDTO(
                    customerBLL.GetCustomerId(_customer.CitizenId),
                    AccountBLL.Account.UserName, roomType,
                    DateTime.Now.ToString("yyyy-MM-dd"), checkIn,
                    totalDay, checkOut, _total);

                if (!rentingBLL.InsertRenting(renting))
				{
					throw new Exception("Insert renting infor fail.");
				}

                MessageBox.Show("Insert renting successfully");
				//ReloadBooking?.Invoke();
				this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region Header
        private void btn_Exit_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

		private void btn_Minimize_Click(object sender, RoutedEventArgs e)
		{
			this.WindowState = WindowState.Minimized;
		}

		private void Border_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.ChangedButton == MouseButton.Left)
			{
				this.DragMove();
			}
		}
		#endregion
	}
}
