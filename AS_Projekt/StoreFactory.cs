using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AS_Projekt.interfaces;
using AS_Projekt.db;
using AS_Projekt.xml;

namespace AS_Projekt
{
    class StoreFactory
    {
        public static IStore CreateStore(string storeType)
        {
            switch (storeType)
            {
                case "MSSQL":
                    return new Database();
                case "XML":
                    return new AS_Projekt.xml.xml();
                default:
                    return null;
            }
        }
    }
}
