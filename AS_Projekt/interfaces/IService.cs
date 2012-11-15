using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using as_projekt.data;

namespace AS_Projekt.interfaces
{
	public interface IService
	{
		bool insertDepartment(Department department);
		bool deleteDepartment(int id);
		bool updateDepartment(Department department);
		Department getDepartment(int id);
		List<Department> getDepartments();
		bool insertEmployee(Employee employee);
		bool deleteEmployee(int id);
		bool updateEmployee(Employee employee);
		Employee getEmployee(int id);
		List<Employee> getEmployees();
	}
}
