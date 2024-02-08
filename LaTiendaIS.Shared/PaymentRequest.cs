using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaTiendaIS.Shared
{
    public class PaymentRequest
    {
        public string site_transaction_id { get; set; }
        public int payment_method_id { get; set; }
        public string token { get; set; }
        public string bin { get; set; }
        public double amount { get; set; }
        public string currency { get; set; }
        public int installments { get; set; }
        public string description { get; set; }
        public string payment_type { get; set; }
        public string establishment_name { get; set; }
        public List<SubPayment> sub_payments { get; set; }
    }

    public class SubPayment
    {
        public string site_id { get; set; }
        public double amount { get; set; }
        public int? installments { get; set; }
    }

}
