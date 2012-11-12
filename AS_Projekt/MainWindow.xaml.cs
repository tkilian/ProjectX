using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AS_Projekt.db;
using as_projekt.data;

namespace AS_Projekt
{
  /// <summary>
  /// Interaktionslogik für MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
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

    }
  }
}
