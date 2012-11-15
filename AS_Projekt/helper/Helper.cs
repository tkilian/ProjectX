using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using as_projekt.data;
using AS_Projekt.db;

namespace AS_Projekt.helper
{
    public class Helper
    {
        public static Employee CreateEmployee(int id, String firstname, String lastname, int gender, Int32 department_id, String department_name)
        {
            Department employeeDepartment = null;
            if (department_id != 0)
            {
                employeeDepartment = new Department(department_id, department_name);
            }
            return new Employee(id, firstname, lastname, (EmployeeGender)gender, employeeDepartment);
        }

        public static Department CreateDepartment(int id, String name)
        {
            return new Department(id, name);
        }
    }
}
