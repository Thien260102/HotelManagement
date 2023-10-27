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
	public class RoomTypeBLL
	{
		#region Fields & Properties


		#endregion

		#region Methods
		public List<RoomTypeDTO> GetAllRoomTypes() => RoomTypeDAL.Instance.GetAll();
		
		#endregion
	}
}
