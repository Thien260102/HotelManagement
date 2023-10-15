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
    /// Interaction logic for Room.xaml
    /// </summary>
    public partial class Room : UserControl
    {
        public Room()
        {
            InitializeComponent();
            
        }
        public void DisableButton()
        {
            BdAvaliable.Background = Brushes.White;
            BdAvaliable.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString(Rule.BUTTON.NORMAL);
            btn_Avaliable.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(Rule.BUTTON.NORMAL);
            BdBooked.Background = Brushes.White;
            BdBooked.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString(Rule.BUTTON.NORMAL);
            btn_Booked.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(Rule.BUTTON.NORMAL);
            BdFixing.Background = Brushes.Transparent;
            BdFixing.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString(Rule.BUTTON.NORMAL);
            btn_Fixing.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(Rule.BUTTON.NORMAL);
           
        }
        private void btn_Avaliable_Click(object sender, RoutedEventArgs e)
        {
            DisableButton();
            BdAvaliable.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#E8F1FD");
            BdAvaliable.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#1570EF");
            btn_Avaliable.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#1570EF");
        }

        private void btn_Booked_Click(object sender, RoutedEventArgs e)
        {
            DisableButton();
            BdBooked.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FAC5C1");
            BdBooked.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#F04438");
            btn_Booked.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#F04438");
        }

        private void btn_Fixing_Click(object sender, RoutedEventArgs e)
        {
            DisableButton();
            BdFixing.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FDDDB3");
            BdFixing.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#E18308");
            btn_Fixing.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#E18308");
        }

        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {
            Roominfo roominfo = new Roominfo();
            roominfo.Show();
            //Checkin checkin = new Checkin();
            //checkin.Show();
        }

        private void btn_Delete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
