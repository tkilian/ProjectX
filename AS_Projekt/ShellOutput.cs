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
        IService service;
        // Constructor
        public ShellOutput(IService service)
        {
            //Init
            this.service = service;
            
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
                        Console.Clear();
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
                        Console.WriteLine("Invalid selection. Please select 1, 2, 3, ..., 9");
                        break;
                }
            } while (selectionInt != 9);
        }

        private void ShowAllDep()
        {
            List<Department> listDeps = service.getDepartments();
            if(listDeps.Count == 0)
                Console.WriteLine("No Departments found!");
            else
                foreach (Department d_temp in listDeps)
                    Console.WriteLine("ID: " + d_temp.Id + " Name: " + d_temp.Name);

            Console.WriteLine("Press Enter to go back to mainmenu");
            Console.ReadLine();
            Console.Clear();
        }

        private void ShowAllEmp()
        {
            List<Employee> listEmpl = service.getEmployees();
            
            if (listEmpl.Count == 0)
                Console.WriteLine("No Emloyees found!");
            else
                foreach (Employee d_temp in listEmpl)
                    Console.WriteLine("ID: " + d_temp.Id + " Lastname: " + d_temp.Lastname + " Firstname: " +d_temp.Firstname + " Department: "  + d_temp.Department);
            
            Console.WriteLine("Press Enter to go back to mainmenu");
            Console.ReadLine();
            Console.Clear();
        }// end InitSelection

        private void DelDep()
        {
            Console.WriteLine("Which Department do u want to delete?");
            List<Department> listDeps = service.getDepartments();

            foreach (Department d_temp in listDeps)
                Console.WriteLine(d_temp.Id + " -> " + d_temp.Name);

            string selDepID = Console.ReadLine();
            int intselID;
            bool successDepID = int.TryParse(selDepID, out intselID);          
            service.deleteDepartment(intselID);
            Console.WriteLine("Department deleted");
            Console.WriteLine("Press Enter to go back to mainmenu");
            Console.ReadLine();
            Console.Clear();
        }

        private void DelEmp()
        {
            Console.WriteLine("Which Employee do u want to delete?");
            List<Employee> listEmpl = service.getEmployees();
            foreach (Employee d_temp in listEmpl)
                Console.WriteLine(d_temp.Id + " -> " + d_temp.Lastname + " " + d_temp.Firstname);

            string selEmpID = Console.ReadLine();
            int intselID;
            bool successDepID = int.TryParse(selEmpID, out intselID);
            service.deleteEmployee(intselID);
            Console.WriteLine("Employee deleted");
            Console.WriteLine("Press Enter to go back to mainmenu");
            Console.ReadLine();
            Console.Clear();
        } 

        public void GetEmplByID()
        {
            int selEmplID;
            Console.WriteLine("Input ID.");
            string selectionEmplIDString = Console.ReadLine();
            //try to parse string to int
            bool successEmplID = int.TryParse(selectionEmplIDString, out selEmplID);
            Employee d_temp = service.getEmployee(selEmplID);
            if( d_temp != null )
                Console.WriteLine(d_temp.Lastname + ", " + d_temp.Firstname + ", " + d_temp.Gender.ToString() + " -> " + d_temp.Department.Id);
            else
                Console.WriteLine("Employee not found!");
            
            Console.WriteLine("Press Enter to go back to mainmenu");
            Console.ReadLine();
            Console.Clear();
            
        }

        public void GetDepByID()
        {
            int selDepID;
            Console.WriteLine("Input ID.");
            string selectionEmplIDString = Console.ReadLine();
            //try to parse string to int
            bool successEmplID = int.TryParse(selectionEmplIDString, out selDepID);
            Department d_temp = service.getDepartment(selDepID);
            if (d_temp != null)
                Console.WriteLine(d_temp.Id + ", " + d_temp.Name);
            else
                Console.WriteLine("Department not found!");
            
            Console.WriteLine("Press Enter to go back to mainmenu");
            Console.ReadLine();
            Console.Clear();
            
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
                    case ((int) EmployeeGender.Male):
                    case ((int) EmployeeGender.Female):
                        successGender = successGender && true;
                        break;
                    default:
                        successGender = false;
                        break;
                }

                if (!successGender)
                {
                    Console.WriteLine("Invalid Input - try again");
                    Console.WriteLine("Choose gender: {0} = Male | {1} = Female", (int)EmployeeGender.Male, (int)EmployeeGender.Female);
                }
            } while (!successGender);

            Console.WriteLine("Input department or create a new one:");
            List<Department> listDeps = service.getDepartments();

            foreach (Department d_temp in listDeps)
                Console.WriteLine(d_temp.Id + " -> " + d_temp.Name);

            Department result = null;
            if (listDeps.Count == 0)
            {
                Console.WriteLine("No Departmens found - Create a new one!");
                string selDepID = "";
                do
                {
                    Console.WriteLine("Department name");
                    selDepID = Console.ReadLine();

                } while (selDepID == null);

                result = new Department(selDepID);
                service.insertDepartment(result);
                result = service.getDepartment(result.Id);
                Console.WriteLine("Department inserted");
            }
            else
            {
                int selDepID;
                Console.WriteLine("Input ID.");
                string selectionEmplIDString = Console.ReadLine();
                //try to parse string to int
                bool successEmplID = int.TryParse(selectionEmplIDString, out selDepID);
                result = service.getDepartment(selDepID);                
            }
            
            Employee emp = new Employee(0, firstname, lastname, (EmployeeGender)selectionIntGender, result);
            service.insertEmployee(emp);
            Console.WriteLine("Employee inserted");
            Console.WriteLine("Press Enter to go back to mainmenu");
            Console.ReadLine();
            Console.Clear();
        }//Ende InsertEmpl

        public void InsertDep()
        {
            Console.WriteLine("Input departmentname");
            string depname = Console.ReadLine();
            Department dep = new Department(depname);
            service.insertDepartment(dep);
            Console.WriteLine("Department inserted");

            Console.WriteLine("Press Enter to go back to mainmenu");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
