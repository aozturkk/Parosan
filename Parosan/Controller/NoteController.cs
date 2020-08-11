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
    class NoteController
    {

        private static int lastID = 0;
        private DatabaseController db = new DatabaseController();
        public List<NoteModel> listNote()
        {
            CryptoService cryptoService = new CryptoService();
            

            SQLiteCommand sqlCommand = new SQLiteCommand("select * from note where user_id=" + UserModel.id, db.connection());     
            SQLiteDataReader reader = sqlCommand.ExecuteReader();

            List<NoteModel> notes = new List<NoteModel>();

            while (reader.Read())
            {
                NoteModel temp = new NoteModel();
                
                temp.id = reader.GetInt32(reader.GetOrdinal("id")) ;
                temp.title = cryptoService.textDecrytion( reader.GetString(reader.GetOrdinal("title")) );
                temp.content = cryptoService.textDecrytion( reader.GetString(reader.GetOrdinal("content")) );
                temp.date = cryptoService.textDecrytion( reader.GetString(reader.GetOrdinal("date")) );
                temp.user_id = reader.GetInt32(reader.GetOrdinal("user_id"));
                lastID = temp.id;
                notes.Add(temp);

            }

            db.connection().Close();

            return notes;
            
        }

        public void addNewNote(NoteModel note)
        {

            CryptoService cryptoService = new CryptoService();
            SQLiteCommand sqlCommand = new SQLiteCommand(db.connection());

            sqlCommand.CommandText = "insert into note (id,title,content,date,user_id) Values (@id, @title,@content,@date,@user_id)";
            sqlCommand.Parameters.AddWithValue("id", lastID + 1);
            sqlCommand.Parameters.AddWithValue("title", cryptoService.textEncrytion(note.title));
            sqlCommand.Parameters.AddWithValue("content", cryptoService.textEncrytion(note.content));
            sqlCommand.Parameters.AddWithValue("date", cryptoService.textEncrytion(note.date));
            sqlCommand.Parameters.AddWithValue("user_id", UserModel.id);
            sqlCommand.ExecuteNonQuery();

            db.connection().Close();
        }

      
        public void updateNote(NoteModel updatedNote)
        {
            CryptoService cryptoService = new CryptoService();

            SQLiteCommand sqlCommand = new SQLiteCommand(db.connection());

            sqlCommand.CommandText = "update note set title=@title,content=@content,date=@date where id = '" + updatedNote.id + "'" + " AND user_id='" + UserModel.id + "'";

            sqlCommand.Parameters.AddWithValue("@title", cryptoService.textEncrytion(updatedNote.title));
            sqlCommand.Parameters.AddWithValue("@content", cryptoService.textEncrytion(updatedNote.content));
            sqlCommand.Parameters.AddWithValue("@date", cryptoService.textEncrytion(updatedNote.date));
            
            sqlCommand.ExecuteNonQuery();

            db.connection().Close();
        }


        public void deleteNote(int id)
        {   

            SQLiteCommand sqlCommand = new SQLiteCommand(db.connection());
            sqlCommand.CommandText = "DELETE FROM note WHERE id=" + id + " AND user_id=" + UserModel.id;
            sqlCommand.ExecuteScalar();
            db.connection().Close();
        }
    }
}
