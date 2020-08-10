using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parosan.Model
{
    class NoteModel
    {

        public int id { get; set; }
        public String title { get; set; }
        public String content { get; set; }
        public String date { get; set; }
        public int user_id { get; set; }
    }
}
