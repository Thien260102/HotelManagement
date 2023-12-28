using HotelManagement.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.DataAccessLayer
{
	public class RoomDAL
	{
        #region Fields & Properties
        private static RoomDAL instance;

        public static RoomDAL Instance
        {
            get { if (instance == null) instance = new RoomDAL(); return instance; }
            private set { instance = value; }
        }
        #endregion

        #region Methods
        public List<RoomDTO> GetAll()
        {
            List<RoomDTO> rooms = new List<RoomDTO>();

            string query = "Select * from ROOM ";

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { });

            foreach (DataRow element in data.Rows)
            {
                rooms.Add(new RoomDTO(element));
            }

            return rooms;
        }

        public List<RoomDTO> GetAll(Rule.ROOM_STATE state)
        {
            List<RoomDTO> rooms = new List<RoomDTO>();

            string query = "Select * from ROOM " +
                "Where State = @state ";

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { (int)state });

            foreach (DataRow element in data.Rows)
            {
                rooms.Add(new RoomDTO(element));
            }

            return rooms;
        }

        public RoomDTO GetRoomInfor(int id)
		{
            string query = "Select * from ROOM " +
                "where ID = @id ";

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { id });

            if (data.Rows.Count == 1)
                return new RoomDTO(data.Rows[0]);

            return null;
        }

        public int CountExistRoom(string roomName, int floor)
        {
            string query = "Select Count(*) from ROOM " +
                "where Name = @name and FloorNumber = @floor ";

            return (int)DataProvider.Instance.ExecuteScalar(query,
                new object[] { roomName, floor });
        }

        public int CountRoomState(Rule.ROOM_STATE state)
		{
            string query = "Select Count(*) from ROOM " +
                "where State = @state ";

            return (int)DataProvider.Instance.ExecuteScalar(query,
                new object[] { state });
        }

        public Rule.STATE AddNewRoom(RoomDTO room)
        {
            string query = "Insert into ROOM " +
                "Values( @Name , @floor , @State , @TypeId , @Note ) ";

            if (CountExistRoom(room.Name, room.Floor) > 0)
            {
                return Rule.STATE.EXIST;
            }

            if (DataProvider.Instance.ExecuteNonQuery(query,
                new object[] { room.Name, room.Floor, 
                               room.State, room.RoomTypeId, room.Note }) > 0)
            {
                return Rule.STATE.SUCCESS;
            }


            return Rule.STATE.FAIL;
        }

        public Rule.STATE UpdateRoom(RoomDTO room, bool isCheck)
        {
            string query = "UPDATE ROOM " +
                "SET Name = @name , FloorNumber = @floor , State = @state , TypeID = @typeId , Note = @note " +
                "WHERE ID = @id ";

            if (isCheck && CountExistRoom(room.Name, room.Floor) > 0)
            {
                return Rule.STATE.EXIST;
            }

            if (DataProvider.Instance.ExecuteNonQuery(query,
                new object[] { room.Name, room.Floor, room.State, 
                               room.RoomTypeId, room.Note, room.Id }) > 0)
            {
                return Rule.STATE.SUCCESS;
            }

            return Rule.STATE.FAIL;
        }

        public bool RemoveRoom(int id)
        {
            string query = "Delete from ROOM " +
                "where ID = @id ";

            if (DataProvider.Instance.ExecuteNonQuery(query, new object[] { id }) > 0)
                return true;

            return false;
        }

        public int CountRoomWith(int typeId)
		{
            string query = "Select Count(*) from ROOM " +
                "where TypeID = @typeId ";

            return (int)DataProvider.Instance.ExecuteScalar(query,
                new object[] { typeId });
        }
        #endregion
    }
}
