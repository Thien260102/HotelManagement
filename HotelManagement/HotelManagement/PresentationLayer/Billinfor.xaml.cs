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
    /// Interaction logic for Billinfor.xaml
    /// </summary>
    public partial class Billinfor : Window
    {
        #region Fields & Properties
        RentingDTO _renting;
        decimal _total;

        int _customerId = -1;

		public Action ReloadParent;
		#endregion

		public Billinfor()
        {
            InitializeComponent();
            _renting = new RentingDTO();
        }

        public void SetData(RentingDTO renting)
		{
            _renting = renting;

            txt_Employee.Text = new AccountBLL().GetCurrentEmployeeName();
            txt_Employee.IsReadOnly = true;

            //Customer
            var customer = new CustomerBLL().GetCustomer(renting.CustomerId);
            txt_CitizenID.Text = customer.CitizenId;
            txt_CitizenID.IsReadOnly = true;
            txt_CustomerName.Text = customer.FullName;
            txt_CustomerName.IsReadOnly = true;
            txt_NumberPhone.Text = customer.PhoneNumber;
            txt_NumberPhone.IsReadOnly = true;

            _customerId = customer.Id;

            // date
            txt_DateCreate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txt_DateCreate.IsReadOnly = true;

            txt_DateCheckin.SelectedDate = DateTime.Parse(renting.CheckinDate);
            txt_DateCheckin.IsEnabled = false;

            txt_Totalday.Text = renting.TotalDay.ToString();
            txt_Totalday.IsReadOnly = true;

            txt_DateCheckout.SelectedDate = DateTime.Parse(renting.CheckoutDate);
            txt_DateCheckout.IsEnabled = false;
            tp_CheckoutDate.SelectedTime = DateTime.Now;
            tp_CheckoutDate.IsEnabled = false;

            int exceed = (int)(Math.Floor((DateTime.Now - DateTime.Parse(renting.CheckinDate)).TotalDays - renting.TotalDay));
            if (((DateTime)tp_CheckoutDate.SelectedTime).Hour > Rule.TIME_CHECKOUT)
			{
                exceed++;
			}
            txt_ExceedDay.Text = exceed.ToString();
            txt_ExceedDay.IsReadOnly = true;

            // Room
            var room = new RoomBLL().GetRoom(renting.RoomId);
            txt_RoomID.Text = room.Id.ToString();
            txt_RoomID.IsReadOnly = true;
            cb_RoomType.Items.Add(room.RoomTypeName);
            cb_RoomType.SelectedIndex = 0;
            cb_RoomType.IsReadOnly = true;

            txt_Total.IsReadOnly = true;

            CalculateToTal();
		}

        public void CalculateToTal()
		{
            _total = 0;
            int exceedDay = int.Parse(txt_ExceedDay.Text);

            var roomType = new RoomTypeBLL().GetRoomType(_renting.RoomTypeName);

            _total = exceedDay * roomType.Price;

            txt_Total.Text = new MoneyConverter().Convert(_total, null, null, null).ToString().Trim();
        }

		#region Events
		private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string userName = txt_Employee.Text.Trim();
                string billDate = txt_DateCreate.Text.Trim();

                int totalDay;
                if (!int.TryParse(txt_Totalday.Text.Trim(), out totalDay))
                {
                    throw new Exception("Please fill correct Total day.");
                }

                _renting.IsPaid = true;
                if (!new RentingBLL().UpdateRenting(_renting))
				{
                    throw new Exception("Update renting fail");
				}

                BillDTO bill = new BillDTO(billDate, userName, _customerId, _renting.Id,
                                           totalDay, _total);

                if (!new BillBLL().InsertBill(bill))
				{
                    throw new Exception("Export bill fail");
				}

                ReloadParent?.Invoke();

                new MessageBoxCustom("Export bill successfully", MessageType.Success, MessageButtons.Ok).ShowDialog();
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
