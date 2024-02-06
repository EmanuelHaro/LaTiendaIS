using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LaTiendaIS.Shared
{
    public class TokenRequest
    {
        public string card_number { get; set; }
        public string card_expiration_month { get; set; }
        public string card_expiration_year { get; set; }
        public string security_code { get; set; }
        public string card_holder_name { get; set; }
        public CardHolderIdentification card_holder_identification { get; set; }
    }
    public class CardHolderIdentification
    {
        public string type { get; set; }
        public string number { get; set; }
    }
}
