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
    /// Interaction logic for Report.xaml
    /// </summary>
    public partial class Report : UserControl
    {
        public Report()
        {
            InitializeComponent();
        }

        private void btn_Export_Click(object sender, RoutedEventArgs e)
        {
            List<RoomDTO> rooms = new List<RoomDTO>()
            {
                new RoomDTO(1, "abc", 1, 0, 1, "None"),
            };
            new ReportUtilities().ExportToExcel(rooms, Combobox_TypeReport.Text + DateTime.Now.ToString("HHmmss dd-MM-yyyy"));
        }
    }
}
