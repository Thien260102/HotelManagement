using HotelManagement.BusinessLogicLayer;
using HotelManagement.DataTransferObject;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
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
            Combobox_TypeSearch.SelectedIndex = _currentSearchType;
            //Combobox_TypeSearch.SelectionChanged += SelectSearch;
        }

    }
}
