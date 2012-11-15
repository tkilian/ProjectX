using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using as_projekt.data;
using AS_Projekt.interfaces;

namespace AS_Projekt.services
{
  class BusinessLogic : IService
  {
    private IStore store;

    public BusinessLogic(IStore store)
    {
      this.store = store;
    }

    public bool insertDepartment(Department department)
    {
      return store.insertDepartment(department);
    }

    public bool deleteDepartment(int id)
    {
      return store.deleteDepartmentById(id);
    }

    public bool updateDepartment(Department department)
    {
      return store.updateDepartment(department);
    }

    public Department getDepartment(int id)
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

    public bool deleteEmployee(int id)
    {
      return store.deleteEmployeeById(id);
    }

    public bool updateEmployee(Employee employee)
    {
      return store.updateEmployee(employee);
    }

    public Employee getEmployee(int id)
    {
      return store.getEmployeeById(id);
    }

    public List<Employee> getEmployees()
    {
      return store.getAllEmployees();
    }
  }
}
