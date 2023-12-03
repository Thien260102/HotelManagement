using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.DataAccessLayer
{
    public class DataProvider
    {
        #region Fields & Properties
        private string connectionSTR = "Data Source=MSI;Initial Catalog=HotelManagement;Integrated Security=True";

        private static DataProvider instance;
        public static DataProvider Instance
        {
            get { if (instance == null) instance = new DataProvider(); return instance; }
            private set { DataProvider.instance = value; }
        }
		#endregion

		#region Methods

		private DataProvider() { }

        // data
        public DataTable ExecuteQuery(string query, object[] parameter = null)
        {
            DataTable data = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);


                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');

                    int i = 0;
                    foreach (string item in listPara)
                        if (item.Contains('@'))
                            command.Parameters.AddWithValue(item, parameter[i++]);


                }

                SqlDataAdapter adapter = new SqlDataAdapter(command);

                adapter.Fill(data);

                connection.Close();
            }

            return data;
        }

        // number of row affected
        public int ExecuteNonQuery(string query, object[] parameter = null)
        {
            int data = 0;

            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);


                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');

                    int i = 0;
                    foreach (string item in listPara)
                        if (item.Contains('@'))
                            command.Parameters.AddWithValue(item, parameter[i++]);


                }

                data = command.ExecuteNonQuery();

                connection.Close();
            }

            return data;
        }

        //để sử dụng cho câu lệnh select count(*)
        public object ExecuteScalar(string query, object[] parameter = null)
        {
            object data = 0;

            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);


                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');

                    int i = 0;
                    foreach (string item in listPara)
                        if (item.Contains('@'))
                            command.Parameters.AddWithValue(item, parameter[i++]);


                }

                data = command.ExecuteScalar();

                connection.Close();
            }

            return data;
        }

		#endregion
	}
}
