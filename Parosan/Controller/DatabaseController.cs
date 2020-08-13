using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parosan.Controller
{
    class DatabaseController
    {

        public SQLiteConnection connection()
        {
         string databasePath = @"Data Source=" + Environment.CurrentDirectory + "\\db\\parosan.db;Version=3;New=false;Compress=True;Read Only=False";
         SQLiteConnection dbConnection;
         
         dbConnection = new SQLiteConnection(databasePath);
         dbConnection.Open();

         return dbConnection;
        }

        public void createDatabase()
        {
            SQLiteConnection.CreateFile("db/parosan.db");
            string userTable = "CREATE TABLE user (id INTEGER,username TEXT,password TEXT,PRIMARY KEY(id))";
            string passwordTable = "CREATE TABLE password (id INTEGER,account_name TEXT,username TEXT,password TEXT,address TEXT,user_id INTEGER,PRIMARY KEY(id,user_id))";
            string paymentTable = "CREATE TABLE payment (id INTEGER,title TEXT,card_number TEXT,expiry_date TEXT,cvc TEXT,pin TEXT,user_id INTEGER,PRIMARY KEY(id,user_id))";
            string personTable = "CREATE TABLE person (id INTEGER,name TEXT,surname TEXT,phone TEXT,email TEXT,description TEXT,user_id INTEGER,PRIMARY KEY(id,user_id))";
            string noteTable = "CREATE TABLE note (id INTEGER,title TEXT,content TEXT,date TEXT,user_id INTEGER,PRIMARY KEY(id,user_id))";

            SQLiteConnection dbConnection = connection();
            SQLiteCommand createUserTable = new SQLiteCommand(userTable, dbConnection);
            createUserTable.ExecuteNonQuery();
            SQLiteCommand createPasswordTable = new SQLiteCommand(passwordTable, dbConnection);
            createPasswordTable.ExecuteNonQuery();
            SQLiteCommand createPaymentTable = new SQLiteCommand(paymentTable, dbConnection);
            createPaymentTable.ExecuteNonQuery();
            SQLiteCommand createPersonTable = new SQLiteCommand(personTable, dbConnection);
            createPersonTable.ExecuteNonQuery();
            SQLiteCommand createNoteTable = new SQLiteCommand(noteTable, dbConnection);
            createNoteTable.ExecuteNonQuery();

            dbConnection.Close();

        }

    }
}
