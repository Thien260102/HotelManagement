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
				{
					return 3;
				}

				if (Account.UserName != userName || Account.Password != passWord)
				{
					return 2;
				}

				if (!Account.IsAvailable)
				{
					return 4;
				}

				return 0;
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

		public bool InsertAccount(AccountDTO account)
		{
			switch (AccountDAL.Instance.AddNewAccount(account))
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

		public bool UpdateAccount(AccountDTO account)
		{
			switch (AccountDAL.Instance.UpdateAccount(account))
			{
				case Rule.STATE.SUCCESS:

					return true;

				case Rule.STATE.FAIL:

					return false;
			}

			return false;
		}

		public bool UpdateRole(int employeeId, int roleId)
		{
			string userName = AccountDAL.Instance.GetAccount(employeeId).UserName;

			switch(AccountDAL.Instance.UpdateRole(userName, roleId))
			{
				case Rule.STATE.SUCCESS:
					
					return true;

				case Rule.STATE.FAIL:
					
					return false;
			}

			return false;
		}

		public AccountDTO GetAccount(int employeeId) => AccountDAL.Instance.GetAccount(employeeId);

		public bool RemoveAccount(string userName) => AccountDAL.Instance.RemoveAccount(userName);

		#endregion
	}
}
