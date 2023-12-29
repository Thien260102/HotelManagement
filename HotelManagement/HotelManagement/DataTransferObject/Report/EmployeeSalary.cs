using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.DataTransferObject.Report
{
	public class EmployeeSalary
	{
		#region Fields & Properties
		public string FullName { get; set; }

		public string PhoneNumber { get; set; }

		public int Worked { get; set; }

		public int Excused { get; set; }

		public int UnExcused { get; set; }

		public decimal Salary { get; set; }

		public string Note { get; set; }
		#endregion

		#region Methods
		public EmployeeSalary()
		{

		}

		public EmployeeSalary(string fullname, string phone, int worked,
							  int excused, int unExcused, decimal salary)
		{
			FullName = fullname;
			PhoneNumber = phone;
			Worked = worked;
			Excused = excused;
			UnExcused = unExcused;
			Salary = salary;
		}

		public void CalculateSalary()
		{
			Note = $"Basic salary: {Salary}";
			decimal excused = (decimal)Rule.EXCUSED * Salary * Excused;
			Note += $"\t\tMinus leave excused: {excused}";
			decimal unExcused = (decimal)Rule.UNEXCUSED * Salary * UnExcused;
			Note += $"\t\tMinus leave unexcused: {unExcused}";

			Salary -= excused + unExcused;
		}
		#endregion
	}
}
