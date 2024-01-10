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
		List<RoomTypeDTO> _roomTypes;
        int _currentType = 0;

        CustomerDTO _customer;

        decimal _total = -1;

        List<VoucherDTO> _vouchers = new List<VoucherDTO>();
        int _currentVoucher = 0;

        public Action ReloadBooking;
		#endregion

		public ReceiveBooking()
        {
            InitializeComponent();

            LoadData();

            cb_Voucher.IsReadOnly = true;
            cb_Voucher.SelectionChanged += SelectVoucher;
            txt_Discount.IsReadOnly = true;
        }

		private void LoadData()
        {
            // room type
            _roomTypes = new RoomTypeBLL().GetAllRoomTypes();
            foreach (var roomType in _roomTypes)
            {
                cb_RoomType.Items.Add(roomType.Name);
            }
            cb_RoomType.SelectedIndex = 0;
            cb_RoomType.SelectionChanged += SelectRoomType;

            txt_DateCheckin.LostFocus += CalculateTotal;
            txt_Totalday.LostFocus += CalculateTotal;
            txt_Totalday.PreviewTextInput += InputOnlyNumber;

            txt_DateCheckin.SelectedDate = DateTime.Now;
            txt_DateBooking.SelectedDate = DateTime.Now;
            txt_DateBooking.IsDropDownOpen = false;

            txt_EmployeeName.Text = new AccountBLL().GetCurrentEmployeeName();
            txt_EmployeeName.IsReadOnly = true;

            txt_Total.IsReadOnly = true;
            txt_Total.Text = "0";

            txt_CCCD.PreviewTextInput += InputOnlyNumber;
            txt_Phone.PreviewTextInput += InputOnlyNumber;

            txt_Phone.LostFocus += CheckCustomer;
            txt_CCCD.LostFocus += CheckCustomer;

            _customer = new CustomerDTO();
        }

        #region Events
        private void SelectVoucher(object sender, SelectionChangedEventArgs e)
        {
            if(_vouchers.Count > _currentVoucher)
            {
                _currentVoucher = cb_Voucher.SelectedIndex;
                txt_Discount.Text = _vouchers[_currentVoucher].Ratio.ToString() + " %";
            }
            else
			{
                _currentVoucher = 0;
			}
        }

        private void CheckCustomer(object sender, RoutedEventArgs e)
		{
            var textBox = sender as TextBox;
            if (textBox.Name == "txt_Phone") 
			{
                _customer = new CustomerBLL().GetCustomer("", textBox.Text.Trim());
			}
            else
			{
                _customer = new CustomerBLL().GetCustomer(textBox.Text.Trim(), "");
            }

            cb_Voucher.Items.Clear();
            txt_Discount.Text = "";
            if (_customer.Id != -1)
			{
                txt_Name.Text = _customer.FullName;
                txt_Phone.Text = _customer.PhoneNumber;
                txt_CCCD.Text = _customer.CitizenId;
                //Checkbox_Male.IsChecked = customer.Sex;

                _vouchers = new VoucherBLL().GetVoucherOf(_customer.Id);
                _currentVoucher = 0;

                foreach(var voucher in _vouchers)
				{
                    cb_Voucher.Items.Add(voucher.VoucherTypeName);
				}
                cb_Voucher.SelectedIndex = 0;
                _currentVoucher = 0;

                txt_Discount.Text = _vouchers[_currentVoucher].Ratio.ToString() + " %";

            }
            else
			{
                _vouchers.Clear();
                _currentVoucher = 0;

                txt_Name.Text = "";
                txt_Phone.Text = "";
                txt_CCCD.Text = "";
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
                string name = txt_Name.Text.Trim();
                string citizenId = txt_CCCD.Text.Trim();
                string phone = txt_Phone.Text.Trim();
                int roomType = _roomTypes[_currentType].Id;
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

                if (_customer.Id == -1)
				{
                    _customer.CitizenId = citizenId;
                    _customer.FullName = name;
                    _customer.PhoneNumber = phone;
                    _customer.Nationality = "Vietnamese";
                    _customer.Sex = true;
                    _customer.BirthDay = "";

                    if (!customerBLL.InsertCustomer(_customer))
					{
                        throw new Exception("Insert customer infor fail.");
					}

				}

                BookingDTO booking = new BookingDTO(
                    customerBLL.GetCustomerId(_customer.CitizenId),
                    AccountBLL.Account.UserName, roomType,
                    txt_DateBooking.Text.Trim(), checkIn,
                    totalDay, _total, false);

                string message = $"Total room fee:\t\t  {new MoneyConverter().Convert(_total, null, null, null).ToString().Trim()}";

                int discount = 0;
                if(_vouchers.Count > _currentVoucher)
				{
                    discount = _vouchers[_currentVoucher].Ratio;
                    message += $"\nApply discount '{discount}%':\t- {new MoneyConverter().Convert(_total * discount / 100, null, null, null).ToString().Trim()}";

                    _total -= _total * discount / 100;
                    txt_Total.Text = new MoneyConverter().Convert(_total, null, null, null).ToString().Trim();
                    message += $"\nCustomer must pay:\t  {new MoneyConverter().Convert(_total, null, null, null).ToString().Trim()}";
                }

                var popup = new MessageBoxCustom(message, MessageType.Confirmation, MessageButtons.YesNo).ShowDialog();
                if(!popup.Value)
				{
                    _total = totalDay * _roomTypes[_currentType].Price;
                    txt_Total.Text = new MoneyConverter().Convert(_total, null, null, null).ToString().Trim();
                    return;
				}

                if (!bookingBLL.InsertBooking(booking))
                {
                    throw new Exception("Insert booking infor fail.");
                }

                new MessageBoxCustom("Insert booking successfully.", MessageType.Success, MessageButtons.Ok).ShowDialog();
                ReloadBooking?.Invoke();
                this.Close();
            }
            catch (Exception ex)
            {
                new MessageBoxCustom(ex.Message, MessageType.Error, MessageButtons.Ok).ShowDialog();
            }
        }
		#endregion
	}
}
