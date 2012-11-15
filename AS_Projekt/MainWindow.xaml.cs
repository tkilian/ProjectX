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
        private Dictionary<int, Department> allDepartments;

        public MainWindow(IService service)
        {
        	InitializeComponent();
        	this.service = service;
            allDepartments = new Dictionary<int, Department>();
        }

        public void InitializeService(IService service)
        {
            this.service = service;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.loadList();
        }

        private void btnSaveEmployee_Click(object sender, RoutedEventArgs e)
        {

            if (tbFirstname.Text.Equals("") || tbSurname.Text.Equals("")) 
            {
                MessageBox.Show("Please enter Surname and Firstname");
                return;
            }

            int gender = 1;
            if (rbGenderFemale.IsChecked == true)
            {
                gender = 0;
            }

            Department result = null;
            if (cbDepartment.SelectedIndex > -1)
            {
                result = (Department)cbDepartment.Items.GetItemAt(cbDepartment.SelectedIndex);
            }

            Employee emp = new Employee(tbFirstname.Text, tbSurname.Text, (EmployeeGender)gender, result);
            service.insertEmployee(emp);

            this.loadList();
        }

        private void btnSaveDepartment_Click(object sender, RoutedEventArgs e)
        {
            if (tbDepartment.Text.Equals(""))
            {
                MessageBox.Show("Please enter a Department name");
                return;
            }
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
                foreach(Department dep in listDeps) 
                {
                    lbDepartments.Items.Add(dep);
                }

                cbDepartment.Items.Clear();
                foreach (Department dep in listDeps)
                {
                    cbDepartment.Items.Add(dep);
                }

                lbEmployees.Items.Clear();
                List<Employee> listEmpl = service.getEmployees();
                foreach (Employee emp in listEmpl)
                {
                    lbEmployees.Items.Add(emp);
                }
                lbEmployees.Items.Refresh();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void deleteEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (lbEmployees.SelectedIndex < 0) return;

            Employee employee = (Employee)lbEmployees.Items.GetItemAt(lbEmployees.SelectedIndex);
            service.deleteEmployee(employee.Id);

            this.loadList();
        }

        private void deleteDepartment_Click(object sender, RoutedEventArgs e)
        {
            if (lbDepartments.SelectedIndex < 0) return;

            Department department = (Department)lbDepartments.Items.GetItemAt(lbDepartments.SelectedIndex);
            service.deleteDepartment(department.Id);

            this.loadList();
        }
    }
}