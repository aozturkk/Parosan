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
    class PaymentController
    {
        private string databasePath = @"Data Source=" + Environment.CurrentDirectory + "\\db\\parosan.db;Version=3;New=false;Compress=True;Read Only=False";
        SQLiteConnection dbConnection;
        private static int lastID = 0;

        public void connectionDB()
        {

            dbConnection = new SQLiteConnection(databasePath);
            dbConnection.Open();


        }

        public List<PaymentModel> printPayment()
        {

            CryptoService cryptoService = new CryptoService();

            connectionDB();
            SQLiteCommand sqlCommand = new SQLiteCommand("select * from payment where user_id=" + UserModel.id, dbConnection);
            SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(sqlCommand);




            List<PaymentModel> payment = new List<PaymentModel>();

            

            dbConnection.Close();
            return payment;

        }
    }
}
