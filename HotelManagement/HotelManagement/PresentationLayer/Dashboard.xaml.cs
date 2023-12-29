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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HotelManagement.PresentationLayer
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : UserControl
    {
        public Dashboard()
        {
            InitializeComponent();

            LoadData();
        }

        private void LoadData()
		{
            labelAvaliable.Content = new RoomBLL().CountAvailableRoom();
            labelBooking.Content = new BookingBLL().CountBooking();
            labelCheckin.Content = new RentingBLL().CountRentingToday();
            labelGuests.Content = new RentingBLL().CountCustomers();

            var rooms = new RoomBLL().GetRooms(Rule.ROOM_STATE.AVAILABLE);
            foreach(var room in rooms)
			{
                DBoardRoom dBoardRoom = new DBoardRoom();
                dBoardRoom.SetData(room);

                panelRooms.Children.Add(dBoardRoom);
			}

            var vouchers = new VoucherBLL().GetAllVoucherTypes();
            foreach(var voucher in vouchers)
			{
                DBoardVoucher dBoardVoucher = new DBoardVoucher();
                dBoardVoucher.SetData(voucher);

                panelVouchers.Children.Add(dBoardVoucher);
			}
		}
    }
}
