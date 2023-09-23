using HotelManagement.BusinessLogicLayer;
using System.Windows;
using System.Windows.Input;

namespace HotelManagement.PresentationLayer
{
	/// <summary>
	/// Interaction logic for Login.xaml
	/// </summary>
	public partial class Login : Window
	{
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

        private void iconClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void buttonLogin_Click(object sender, RoutedEventArgs e)
        {
			string user = textUserName.Text.Trim();
			string pass = textPass.Password.ToString().Trim();

            AccountBLL accountBLL = new AccountBLL();
            switch(accountBLL.Login(user, pass))
			{
                case 0:
                    MessageBox.Show("Login Successful!");
                    break;

                case 1:
                    MessageBox.Show("Please fill all information.");
                    break;

                case 2:
                    MessageBox.Show("Wrong username or password");
                    break;

                case 3:
                    MessageBox.Show("Not exist username");
                    break; 
			}
            
		}

        private void checkBoxPass_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
			Signup signup = new Signup();
            signup.Show();
			this.Close();
		}

    }
}
