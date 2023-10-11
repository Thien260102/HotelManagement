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
    /// Interaction logic for Employee.xaml
    /// </summary>
    public partial class Employee : Window
    {
        public Employee()
        {
            InitializeComponent();
        }

        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {
            Employeeinfo employee = new Employeeinfo();
            employee.Show();
        }

        private void btn_Delete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Minimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btn_Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btn_Logout_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close(); 
        }
    }
}
