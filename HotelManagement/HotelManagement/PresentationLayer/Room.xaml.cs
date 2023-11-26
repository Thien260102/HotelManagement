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
    /// Interaction logic for Room.xaml
    /// </summary>
    public partial class Room : UserControl
    {
        List<RoomDTO> rooms;
        Dictionary<Rule.ROOM_STATE, List<RoomDTO>> dicRooms;
        int currentRoom;

        public Room()
        {
            InitializeComponent();

            LoadRoom();
            DataGridRoom.SelectionChanged += SelectRoom;
        }

		private void LoadRoom()
        {
            try
			{
                currentRoom = -1;
                rooms = new RoomBLL().GetAllRooms();

                dicRooms = new Dictionary<Rule.ROOM_STATE, List<RoomDTO>>();
                dicRooms.Add(Rule.ROOM_STATE.AVAILABLE, rooms.FindAll(element => element.State == (int)Rule.ROOM_STATE.AVAILABLE));
                dicRooms.Add(Rule.ROOM_STATE.RENTING, rooms.FindAll(element => element.State == (int)Rule.ROOM_STATE.RENTING));
                dicRooms.Add(Rule.ROOM_STATE.FIXING, rooms.FindAll(element => element.State == (int)Rule.ROOM_STATE.FIXING));

                btn_Avaliable.Content = $"Available({dicRooms[Rule.ROOM_STATE.AVAILABLE].Count})";
                btn_Booked.Content = $"Booked({dicRooms[Rule.ROOM_STATE.RENTING].Count})";
                btn_Fixing.Content = $"Fixing({dicRooms[Rule.ROOM_STATE.FIXING].Count})";
                btn_All.Content = $"Fixing({rooms.Count})";

                AllRooms();
            }
            catch (Exception ex)
			{
                MessageBox.Show(ex.Message);
			}
            
		}

        private void FilterRoom(Rule.ROOM_STATE state)
		{
            currentRoom = -1;
            DataGridRoom.ItemsSource = dicRooms[state];
        }

        private void AllRooms()
		{
            DisableButton();
            BdAll.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#E8F1FD");
            BdAll.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#1570EF");
            btn_All.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#1570EF");

            btn_All.Content = $"All({rooms.Count})";
            DataGridRoom.ItemsSource = rooms;
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

            BdAll.Background = Brushes.Transparent;
            BdAll.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString(Rule.BUTTON.NORMAL);
            btn_All.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(Rule.BUTTON.NORMAL);

        }

        #region Events
        private void SelectRoom(object sender, SelectionChangedEventArgs e)
        {
            currentRoom = DataGridRoom.SelectedIndex;
        }

        private void btn_Avaliable_Click(object sender, RoutedEventArgs e)
        {
            DisableButton();
            BdAvaliable.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#E8F1FD");
            BdAvaliable.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#1570EF");
            btn_Avaliable.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#1570EF");

            btn_Avaliable.Content = $"Available({dicRooms[Rule.ROOM_STATE.AVAILABLE].Count})";
            FilterRoom(Rule.ROOM_STATE.AVAILABLE);
        }

        private void btn_Booked_Click(object sender, RoutedEventArgs e)
        {
            DisableButton();
            BdBooked.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FAC5C1");
            BdBooked.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#F04438");
            btn_Booked.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#F04438");

            btn_Booked.Content = $"Booked({dicRooms[Rule.ROOM_STATE.RENTING].Count})";
            FilterRoom(Rule.ROOM_STATE.RENTING);
        }

        private void btn_Fixing_Click(object sender, RoutedEventArgs e)
        {
            DisableButton();
            BdFixing.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FDDDB3");
            BdFixing.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#E18308");
            btn_Fixing.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#E18308");

            btn_Fixing.Content = $"Fixing({dicRooms[Rule.ROOM_STATE.FIXING].Count})";
            FilterRoom(Rule.ROOM_STATE.FIXING);
        }

        private void btn_All_Click(object sender, RoutedEventArgs e)
        {
            AllRooms();
        }

        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {
            if (Utilities.GetRole() != Rule.ROLE.ADMIN)
            {
                MessageBox.Show("You do not have an authority.");
                return;
            }

            Roominfo roominfo = new Roominfo();
            roominfo.ReloadRoom += LoadRoom;
            roominfo.Show();
        }

        private void btn_Update_Click(object sender, RoutedEventArgs e)
        {
            if (Utilities.GetRole() != Rule.ROLE.ADMIN)
            {
                MessageBox.Show("You do not have an authority.");
                return;
            }

            if (currentRoom == -1)
			{
                MessageBox.Show("Choosing your room you want to change.");
                return;
            }

            Roominfo roominfo = new Roominfo();
            roominfo.ReloadRoom += LoadRoom;
            roominfo.SetData(rooms[currentRoom]);
            roominfo.Show();
        }

        private void btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            if (Utilities.GetRole() != Rule.ROLE.ADMIN)
            {
                MessageBox.Show("You do not have an authority.");
                return;
            }

            if (currentRoom == -1)
            {
                MessageBox.Show("Choosing your room you want to remove.");
                return;
            }

            try
			{
                if (new RoomBLL().RemoveRoom(rooms[currentRoom].Id))
				{
                    LoadRoom();
                    MessageBox.Show("Remove room successfully.");
				}
            }
            catch (Exception ex)
			{
                MessageBox.Show(ex.Message);
			}
        }
        #endregion

    }
}
