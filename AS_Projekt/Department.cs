using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace as_projekt.data
{
  public class Department
  {
    public int Id { get; private set; }
    public String Name { get; private set; }

    public Department(int id, String name)
    {
      Id = id;
      Name = name;
    }

    public Department(String name)
    {
      Id = 0;
      Name = name;
    }

    public bool IsNew() {
      return (Id == 0);
    }
  }
}
