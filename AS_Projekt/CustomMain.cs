using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
      //DB oder XML


      //Startet Konsolenanwendung
      RunConsole();
      //Startet WPF-Dreck
      //RunWPF();
         
    }

    private static void RunConsole()
    {
      ConsoleManager.ConsoleManage.Show();
      Console.WriteLine("HALLO");
      System.Diagnostics.Trace.WriteLine("HALLO");
      Console.ReadLine();   
    }

    private static void RunWPF()
    {
      AS_Projekt.App app = new AS_Projekt.App();
      app.InitializeComponent();
      app.Run();
    }
  }
}
