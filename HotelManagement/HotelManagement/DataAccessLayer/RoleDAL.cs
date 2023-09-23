using HotelManagement.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.DataAccessLayer
{
	public class RoleDAL
	{
        #region Fields & Properties
        private static RoleDAL instance;

        public static RoleDAL Instance
        {
            get { if (instance == null) instance = new RoleDAL(); return instance; }
            private set { instance = value; }
        }
        #endregion

        #region Methods
        public string GetRoleName(int id)
        {
            string query = "Select * from ROLE " +
                "where Id = @id ";

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { id });

            RoleDTO role = new RoleDTO();

            if (data.Rows.Count == 1)
                role = new RoleDTO(data.Rows[0]);

            return role.RoleName;
        }

        public int GetRoleId(string roleName)
		{
            string query = "Select * from ROLE " +
                "where RoleName = @rolename ";

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { roleName });

            if (data.Rows.Count == 1)
                return new RoleDTO(data.Rows[0]).Id;

            return 0;
		}
        #endregion
    }
}
