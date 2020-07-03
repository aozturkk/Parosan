using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parosan.Model
{
    class PaymentModel
    {
        public int id { get; set; }
        public String title { get; set; }
        public String card_number { get; set; }
        public String expiry_date { get; set; }
        public String cvc { get; set; }
        public String pin { get; set; }
        public int user_id { get; set; }
    }
}
