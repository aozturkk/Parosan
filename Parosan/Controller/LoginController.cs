using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parosan.Controller
{
    public class LoginController
    {

        private string databasePath = @"Data Source=" + Environment.CurrentDirectory + "\\db\\parosan.db;Version=3;New=false;Compress=True;Read Only=False";
        SQLiteConnection dbConnection;

        public bool checkUser(string username,string password)
        {

            dbConnection = new SQLiteConnection(databasePath);
            dbConnection.Open();
            SQLiteCommand sqlCommand = new SQLiteCommand("select * from user where username=@username and password = @password", dbConnection);


            sqlCommand.Parameters.AddWithValue("@username", username);
            sqlCommand.Parameters.AddWithValue("@password", password);
            sqlCommand.ExecuteNonQuery();
            SQLiteDataReader reader = sqlCommand.ExecuteReader();
            int count = 0;

            while (reader.Read())
            {
                count++;
            }

            if (count == 1)
            {
                return true;
            }
            else
            {
                return false;
            }

           
        }
    }
}
