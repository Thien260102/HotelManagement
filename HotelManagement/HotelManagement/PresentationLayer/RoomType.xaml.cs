using HotelManagement.BusinessLogicLayer;
using HotelManagement.DataTransferObject;
using MaterialDesignThemes.Wpf;
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
    /// Interaction logic for RoomType.xaml
    /// </summary>
    public partial class RoomType : UserControl
    {
        List<RoomTypeDTO> roomTypes;
        int current = -1;

        public RoomType()
        {
            InitializeComponent();

            LoadRoomType();
            DataGridRoomType.SelectedCellsChanged += SelectRoomType;
        }

        private void LoadRoomType()
		{
            roomTypes = new RoomTypeBLL().GetAllRoomTypes();

            DataGridRoomType.ItemsSource = roomTypes;
		}

        private void SelectRoomType(object sender, SelectedCellsChangedEventArgs e)
        {
            current = DataGridRoomType.SelectedIndex;
        }

        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {
            RoomTypeinfo roomTypeinfo = new RoomTypeinfo();
            roomTypeinfo.Show();
            roomTypeinfo.ReloadRoomType += LoadRoomType;
        }

        private void btn_Update_Click(object sender, RoutedEventArgs e)
        {
            if (current == -1)
            {
                new MessageBoxCustom("Choosing your Room Type you want to change", MessageType.Info, MessageButtons.Ok).ShowDialog();
                return;
            }

            RoomTypeinfo roomTypeinfo = new RoomTypeinfo();
            roomTypeinfo.Show();
            roomTypeinfo.SetData(roomTypes[current]); 
            roomTypeinfo.ReloadRoomType += LoadRoomType;
        }

        private void btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            if (current == -1)
            {
                new MessageBoxCustom("Choosing your Room Type you want to delete", MessageType.Info, MessageButtons.Ok).ShowDialog();
                return;
            }

            if (new RoomTypeBLL().RemoveRoomType(roomTypes[current].Id))
			{
                new MessageBoxCustom("Remove room type successfully", MessageType.Info, MessageButtons.Ok).ShowDialog();
                LoadRoomType();
			}
        }
    }
}
