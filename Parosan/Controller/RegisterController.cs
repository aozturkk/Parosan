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
        private static int lastID = 0;
        public bool registerUser(string username, string password)
        {

            HasingService hasingService = new HasingService();


            if (checkUsername(hasingService.sha256Hash(username)))
            {

            
                SQLiteCommand sqlCommand = new SQLiteCommand(dbConnection);

                sqlCommand.CommandText = "insert into user (id, username,password) Values (@id,@username,@password)";
                sqlCommand.Parameters.AddWithValue("id", lastID + 1 );
                sqlCommand.Parameters.AddWithValue("username", hasingService.sha256Hash(username));
                sqlCommand.Parameters.AddWithValue("password", hasingService.sha256Hash(password));


                sqlCommand.ExecuteNonQuery();

                dbConnection.Close();
            
                return true;
            }
            else
            {
                return false;
            }

            
        }

        //Check this username been used before
        public bool checkUsername(string username)
        {
            

            dbConnection = new SQLiteConnection(databasePath);
            dbConnection.Open();

            SQLiteCommand sqlCommand = new SQLiteCommand("select * from user ", dbConnection);
            SQLiteDataReader reader = sqlCommand.ExecuteReader();

          

            while (reader.Read())
            {
                if (username == reader.GetString(reader.GetOrdinal("username")) )
                {
                    dbConnection.Close();
                    return false;
                }
                lastID = reader.GetInt32(reader.GetOrdinal("id")); 
            }

           
            return true;
        }
    }
}
