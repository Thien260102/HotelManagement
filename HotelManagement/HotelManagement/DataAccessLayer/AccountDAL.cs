using HotelManagement.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.DataAccessLayer
{
	public class AccountDAL
	{
		#region Fields & Properties
		private static AccountDAL instance;

        public static AccountDAL Instance
        {
            get { if (instance == null) instance = new AccountDAL(); return instance; }
            private set { instance = value; }
        }
        #endregion

        #region Methods
        public AccountDTO GetAccount(string userName)
        {
            string query = "Select * from Account " +
                "where UserName = @userName ";

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { userName });

            AccountDTO account = new AccountDTO();

            if (data.Rows.Count == 1)
                account = new AccountDTO(data.Rows[0]);

            if (account.UserName != userName)
                account = new AccountDTO();
            return account;
        }

        public Rule.STATE AddNewAccount(AccountDTO account)
        {
            string query = "Insert into ACCOUNT " +
                "Values( @userName , @pass , @email , @available , @roleId , @employeeID ) ";

            if(IsExistUserNameOrEmail(account.UserName, account.Email))
			{
                return Rule.STATE.EXIST;
			}

            if (DataProvider.Instance.ExecuteNonQuery(query,
                new object[] { account.UserName, account.Password, account.Email
                             , account.IsAvailable, account.RoleID, account.EmployeeID }) > 0)
            {
                return Rule.STATE.SUCCESS;
            }


            return Rule.STATE.FAIL;
        }

        public bool IsExistUserNameOrEmail(string userName, string email)
		{
            string query = "Select * from ACCOUNT " +
                "where UserName = @username or Email = @email ";

            if (DataProvider.Instance.ExecuteNonQuery(query,
                new object[] { userName, email }) > 0)
            {
                return true;
            }

            return false;
		}
        #endregion
    }
}
