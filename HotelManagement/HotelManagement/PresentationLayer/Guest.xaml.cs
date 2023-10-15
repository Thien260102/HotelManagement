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
        public Guest()
        {
            InitializeComponent();
        }
        public void DisableButton()
        {
            BdCheckin.Background = Brushes.White;
            BdCheckin.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString(Rule.BUTTON.NORMAL);
            btn_Checkin.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(Rule.BUTTON.NORMAL);
            BdCheckout.Background = Brushes.White;
            BdCheckout.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString(Rule.BUTTON.NORMAL);
            btn_Checkout.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(Rule.BUTTON.NORMAL);
            BdAll.Background = Brushes.Transparent;
            BdAll.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString(Rule.BUTTON.NORMAL);
            btn_All.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(Rule.BUTTON.NORMAL);

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

        private void btn_All_Click(object sender, RoutedEventArgs e)
        {
            DisableButton();
            BdAll.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#E8F1FD");
            BdAll.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#1570EF");
            btn_All.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#1570EF");
        }
    }
}
