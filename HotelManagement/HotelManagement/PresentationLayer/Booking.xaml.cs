using HotelManagement.BusinessLogicLayer;
using HotelManagement.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HotelManagement.PresentationLayer
{
    /// <summary>
    /// Interaction logic for Booking.xaml
    /// </summary>
    public partial class Booking : UserControl
    {
        List<BookingDTO> _bookings;
        int _currentBooking = -1;
        List<string> _searchTypes;
        int _currentSearchType = 0;

        public Booking()
        {
            InitializeComponent();

            LoadData();

            DataGridBooking.SelectedCellsChanged += SelectBooking;
            DataGridBooking.MouseDoubleClick += MakeRentingRoom;
            Combobox_TypeSearch.SelectionChanged += SelectTypeSearch;
            Combobox_TypeSearch.SelectedIndex = _currentSearchType;

            txt_Search.KeyDown += Finding;
        }

        private void LoadData()
        {
            _bookings = new BookingBLL().GetAll();

            DataGridBooking.ItemsSource = _bookings;

            _searchTypes = new List<string>()
            {
                "Name",
                "Phone",
            };
            foreach (var file in _searchTypes)
            {
                Combobox_TypeSearch.Items.Add(file);
            }
        }

        #region Events
        private void SelectTypeSearch(object sender, SelectionChangedEventArgs e)
        {
            _currentSearchType = Combobox_TypeSearch.SelectedIndex;
        }

        private void Finding(object sender, KeyEventArgs e)
        {
            TextBox text = sender as TextBox;
            if (e.Key == Key.Return)
            {
                var filter = new List<BookingDTO>();

                switch (_currentSearchType)
                {
                    case 0: // Name
                        filter = (from booking in _bookings
                                  where booking.CustomerName.ToLower().Contains(text.Text.ToLower())
                                  select booking).ToList();
                        break;

                    case 1: // Phone
                        filter = (from booking in _bookings
                                  where (new CustomerBLL().GetCustomer(booking.CustomerId).PhoneNumber).ToLower().Contains(text.Text.ToLower())
                                  select booking).ToList();
                        break;
                }
                DataGridBooking.ItemsSource = filter;
            }
        }

        private void MakeRentingRoom(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DataGrid grid = sender as DataGrid;
                if (grid != null && grid.SelectedItems != null && grid.SelectedItems.Count == 1)
                {
                    DataGridRow dgr = grid.ItemContainerGenerator.ContainerFromItem(grid.SelectedItem) as DataGridRow;

                    var booking = _bookings[dgr.GetIndex()];
                    if (booking.IsRented
                     || (Utilities.GetDefaultCheckinTime(DateTime.Parse(booking.CheckinDate)).AddDays(booking.TotalDay))
                        < DateTime.Now)
                    {
                        new MessageBoxCustom("Booking is expired or rented", MessageType.Warning, MessageButtons.Ok).ShowDialog();
                        return;
                    }

                    RentingRoom rentingRoom = new RentingRoom();
                    rentingRoom.Show();
                    rentingRoom.SetData(booking);
                    rentingRoom.ReloadParent = LoadData;
                }
            }
        }

        private void SelectBooking(object sender, SelectedCellsChangedEventArgs e)
		{
            _currentBooking = DataGridBooking.SelectedIndex;
		}

        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {
            ReceiveBooking receiveBooking = new ReceiveBooking();
            receiveBooking.Show();
            receiveBooking.ReloadBooking += LoadData;
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            if (_currentBooking == -1)
            {
                new MessageBoxCustom("Please choose your booking you want to cancel", MessageType.Info, MessageButtons.Ok).ShowDialog();
                return;
            }

            var Result = MessageBox.Show("Do you want to cancel this booking?", "Cancel Booking", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (Result == MessageBoxResult.Yes)
            {
                try
                {
                    int remainDays = (int)(DateTime.Parse(_bookings[_currentBooking].CheckinDate).Date
                                - DateTime.Now.Date).TotalDays;
                    double ratio = 0d;
                    if (remainDays > 15)
                    {
                        ratio = 0.7d;
                    }
                    else if (remainDays > 7 && remainDays <= 15)
                    {
                        ratio = 0.5d;
                    }
                    else if (remainDays > 3 && remainDays <= 7)
                    {
                        ratio = 0.3d;
                    }

                    decimal refund = _bookings[_currentBooking].Total * (decimal)ratio;

                    new BookingBLL().RemoveBooking(_bookings[_currentBooking].Id);

                    LoadData(); 

                    MessageBox.Show($"The refunding is {new MoneyConverter().Convert(refund, null, null, null)}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

		#endregion
	}
}
