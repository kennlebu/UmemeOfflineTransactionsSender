using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace UmemeTransactionService.Models
{
    class UMEME_TransactionResponse
    {
        public ExtensionDataObject ExtensionData { get; set; }
        public string StatusCode { get; set; }
        public string Token { get; set; }
        public string StatusDescription { get; set; }
        public string ReceiptNumber { get; set; }
        public string BankReceiptNumber { get; set; }
    }
}
