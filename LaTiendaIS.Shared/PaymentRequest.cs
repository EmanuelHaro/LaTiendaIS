using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaTiendaIS.Shared
{
    public class PaymentRequest
    {
        public string SiteTransactionId { get; set; }
        public int PaymentMethodId { get; set; }
        public string Token { get; set; }
        public string Bin { get; set; }
        public double Amount { get; set; }
        public string Currency { get; set; }
        public int Installments { get; set; }
        public string Description { get; set; }
        public string PaymentType { get; set; }
        public string EstablishmentName { get; set; }
        public List<SubPayment> SubPayments { get; set; }
    }

    public class SubPayment
    {
        public string SiteId { get; set; }
        public double Amount { get; set; }
        public int? Installments { get; set; }
    }

}
