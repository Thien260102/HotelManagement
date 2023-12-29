using HotelManagement.DataAccessLayer;
using HotelManagement.DataTransferObject;
using HotelManagement.DataTransferObject.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HotelManagement.BusinessLogicLayer
{
	public class EmployeeSalaryReportBLL
	{
		public List<EmployeeSalary> CalculateEmployeeSalaryMonth(int month, int year)
		{
			List<EmployeeSalary> reports = new List<EmployeeSalary>();

			var employees = new EmployeeBLL().GetAllEmployees(month, year);
			var attendances = new AttendanceBLL().GetAll(month, year);

			foreach (var employee in employees)
			{
				var employeeSalary = new EmployeeSalary();
				employeeSalary.FullName = employee.FullName;
				employeeSalary.PhoneNumber = employee.PhoneNumber;

				employeeSalary.Worked = 
					attendances.Count(element => element.EmployeeId == employee.Id
											  && element.State == (int)Rule.ATTENDANCE.WORKED);
				employeeSalary.Excused =
					attendances.Count(element => element.EmployeeId == employee.Id
											  && element.State == (int)Rule.ATTENDANCE.EXCUSED);
				employeeSalary.UnExcused = Utilities.GetDayInMonth(month, year) - (employeeSalary.Worked + employeeSalary.Excused) - 4;
				
				employeeSalary.Salary = employee.Salary;
				employeeSalary.CalculateSalary();

				reports.Add(employeeSalary);
			}

			return reports;
		}
	}
}
