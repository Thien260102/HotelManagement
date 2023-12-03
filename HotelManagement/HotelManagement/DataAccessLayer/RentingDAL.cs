using HotelManagement.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Data;

namespace HotelManagement.DataAccessLayer
{
	public class RentingDAL
	{
        #region Fields & Properties
        private static RentingDAL instance;

        public static RentingDAL Instance
        {
            get { if (instance == null) instance = new RentingDAL(); return instance; }
            private set { instance = value; }
        }
        #endregion

        #region Methods
        private RentingDAL() { }

        public List<RentingDTO> GetAll()
        {
            List<RentingDTO> rentings = new List<RentingDTO>();

            string query = "Select * from RENTING ";

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { });

            foreach (DataRow row in data.Rows)
            {
                rentings.Add(new RentingDTO(row));
            }

            return rentings;
        }

        public RentingDTO GetRenting(int id)
        {
            string query = "Select * from RENTING " +
                "where ID = @id ";

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { id });

            if (data.Rows.Count > 0)
            {
                return new RentingDTO(data.Rows[0]);
            }

            return null;
        }

        public Rule.STATE AddNewRenting(RentingDTO renting)
        {
            string query = "Insert into RENTING " +
                "Values( @customerId , @userName , @roomId , @createDate , " +
                " @checkinDate , @totalDay , @checkoutDate , @total , @isPaid ) ";

            //if (IsExistBooking(booking.CustomerId, booking.CheckinDate))
            //{
            //	return Rule.STATE.EXIST;
            //}

            if (DataProvider.Instance.ExecuteNonQuery(query,
                new object[] { renting.CustomerId, renting.Username, renting.RoomId,
                               renting.CreateDate, renting.CheckinDate,
                               renting.TotalDay, renting.CheckoutDate, renting.Total, renting.IsPaid }) > 0)
            {
                return Rule.STATE.SUCCESS;
            }

            return Rule.STATE.FAIL;
        }

        public bool IsExistRenting(int customerId, string checkinDate)
        {
            string query = "Select Count(*) from Renting " +
                "where CustomerID = @customerId And " +
                "DATEADD(DD, TotalDay , CheckinDate ) >= @checkinDate ";

            if ((int)DataProvider.Instance.ExecuteScalar(query,
                new object[] { customerId, checkinDate }) > 0)
            {
                return true;
            }

            return false;
        }

        public bool RemoveRenting(int Id)
        {
            string query = "Delete from RENTING " +
                "where ID = @id ";

            if (DataProvider.Instance.ExecuteNonQuery(query, new object[] { Id }) > 0)
                return true;

            return false;
        }

        #endregion
    }
}
