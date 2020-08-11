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
        private DatabaseController db = new DatabaseController();
        private static int lastID = 0;

       
        public List<PaymentModel> printPayment()
        {

            CryptoService cryptoService = new CryptoService();

            SQLiteCommand sqlCommand = new SQLiteCommand("select * from payment where user_id=" + UserModel.id, db.connection());
            SQLiteDataReader reader = sqlCommand.ExecuteReader();


            List<PaymentModel> payment = new List<PaymentModel>();

            while(reader.Read())
            {
                PaymentModel temp = new PaymentModel();
                temp.id = reader.GetInt32(reader.GetOrdinal("id"));
                temp.title = cryptoService.textDecrytion( reader.GetString(reader.GetOrdinal("title") ) );
                temp.card_number = cryptoService.textDecrytion(reader.GetString(reader.GetOrdinal("card_number")));
                temp.expiry_date = cryptoService.textDecrytion(reader.GetString(reader.GetOrdinal("expiry_date")));
                temp.cvc = cryptoService.textDecrytion(reader.GetString(reader.GetOrdinal("cvc")));
                temp.pin = cryptoService.textDecrytion(reader.GetString(reader.GetOrdinal("pin")));
                temp.user_id = reader.GetInt32(reader.GetOrdinal("user_id"));

                payment.Add(temp);
                lastID = temp.id;
            }


            db.connection().Close();
            return payment;

        }

        public void addPayment(PaymentModel newPayment)
        {

            CryptoService cryptoService = new CryptoService();

            
            SQLiteCommand sqlCommand = new SQLiteCommand(db.connection());

            sqlCommand.CommandText = "insert into payment (id,title,card_number,expiry_date,cvc,pin,user_id) Values (@id, @title,@card_number,@expiry_date,@cvc,@pin,@user_id)";
            sqlCommand.Parameters.AddWithValue("id", lastID + 1);
            sqlCommand.Parameters.AddWithValue("title", cryptoService.textEncrytion(newPayment.title));
            sqlCommand.Parameters.AddWithValue("card_number", cryptoService.textEncrytion(newPayment.card_number));
            sqlCommand.Parameters.AddWithValue("expiry_date", cryptoService.textEncrytion(newPayment.expiry_date));
            sqlCommand.Parameters.AddWithValue("cvc", cryptoService.textEncrytion(newPayment.cvc));
            sqlCommand.Parameters.AddWithValue("pin", cryptoService.textEncrytion(newPayment.pin));
            sqlCommand.Parameters.AddWithValue("user_id", UserModel.id);
            sqlCommand.ExecuteNonQuery();

            db.connection().Close();
        }

        public void deletePayment(int id)
        {
            
            SQLiteCommand sqlCommand = new SQLiteCommand(db.connection());
            sqlCommand.CommandText = "DELETE FROM payment WHERE id=" + id + " AND user_id=" + UserModel.id;
            sqlCommand.ExecuteScalar();
            db.connection().Close();
        }

        public void updatePayment(PaymentModel updatedPayment)
        {
            CryptoService cryptoService = new CryptoService();


            SQLiteCommand sqlCommand = new SQLiteCommand(db.connection());

            sqlCommand.CommandText = "update payment set title=@title,card_number=@card_number,expiry_date=@expiry_date,cvc=@cvc,pin=@pin where id = '" + updatedPayment.id + "'" + " AND user_id='" + UserModel.id + "'";

            sqlCommand.Parameters.AddWithValue("@title", cryptoService.textEncrytion(updatedPayment.title));
            sqlCommand.Parameters.AddWithValue("@card_number", cryptoService.textEncrytion(updatedPayment.card_number));
            sqlCommand.Parameters.AddWithValue("@expiry_date", cryptoService.textEncrytion(updatedPayment.expiry_date));
            sqlCommand.Parameters.AddWithValue("@cvc", cryptoService.textEncrytion(updatedPayment.cvc));
            sqlCommand.Parameters.AddWithValue("@pin", cryptoService.textEncrytion(updatedPayment.pin));
            sqlCommand.ExecuteNonQuery();

            db.connection().Close();
        }
    }
}
