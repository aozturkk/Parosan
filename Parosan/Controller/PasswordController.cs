using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows.Controls;
using System.Data;
using Parosan.Model;
using Parosan.Service;

namespace Parosan.Controller
{

    class PasswordController
    {
        private DatabaseController db = new DatabaseController();
        private static int lastID = 0;


     

        public List<PasswordModel> printPassword()
        {
            
            CryptoService cryptoService = new CryptoService();

            SQLiteCommand sqlCommand = new SQLiteCommand("select * from password where user_id="+UserModel.id, db.connection());
            SQLiteDataReader reader = sqlCommand.ExecuteReader();

            List<PasswordModel> passwords = new List<PasswordModel>();

            while (reader.Read())
            {
                PasswordModel temp = new PasswordModel();

                temp.id = reader.GetInt32( reader.GetOrdinal("id") );
                temp.account_name = cryptoService.textDecrytion( reader.GetString( reader.GetOrdinal("account_name") ) );
                temp.username = cryptoService.textDecrytion( reader.GetString( reader.GetOrdinal("username") ) );
                temp.password = cryptoService.textDecrytion( reader.GetString( reader.GetOrdinal("password") ) );
                temp.address = cryptoService.textDecrytion( reader.GetString( reader.GetOrdinal("address") ) );
                temp.user_id = reader.GetInt32( reader.GetOrdinal("user_id") );
                passwords.Add(temp);
                lastID = temp.id;
            }

            db.connection().Close();
            return passwords;

        }
        public void addPassword(PasswordModel newPassword)
        {

            CryptoService cryptoService = new CryptoService();

            SQLiteCommand sqlCommand = new SQLiteCommand(db.connection());

            sqlCommand.CommandText = "insert into password (id, account_name,username,password,address,user_id) Values (@id, @account_name,@username,@password,@address,@user_id)";          
            sqlCommand.Parameters.AddWithValue("id", lastID + 1 );
            sqlCommand.Parameters.AddWithValue("account_name", cryptoService.textEncrytion( newPassword.account_name));
            sqlCommand.Parameters.AddWithValue("username", cryptoService.textEncrytion(newPassword.username));
            sqlCommand.Parameters.AddWithValue("password", cryptoService.textEncrytion(newPassword.password));
            sqlCommand.Parameters.AddWithValue("address", cryptoService.textEncrytion(newPassword.address));
            sqlCommand.Parameters.AddWithValue("user_id", UserModel.id);
            sqlCommand.ExecuteNonQuery();

            db.connection().Close();
        }
        public void deletePassword(int id)
        {

            SQLiteCommand sqlCommand = new SQLiteCommand(db.connection());
            sqlCommand.CommandText = "DELETE FROM password WHERE id=" + id + " AND user_id="+UserModel.id;
            sqlCommand.ExecuteScalar();
            db.connection().Close();
        }

        public void updatePassword(PasswordModel updatedPassword)
        {
            CryptoService cryptoService = new CryptoService();

            SQLiteCommand sqlCommand = new SQLiteCommand(db.connection());

            sqlCommand.CommandText = "update password set account_name=@account_name,username=@username,password=@password,address=@address where id = '" + updatedPassword.id+"'" + " AND user_id='" + UserModel.id+"'"; 
      
            sqlCommand.Parameters.AddWithValue("@account_name", cryptoService.textEncrytion(updatedPassword.account_name));
            sqlCommand.Parameters.AddWithValue("@username", cryptoService.textEncrytion(updatedPassword.username));
            sqlCommand.Parameters.AddWithValue("@password", cryptoService.textEncrytion(updatedPassword.password));
            sqlCommand.Parameters.AddWithValue("@address", cryptoService.textEncrytion(updatedPassword.address));
 
            sqlCommand.ExecuteNonQuery();

            db.connection().Close();
        }

    }
}