using HotelManagement.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.DataAccessLayer
{
	public class RoomTypeDAL
	{
        #region Fields & Properties
        private static RoomTypeDAL instance;

        public static RoomTypeDAL Instance
        {
            get { if (instance == null) instance = new RoomTypeDAL(); return instance; }
            private set { instance = value; }
        }
        #endregion

        #region Methods
        public string GetRoleName(int id)
        {
            string query = "Select * from ROOM_TYPE " +
                "where Id = @id ";

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { id });

            if (data.Rows.Count == 1)
                return new RoomTypeDTO(data.Rows[0]).Name;

            return null;
        }

        public int GetRoomTypeID(string roomTypeName)
        {
            string query = "Select * from ROOM_TYPE " +
                "where Name = @name ";

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { roomTypeName });

            if (data.Rows.Count == 1)
                return new RoomTypeDTO(data.Rows[0]).Id;

            return 0;
        }

        public List<RoomTypeDTO> GetAll()
		{
            List<RoomTypeDTO> roomTypes = new List<RoomTypeDTO>();

            string query = "Select * from ROOM_TYPE ";

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { });

            foreach (DataRow element in data.Rows)
            {
                roomTypes.Add(new RoomTypeDTO(element));
            }

            return roomTypes;
        }
        #endregion
    }
}
