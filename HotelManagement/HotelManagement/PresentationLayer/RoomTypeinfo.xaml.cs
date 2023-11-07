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
    /// Interaction logic for RoomTypeinfo.xaml
    /// </summary>
    public partial class RoomTypeinfo : Window
    {
        RoomTypeDTO roomType;
        string originName;

        public Action ReloadRoomType;

        public RoomTypeinfo()
        {
            InitializeComponent();

            roomType = new RoomTypeDTO();

            txt_NumberPeople.PreviewTextInput += InputOnlyNumber;
            txt_Price.PreviewTextInput += InputOnlyNumber;
        }

        public void SetData(RoomTypeDTO roomType)
		{
            this.roomType = roomType;
            txt_RoomTypeName.Text = roomType.Name;
            txt_NumberPeople.Text = roomType.HighestNumberPeople.ToString();
            txt_Price.Text = roomType.Price.ToString("###");

            originName = roomType.Name;
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
                string name = txt_RoomTypeName.Text.Trim();
                name = name.ToUpper();
                if (string.IsNullOrEmpty(name))
				{
                    throw new Exception("Please fill the name");
				}

                int highest;
                if (!int.TryParse(txt_NumberPeople.Text.Trim(), out highest))
				{
                    throw new Exception("Highest Number People must be number");
                }
                
                decimal price;
                if (!decimal.TryParse(txt_Price.Text.Trim(), out price))
                {
                    throw new Exception("Price must be number");
                }

                bool isCheck = true;
                if (originName == name)
				{
                    isCheck = false;
				}
                roomType.Name = name;
                roomType.HighestNumberPeople = highest;
                roomType.Price = price;

                RoomTypeBLL roomTypeBLL = new RoomTypeBLL();
                if (roomType.Id == -1)  // Add new room
                {
                    roomType.Id = roomTypeBLL.CountAll() + 1;
                    if (roomTypeBLL.AddNewRoomType(roomType))
                    {
                        MessageBox.Show("Add new room type successfully");
                        ReloadRoomType?.Invoke();
                        this.Close();
                    }
                }
                else                // Update room
                {

                    if (roomTypeBLL.UpdateRoomType(roomType, isCheck))
                    {
                        MessageBox.Show("Update room type successfully");
                        ReloadRoomType?.Invoke();
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
