using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;
using AS_Projekt.services;
using AS_Projekt.interfaces;
using as_projekt.data;

namespace AS_Projekt
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IStore store;

        public MainWindow()
        {
            InitializeComponent();
            store = StoreFactory.CreateStore("XML");

            ////////////////////////////////////////////
            /// Database Usage 
            ////////////////////////////////////////////

            // Database db = new Database();

            // Department department = new Department(1, "Woot Inc");
            // db.insertDepartment(department);
            // db.deleteDepartmentById(1);
            // db.updateDepartmentById(1);
            // List<Department> departments = db.getAllDepartments();

            // Employee employee = new Employee(2, "WAT WAT", "WAT", EmployeeGender.Male, department);
            // db.insertEmployee(employee);
            // db.deleteEmployeeById(1);
            // db.updateEmployee(employee);
            // List <Employee> employees = db.getAllEmployees();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.loadList();
        }

        private void btnSaveEmployee_Click(object sender, RoutedEventArgs e)
        {
            int gender = 1;
            if (rbGenderFemale.IsChecked == true)
            {
                gender = 0;
            }
            Console.WriteLine(rbGenderFemale.IsChecked + " " + rbGenderMale.IsChecked);
            Department result = new Department(Convert.ToString(cbDepartment.SelectedIndex));
            Employee emp = new Employee(0, tbFirstname.Text, tbSurname.Text, (EmployeeGender)gender, result);
            store.insertEmployee(emp);

            this.loadList();
        }

        private void btnSaveDepartment_Click(object sender, RoutedEventArgs e)
        {
            Department dep = new Department(tbDepartment.Text);
            store.insertDepartment(dep);

            this.loadList();
        }

        private void loadList()
        {
            try
            {
                lbDepartments.Items.Clear();
                List<Department> listDeps = store.getAllDepartments();
                lbDepartments.ItemsSource = listDeps;

                lbEmployees.Items.Clear();
                List<Employee> listEmpl = store.getAllEmployees();


                foreach (Employee d_temp in listEmpl)
                    lbEmployees.Items.Add("ID: " + d_temp.Id + " Lastname: " + d_temp.Lastname + " Firstname: " + d_temp.Firstname + " Department: " + d_temp.Department);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}