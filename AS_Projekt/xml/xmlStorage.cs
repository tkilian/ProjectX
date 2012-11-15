using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using as_projekt.data;
using AS_Projekt.interfaces;

namespace AS_Projekt.xml
{
    class xmlStorage : AS_Projekt.interfaces.IStore
    {

        XmlDocument employeesDoc = new XmlDocument();
        XmlDocument departmentsDoc = new XmlDocument();
        XmlElement departmentsRoot = null;
        XmlElement employeesRoot = null;

        public xmlStorage()
        {
            setup();
        }

        private void setup()
        {
          departmentsDoc.Load(@"..\\..\\data\\xml\\departments.xml");
            departmentsRoot = departmentsDoc.DocumentElement;
            employeesDoc.Load(@"..\\..\\data\\xml\\employees.xml");
            employeesRoot = employeesDoc.DocumentElement;          
        }

        public bool insertEmployee(Employee employee) 
        {

          XmlNode emp = employeesDoc.CreateElement("employee");
          XmlAttribute empId = employeesDoc.CreateAttribute("Id");
          empId.Value = Convert.ToString(employee.Id);
          XmlAttribute empFirstname = employeesDoc.CreateAttribute("Firstname");
          empFirstname.Value = employee.Firstname;
          XmlAttribute empLastname = employeesDoc.CreateAttribute("Lastname");
          empLastname.Value = employee.Lastname;
          XmlAttribute empGender = employeesDoc.CreateAttribute("Gender");
          empGender.Value = Convert.ToString(employee.Gender);
          XmlAttribute empDepartment = employeesDoc.CreateAttribute("Department");
          empDepartment.Value = Convert.ToString(employee.Department.Id);
          emp.Attributes.Append(empId);
          emp.Attributes.Append(empFirstname);
          emp.Attributes.Append(empLastname); 
          emp.Attributes.Append(empDepartment);
          employeesRoot.AppendChild(emp);
          employeesDoc.Save(@"..\\..\\data\\xml\\employees.xml");

          return true; 
        }

        public bool updateEmployee(Employee employee) 
        {
          deleteEmployeeById(employee.Id);
          insertEmployee(employee);
          return true;
        }

        public bool deleteEmployeeById(int id) 
        {

          
          foreach (XmlNode employee in employeesRoot.ChildNodes)
          {
            if (Convert.ToInt32(employee.Attributes["Id"].InnerText) == id)
            {
              employeesRoot.RemoveChild(employee);
              employeesDoc.Save(@"..\\..\\data\\xml\\employees.xml");
              return true;
            }
          }
          return false;
         
        }


        public Employee getEmployeeById(int id) { 

            Employee emp = new Employee (1, "a", "f", new EmployeeGender(), new Department("test"));
            List<Employee> employees = getAllEmployees();
            foreach (Employee employee in employees)
            {
                if (employee.Id == id)
                    emp = employee;
            }
            
            return emp;
        }
        public List<Employee> getAllEmployees()
        {
            List<Employee> employees = new List<Employee>();
            foreach (XmlNode employee in employeesRoot.ChildNodes) 
            {
                
              employees.Add(new Employee(employee.Attributes["Firstname"].InnerText,employee.Attributes["Lastname"].InnerText,  (EmployeeGender)Convert.ToInt32(employee.Attributes["Gender"].InnerText), getDepartmentById(Convert.ToInt32(employee.Attributes["Id"].InnerText))));
              
            }

            return employees;
        }

        public bool insertDepartment(Department department)
        {
           
                XmlNode dep = departmentsDoc.CreateElement("department");
                XmlAttribute depId = departmentsDoc.CreateAttribute("Id");
                depId.Value = Convert.ToString(department.Id);
                XmlAttribute depName = departmentsDoc.CreateAttribute("Name");
                depName.Value= department.Name;
                dep.Attributes.Append(depId);
                dep.Attributes.Append(depName);
                departmentsRoot.AppendChild(dep);
                departmentsDoc.Save(@"..\\..\\data\\xml\\departments.xml");
                return true;
        }

        public bool updateDepartment(Department department)
        {
            if (department.Id == 0) throw new ArgumentException();
            deleteDepartmentById(department.Id);
            insertDepartment(department);

          /*
            foreach (XmlNode dep in departmentsRoot.ChildNodes)
            {
                if (Convert.ToInt32(dep.Attributes["Id"].InnerText) == department.Id)
                {
                    dep.Attributes["Name"].InnerText = department.Name;
                  
                    departmentsDoc.Save(@"..\\..\\data\\xml\\departments.xml");
                    return true;
                }
            }
            return false;
           * */
            return true;
        }

        public bool deleteDepartmentById(int id)
        {
            List<Department> departments = getAllDepartments();
            foreach (XmlNode employee in employeesRoot.ChildNodes)
            {
                if (Convert.ToInt32(employee.Attributes["Department"].InnerText) == id)
                {
                    employee.Attributes["Department"].InnerText = null; 
                    employeesDoc.Save(@"..\\..\\data\\xml\\employees.xml");
                    
                }
            }
     
            foreach (XmlNode department in departmentsRoot.ChildNodes)
            {
                if (Convert.ToInt32(department.Attributes["Id"].InnerText) == id)
                {
                    departmentsRoot.RemoveChild(department);
                    departmentsDoc.Save(@"..\\..\\data\\xml\\departments.xml");
                    return true;
                }
            }
           return false;
        }

        public Department getDepartmentById(int id)
        {
            Department department = null;
            List<Department> departments = getAllDepartments();
            foreach (Department dep in departments)
            {
                if (dep.Id == id)
                    department = dep;
            }
            return department;
        }

        public List<Department> getAllDepartments()
        {
            
            List<Department> departments = new List<Department>();
            foreach (XmlNode department in departmentsRoot.ChildNodes) 
            { 
                departments.Add(new Department(Convert.ToInt32(department.Attributes["Id"].InnerText),department.Attributes["Name"].InnerText));
            }
            return departments;
        }

    }
}


