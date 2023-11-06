using HotelManagement.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.DataAccessLayer
{
	public class AttendanceDAL
	{
        #region Fields & Properties
        private static AttendanceDAL instance;

        public static AttendanceDAL Instance
        {
            get { if (instance == null) instance = new AttendanceDAL(); return instance; }
            private set { instance = value; }
        }
        #endregion

        #region Methods
        private AttendanceDAL() { }

        public List<AttendanceDTO> GetAll()
        {
            List<AttendanceDTO> attendances = new List<AttendanceDTO>();

            string query = "Select * from ATTENDANCE ";

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { });

            foreach (DataRow row in data.Rows)
            {
                attendances.Add(new AttendanceDTO(row));
            }

            return attendances;
        }

        public List<AttendanceDTO> GetAll(int employeeId)
        {
            List<AttendanceDTO> attendances = new List<AttendanceDTO>();

            string query = "Select * from ATTENDANCE " +
                "Where EmployeeID = @employeeid ";

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { employeeId });

            foreach (DataRow row in data.Rows)
            {
                attendances.Add(new AttendanceDTO(row));
            }

            return attendances;
        }

        public AttendanceDTO GetAttendance(int id)
        {
            string query = "Select * from ATTENDANCE " +
                "where ID = @id ";

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { id });

            if (data.Rows.Count > 0)
            {
                return new AttendanceDTO(data.Rows[0]);
            }

            return null;
        }

        public Rule.STATE AddNewAttendace(AttendanceDTO attendace)
        {
            string query = "Insert into ATTENDANCE " +
                "Values( @employeeID , @Date , @State , @Note ) ";

            if (IsExistDate(attendace.EmployeeId, attendace.Date))
            {
                return Rule.STATE.EXIST;
            }

            if (DataProvider.Instance.ExecuteNonQuery(query,
                new object[] { attendace.EmployeeId, attendace.Date,
                               attendace.State, attendace.Note }) > 0)
            {
                return Rule.STATE.SUCCESS;
            }

            return Rule.STATE.FAIL;
        }

        public bool IsExistDate(int employeeId, string date)
        {
            string query = "Select Count(*) from ATTENDANCE " +
                "where Date = @date And EmployeeID = @employeeId ";

            if ((int)DataProvider.Instance.ExecuteScalar(query,
                new object[] { date, employeeId }) > 0)
            {
                return true;
            }

            return false;
        }

        public bool RemoveAttendance(int Id)
        {
            string query = "Delete from ATTENDANCE " +
                "where Id = @id ";

            if (DataProvider.Instance.ExecuteNonQuery(query, new object[] { Id }) > 0)
                return true;

            return false;
        }

        public Rule.STATE UpdateAttendance(AttendanceDTO attendace, bool isCheck)
        {
            string query = "UPDATE ATTENDANCE " +
                "SET Date = @date , State = @state , Note = @note " +
                "WHERE ID = @id ";

            if (isCheck && IsExistDate(attendace.EmployeeId, attendace.Date))
			{
                return Rule.STATE.EXIST;
			}

            if (DataProvider.Instance.ExecuteNonQuery(query,
                new object[] { attendace.Date, attendace.State,
                               attendace.Note, attendace.Id }) > 0)
            {
                return Rule.STATE.SUCCESS;
            }

            return Rule.STATE.FAIL;
        }
        #endregion
    }
}
