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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HotelManagement.PresentationLayer
{
    /// <summary>
    /// Interaction logic for Guest.xaml
    /// </summary>
    public partial class Guest : UserControl
    {
        List<CustomerDTO> _customers;
        int _currentCustomer = -1;
        List<string> _searchTypes;
        int _currentSearchType = 0;

        public Guest()
        {
            InitializeComponent();

            LoadData();
            DataGrid_Guest.SelectedCellsChanged += SelectGuest;

            txt_Search.KeyDown += Finding;
            Combobox_TypeSearch.SelectionChanged += SelectTypeSearch;
        }

		private void LoadData()
		{
            _customers = new CustomerBLL().GetAllCustomers();

            DataGrid_Guest.ItemsSource = _customers;

            _searchTypes = new List<string>()
            {
                "Name",
                "Phone",
                "Citizen ID"
            };
            foreach (var file in _searchTypes)
            {
                Combobox_TypeSearch.Items.Add(file);
            }
            Combobox_TypeSearch.SelectedIndex = _currentSearchType;
            //Combobox_TypeSearch.SelectionChanged += SelectSearch;
        }

        private void Finding(object sender, KeyEventArgs e)
        {
            TextBox text = sender as TextBox;
            if (e.Key == Key.Return)
            {
                var filter = new List<CustomerDTO>();

                switch (_currentSearchType)
                {
                    case 0: // name
                        filter = (from customer in _customers
                                  where customer.FullName.ToLower().Contains(text.Text.ToLower())
                                  select customer).ToList();
                        break;

                    case 1: // phone
                        filter = (from customer in _customers
                                  where customer.PhoneNumber.ToLower().Contains(text.Text)
                                  select customer).ToList();
                        break;

                    case 2: // citizen id
                        filter = (from customer in _customers
                                  where customer.CitizenId.ToLower().Contains(text.Text)
                                  select customer).ToList();
                        break;
                }
                DataGrid_Guest.ItemsSource = filter;
            }
        }

        private void SelectTypeSearch(object sender, SelectionChangedEventArgs e)
        {
            _currentSearchType = Combobox_TypeSearch.SelectedIndex;
        }

        private void SelectGuest(object sender, SelectedCellsChangedEventArgs e)
        {
            _currentCustomer = DataGrid_Guest.SelectedIndex;
        }

        #region Button
        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {
            GuestInfor guestInfor = new GuestInfor();
            guestInfor.ReloadGuest += LoadData;
            guestInfor.Show();
        }

        private void btn_Update_Click(object sender, RoutedEventArgs e)
        {
            if (_currentCustomer == -1)
            {
                new MessageBoxCustom("Please choose customer you want to change", MessageType.Info, MessageButtons.Ok).ShowDialog();
                return;
            }

            GuestInfor guestInfor = new GuestInfor();
            guestInfor.SetData(_customers[_currentCustomer]);
            guestInfor.ReloadGuest += LoadData;
            guestInfor.Show();
        }

        private void btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            if (_currentCustomer == -1)
            {
                new MessageBoxCustom("Please choose customer you want to remove", MessageType.Info, MessageButtons.Ok).ShowDialog();
                return;
            }

            new CustomerBLL().RemoveCustomer(_customers[_currentCustomer].Id);
            LoadData();
        }
		#endregion
	}
}
