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

		#endregion

		public Bill()
        {
            InitializeComponent();

            _bills = new BillBLL().GetAll();
            
            DataGridBill.ItemsSource = _bills;
            DataGridBill.IsReadOnly = true;
        }
        public void DisableButton()
        {
            BdCheckin.Background = Brushes.White;
            BdCheckin.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString(Rule.BUTTON.NORMAL);
            btn_Checkin.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(Rule.BUTTON.NORMAL);
            BdCheckout.Background = Brushes.White;
            BdCheckout.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString(Rule.BUTTON.NORMAL);
            btn_Checkout.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(Rule.BUTTON.NORMAL);

        }

        private void btn_Checkin_Click(object sender, RoutedEventArgs e)
        {
            DisableButton();
            BdCheckin.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#E8F1FD");
            BdCheckin.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#1570EF");
            btn_Checkin.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#1570EF");
            //Checkout checkout   = new Checkout();
            //checkout.Show();
        }

        private void btn_Checkout_Click(object sender, RoutedEventArgs e)
        {
            DisableButton();
            BdCheckout.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#E8F1FD");
            BdCheckout.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#1570EF");
            btn_Checkout.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#1570EF");
        }
    }
}
