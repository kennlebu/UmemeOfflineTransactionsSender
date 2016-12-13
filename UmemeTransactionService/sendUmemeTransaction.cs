using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UmemeTransactionService.Helpers;
using UmemeTransactionService.Models;
using UmemeTransactionService.UmemeBillingInterface;

namespace UmemeTransactionService
{
    class sendUmemeTransaction
    {
        private String vendorCode = ConfigurationManager.AppSettings["VendorCode"];
        private String password = ConfigurationManager.AppSettings["VendorKey"];
        private string UMEME_Account = ConfigurationManager.AppSettings["UMEME_Account"];

        // Add this line before a method call to suppress SSL handsake error. (SecurityNegotiationException)
        // Add it every time the Service Reference is updated coz it automatically deletes manually written code
        //System.Net.ServicePointManager.ServerCertificateValidationCallback += delegate { return true; };
        //


        public void UMEME_PostBankUmemePayment()
        {
            DBOperationsUtilities dbops = new DBOperationsUtilities();
            DateTime date = DateTime.Now;
            Dictionary<string, object> dictionary = new Dictionary<string, object>();//for only response code and status
            string saved = "0";
            
            Transaction tran = dbops.offlineTransaction();
            if (tran.CustomerRef != null && tran.CustomerRef != "")
            {
                tran.Password = password;
                tran.VendorCode = vendorCode;

                // ... logging the request
                string request_message = "UMEME (Offline) Post Bank Umeme Payment. DETAILS(Customer Ref.: " + tran.CustomerRef + "Customer Name: " + tran.CustomerName + " Tran Amount: " + tran.TranAmount + " )";
                ApplicationLogger.WriteToFile("{0} REQUEST: " + request_message);

                try
                {
                    Response response = new EPaymentSoapClient().PostBankUmemePayment(tran);

                    if (response.StatusCode.Equals("0"))
                    {
                        // ... logging the success
                        string message = "UMEME (Offline) Post Bank Umeme Payment. DETAILS(Receipt Number.: " + response.ReceiptNumber + " Customer Ref.: " + tran.CustomerRef + " Customer Name: " + tran.CustomerName + " Bank Ref.: " + tran.VendorTranId + ")";
                        ApplicationLogger.WriteToFile("{0} SUCCESS: " + message);

                        UMEME_TransactionResponse tr = new UMEME_TransactionResponse();
                        tr.ExtensionData = response.ExtensionData;
                        tr.ReceiptNumber = response.ReceiptNumber;
                        tr.StatusCode = response.StatusCode;
                        tr.StatusDescription = response.StatusDescription;
                        tr.Token = response.Token;
                        tr.BankReceiptNumber = tran.VendorTranId;

                        Transaction tran_to_update = new Transaction();
                        tran_to_update.StatusCode = response.StatusCode;
                        tran_to_update.StatusDescription = response.StatusDescription;
                        tran_to_update.VendorTranId = tran.VendorTranId;
                        tran_to_update.Reversal = tran.Reversal;
                        tran_to_update.Offline = "0";

                        saved = dbops.updateUmemeTransaction(tran_to_update, "2");
                        saved = dbops.saveUmemeVendorResponse(tran.VendorTranId, response.ReceiptNumber, response.StatusCode, response.StatusDescription, "SUCCESS");

                    }
                    else
                    {

                        // ... logging the failure
                        string failure_message = "UMEME (Offline) Post Bank Umeme Payment. DETAILS(Customer Ref.: " + tran.CustomerRef + " Customer Name: " + tran.CustomerName + ", Teller: " + tran.Teller + ")";
                        ApplicationLogger.WriteToFile("{0} FAILURE: " + failure_message);

                        Transaction tran_to_update = new Transaction();
                        tran_to_update.StatusCode = response.StatusCode;
                        tran_to_update.StatusDescription = response.StatusDescription;
                        tran_to_update.VendorTranId = tran.VendorTranId;
                        tran_to_update.Reversal = tran.Reversal;
                        tran_to_update.Offline = tran.Offline;

                        saved = dbops.updateUmemeTransaction(tran_to_update, "2");
                        saved = dbops.saveUmemeVendorResponse(tran.VendorTranId, "", response.StatusCode, response.StatusDescription, "FAILURE");


                        // Do a reversal??

                    }
                }
                catch (Exception e)
                {
                    ApplicationLogger.WriteToFile("{0} SYSTEM FAILURE: " + e.Message + e.StackTrace);
                    //return 1;
                }
            }
            ApplicationLogger.WriteToFile("{0} No offline transactions found");

        }
    }
}
