using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.DataTransferObject
{
	public class RoleDTO
	{
		#region Fields & Properties
		public int Id;

		public string RoleName;
		#endregion

		#region Methods

		public RoleDTO()
		{

		}

		public RoleDTO(int id, string roleName)
		{
			Id = id;
			RoleName = roleName;
		}

		public RoleDTO(DataRow row)
		{
			Id = (int)row["ID"];
			RoleName = row["RoleName"].ToString();
		}
		#endregion
	}
}
