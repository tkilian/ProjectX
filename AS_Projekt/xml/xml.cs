using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using as_projekt.data;


namespace AS_Projekt.xml
{
    class xml
    {

        XmlDocument employeesDoc = new XmlDocument();
        XmlDocument departmentsDoc = new XmlDocument();
        XmlElement departmentsRoot = null;
        XmlElement employeesRoot = null;

        public xml()
        {
            setup();
        }

        private void setup()
        {
            departmentsDoc.Load(@"departments.xml");
            departmentsRoot = departmentsDoc.DocumentElement;
            employeesDoc.Load(@"employees.xml");
            employeesRoot = employeesDoc.DocumentElement;          
        }

        public bool insertEmployee(Employee employee) { return true; }

        public bool updateEmployee(Employee employee) { return true; }

        public bool deleteEmployeeById(int id) { return true; }

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
                
                
               // employees.Add(new Employee(employee.Attributes["Firstname"].InnerText,employee.Attributes["Lastname"].InnerText,  employee.Attributes["Gender"].InnerText), getDepartmentById(Convert.ToInt32(employee.Attributes["Id"].InnerText););
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
                return true;
        }

        public bool updateDepartment(Department department)
        {
            if (department.Id == 0) throw new ArgumentException();
            foreach (XmlNode dep in departmentsRoot.ChildNodes)
            {
                if (Convert.ToInt32(dep.Attributes["Id"].InnerText) == department.Id)
                {
                    dep.Attributes["Name"].InnerText = department.Name;
                    return true;
                }
            }
            return false;
        }

        public bool deleteDepartmentById(int id)
        {
            List<Department> departments = getAllDepartments();
            foreach (XmlNode department in departmentsRoot.ChildNodes)
            {
                if (Convert.ToInt32(department.Attributes["Id"].InnerText) == id)
                {
                    departmentsRoot.RemoveChild(department);
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


