using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows.Controls;
using System.Data;

namespace Parosan.Controller
{

    class PasswordController
    {
        public string databasePath = @"Data Source="+Environment.CurrentDirectory+"\\db\\parosan.db;Version=3;New=false;Compress=True;Read Only=False";
        SQLiteConnection dbConnection;
        public void connectionDB()
        {

            dbConnection = new SQLiteConnection(databasePath);
            dbConnection.Open();


        }

        public void printPassword(DataGrid dataGrid)
        {
            connectionDB();
            SQLiteCommand sqlCommand = new SQLiteCommand("select * from password",dbConnection);
            SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(sqlCommand);
            
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = dataTable.DefaultView;

        }
    }
}
