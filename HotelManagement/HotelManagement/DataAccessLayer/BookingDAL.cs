using HotelManagement.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.DataAccessLayer
{
	public class BookingDAL
	{
        #region Fields & Properties
        private static BookingDAL instance;

        public static BookingDAL Instance
        {
            get { if (instance == null) instance = new BookingDAL(); return instance; }
            private set { instance = value; }
        }
        #endregion

        #region Methods
        private BookingDAL() { }

        public List<BookingDTO> GetAll()
        {
            List<BookingDTO> bookings = new List<BookingDTO>();

            string query = "Select * from BOOKING ";

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { });

            foreach (DataRow row in data.Rows)
            {
                bookings.Add(new BookingDTO(row));
            }

            return bookings;
        }

        public BookingDTO GetBooking(int id)
        {
            string query = "Select * from BOOKING " +
                "where ID = @id ";

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { id });

            if (data.Rows.Count > 0)
            {
                return new BookingDTO(data.Rows[0]);
            }

            return null;
        }

        public Rule.STATE AddNewBooking(BookingDTO booking)
        {
            string query = "Insert into BOOKING " +
                "Values( @customerId , @userName , @roomTypeId , @createDate , " +
                " @checkin , @totalDay , @total ) ";

			if (IsExistBooking(booking.CustomerId, booking.CheckinDate))
			{
				return Rule.STATE.EXIST;
			}

			if (DataProvider.Instance.ExecuteNonQuery(query,
                new object[] { booking.CustomerId, booking.Username, booking.RoomTypeId, 
                               booking.CreateDate, booking.CheckinDate, 
                               booking.TotalDay, booking.Total}) > 0)
            {
                return Rule.STATE.SUCCESS;
            }

            return Rule.STATE.FAIL;
        }

        public bool IsExistBooking(int customerId, string checkinDate)
        {
            string query = "Select Count(*) from BOOKING " +
                "where CustomerID = @customerId And " +
                "DATEADD(DD, TotalDay , CheckinDate ) >= @checkinDate ";

            if ((int)DataProvider.Instance.ExecuteScalar(query,
                new object[] { customerId, checkinDate }) > 0)
            {
                return true;
            }

            return false;
        }

        public bool RemoveBooking(int Id)
        {
            string query = "Delete from BOOKING " +
                "where ID = @id ";

            if (DataProvider.Instance.ExecuteNonQuery(query, new object[] { Id }) > 0)
                return true;

            return false;
        }

        #endregion
    }
}
