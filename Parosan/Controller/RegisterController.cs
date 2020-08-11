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
        private DatabaseController db = new DatabaseController();
        private static int lastID = 0;
        public bool registerUser(string username, string password)
        {

            HasingService hasingService = new HasingService();


            if (checkUsername(hasingService.sha256Hash(username)))
            {

            
                SQLiteCommand sqlCommand = new SQLiteCommand(db.connection());

                sqlCommand.CommandText = "insert into user (id, username,password) Values (@id,@username,@password)";
                sqlCommand.Parameters.AddWithValue("id", lastID + 1 );
                sqlCommand.Parameters.AddWithValue("username", hasingService.sha256Hash(username));
                sqlCommand.Parameters.AddWithValue("password", hasingService.sha256Hash(password));


                sqlCommand.ExecuteNonQuery();

                db.connection().Close();
            
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
            

            SQLiteCommand sqlCommand = new SQLiteCommand("select * from user ", db.connection());
            SQLiteDataReader reader = sqlCommand.ExecuteReader();

          

            while (reader.Read())
            {
                if (username == reader.GetString(reader.GetOrdinal("username")) )
                {
                    db.connection().Close();
                    return false;
                }
                lastID = reader.GetInt32(reader.GetOrdinal("id")); 
            }

            db.connection().Close();
            return true;
        }
    }
}
