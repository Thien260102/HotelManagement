using HotelManagement.BusinessLogicLayer;
using HotelManagement.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for Roominfo.xaml
    /// </summary>
    public partial class Roominfo : Window
    {
		#region Fields & Properties
		List<RoomTypeDTO> roomTypes;

        RoomDTO room;
        string originName = null;
        int originFloor = -1;

        public Action ReloadRoom;
		#endregion

		public Roominfo()
        {
            InitializeComponent();

            roomTypes = new RoomTypeBLL().GetAllRoomTypes();
            foreach (var element in roomTypes)
			{
                cb_RoomType.Items.Add(element.Name);
			}

            cb_State.Items.Add(Rule.ROOM_STATE.AVAILABLE.ToString());
            cb_State.Items.Add(Rule.ROOM_STATE.RENTING.ToString());
            cb_State.Items.Add(Rule.ROOM_STATE.FIXING.ToString());

            cb_RoomType.SelectedIndex = 0;
            cb_State.SelectedIndex = 0;

            room = new RoomDTO();

            txt_Floor.PreviewTextInput += InputOnlyNumber;
        }

        public void SetData(RoomDTO room)
		{
            this.room = room;
            txt_RoomName.Text = room.Name;
            txt_Floor.Text = room.Floor.ToString();
            txt_RoomFacility.Text = room.Note;
            cb_RoomType.Text = roomTypes.Find(element => element.Id == room.RoomTypeId).Name;

            originFloor = room.Floor;
            originName = room.Name;
            
            switch((Rule.ROOM_STATE)room.State)
			{
                case Rule.ROOM_STATE.AVAILABLE:
                    cb_State.Text = Rule.ROOM_STATE.AVAILABLE.ToString();
                    break;

                case Rule.ROOM_STATE.RENTING:
                    cb_State.Text = Rule.ROOM_STATE.RENTING.ToString();
                    break;

                case Rule.ROOM_STATE.FIXING:
                    cb_State.Text = Rule.ROOM_STATE.FIXING.ToString();
                    break;
            }
		}

        private void InputOnlyNumber(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = txt_RoomName.Text.Trim();
                string note = txt_RoomFacility.Text.Trim();
                Rule.ROOM_STATE state = Rule.ROOM_STATE.AVAILABLE;

                int floor = 0;
                if (!int.TryParse(txt_Floor.Text.Trim(), out floor) || floor < 1)
				{
                    throw new Exception("Floor must be positive number");
				}

                if (!Enum.TryParse<Rule.ROOM_STATE>(cb_State.SelectedItem.ToString(), true, out state))
				{
                    throw new Exception("Room state error");
                }

                int roomTypeId = roomTypes.Find(element => element.Name == cb_RoomType.Text.Trim()).Id;

                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(note))
                {
                    throw new Exception("Please fill all information");
                }

                room.Name = name;
                room.Floor = floor;
                room.Note = note;
                room.State = (int)state;
                room.RoomTypeId = roomTypeId;

                bool isCheck = true;
                if (originName == name && originFloor == floor)
				{
                    isCheck = false;
				}

                RoomBLL roomBLL = new RoomBLL();
                if (room.Id == -1)  // Add new room
                {
                    if (roomBLL.AddNewRoom(room))
					{
                        MessageBox.Show("Add new room successfully");
                        ReloadRoom?.Invoke();
                        this.Close();
					}
                }
                else                // Update room
				{
                    if (roomBLL.UpdateRoom(room, isCheck))
					{
                        MessageBox.Show("Update room successfully");
                        ReloadRoom?.Invoke();
                        this.Close();
                    }
				}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
