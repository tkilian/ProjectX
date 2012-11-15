using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AS_Projekt.interfaces;
using as_projekt.data;

namespace AS_Projekt
{
    class ShellOutput
    {
        int selectionInt = 0;
        IStore store;
        // Constructor
        public ShellOutput()
        {
            //Init
            //store = StoreFactory.CreateStore("MSSQL");
            store = StoreFactory.CreateStore("SQL");
            ConsoleManager.ConsoleManage.Show();
            Console.WriteLine("### Employee / Department - Managementsystem ###");
            InitSelection();
            //Console.ReadLine();
        }
        public void InitSelection()
        {
            do
            {
                Console.WriteLine("Please choose the task.");
                Console.WriteLine("Get employee by ID = 1");
                Console.WriteLine("Get department by ID = 2");
                Console.WriteLine("Insert employee = 3");
                Console.WriteLine("Insert department ID = 4");
                Console.WriteLine("Delete employee by ID = 5");
                Console.WriteLine("Delete department by ID = 6");
                Console.WriteLine("Show all employees = 7");
                Console.WriteLine("Show all departments = 8");
                Console.WriteLine("Quit programm = 9");
                string selectionString = Console.ReadLine();
                //try to parse string to int
                bool success = int.TryParse(selectionString, out selectionInt);

                switch (selectionInt)
                {
                    case 1:
                        Console.WriteLine("You selected " + selectionString);
                        Console.Clear();
                        GetEmplByID();
                        break;
                    case 2:
                        Console.WriteLine("You selected " + selectionString);
                        GetDepByID();
                        break;
                    case 3:
                        Console.WriteLine("You selected " + selectionString);
                        Console.Clear();
                        InsertEmpl();
                        break;
                    case 4:
                        Console.WriteLine("You selected " + selectionString);
                        Console.Clear();
                        InsertDep();
                        break;
                    case 5:
                        Console.WriteLine("You selected " + selectionString);
                        Console.Clear();
                        DelEmp();
                        break;
                    case 6:
                        Console.WriteLine("You selected " + selectionString);
                        Console.Clear();
                        DelDep();
                        break;
                    case 7:
                        Console.WriteLine("You selected " + selectionString);
                        Console.Clear();
                        ShowAllEmp();
                        break;
                    case 8:
                        Console.WriteLine("You selected " + selectionString);
                        Console.Clear();
                        ShowAllDep();
                        break;
                    case 9:
                        Console.WriteLine("You selected " + selectionString + ". Shutting down ...");
                        System.Threading.Thread.Sleep(1000);
                        break;
                    default:
                        Console.WriteLine("Invalid selection. Please select 1, 2, 3....");
                        break;
                }
            } while (selectionInt != 5);
        }

        private void ShowAllDep()
        {
            List<Department> listDeps = store.getAllDepartments();

            foreach (Department d_temp in listDeps)
                Console.WriteLine("ID: " + d_temp.Id + " Name: " + d_temp.Name);
        }

        private void ShowAllEmp()
        {
            List<Employee> listEmpl = store.getAllEmployees();

            foreach (Employee d_temp in listEmpl)
                Console.WriteLine("ID: " + d_temp.Id + " Lastname: " + d_temp.Lastname + " Firstname: " +d_temp.Firstname + " Department: "  + d_temp.Department);
        }// end InitSelection

        private void DelDep()
        {
            Console.WriteLine("Which Department do u want to delete?");
            List<Department> listDeps = store.getAllDepartments();

            foreach (Department d_temp in listDeps)
                Console.WriteLine(d_temp.Id + " -> " + d_temp.Name);

            string selDepID = Console.ReadLine();
            int intselID;
            bool successDepID = int.TryParse(selDepID, out intselID);          
            store.deleteDepartmentById(intselID);
            Console.WriteLine("Department deleted");
        }

        private void DelEmp()
        {
            Console.WriteLine("Which Employee do u want to delete? Kackn00b!");
            List<Employee> listEmpl = store.getAllEmployees();
            foreach (Employee d_temp in listEmpl)
                Console.WriteLine(d_temp.Id + " -> " + d_temp.Lastname + " " + d_temp.Firstname);

            string selEmpID = Console.ReadLine();
            int intselID;
            bool successDepID = int.TryParse(selEmpID, out intselID);
            store.deleteEmployeeById(intselID);
            Console.WriteLine("Employee deleted");
        } 

        public void GetEmplByID()
        {
            int selEmplID;
            Console.WriteLine("Input ID.");
            Console.ReadLine();
            string selectionEmplIDString = Console.ReadLine();
            //try to parse string to int
            bool successEmplID = int.TryParse(selectionEmplIDString, out selEmplID);
            Employee d_temp = store.getEmployeeById(selEmplID);
            if( d_temp != null )
                Console.WriteLine(d_temp.ToString());
            else
                Console.WriteLine("Department not found!");
            
        }

        public void GetDepByID()
        {
            int selDepID;
            Console.WriteLine("Input ID.");
            string selectionEmplIDString = Console.ReadLine();
            //try to parse string to int
            bool successEmplID = int.TryParse(selectionEmplIDString, out selDepID);
            store.getDepartmentById(selDepID);
            
        }

        public void InsertEmpl()
        {

            int selectionIntGender;
            Console.WriteLine("Input firstname:");     
            string firstname = Console.ReadLine();
            Console.WriteLine("Input lastname:");
            string lastname = Console.ReadLine();
            bool successGender = false;
            do
            {
                Console.WriteLine("Choose gender: {0} = Male | {1} = Female", (int)EmployeeGender.Male, (int)EmployeeGender.Female);
                string selectionGender = Console.ReadLine();
                successGender = int.TryParse(selectionGender, out selectionIntGender);
                switch (selectionIntGender)
                {
                    case 0:
                    case 1:
                        successGender = successGender && true;
                        break;
                    default:
                        successGender = false;
                        break;
                }

                if (!successGender)
                {
                    Console.WriteLine("Invalid Inp-ut - try again");
                    Console.WriteLine("Choose gender: {0} = Male | {1} = Female", (int)EmployeeGender.Male, (int)EmployeeGender.Female);
                }
            } while (!successGender);
 
            Console.WriteLine("Input department or create a new one:");
            List<Department> listDeps = store.getAllDepartments();

            foreach (Department d_temp in listDeps)
                Console.WriteLine(d_temp.Id + " -> " + d_temp.Name);

            if (listDeps.Count == 0)
                Console.WriteLine("No Departmens found - Create a new one!");

            string selDepID = Console.ReadLine();
            int intselID;
            bool successDepID = int.TryParse(selDepID, out intselID);

            Department result = null;
            if( successDepID ) 
            {
                foreach (Department d_temp in listDeps)
                {
                    if (d_temp.Id == intselID)
                    {
                        result = d_temp;
                        break;
                    }
                }
            }
            else 
                result = new Department(selDepID);

            Employee emp = new Employee(0, firstname, lastname, (EmployeeGender)selectionIntGender, result);
            store.insertEmployee(emp);
            Console.WriteLine("Employee insertet");
        }//Ende InsertEmpl

        public void InsertDep()
        {
            Console.WriteLine("Input departmentname");
            string depname = Console.ReadLine();
            Department dep = new Department(depname);
            store.insertDepartment(dep);
            Console.WriteLine("Department inserted");
        }
    }
}
