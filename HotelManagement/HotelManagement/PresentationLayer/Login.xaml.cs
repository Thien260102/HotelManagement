using HotelManagement.BusinessLogicLayer;
using HotelManagement.DataTransferObject;
using System.Windows;
using System.Windows.Input;

namespace HotelManagement.PresentationLayer
{
	/// <summary>
	/// Interaction logic for Login.xaml
	/// </summary>
	public partial class Login : Window
	{
        #region Fields & Properties
        
		#endregion

		#region Methods
		public Login()
		{
			InitializeComponent();

		}

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void btn_Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void LoginHandle()
		{
            string user = textUserName.Text.Trim();
            string pass = textPass.Password.ToString().Trim();

            AccountBLL accountBLL = new AccountBLL();
            switch (accountBLL.Login(user, pass))
            {
                case 0:
                    new MessageBoxCustom("Login Successful!", MessageType.Info, MessageButtons.Ok).ShowDialog();
                    //MessageBox.Show("Login Successful!");
                    Main main = new Main();
                    main.Show();
                    this.Close();
                    break;

                case 1:
                    new MessageBoxCustom("Please fill all information.", MessageType.Info, MessageButtons.Ok).ShowDialog();
                    break;

                case 2:
                    new MessageBoxCustom("Wrong username or password", MessageType.Info, MessageButtons.Ok).ShowDialog();
                    break;

                case 3:
                    new MessageBoxCustom("Not exist username", MessageType.Info, MessageButtons.Ok).ShowDialog();
                    break;

                case 4:
                    new MessageBoxCustom("Your account not available. Please contact with your boss.", MessageType.Info, MessageButtons.Ok).ShowDialog();
                    break;
            }
        }
        
        private void buttonLogin_Click(object sender, RoutedEventArgs e)
        {
            LoginHandle();
        }

        private void checkBoxPass_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void KeyEvent(object sender, KeyEventArgs e)
		{
            if(e.Key == Key.Enter)
			{
                LoginHandle();

            }
		}

        #endregion
    }
}
