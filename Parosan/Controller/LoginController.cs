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

        private DatabaseController db = new DatabaseController();

        public bool checkUser(string username,string password)
        {
            HasingService hasingService = new HasingService();
            CryptoService cryptoService = new CryptoService();

            string usernameHash = hasingService.sha256Hash( username );
            string passwordHash = hasingService.sha256Hash( password );

            SQLiteCommand sqlCommand = new SQLiteCommand("select * from user where username=@username and password = @password", db.connection());


            sqlCommand.Parameters.AddWithValue("@username", usernameHash);
            sqlCommand.Parameters.AddWithValue("@password", passwordHash);
            
            SQLiteDataReader reader = sqlCommand.ExecuteReader();
            int count = 0;

            while (reader.Read())
            {
                UserModel.id = reader.GetInt32(reader.GetOrdinal("id")) ;               
                count++;
            }

            reader.Close();
            db.connection().Close();

            if (count == 1)
            {
                UserModel.username = username;
                UserModel.key = Convert.ToBase64String( cryptoService.generateKey( username + password , 32) );
                UserModel.iv = Convert.ToBase64String(cryptoService.generateKey(username+"parosan", 16));
                return true;

            }
            else
            {
                return false;
            }

           
        }
    }
}
