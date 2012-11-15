using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using as_projekt.data;
using AS_Projekt.interfaces;

namespace AS_Projekt.services
{
  class Bll
  {
    private IStore store;

    public Bll(IStore store)
    {
        this.store = store;
    }

    public bool insertDepartment(Department department)
    {
      return store.insertDepartment(department);
    }

    public bool deleteDepartment(Department department)
    {
      return store.deleteDepartmentById(department.Id);
    }

    public bool updateDepartment(Department department)
    {
      return store.updateDepartment(department);
    }

    public Department getDepartmentById(int id)
    {
      return store.getDepartmentById(id);
    }

    public List<Department> getDepartments()
    {
      return store.getAllDepartments();
    }

    public bool insertEmployee(Employee employee)
    {
      return store.insertEmployee(employee);
    }

    public bool deleteEmployee(Employee employee)
    {
      return store.deleteEmployeeById(employee.Id);
    }

    public bool updateEmployee(Employee employee)
    {
      return store.updateEmployee(employee);
    }

    public Employee getEmployeeById(int id)
    {
      return store.getEmployeeById(id);
    }

    public List<Employee> getEmployees()
    {
      return store.getAllEmployees();
    }
  }
}
