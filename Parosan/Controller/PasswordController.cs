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
        private string databasePath = @"Data Source=" + Environment.CurrentDirectory + "\\db\\parosan.db;Version=3;New=false;Compress=True;Read Only=False";
        SQLiteConnection dbConnection;
        private static int lastID = 0;


        public void connectionDB()
        {

            dbConnection = new SQLiteConnection(databasePath);
            dbConnection.Open();


        }

        public List<PasswordModel> printPassword()
        {
            
            CryptoService cryptoService = new CryptoService();

            connectionDB();
            SQLiteCommand sqlCommand = new SQLiteCommand("select * from password where user_id="+UserModel.id, dbConnection);
            SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(sqlCommand);


            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);


            List<PasswordModel> passwords = new List<PasswordModel>();

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                PasswordModel temp = new PasswordModel();
                temp.id = Convert.ToInt32(dataTable.Rows[i]["id"]);
                temp.account_name = cryptoService.textDecrytion( dataTable.Rows[i]["account_name"].ToString() );
                temp.username = cryptoService.textDecrytion( dataTable.Rows[i]["username"].ToString() );
                temp.password = cryptoService.textDecrytion( dataTable.Rows[i]["password"].ToString() );
                temp.user_id = dataTable.Rows[i]["user_id"].ToString();
                temp.address = cryptoService.textDecrytion(dataTable.Rows[i]["address"].ToString() );
                passwords.Add(temp);
                lastID = Convert.ToInt32(dataTable.Rows[i]["id"]);
            }
            lastID++;
            dbConnection.Close();
            return passwords;

        }
        public void addPassword(PasswordModel newPassword)
        {

            CryptoService cryptoService = new CryptoService();

            connectionDB();
            SQLiteCommand sqlCommand = new SQLiteCommand(dbConnection);

            sqlCommand.CommandText = "insert into password (id, account_name,username,password,address,user_id) Values (@id, @account_name,@username,@password,@address,@user_id)";
            sqlCommand.Prepare();
            sqlCommand.Parameters.AddWithValue("id", lastID);
            sqlCommand.Parameters.AddWithValue("account_name", cryptoService.textEncrytion( newPassword.account_name));
            sqlCommand.Parameters.AddWithValue("username", cryptoService.textEncrytion(newPassword.username));
            sqlCommand.Parameters.AddWithValue("password", cryptoService.textEncrytion(newPassword.password));
            sqlCommand.Parameters.AddWithValue("address", cryptoService.textEncrytion(newPassword.address));
            sqlCommand.Parameters.AddWithValue("user_id", UserModel.id);
            sqlCommand.ExecuteNonQuery();
           
            dbConnection.Close();
        }
        public void deletePassword(int id)
        {
            connectionDB();

            SQLiteCommand sqlCommand = new SQLiteCommand(dbConnection);
            sqlCommand.CommandText = "DELETE FROM password WHERE id=" + id + " AND user_id="+UserModel.id;
            sqlCommand.ExecuteScalar();
            dbConnection.Close();
        }

        public void updatePassword(PasswordModel updatedPassword)
        {
            CryptoService cryptoService = new CryptoService();

            connectionDB();

            SQLiteCommand sqlCommand = new SQLiteCommand(dbConnection);

            sqlCommand.CommandText = "update password set account_name=@account_name,username=@username,password=@password,address=@address where id = '" + updatedPassword.id+"'" + " AND user_id='" + UserModel.id+"'"; 
      
            sqlCommand.Parameters.AddWithValue("@account_name", cryptoService.textEncrytion(updatedPassword.account_name));
            sqlCommand.Parameters.AddWithValue("@username", cryptoService.textEncrytion(updatedPassword.username));
            sqlCommand.Parameters.AddWithValue("@password", cryptoService.textEncrytion(updatedPassword.password));
            sqlCommand.Parameters.AddWithValue("@address", cryptoService.textEncrytion(updatedPassword.address));
 
            sqlCommand.ExecuteNonQuery();

            dbConnection.Close();
        }

    }
}