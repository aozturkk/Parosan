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
        private string databasePath = @"Data Source=" + Environment.CurrentDirectory + "\\db\\parosan.db;Version=3;New=false;Compress=True;Read Only=False";
        SQLiteConnection dbConnection;
        private static int lastID = 0;

        public void connectionDB()
        {

            dbConnection = new SQLiteConnection(databasePath);
            dbConnection.Open();


        }

        public List<PersonModel> printPerson()
        {

            CryptoService cryptoService = new CryptoService();

            connectionDB();
            SQLiteCommand sqlCommand = new SQLiteCommand("select * from person where user_id=" + UserModel.id, dbConnection);
            SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(sqlCommand);

            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);


            List<PersonModel> persons = new List<PersonModel>();

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                PersonModel temp = new PersonModel();
                temp.id = Convert.ToInt32(dataTable.Rows[i]["id"]);
                temp.name = cryptoService.textDecrytion(dataTable.Rows[i]["name"].ToString());
                temp.surname = cryptoService.textDecrytion(dataTable.Rows[i]["surname"].ToString());
                temp.phone = cryptoService.textDecrytion(dataTable.Rows[i]["phone"].ToString());
                temp.email = cryptoService.textDecrytion(dataTable.Rows[i]["email"].ToString());
                temp.description = cryptoService.textDecrytion(dataTable.Rows[i]["description"].ToString());
                temp.user_id = Convert.ToInt32(dataTable.Rows[i]["user_id"]);

                persons.Add(temp);
                lastID = Convert.ToInt32(dataTable.Rows[i]["id"]);
            }


            dbConnection.Close();
            return persons;

        }

        public void addPerson(PersonModel newPerson)
        {

            CryptoService cryptoService = new CryptoService();

            connectionDB();
            SQLiteCommand sqlCommand = new SQLiteCommand(dbConnection);

            sqlCommand.CommandText = "insert into person (id,name,surname,phone,email,description,user_id) Values (@id,@name,@surname,@phone,@email,@description,@user_id)";
            sqlCommand.Parameters.AddWithValue("id", lastID + 1);
            sqlCommand.Parameters.AddWithValue("name", cryptoService.textEncrytion(newPerson.name));
            sqlCommand.Parameters.AddWithValue("surname", cryptoService.textEncrytion(newPerson.surname));
            sqlCommand.Parameters.AddWithValue("phone", cryptoService.textEncrytion(newPerson.phone));
            sqlCommand.Parameters.AddWithValue("email", cryptoService.textEncrytion(newPerson.email));
            sqlCommand.Parameters.AddWithValue("description", cryptoService.textEncrytion(newPerson.description));
            sqlCommand.Parameters.AddWithValue("user_id", UserModel.id);
            sqlCommand.ExecuteNonQuery();

            dbConnection.Close();
        }

        public void deletePerson(int id)
        {
            connectionDB();

            SQLiteCommand sqlCommand = new SQLiteCommand(dbConnection);
            sqlCommand.CommandText = "DELETE FROM person WHERE id=" + id + " AND user_id=" + UserModel.id;
            sqlCommand.ExecuteScalar();
            dbConnection.Close();
        }

        public void updatePerson(PersonModel updatedPerson)
        {
            CryptoService cryptoService = new CryptoService();

            connectionDB();

            SQLiteCommand sqlCommand = new SQLiteCommand(dbConnection);

            sqlCommand.CommandText = "update person set name=@name,surname=@surname,phone=@phone,email=@email,description=@description where id = '" + updatedPerson.id + "'" + " AND user_id='" + UserModel.id + "'";

            sqlCommand.Parameters.AddWithValue("@name", cryptoService.textEncrytion(updatedPerson.name));
            sqlCommand.Parameters.AddWithValue("@surname", cryptoService.textEncrytion(updatedPerson.surname));
            sqlCommand.Parameters.AddWithValue("@phone", cryptoService.textEncrytion(updatedPerson.phone));
            sqlCommand.Parameters.AddWithValue("@email", cryptoService.textEncrytion(updatedPerson.email));
            sqlCommand.Parameters.AddWithValue("@description", cryptoService.textEncrytion(updatedPerson.description));
            sqlCommand.ExecuteNonQuery();

            dbConnection.Close();
        }
    }
}

