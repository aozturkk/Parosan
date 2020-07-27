using System;
using System.Collections.Generic;
using System.Data;
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

            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);


            List<PaymentModel> payment = new List<PaymentModel>();

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                PaymentModel temp = new PaymentModel();
                temp.id = Convert.ToInt32(dataTable.Rows[i]["id"]);
                temp.title = cryptoService.textDecrytion(dataTable.Rows[i]["title"].ToString());
                temp.card_number = cryptoService.textDecrytion(dataTable.Rows[i]["card_number"].ToString());
                temp.expiry_date = cryptoService.textDecrytion(dataTable.Rows[i]["expiry_date"].ToString());
                temp.cvc = cryptoService.textDecrytion(dataTable.Rows[i]["cvc"].ToString());
                temp.pin = cryptoService.textDecrytion(dataTable.Rows[i]["pin"].ToString());
                temp.user_id = Convert.ToInt32(dataTable.Rows[i]["user_id"]);

                payment.Add(temp);
                lastID = Convert.ToInt32(dataTable.Rows[i]["id"]);
            }


            dbConnection.Close();
            return payment;

        }

        public void addPayment(PaymentModel newPayment)
        {

            CryptoService cryptoService = new CryptoService();

            connectionDB();
            SQLiteCommand sqlCommand = new SQLiteCommand(dbConnection);

            sqlCommand.CommandText = "insert into payment (id,title,card_number,expiry_date,cvc,pin,user_id) Values (@id, @title,@card_number,@expiry_date,@cvc,@pin,@user_id)";
            sqlCommand.Parameters.AddWithValue("id", lastID + 1);
            sqlCommand.Parameters.AddWithValue("title", cryptoService.textEncrytion(newPayment.title));
            sqlCommand.Parameters.AddWithValue("card_number", cryptoService.textEncrytion(newPayment.card_number));
            sqlCommand.Parameters.AddWithValue("expiry_date", cryptoService.textEncrytion(newPayment.expiry_date));
            sqlCommand.Parameters.AddWithValue("cvc", cryptoService.textEncrytion(newPayment.cvc));
            sqlCommand.Parameters.AddWithValue("pin", cryptoService.textEncrytion(newPayment.pin));
            sqlCommand.Parameters.AddWithValue("user_id", UserModel.id);
            sqlCommand.ExecuteNonQuery();

            dbConnection.Close();
        }

        public void deletePayment(int id)
        {
            connectionDB();

            SQLiteCommand sqlCommand = new SQLiteCommand(dbConnection);
            sqlCommand.CommandText = "DELETE FROM payment WHERE id=" + id + " AND user_id=" + UserModel.id;
            sqlCommand.ExecuteScalar();
            dbConnection.Close();
        }

        public void updatePayment(PaymentModel updatedPayment)
        {
            CryptoService cryptoService = new CryptoService();

            connectionDB();

            SQLiteCommand sqlCommand = new SQLiteCommand(dbConnection);

            sqlCommand.CommandText = "update payment set title=@title,card_number=@card_number,expiry_date=@expiry_date,cvc=@cvc,pin=@pin where id = '" + updatedPayment.id + "'" + " AND user_id='" + UserModel.id + "'";

            sqlCommand.Parameters.AddWithValue("@title", cryptoService.textEncrytion(updatedPayment.title));
            sqlCommand.Parameters.AddWithValue("@card_number", cryptoService.textEncrytion(updatedPayment.card_number));
            sqlCommand.Parameters.AddWithValue("@expiry_date", cryptoService.textEncrytion(updatedPayment.expiry_date));
            sqlCommand.Parameters.AddWithValue("@cvc", cryptoService.textEncrytion(updatedPayment.cvc));
            sqlCommand.Parameters.AddWithValue("@pin", cryptoService.textEncrytion(updatedPayment.pin));
            sqlCommand.ExecuteNonQuery();

            dbConnection.Close();
        }
    }
}
