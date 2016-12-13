using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmemeTransactionService.Helpers
{
    class Codes
    {
        //                id	code	message
        //1003	107	Invalid Account Number
        //1004	108	Insufficient Funds
        //1005	109	Transcation not permitted to card holder
        //1006	110	Withdrawal Amount Exceed Limit
        //1007	111	Card Issuer inoperative
        //1008	112	Transfer Limit Exceed
        //1009	113	Requested Block Operation Failed since account is blocked/frozen 
        //1010	114	Card Issuer Timed out

        public static string SUCCESS = "000";
        public static string INVALIDPIN = "101";
        public static string NOREGISTERED = "102";
        public static string ACCOUNTDISABLED = "103";
        public static string TOOMANYATTEMPTS = "116";
        public static string DEVICECHANGED = "104";
        public static string NOTACTIVATED = "105";
        public static string FUNNY = "106";
        public static string GENERROR = "303";

        public static string INVALIDACCTNO = "107";
        public static string INSUFFUNDS = "108";
        public static string TRANSNOTPERMITED = "109";
        public static string EXCEEDWITHDRAMOUNT = "110";
        public static string CARDISSUER_INOP = "111";
        public static string EXCEDTRANSFERLIMT = "112";
        public static string ACCTFROZEN = "113";
        public static string TIMEDOUT = "114";
        public static string MORTARERROR = "115";

        public static string INVALID_TAN = "118";
        public static string EXPIRED_TAN = "119";
        public static string USED_TAN = "120";
        public static string INACTIVE_TAN = "121";
        public static string INVALID_CHANNEL = "122";

    }
}
