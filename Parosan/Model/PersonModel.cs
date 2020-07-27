using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parosan.Model
{
    class PersonModel
    {
        public int id { get; set; }
        public String name { get; set; }
        public String surname { get; set; }
        public String phone { get; set; }
        public String email { get; set; }
        public String description { get; set; }
        public int user_id { get; set; }
    }
}
