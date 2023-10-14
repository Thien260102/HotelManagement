using HotelManagement.PresentationLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace HotelManagement
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{

		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			InitializeApp();
		}

		private void InitializeApp()
		{
			//Login login = new Login();
			//login.Show();
			Main main = new Main();
			main.Show();

		}
	}
}
