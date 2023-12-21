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
		#region Fields & Properties
		CustomerDTO _customer;
        RoomDTO _roomInfor;

        List<RoomTypeDTO> _roomTypes;
        int _currentType = 0;

        decimal _total = 0;

        bool _fromBooking = false;
        BookingDTO _booking;
        List<RoomDTO> _roomsAvailable;

        public Action ReloadParent;
		#endregion

		public RentingRoom()
		{
			InitializeComponent();

            txt_DateCheckin.SelectedDate = txt_DateCheckout.SelectedDate = DateTime.Now.Date;
            tp_CheckinDate.SelectedTime = tp_CheckoutDate.SelectedTime = DateTime.Now;
        }

		public void SetData(RoomDTO roomInfor)
		{
            this._roomInfor = roomInfor;

            // room type
            cb_RoomType.Items.Add(roomInfor.RoomTypeName);
            cb_RoomType.SelectedIndex = 0;
            cb_RoomType.IsReadOnly = true;
            _roomTypes = new RoomTypeBLL().GetAllRoomTypes();

            _currentType = _roomTypes.FindIndex(element => element.Name == cb_RoomType.Text.Trim());
            //foreach (var roomType in _roomTypes)
            //{
            //    cb_RoomType.Items.Add(roomType.Name);
            //}
            //cb_RoomType.Text = roomInfor.RoomTypeName;
            //cb_RoomType.SelectionChanged += SelectRoomType;
            //_currentType = _roomTypes.FindIndex(e => e.Id == roomInfor.RoomTypeId);

            txt_RoomID.Text = roomInfor.Id.ToString();
			txt_RoomID.IsReadOnly = true;

			txt_DateCreate.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
            txt_DateCreate.IsReadOnly = true;

            txt_Employee.Text = new AccountBLL().GetCurrentEmployeeName();
            txt_Employee.IsReadOnly = true;

            txt_NumberPhone.PreviewTextInput += InputOnlyNumber;
			txt_CitizenID.PreviewTextInput += InputOnlyNumber;
            txt_Totalday.PreviewTextInput += InputOnlyNumber;
            txt_TotalPeople.PreviewTextInput += InputOnlyNumber;

			txt_NumberPhone.LostFocus += CheckCustomer;
			txt_CitizenID.LostFocus += CheckCustomer;

            txt_DateCheckin.LostFocus += CalculateTotal;
            txt_Totalday.LostFocus += CalculateTotal;

            txt_Total.IsReadOnly = true;
        }

        public void SetData(BookingDTO bookingDTO)
        { 
            _roomsAvailable = new RoomBLL().GetRooms(Rule.ROOM_STATE.AVAILABLE);
            if (_roomsAvailable.Count == 0)
            {
                var result = new MessageBoxCustom("All rooms are not available", MessageType.Info, MessageButtons.Ok).ShowDialog();

                if (result.Value)
				{
                    Voucherinfo voucher = new Voucherinfo();
                    voucher.Show();
                    voucher.SetData(bookingDTO.CustomerId);
                    this.Close();
				}
            }

            _fromBooking = true;
            _booking = bookingDTO;

            // room type
            _roomTypes = new RoomTypeBLL().GetAllRoomTypes();
            foreach (var roomType in _roomTypes)
            {
                cb_RoomType.Items.Add(roomType.Name);
            }
            cb_RoomType.SelectionChanged += SelectRoomType;
            _currentType = _roomTypes.FindIndex(e => e.Id == bookingDTO.RoomTypeId);
            cb_RoomType.SelectedItem = bookingDTO.RoomTypeName;
            cb_RoomType.IsEnabled = false;

            //txt_RoomID.Text = bookingDTO.Id.ToString();
            txt_RoomID.IsReadOnly = true;

            txt_DateCreate.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
            txt_DateCreate.IsReadOnly = true;

            txt_Employee.Text = new AccountBLL().GetCurrentEmployeeName();
            txt_Employee.IsReadOnly = true;

            _customer = new CustomerBLL().GetCustomer(bookingDTO.CustomerId);
            txt_CustomerName.Text = _customer.FullName;
            txt_CustomerName.IsReadOnly = true;
            txt_NumberPhone.Text = _customer.PhoneNumber;
            txt_NumberPhone.IsReadOnly = true;
            txt_CitizenID.Text = _customer.CitizenId;
            txt_CitizenID.IsReadOnly = true;
            Checkbox_Male.IsChecked = _customer.Sex;
            Checkbox_Male.IsEnabled = false;

            txt_NumberPhone.PreviewTextInput += InputOnlyNumber;
            txt_CitizenID.PreviewTextInput += InputOnlyNumber;
            txt_Totalday.PreviewTextInput += InputOnlyNumber;
            txt_TotalPeople.PreviewTextInput += InputOnlyNumber;

            txt_NumberPhone.LostFocus += CheckCustomer;
            txt_CitizenID.LostFocus += CheckCustomer;

            txt_DateCheckin.LostFocus += CalculateTotal;
            txt_Totalday.LostFocus += CalculateTotal;

            txt_DateCheckin.Text = bookingDTO.CheckinDate;
            txt_DateCheckin.IsDropDownOpen = false;

            txt_Totalday.Text = bookingDTO.TotalDay.ToString();
            txt_Totalday.IsReadOnly = true;

            txt_DateCheckin.IsEnabled = false;
            txt_DateCheckout.SelectedDate = ((DateTime)txt_DateCheckin.SelectedDate).AddDays(bookingDTO.TotalDay);
            txt_DateCheckout.IsEnabled = false;

            txt_Total.IsReadOnly = true;
        }

        public void SetData()
		{
            _roomsAvailable = new RoomBLL().GetRooms(Rule.ROOM_STATE.AVAILABLE);
            if (_roomsAvailable.Count == 0)
            {
                var result = new MessageBoxCustom("All rooms are not available", MessageType.Info, MessageButtons.Ok).ShowDialog();

                if (result.Value)
                {
                    this.Close();
                }
            }

            // room type
            _roomTypes = new RoomTypeBLL().GetAllRoomTypes();
            foreach (var roomType in _roomTypes)
            {
                cb_RoomType.Items.Add(roomType.Name);
            }
            cb_RoomType.SelectionChanged += SelectRoomType;
            _currentType = 0;
            cb_RoomType.SelectedItem = _roomTypes[_currentType].Name;
            //cb_RoomType.IsEnabled = false;

            //txt_RoomID.Text = bookingDTO.Id.ToString();
            txt_RoomID.IsReadOnly = true;

            txt_DateCreate.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
            txt_DateCreate.IsReadOnly = true;

            txt_Employee.Text = new AccountBLL().GetCurrentEmployeeName();
            txt_Employee.IsReadOnly = true;

            _customer = new CustomerDTO();//new CustomerBLL().GetCustomer(bookingDTO.CustomerId);
            //txt_CustomerName.Text = _customer.FullName;
            //txt_CustomerName.IsReadOnly = true;
            //txt_NumberPhone.Text = _customer.PhoneNumber;
            //txt_NumberPhone.IsReadOnly = true;
            //txt_CitizenID.Text = _customer.CitizenId;
            //txt_CitizenID.IsReadOnly = true;
            //Checkbox_Male.IsChecked = _customer.Sex;
            //Checkbox_Male.IsEnabled = false;

            txt_NumberPhone.PreviewTextInput += InputOnlyNumber;
            txt_CitizenID.PreviewTextInput += InputOnlyNumber;
            txt_Totalday.PreviewTextInput += InputOnlyNumber;
            txt_TotalPeople.PreviewTextInput += InputOnlyNumber;

            txt_NumberPhone.LostFocus += CheckCustomer;
            txt_CitizenID.LostFocus += CheckCustomer;

            txt_DateCheckin.LostFocus += CalculateTotal;
            txt_Totalday.LostFocus += CalculateTotal;

            txt_DateCheckin.SelectedDate = DateTime.Now.Date;
            txt_DateCheckin.IsDropDownOpen = false;

            //txt_Totalday.Text = bookingDTO.TotalDay.ToString();
            //txt_Totalday.IsReadOnly = true;

            txt_DateCheckin.IsEnabled = false;
            txt_DateCheckout.SelectedDate = DateTime.Now.Date;

            txt_Total.IsReadOnly = true;
        }

        private bool CalculateCostIncurred(int totalPeople)
        {
            string message = "";
            int indexRoomType = _currentType;

            decimal repay = 0;
            if (_fromBooking)
            {
                repay = CalculateRepay();
                string sRepay = new MoneyConverter().Convert(repay, null, null, null).ToString().Trim();
                message += $"Repay: \t\t\t{sRepay}VND\n";
                indexRoomType = _roomTypes.FindIndex(roomType => roomType.Id == _booking.RoomTypeId);
            }

            decimal total = 0;
            double time = (Utilities.GetDefaultCheckinTime((DateTime)txt_DateCheckin.SelectedDate) - (DateTime)tp_CheckinDate.SelectedTime).Hours;
            if (time > 0)
            {
                total += (decimal)time * 0.1m * _roomTypes[indexRoomType].Price;
                string sTotal = new MoneyConverter().Convert(total, null, null, null).ToString().Trim();
                message += $"Soon {time} hours, pay fee: \t{sTotal}VND\n";
            }

            int excessPeople = totalPeople - _roomInfor.TotalPeople;
            if (excessPeople > 0)
            {
                decimal fee = (decimal)excessPeople * 0.25m * _roomTypes[indexRoomType].Price;
                total += fee;
                string sFee = new MoneyConverter().Convert(fee, null, null, null).ToString().Trim();
                message += $"Excess {excessPeople} people, pay fee: \t{sFee}VND\n";
            }

            total -= repay;
            if (total > 0)
            {
                string sTotal = new MoneyConverter().Convert(total, null, null, null).ToString().Trim();
                message += $"Total incurred fee: \t{sTotal}VND";

                var popup = new MessageBoxCustom(message, MessageType.Confirmation, MessageButtons.YesNo).ShowDialog();

                if (!popup.Value)
                {
                    new MessageBoxCustom($"Please choose another checkin datetime or decrease total people", MessageType.Info, MessageButtons.Ok).ShowDialog();
                    return false;
                }
            }
            else if (total <= 0)
            {
                string sTotal = new MoneyConverter().Convert(-total, null, null, null).ToString().Trim();
                message += $"Total repay: \t\t{sTotal}VND";

                new MessageBoxCustom(message, MessageType.Info, MessageButtons.Ok).ShowDialog();
            }

            return true;
        }

        private decimal CalculateRepay()
		{
            decimal repay = _roomTypes.Find(roomType => roomType.Id == _booking.RoomTypeId).Price 
                          - _roomTypes[_currentType].Price;

            if (repay < 0)
			{
                repay = 0;
			}

            return repay;
		}
        #region Events

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

            if (_fromBooking)
			{
                int index = _roomsAvailable.FindIndex(room => room.RoomTypeId == _roomTypes[_currentType].Id);
                if (index == -1)
				{
                    for (int i = _currentType; i < _roomTypes.Count; i++)
					{
                        index = _roomsAvailable.FindIndex(room => room.RoomTypeId == _roomTypes[i].Id);

                        if (index != -1)
						{
                            _currentType = i;
                            cb_RoomType.Text = _roomTypes[_currentType].Name;
                            break;
						}
                    }

                    if (index == -1 && _currentType > 0)
					{
                        for (int i = _currentType; i > -1; i--)
                        {
                            index = _roomsAvailable.FindIndex(room => room.RoomTypeId == _roomTypes[i].Id);

                            if (index != -1)
                            {
                                _currentType = i;
                                cb_RoomType.Text = _roomTypes[_currentType].Name;
                                break;
                            }
                        }
                    }

				}

                if (index == -1)
				{
                    txt_RoomID.Text = "No room";
				}
                else
				{
                    txt_RoomID.Text = _roomsAvailable[index].Id.ToString();
                    _roomInfor = _roomsAvailable[index];
                }
			}
            else
			{
                int index = _roomsAvailable.FindIndex(room => room.RoomTypeId == _roomTypes[_currentType].Id);
                if (index == -1)
                {
                    txt_RoomID.Text = "No room";
                    _roomInfor = new RoomDTO();
                }
                else
                {
                    txt_RoomID.Text = _roomsAvailable[index].Id.ToString();
                    _roomInfor = _roomsAvailable[index];
                }
            }
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
                string checkIn = txt_DateCheckin.Text.Trim();
                string checkOut = txt_DateCheckout.Text.Trim();
                bool sex = (bool)Checkbox_Male.IsChecked;

                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(citizenId) ||
                   string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(checkIn))
                {
                    throw new Exception("Please fill all information");
                }

                int roomType;
                if (!int.TryParse(txt_RoomID.Text.Trim(), out roomType))
				{
                    throw new Exception("Please choose another room");
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

                if (tp_CheckinDate.SelectedTime == null || tp_CheckoutDate.SelectedTime == null)
				{
                    throw new Exception("Please choose time");
                }

                if (!_fromBooking)
				{
                    if (DateTime.Parse(checkIn).Date < DateTime.Now.Date
                        || (DateTime.Parse(checkIn).Date == DateTime.Now.Date &&
                           ((DateTime)tp_CheckinDate.SelectedTime).TimeOfDay < DateTime.Now.TimeOfDay))
                    {
                        throw new Exception("Check in date must be equal or larger than today.");
                    }

                    if (DateTime.Parse(checkIn).Date > DateTime.Parse(checkOut))
                    {
                        throw new Exception("Check out date must be smaller than check out date.");
                    }
                }
                else
				{
                    _total = _booking.Total;
				}

                int totalDay;
                if (!int.TryParse(txt_Totalday.Text.Trim(), out totalDay))
                {
                    throw new Exception("Please fill correct Total day.");
                }

                int totalPeople;
                if (!int.TryParse(txt_TotalPeople.Text.Trim(), out totalPeople))
                {
                    throw new Exception("Please fill correct total people.");
                }

                if (!CalculateCostIncurred(totalPeople))
				{
                    return;
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

                if(_fromBooking)
                {
                    _booking.IsRented = true;
                    if (!new BookingBLL().UpdateBooking(_booking))
                    {
                        throw new Exception("Update booking infor fail");
                    }
                }

                _roomInfor.State = (int)Rule.ROOM_STATE.RENTING;
                new RoomBLL().UpdateRoom(_roomInfor);
                ReloadParent?.Invoke();

                new MessageBoxCustom("Insert renting successfully", MessageType.Success, MessageButtons.Ok).ShowDialog();
                this.Close();

            }
            catch (Exception ex)
            {
                new MessageBoxCustom(ex.Message, MessageType.Error, MessageButtons.Ok).ShowDialog();
            }
        }

		#endregion
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
