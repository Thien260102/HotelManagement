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
    /// Interaction logic for DBoardRoom.xaml
    /// </summary>
    public partial class DBoardRoom : UserControl
    {
        public DBoardRoom()
        {
            InitializeComponent();
        }

        public void SetData(RoomDTO room)
		{
            RoomType.Content = room.RoomTypeName;
            StatusLabel.Content = room.StateName;

            string sPrice = new MoneyConverter().Convert(new RoomTypeBLL().GetRoomType(room.RoomTypeId).Price, null, null, null).ToString().Trim();
            Price.Content = sPrice;
		}
    }
}
