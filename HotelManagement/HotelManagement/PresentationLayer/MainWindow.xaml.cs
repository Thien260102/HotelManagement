﻿using HotelManagement.BusinessLogicLayer;
using HotelManagement.PresentationLayer;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace HotelManagement.PresentationLayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static public String Source = "";

        private UserControl currentUserControl;
        private Button currentButton;
        //  IFirebaseConfig config = new FirebaseConfig { AuthSecret = "Fl7sCjIzoHwpSJjtVO501T21vVAXcsd6QyLKFSuv", BasePath = "https://wpff-debb4-default-rtdb.firebaseio.com" };
        //IFirebaseClient client;
        public static BitmapImage LoadImageFromFileName(string filename)
        {
            BitmapImage bm = new BitmapImage();
            bm.BeginInit();
            bm.UriSource = new Uri(Source + filename);
            bm.EndInit();
            return bm;
        }

        public static string GetAndSaveImage()
        {
            Byte[] BytesOfImage;

            OpenFileDialog ofdPatient = new OpenFileDialog();


            bool? Result = ofdPatient.ShowDialog();

            if (Result == true)
            {

                string path_with_image = ofdPatient.FileName;

                try
                {
                    try
                    {
                        FileStream fsRead = new FileStream(path_with_image, FileMode.Open);
                        BytesOfImage = new Byte[fsRead.Length];
                        fsRead.Read(BytesOfImage, 0, BytesOfImage.Length);
                        fsRead.Close();
                    }
                    catch { return ""; }

                    string filename = System.IO.Path.GetFileName(path_with_image);
                    string destFolder = System.IO.Path.Combine(MainWindow.Source, filename);
                    System.IO.File.Copy(path_with_image, destFolder, true);

                    return path_with_image;
                }
                catch { }

            }
            return "";
        }


        public MainWindow()
        {
            InitializeComponent();

            // lấy source để chứa image
            Source = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\Image\\";
        }

        private void OpenUserControl(UserControl userControl)
        {
            if (currentUserControl != null)
            {
                panelMain.Children.Clear();
            }

            currentUserControl = userControl;
            panelMain.Children.Add(currentUserControl);
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Email.Text = Login.LuuThongTin.Email;
            currentButton = ButtonHome;
            OpenUserControl(new Home());
            //string query = "Select * from User where User_Email = @Email ";
            //DataTable data = DataBase.Instance.ExecuteQuery(query, new object[] { Login.LuuThongTin.Email });


            //foreach (DataRow dr in data.Rows)
            //{
            //    if (dr["User_Avatar"] != DBNull.Value)
            //        avatar.Source = MainWindow.LoadImageFromFileName(dr["User_Avatar"].ToString());
            //}

            //    client = new FireSharp.FirebaseClient(config);
            //if(client!= null) { MessageBox.Show("hehe"); };
        }

        void Button_Choose(object senderButton)
        {
            if (senderButton != null)
            {
                DisableButton();
                currentButton = (Button)senderButton;
                currentButton.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(Rule.BUTTON.NORMAL);
                currentButton.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(Rule.BUTTON.HIGHLIGHT);
                currentButton.FontWeight = FontWeights.Bold;
            }
        }

        public void DisableButton()
        {
            if (currentButton != null)
            {
                currentButton.Background = Brushes.Transparent;
                currentButton.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(Rule.BUTTON.NORMAL);
                currentButton.FontWeight = FontWeights.Normal;
            }
        }

        private void Home_Change_Click(object sender, RoutedEventArgs e)
        {
            Button_Choose(sender);
            //OpenChildForm(new Home());
        }

        private void Tour_Change_Click(object sender, RoutedEventArgs e)
        {
            Button_Choose(sender);
            //OpenChildForm(new Tour());
        }

        private void Hotel_Change_Click(object sender, RoutedEventArgs e)
        {
            Button_Choose(sender);
            //OpenChildForm(new Hotel());
        }

        private void Admin_Change_Click(object sender, RoutedEventArgs e)
        {
			if (AccountBLL.Account.RoleID != (int)Rule.ROLE.ADMIN)
			{
				MessageBox.Show("Bạn không có quyền truy cập vào mục này!");
			}
			else
			{
				OpenUserControl(new Administration());
                Button_Choose(sender);
            }
		}

        private void Flight_Change_Click(object sender, RoutedEventArgs e)
        {
            Button_Choose(sender);
            //OpenChildForm(new Plane_Ticket());
        }

        private void Car_Change_Click(object sender, RoutedEventArgs e)
        {
            Button_Choose(sender);
            //OpenChildForm(new Car_Rental());
        }

        private void Chart_Change_Click(object sender, RoutedEventArgs e)
        {
            Button_Choose(sender);
            //OpenChildForm(new Chart());
        }

        private void AboutUs_Change_Click(object sender, RoutedEventArgs e)
        {
            Button_Choose(sender);
            //OpenChildForm(new About_Us());
        }

        private void Profile_Change_Click(object sender, RoutedEventArgs e)
        {
            Button_Choose(sender);
            //OpenChildForm(new Profile());
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Button_Choose(sender);
            if (MessageBox.Show("Do you want to sign out?", "Notification", MessageBoxButton.YesNoCancel, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
				Login login = new Login();
				login.Show();
				this.Close();
            }
        }
    }
}
