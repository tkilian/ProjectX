﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Data.SQLite;
using as_projekt.data;
using AS_Projekt.helper;

namespace AS_Projekt.db
{
    public class Database
    {
        private String dataSourceName = "as_projekt.db";
        private String connectionString = "";

        private SQLiteConnection dbConnection = null;
        public SQLiteConnection DbConnection
        {
            get
            {
                if (dbConnection == null)
                {
                    dbConnection = new SQLiteConnection(this.connectionString);
                }

                return dbConnection;
            }
            private set {}
        }

        public Database()
        {
            connectionString = "Data Source=" + dataSourceName;
            setup();
        }

        private void setup()
        {
            SQLiteConnection connection = DbConnection;

            try
            {
                dbConnection.Open();

                SQLiteCommand command = new SQLiteCommand(dbConnection);
                command.CommandText = "CREATE TABLE IF NOT EXISTS `employees` (id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT" +
                    ", firstname VARCHAR(255) NOT NULL, lastname VARCHAR(255) NOT NULL, gender INTEGER NOT NULL, fk_department_nr INTEGER NOT NULL)";
                command.ExecuteNonQuery();

                command.CommandText = "CREATE TABLE IF NOT EXISTS `departments` (id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, name VARCHAR(255) NOT NULL)";
                
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                connection.Close();
            }
        }
        public bool insertEmployee(Employee employee) 
        {
            SQLiteConnection connection = DbConnection;

            try
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(connection);

                command.CommandText = "INSERT INTO `employees` (firstname, lastname, gender, fk_department_nr) VALUES (@firstname, @lastname, @gender, @department)";
                command.Prepare();

                command.Parameters.AddWithValue("@firstname", employee.Firstname);
                command.Parameters.AddWithValue("@lastname", employee.Lastname);
                command.Parameters.AddWithValue("@gender", employee.Gender);
                command.Parameters.AddWithValue("@department", employee.Department.Id);

                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                connection.Close();
            }

            return true;
        }

        public Employee getEmployeeById(int id)
        {
            SQLiteConnection connection = DbConnection;
            Employee employee = null;

            try
            {
                SQLiteCommand command = new SQLiteCommand(connection);
                SQLiteDataReader reader;

                connection.Open();

                command.CommandText = "SELECT * FROM `employees` e LEFT OUTER JOIN `departments` d ON e.fk_department_nr = d.id WHERE e.id = @id";
                command.Prepare();

                command.Parameters.AddWithValue("@id", id);

                reader = command.ExecuteReader();

                try
                {
                    while (reader.Read())
                    {
                        employee = Helper.CreateEmployee(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetString(6));
                    }

                }
                finally
                {
                    // Aufräumen
                    reader.Close();
                    reader.Dispose();
                    command.Dispose();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                connection.Close();
            }

            return employee;
        }

        public List<Employee> getAllEmployees()
        {
            SQLiteConnection connection = DbConnection;
            List<Employee> employees = new List<Employee>();

            try
            {
                SQLiteCommand command = new SQLiteCommand(connection);
                SQLiteDataReader reader;

                connection.Open();

                command.CommandText = "SELECT e.*, d.* FROM employees e LEFT OUTER JOIN departments d ON e.fk_department_nr = d.id";

                reader = command.ExecuteReader();

                try
                {
                    while (reader.Read())
                    {
                        employees.Add(Helper.CreateEmployee(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetString(6)));
                    }
                }
                finally
                {
                    // Aufräumen
                    reader.Close();
                    reader.Dispose();
                    command.Dispose();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                connection.Close();
            }

            return employees;
        }

        public bool insertDepartment(Department department)
        {
            SQLiteConnection connection = DbConnection;

            try
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(connection);

                command.CommandText = "INSERT INTO `departments` (name) VALUES (@name)";
                command.Prepare();

                command.Parameters.AddWithValue("@name", department.Name);

                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                connection.Close();
            }

            return true;
        }

        public Department getDepartmentById(int id)
        {
            SQLiteConnection connection = DbConnection;
            Department department = null;

            try
            {
                SQLiteCommand command = new SQLiteCommand(connection);
                SQLiteDataReader reader;

                connection.Open();

                command.CommandText = "SELECT * FROM `departments` WHERE id = @id";
                command.Prepare();

                command.Parameters.AddWithValue("@id", id);

                reader = command.ExecuteReader();

                try
                {
                    while (reader.Read())
                    {
                        department = Helper.CreateDepartment(reader.GetInt32(0), reader.GetString(1));
                    }

                }
                finally
                {
                    // Aufräumen
                    reader.Close();
                    reader.Dispose();
                    command.Dispose();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                connection.Close();
            }

            return department;
        }

        public List<Department> getAllDepartments()
        {
            SQLiteConnection connection = DbConnection;
            List<Department> departments = new List<Department>();

            try
            {
                SQLiteCommand command = new SQLiteCommand(connection);
                SQLiteDataReader reader;

                connection.Open();

                command.CommandText = "SELECT * FROM `departments`";

                reader = command.ExecuteReader();

                try
                {
                    while (reader.Read())
                    {
                        departments.Add(Helper.CreateDepartment(reader.GetInt32(0), reader.GetString(1)));
                    }

                }
                finally
                {
                    // Aufräumen
                    reader.Close();
                    reader.Dispose();
                    command.Dispose();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                connection.Close();
            }

            return departments;
        }
    }
}