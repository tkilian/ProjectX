using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using as_projekt.data;

namespace AS_Projekt.helper
{
    public class Helper
    {
        public static Employee CreateEmployee(int id, String firstname, String lastname, int gender, int departmend_id)
        {
            return new Employee(id, firstname, lastname, (EmployeeGender)gender, new Department("bla"));
        }
    }
}
