using System.Data;

namespace HotelManagement.DataTransferObject
{
	public class AccountDTO
    {
		#region Fields & Properties
		public string UserName;

		public string Password;

		public string Email;

		public bool IsAvailable;

		public int RoleID;

		public int EmployeeID;
        #endregion

        #region Methods
        public AccountDTO()
        {
            UserName = "";
            Password = "";
            Email = "";
            IsAvailable = true;
            RoleID = 1;
            EmployeeID = 1;
        }
        public AccountDTO(string userName, string password, string email, bool available, int roleId, int employeeId)
        {
            UserName = userName;
            Password = password;
            Email = email;
            IsAvailable = available;
            RoleID = roleId;
            EmployeeID = employeeId;
        }

        public AccountDTO(DataRow row)
        {
            UserName = row["UserName"].ToString();
            Password = row["Password"].ToString();
            Email = row["Email"].ToString();
            IsAvailable = (bool)row["IsAvailable"];
            RoleID = (int)row["RoleID"];
            EmployeeID = (int)row["EmployeeID"];
        }

        #endregion
    }
}
