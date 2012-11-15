
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
            List<Department> dept = bll.getDepartments();

            lbEmployees.ItemsSource = dept;
        }

        private void btnSaveEmployee_Click(object sender, RoutedEventArgs e)
        {
            /*
             , bll.getDepartmentById(cbDepartment.SelectedIndex)
             */
            Employee employee = new Employee(tbFirstname, tbSurname, (EmployeeGender)rbGenderMale);
            bll.insertEmployee(employee)
        }
    }
}