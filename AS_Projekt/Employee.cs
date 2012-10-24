﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace as_projekt.data
{
  public enum EmployeeGender { Male, Female }
  
  public class Employee
  {
    public int Id { get; private set; }
    public String Firstname { get; private set; }
    public String Lastname { get; private set; }
    public EmployeeGender Gender { get; private set; }
    public Department Department { get; private set; }

    public Employee(int id, String firstname, String lastname, EmployeeGender gender, Department department)
    {
      Id = id;
      Firstname = firstname;
      Lastname = lastname;
      Gender = gender;
      Department = department;
    }


    public Employee(String firstname, String lastname, EmployeeGender gender, Department department)
    {
      Id = 0;
      Firstname = firstname;
      Lastname = lastname;
      Gender = gender;
      Department = department;
    }

    public bool IsNew()
    {
      return (Id == 0);
    }

  }
}
