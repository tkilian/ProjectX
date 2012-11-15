using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AS_Projekt.interfaces;
using AS_Projekt.db;
using AS_Projekt.xml;
using AS_Projekt.services;

namespace AS_Projekt
{
  class CustomMain
  {
    /// <summary>
    /// Application Entry Point.
    /// </summary>
    [System.STAThreadAttribute()]
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    public static void Main()
    {
      //IService service = new BusinessLogic(new Database());
      IService service = new BusinessLogic(new xmlStorage());
      
      //DB oder XML
      //Startet Konsolenanwendung
      RunConsole(service);
      //Startet WPF-Dreck
      //RunWPF(service);
         
    }

    private static void RunConsole(IService service)
    {
        new ShellOutput(service);
    }

    private static void RunWPF(IService service)
    {
      AS_Projekt.App app = new AS_Projekt.App();
      app.Run(new MainWindow(service));
    }
  }
}
