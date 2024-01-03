using HotelManagement.BusinessLogicLayer;
using HotelManagement.DataTransferObject;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace HotelManagement.PresentationLayer
{
    /// <summary>
    /// Interaction logic for Bill.xaml
    /// </summary>
    public partial class Bill : UserControl
    {
        #region Fields & Properties
        List<BillDTO> _bills;
        List<string> _searchTypes;
        int _currentSearchType = 0;
        #endregion

        public Bill()
        {
            InitializeComponent();

            _bills = new BillBLL().GetAll();
            
            DataGridBill.ItemsSource = _bills;
            DataGridBill.IsReadOnly = true;
            _searchTypes = new List<string>()
            {
                "Citizen ID",
                "Bill Date",
                "Bill ID"
            };
            foreach (var file in _searchTypes)
            {
                Combobox_TypeSearch.Items.Add(file);
            }
            Combobox_TypeSearch.SelectionChanged += SelectTypeSearch;
            Combobox_TypeSearch.SelectedIndex = _currentSearchType;

            txt_Search.KeyDown += Finding;
        }

        private void SelectTypeSearch(object sender, SelectionChangedEventArgs e)
        {
            _currentSearchType = Combobox_TypeSearch.SelectedIndex;
        }

        private void Finding(object sender, KeyEventArgs e)
        {
            TextBox text = sender as TextBox;
            if (e.Key == Key.Return)
            {
                var filter = new List<BillDTO>();

                switch (_currentSearchType)
                {
                    case 0: // citizenid
                        filter = (from bill in _bills
                                  where (new CustomerBLL().GetCustomer(bill.CustomerId).CitizenId).ToLower().Contains(text.Text.ToLower())
                                  select bill).ToList();
                        break;

                    case 1: // Bill date
                        filter = (from bill in _bills
                                  where bill.BillDate.ToLower().Contains(text.Text)
                                  select bill).ToList();
                        break;

                    case 2: // bill Id
                        filter = (from bill in _bills
                                  where bill.Id.ToString().ToLower().Contains(text.Text)
                                  select bill).ToList();
                        break;
                }
                DataGridBill.ItemsSource = filter;
            }
        }
    }
}
