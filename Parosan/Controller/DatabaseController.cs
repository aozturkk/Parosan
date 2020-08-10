using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parosan.Controller
{
    class DatabaseController
    {

        public SQLiteConnection connection()
        {
         string databasePath = @"Data Source=" + Environment.CurrentDirectory + "\\db\\parosan.db;Version=3;New=false;Compress=True;Read Only=False";
         SQLiteConnection dbConnection;
         dbConnection = new SQLiteConnection(databasePath);
         dbConnection.Open();

         return dbConnection;
        }
    }
}
