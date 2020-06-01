using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows.Controls;
using System.Data;
using Parosan.Model;

namespace Parosan.Controller
{

    class PasswordController
    {
        private string databasePath = @"Data Source="+Environment.CurrentDirectory+"\\db\\parosan.db;Version=3;New=false;Compress=True;Read Only=False";
        SQLiteConnection dbConnection;
        public List<PasswordModel> passwords = new List<PasswordModel>();
        
        public void connectionDB()
        {

            dbConnection = new SQLiteConnection(databasePath);
            dbConnection.Open();


        }

        public void printPassword()
        {
            connectionDB();
            SQLiteCommand sqlCommand = new SQLiteCommand("select * from password",dbConnection);
            SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(sqlCommand);


            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
          
            
            
            
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                PasswordModel temp = new PasswordModel();
                temp.id = dataTable.Rows[i]["id"].ToString();
                temp.account_name = dataTable.Rows[i]["account_name"].ToString();
                temp.username = dataTable.Rows[i]["username"].ToString();
                temp.password = dataTable.Rows[i]["password"].ToString();
                temp.user_id = dataTable.Rows[i]["user_id"].ToString();
                temp.address = dataTable.Rows[i]["address"].ToString();
                passwords.Add(temp);
                
            }



        }
    }
}
