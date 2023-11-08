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

        public string GetRoomTypeName(int id)
        {
            string query = "Select * from ROOM_TYPE " +
                "where Id = @id ";

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { id });

            if (data.Rows.Count == 1)
                return new RoomTypeDTO(data.Rows[0]).Name;

            return null;
        }

        public int CountExistRoom(string roomName)
        {
            string query = "Select Count(*) from ROOM_TYPE " +
                "where Name = @name ";

            return (int)DataProvider.Instance.ExecuteScalar(query,
                new object[] { roomName });
        }

        public Rule.STATE AddNewRoomType(RoomTypeDTO room)
        {
            string query = "Insert into ROOM_TYPE " +
                "Values( @Id , @Name , @number , @price ) ";

            if (CountExistRoom(room.Name) > 0)
            {
                return Rule.STATE.EXIST;
            }

            if (DataProvider.Instance.ExecuteNonQuery(query,
                new object[] { room.Id, room.Name,
                               room.HighestNumberPeople, room.Price }) > 0)
            {
                return Rule.STATE.SUCCESS;
            }


            return Rule.STATE.FAIL;
        }

        public Rule.STATE UpdateRoomType(RoomTypeDTO room, bool isCheck = false)
        {
            string query = "UPDATE ROOM_TYPE " +
                "SET Name = @name , HighestNumberPeople = @number , Price = @price " +
                "WHERE ID = @id ";

            if (isCheck && CountExistRoom(room.Name) > 0)
            {
                return Rule.STATE.EXIST;
            }

            if (DataProvider.Instance.ExecuteNonQuery(query,
                new object[] { room.Name, room.HighestNumberPeople, room.Price, room.Id }) > 0)
            {
                return Rule.STATE.SUCCESS;
            }

            return Rule.STATE.FAIL;
        }

        public bool RemoveRoomType(int id)
        {
            string query = "Delete from ROOM_TYPE " +
                "where ID = @id ";

            if (DataProvider.Instance.ExecuteNonQuery(query, new object[] { id }) > 0)
                return true;

            return false;
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

        public int CountAll()
		{
            string query = "Select Count(*) from ROOM_TYPE";

            return (int)DataProvider.Instance.ExecuteScalar(query,
                new object[] { });
        }
        #endregion
    }
}
