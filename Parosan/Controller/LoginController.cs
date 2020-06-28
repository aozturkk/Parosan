using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parosan.Model;
using Parosan.Service;

namespace Parosan.Controller
{
    public class LoginController
    {

        private string databasePath = @"Data Source=" + Environment.CurrentDirectory + "\\db\\parosan.db;Version=3;New=false;Compress=True;Read Only=False";
        SQLiteConnection dbConnection;

        public bool checkUser(string username,string password)
        {
            HasingService hasingService = new HasingService();

            string usernameHash = hasingService.sha256Hash( username );
            string passwordHash = hasingService.sha256Hash( password );


            dbConnection = new SQLiteConnection(databasePath);
            dbConnection.Open();
            SQLiteCommand sqlCommand = new SQLiteCommand("select * from user where username=@username and password = @password", dbConnection);


            sqlCommand.Parameters.AddWithValue("@username", usernameHash);
            sqlCommand.Parameters.AddWithValue("@password", passwordHash);
            
            SQLiteDataReader reader = sqlCommand.ExecuteReader();
            int count = 0;

            while (reader.Read())
            {
                UserModel.id = reader.GetString(reader.GetOrdinal("id"));               
                count++;
            }

            reader.Close();
            dbConnection.Close();

            if (count == 1)
            {
                UserModel.username = username;
                UserModel.key = hasingService.md5hash(username + password );
                UserModel.iv = (hasingService.md5hash(username + "parosan")).Substring(0, 16); 
                return true;
            }
            else
            {
                return false;
            }

           
        }
    }
}
