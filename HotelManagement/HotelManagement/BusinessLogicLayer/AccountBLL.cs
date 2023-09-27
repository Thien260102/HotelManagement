using HotelManagement.DataAccessLayer;
using HotelManagement.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HotelManagement.BusinessLogicLayer
{
	public class AccountBLL
	{
		#region Fields & Properties

		public static AccountDTO Account = null;

		#endregion

		#region Methods
		public int Login(string userName, string passWord)
		{
			if (userName == "")
			{
				return 1;
			}

			if (passWord == "")
			{
				return 1;
			}

			try
			{
				Account = AccountDAL.Instance.GetAccount(userName);
				if (Account.UserName == "")
					return 3;

				if (Account.UserName == userName && Account.Password == passWord)
				{
					return 0;
				}

				return 2;
			}
			catch
			{
				return 2;
			} 
		}

		public int GetRole()
		{
			try
			{
				switch(RoleDAL.Instance.GetRoleName(Account.RoleID))
				{
					case "Admin":
						return 1;

					case "Manager":
						return 2;

					case "Employee":
						return 3;
				}
			}
			catch
			{}

			return 0;
		}

		public bool InsertAccount(AccountDTO account, int role)
		{
			if(account.RoleID == -1)
			{
				account.RoleID = role;
			}

			if(account.RoleID == -1)
			{
				MessageBox.Show("Role not found");
				return false;
			}

			switch(AccountDAL.Instance.AddNewAccount(account))
			{
				case Rule.STATE.SUCCESS:

					return true;

				case Rule.STATE.EXIST:
					MessageBox.Show("Username or email is existed");
					return false;

				case Rule.STATE.FAIL:
					MessageBox.Show("Add account fail");
					return false;
			}

			return false;
		}
		#endregion
	}
}
