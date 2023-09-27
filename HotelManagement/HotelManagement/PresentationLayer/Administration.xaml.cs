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
	/// Interaction logic for Administration.xaml
	/// </summary>
	public partial class Administration : UserControl
	{
		public Administration()
		{
			InitializeComponent();
		}

		private void UserControl_Loaded()
		{

		}

        public async void load_dataTour()
        {
            
        }
        public async void load_dataHotel()
        {
            
        }
        public async void load_dataCar()
        {
            
        }
        public async void load_dataPlane()
        {
            
        }
        public async void load_dataUser()
        {

        }
        private async void buttonAdd_Click(object sender, RoutedEventArgs e)
        {

        }
        private async void buttonAdd_Car_Click(object sender, RoutedEventArgs e)
        {
            add_DataCar();

        }
        private async void buttonAdd_Hotel_Click(object sender, RoutedEventArgs e)
        {
            add_DataHotel();

        }
        private async void buttonAdd_Ticket_Click(object sender, RoutedEventArgs e)
        {

            add_Data_PlaneTicket();
        }
        private async void buttonAdd_User_Click(object sender, RoutedEventArgs e)
        {
            add_DataUser();

        }

        async void add_Data_PlaneTicket()
        {

        }
        async void add_DataCar()
        {

        }
        async void add_DataHotel()
        {
            

        }
        async void add_DataUser()
        {

            

        }
        void Button_Choose(object senderButton, Border boder)
        {
            
        }

        public void DisableButton()
        {
           
        }

        private void buttonTour_Click(object sender, RoutedEventArgs e)
        {
            Button_Choose(sender, boderTour);
        }
        private void buttonPlane_Click(object sender, RoutedEventArgs e)
        {
            Button_Choose(sender, boderAirTicket);
        }

        private void buttonCar_Click(object sender, RoutedEventArgs e)
        {
            Button_Choose(sender, boderCarRental);
        }

        private void buttonHotel_Click(object sender, RoutedEventArgs e)
        {
            Button_Choose(sender, boderHotel);
        }
        private void buttonUser_Click(object sender, RoutedEventArgs e)
        {
            Button_Choose(sender, boderUser);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            
        }

        private void buttonUpdate_Click(object sender, RoutedEventArgs e)
        {
            

        }
        private void buttonUpdate_Hotel_Click(object sender, RoutedEventArgs e)
        {
            
        }
        private void buttonUpdate_Car_Click(object sender, RoutedEventArgs e)
        {
            

        }
        private void buttonUpdate_Plane_Click(object sender, RoutedEventArgs e)
        {
            
        }
        private void buttonUpdate_User_Click(object sender, RoutedEventArgs e)
        {
            
        }
        private void toursDataGrid_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            
        }
        private void tourHotelGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void tourCarGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void tourPlaneGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void tourUserGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            
        }
        private void buttonDelete_Car_Click(object sender, RoutedEventArgs e)
        {
            
        }
        private void buttonDelete_Hotel_Click(object sender, RoutedEventArgs e)
        {
            
        }
        private void buttonDelete_Plane_Click(object sender, RoutedEventArgs e)
        {
            
        }
        private void buttonDelete_User_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void ImageT_Clicked(object sender, MouseButtonEventArgs e)
        {
            //BitmapImage bm = new BitmapImage();
            //bm.BeginInit();
            //bm.UriSource = new Uri(MainWindow.GetAndSaveImage());
            //bm.EndInit();
            //imageT.Source = bm;

        }

        private void ImageH_Clicked(object sender, MouseButtonEventArgs e)
        {
            //BitmapImage bm = new BitmapImage();
            //bm.BeginInit();
            //bm.UriSource = new Uri(MainWindow.GetAndSaveImage());
            //bm.EndInit();
            //imageH.Source = bm;
        }

        private void ImageP_Clicked(object sender, MouseButtonEventArgs e)
        {
            //BitmapImage bm = new BitmapImage();
            //bm.BeginInit();
            //bm.UriSource = new Uri(MainWindow.GetAndSaveImage());
            //bm.EndInit();
            //Image_Plane.Source = bm;
        }

        private void ImageC_Clicked(object sender, MouseButtonEventArgs e)
        {
            //BitmapImage bm = new BitmapImage();
            //bm.BeginInit();
            //bm.UriSource = new Uri(MainWindow.GetAndSaveImage());
            //bm.EndInit();
            //ImageC.Source = bm;
        }
        private void ImageU_Clicked(object sender, MouseButtonEventArgs e)
        {

        }

        private void buttonUpdate_C_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttonDelete_C_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttonDelete_C_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void buttonUpdate_A_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
