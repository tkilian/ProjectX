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

        private IService service;

        public MainWindow(IService service)
        {
        	InitializeComponent();
        	this.service = service;
        }

<<<<<<< HEAD
        public void InitializeService(IService service)
        {
            this.service = service;
        }

=======
>>>>>>> f15ea9cbebdcfc744ba8f3705a299f6db7926a37
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

            Department result = new Department(Convert.ToString(cbDepartment.SelectedIndex));
            Employee emp = new Employee(tbFirstname.Text, tbSurname.Text, (EmployeeGender)gender, result);
            service.insertEmployee(emp);

            this.loadList();
        }

        private void btnSaveDepartment_Click(object sender, RoutedEventArgs e)
        {
            Department dep = new Department(tbDepartment.Text);
            service.insertDepartment(dep);

            this.loadList();
        }

        private void loadList()
        {
            try
            {
                lbDepartments.Items.Clear();
                List<Department> listDeps = service.getDepartments();
                lbDepartments.ItemsSource = listDeps;

                cbDepartment.Items.Clear();
                cbDepartment.ItemsSource = listDeps;

                lbEmployees.Items.Clear();
                List<Employee> listEmpl = service.getEmployees();


                foreach (Employee d_temp in listEmpl)
                    lbEmployees.Items.Add(d_temp.Lastname + ", " + d_temp.Firstname + " \n   Gender: " + (d_temp.Gender == (EmployeeGender)1 ? "male" : "female" ) + " \n   Department: " + d_temp.Department + " \n   ID: " + d_temp.Id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void deleteEmployee_Click(object sender, RoutedEventArgs e)
        {
            service.deleteEmployee(lbEmployees.SelectedIndex);
        }

        private void deleteDepartment_Click(object sender, RoutedEventArgs e)
        {
            service.deleteDepartment(lbEmployees.SelectedIndex);
        }
    }
}