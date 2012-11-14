using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using as_projekt.data;

namespace AS_Projekt.interfaces
{
  interface IStore
  {
    bool insertEmployee(Employee employee);
    bool updateEmployee(Employee employee);
    bool deleteEmployeeById(int id);
    Employee getEmployeeById(int id);
    List<Employee> getAllEmployees();

    bool insertDepartment(Department department);
    bool updateDepartment(Department department);
    bool deleteDepartmentById(int id);
    Department getDepartmentById(int id);
    List<Department> getAllDepartments();
  }
}
