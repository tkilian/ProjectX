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
            store = StoreFactory.CreateStore("XML");
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
                Console.WriteLine("Insert Employee = 3");
                Console.WriteLine("Insert Department ID = 4");
                Console.WriteLine("Quit programm = 5");
                string selectionString = Console.ReadLine();
                //try to parse string to int
                bool success = int.TryParse(selectionString, out selectionInt);

                switch (selectionInt)
                {
                    case 1:
                        Console.WriteLine("You selected " + selectionString);
                        GetEmplByID();
                        break;
                    case 2:
                        Console.WriteLine("You selected " + selectionString);
                        GetDepByID();
                        break;
                    case 3:
                        Console.WriteLine("You selected " + selectionString);
                        InsertEmpl();
                        break;
                    case 4:
                        Console.WriteLine("You selected " + selectionString);
                        break;
                    case 5:
                        Console.WriteLine("You selected " + selectionString + ". Shutting down ...");
                        System.Threading.Thread.Sleep(1000);
                        break;
                    default:
                        Console.WriteLine("Invalid selection. Please select 1, 2, or 3....");
                        break;
                }
            } while (selectionInt != 5);
        } // end InitSelection

        public void GetEmplByID()
        {
            int selEmplID;
            Console.WriteLine("Nummer eingeben n00b.");
            Console.ReadLine();
            string selectionEmplIDString = Console.ReadLine();
            //try to parse string to int
            bool successEmplID = int.TryParse(selectionEmplIDString, out selEmplID);
            store.getDepartmentById(selEmplID);
            
        }

        public void GetDepByID()
        {
            int selDepID;
            Console.WriteLine("Nummer eingeben n00b.");
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
                    Console.WriteLine("Invalid Input - try again");
                    Console.WriteLine("Choose gender: {0} = Male | {1} = Female", (int)EmployeeGender.Male, (int)EmployeeGender.Female);
                }
            } while (successGender);
            -
            string strgender = Console.ReadLine();
            Console.WriteLine("Input department:");
            string dep = Console.ReadLine();
            Employee emp = new Employee(0, firstname, lastname, (EmployeeGender)selectionIntGender, new Department(dep));        

        }
    }
}
