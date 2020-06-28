using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parosan.Service;

namespace Parosan.Controller
{
 
    class RegisterController
    {
        private string databasePath = @"Data Source=" + Environment.CurrentDirectory + "\\db\\parosan.db;Version=3;New=false;Compress=True;Read Only=False";
        SQLiteConnection dbConnection;

        public bool registerUser(string username, string password)
        {

            HasingService hasingService = new HasingService();

            dbConnection = new SQLiteConnection(databasePath);
            dbConnection.Open();

            SQLiteCommand sqlCommand = new SQLiteCommand("select * from user ", dbConnection);
            SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            string lastID;
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                if (hasingService.sha256Hash( username ) == dataTable.Rows[i]["username"].ToString())
                {
                    return false;
                }
               lastID = dataTable.Rows[i]["id"].ToString();
            }


            return true;
        }
    }
}
