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
    class PersonController
    {
        private DatabaseController db = new DatabaseController();
        private static int lastID = 0;

        

        public List<PersonModel> printPerson()
        {

            CryptoService cryptoService = new CryptoService();

            SQLiteCommand sqlCommand = new SQLiteCommand("select * from person where user_id=" + UserModel.id, db.connection());
            SQLiteDataReader reader = sqlCommand.ExecuteReader();

            List<PersonModel> persons = new List<PersonModel>();

            while (reader.Read())
            {
                PersonModel temp = new PersonModel();
                temp.id = reader.GetInt32(reader.GetOrdinal("id"));
                temp.name = cryptoService.textDecrytion(reader.GetString(reader.GetOrdinal("name")));
                temp.surname = cryptoService.textDecrytion(reader.GetString(reader.GetOrdinal("surname")));
                temp.phone = cryptoService.textDecrytion(reader.GetString(reader.GetOrdinal("phone")));
                temp.email = cryptoService.textDecrytion(reader.GetString(reader.GetOrdinal("email")));
                temp.description = cryptoService.textDecrytion(reader.GetString(reader.GetOrdinal("description")));
                temp.user_id = reader.GetInt32(reader.GetOrdinal("user_id"));

                persons.Add(temp);
                lastID = temp.id;
            }


            db.connection().Close();
            return persons;

        }

        public void addPerson(PersonModel newPerson)
        {

            CryptoService cryptoService = new CryptoService();

          
            SQLiteCommand sqlCommand = new SQLiteCommand(db.connection());

            sqlCommand.CommandText = "insert into person (id,name,surname,phone,email,description,user_id) Values (@id,@name,@surname,@phone,@email,@description,@user_id)";
            sqlCommand.Parameters.AddWithValue("id", lastID + 1);
            sqlCommand.Parameters.AddWithValue("name", cryptoService.textEncrytion(newPerson.name));
            sqlCommand.Parameters.AddWithValue("surname", cryptoService.textEncrytion(newPerson.surname));
            sqlCommand.Parameters.AddWithValue("phone", cryptoService.textEncrytion(newPerson.phone));
            sqlCommand.Parameters.AddWithValue("email", cryptoService.textEncrytion(newPerson.email));
            sqlCommand.Parameters.AddWithValue("description", cryptoService.textEncrytion(newPerson.description));
            sqlCommand.Parameters.AddWithValue("user_id", UserModel.id);
            sqlCommand.ExecuteNonQuery();

            db.connection().Close();
        }

        public void deletePerson(int id)
        {
            
            SQLiteCommand sqlCommand = new SQLiteCommand(db.connection());
            sqlCommand.CommandText = "DELETE FROM person WHERE id=" + id + " AND user_id=" + UserModel.id;
            sqlCommand.ExecuteScalar();
            db.connection().Close();
        }

        public void updatePerson(PersonModel updatedPerson)
        {
            CryptoService cryptoService = new CryptoService();

            SQLiteCommand sqlCommand = new SQLiteCommand(db.connection());

            sqlCommand.CommandText = "update person set name=@name,surname=@surname,phone=@phone,email=@email,description=@description where id = '" + updatedPerson.id + "'" + " AND user_id='" + UserModel.id + "'";

            sqlCommand.Parameters.AddWithValue("@name", cryptoService.textEncrytion(updatedPerson.name));
            sqlCommand.Parameters.AddWithValue("@surname", cryptoService.textEncrytion(updatedPerson.surname));
            sqlCommand.Parameters.AddWithValue("@phone", cryptoService.textEncrytion(updatedPerson.phone));
            sqlCommand.Parameters.AddWithValue("@email", cryptoService.textEncrytion(updatedPerson.email));
            sqlCommand.Parameters.AddWithValue("@description", cryptoService.textEncrytion(updatedPerson.description));
            sqlCommand.ExecuteNonQuery();

            db.connection().Close();
        }
    }
}

