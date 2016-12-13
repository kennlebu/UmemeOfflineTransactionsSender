using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UmemeTransactionService.Helpers;
using UmemeTransactionService.UmemeBillingInterface;

namespace UmemeTransactionService
{
    class DBOperationsUtilities
    {
        #region ... CLASS VARIABLES
        public string DB_CONN_STRING = DbSettingsUtilities.DB_CONN_STRING;
        //ApplicationLogger applogger = new ApplicationLogger();
        #endregion


        #region ... METHOD 1: Updating the Logged Transaction
        public bool updateLoggedTransactionRequest(TransactionLog tranlog)
        {
            bool isUpdated = false;
            //ApplicationLogger applogger = new ApplicationLogger();

            // ... unpacking the transaction log
            string methodName = tranlog.methodName;         // ... Processing Code
            string reqParam4 = tranlog.reqParam4;           // ... msisdn (phone number)
            string reqParam10 = tranlog.reqParam10;         // ... Local Stan

            string respParam1 = tranlog.respParam1;         // ... Response UserName
            string respParam2 = tranlog.respParam2;         // ... Response Processing Code
            string respParam3 = tranlog.respParam3;         // ... Response Transaction Amount
            string respParam4 = tranlog.respParam4;         // ... Response Stan Number
            var respParam5 = tranlog.respParam5;         // ... System Posted Datetime
            var respParam6 = tranlog.respParam6;         // ... Posted Date
            string respParam7 = tranlog.respParam7;         // ... inst
            string respParam8 = tranlog.respParam8;         // ... channel
            string respParam9 = tranlog.respParam9;         // ... Switch Response Code
            string respParam10 = tranlog.respParam10;       // ... devid
            string respParam11 = tranlog.respParam11;       // ... rep_original_stan
            string respParam12 = tranlog.respParam12;       // ... Balance (On Balance Inquiry and Mini Statement)
            string respParam13 = tranlog.respParam13;       // ... 800
            string respParam14 = tranlog.respParam14;       // ... null
            string respParam15 = tranlog.respParam15;       // ... SWT
            string respParam16 = tranlog.respParam16;       // ... List of Transactions (On ministatement)
            string respParam17 = tranlog.respParam17;       // ...
            string respParam18 = tranlog.respParam18;       // ...
            string respParam19 = tranlog.respParam19;       // ... 
            string respParam20 = tranlog.respParam20;       // ... 
            string reqUser = tranlog.reqUser;               // ... Source Account
            var reqDate = tranlog.reqDate;                  // ... Transaction Date



            // ... connecting to the db
            using (SqlConnection conn = new SqlConnection(DB_CONN_STRING))
            {
                try
                {
                    conn.Open();
                    SqlCommand update_tranlogs = new SqlCommand(DbSettingsUtilities.UPDATE_TRAN_LOGS, conn);
                    update_tranlogs.Parameters.Add(new SqlParameter("respParam1", respParam1));
                    update_tranlogs.Parameters.Add(new SqlParameter("respParam2", respParam2));
                    update_tranlogs.Parameters.Add(new SqlParameter("respParam3", respParam3));
                    update_tranlogs.Parameters.Add(new SqlParameter("respParam4", respParam4));
                    update_tranlogs.Parameters.Add(new SqlParameter("respParam5", respParam5));
                    update_tranlogs.Parameters.Add(new SqlParameter("respParam6", respParam6));
                    update_tranlogs.Parameters.Add(new SqlParameter("respParam7", respParam7));
                    update_tranlogs.Parameters.Add(new SqlParameter("respParam8", respParam8));
                    update_tranlogs.Parameters.Add(new SqlParameter("respParam9", respParam9));
                    update_tranlogs.Parameters.Add(new SqlParameter("respParam10", respParam10));
                    update_tranlogs.Parameters.Add(new SqlParameter("respParam11", respParam11));
                    update_tranlogs.Parameters.Add(new SqlParameter("respParam12", respParam12));
                    update_tranlogs.Parameters.Add(new SqlParameter("respParam13", respParam13));
                    update_tranlogs.Parameters.Add(new SqlParameter("respParam14", respParam14));
                    update_tranlogs.Parameters.Add(new SqlParameter("respParam15", respParam15));
                    update_tranlogs.Parameters.Add(new SqlParameter("respParam16", respParam16));
                    update_tranlogs.Parameters.Add(new SqlParameter("respParam17", respParam17));
                    update_tranlogs.Parameters.Add(new SqlParameter("respParam18", respParam18));
                    update_tranlogs.Parameters.Add(new SqlParameter("respParam19", respParam19));
                    update_tranlogs.Parameters.Add(new SqlParameter("respParam20", respParam20));
                    update_tranlogs.Parameters.Add(new SqlParameter("methodName", methodName));
                    update_tranlogs.Parameters.Add(new SqlParameter("reqParam4", reqParam4));
                    update_tranlogs.Parameters.Add(new SqlParameter("reqParam10", reqParam10));
                    update_tranlogs.Parameters.Add(new SqlParameter("reqUser", reqUser));

                    if (update_tranlogs.ExecuteNonQuery() == 1)
                    {
                        isUpdated = true;
                    }

                }
                catch (Exception mm)
                {
                    // ... logging application failures
                    string failure_message = "Transaction Update on Logged Error. Details(Msisdn: " + reqParam4 + " AccountNo: " + reqUser + " ERROR MESSAGE: " + mm.Message + ")";
                    ApplicationLogger.WriteToFile("{0} SYSTEM FAILURE: " + failure_message);
                }   // ... end of try catch
            }
            // ... end of connection


            return isUpdated;
        }
        #endregion


        #region ... METHOD 2: Logging Transaction Logs and Requests
        public bool logTransactionRequest(TransactionLog tranlog)
        {
            bool isLogged = false;
            //ApplicationLogger applogger = new ApplicationLogger();

            // ... unpacking the transaction log
            string methodName = tranlog.methodName;         // ... processing code
            string reqParam1 = tranlog.reqParam1;           // ... username
            string reqParam2 = tranlog.reqParam2;           // ... transaction_amount
            string reqParam3 = tranlog.reqParam3;           // ... reserved field
            string reqParam4 = tranlog.reqParam4;           // ... msisdn (phone number)
            string reqParam5 = tranlog.reqParam5;
            string reqParam6 = tranlog.reqParam6;
            string reqParam7 = tranlog.reqParam7;
            string reqParam8 = tranlog.reqParam8;
            string reqParam9 = tranlog.reqParam9;
            string reqParam10 = tranlog.reqParam10;         // ... localStan
            string reqParam11 = tranlog.reqParam11;         // ... 
            string reqParam12 = tranlog.reqParam12;         // ... 
            string reqParam13 = tranlog.reqParam13;         // ... 
            string reqParam14 = tranlog.reqParam14;         // ...
            string reqParam15 = tranlog.reqParam15;
            string reqParam16 = tranlog.reqParam16;
            string reqParam17 = tranlog.reqParam17;
            string reqParam18 = tranlog.reqParam18;
            string reqParam19 = tranlog.reqParam19;
            string reqParam20 = tranlog.reqParam20;
            string reqUser = tranlog.reqUser;               // ... Source Account
            var reqDate = tranlog.reqDate;                  // ... Transaction Date

            // ... connecting to the db
            using (SqlConnection conn = new SqlConnection(DB_CONN_STRING))
            {
                try
                {
                    conn.Open();
                    SqlCommand insert_tranlogs = new SqlCommand(DbSettingsUtilities.INSERT_TRAN_LOGS, conn);
                    insert_tranlogs.Parameters.Add(new SqlParameter("methodName", methodName));
                    insert_tranlogs.Parameters.Add(new SqlParameter("reqParam1", reqParam1));
                    insert_tranlogs.Parameters.Add(new SqlParameter("reqParam2", reqParam2));
                    insert_tranlogs.Parameters.Add(new SqlParameter("reqParam3", reqParam3));
                    insert_tranlogs.Parameters.Add(new SqlParameter("reqParam4", reqParam4));
                    insert_tranlogs.Parameters.Add(new SqlParameter("reqParam5", reqParam5));
                    insert_tranlogs.Parameters.Add(new SqlParameter("reqParam6", reqParam6));
                    insert_tranlogs.Parameters.Add(new SqlParameter("reqParam7", reqParam7));
                    insert_tranlogs.Parameters.Add(new SqlParameter("reqParam8", reqParam8));
                    insert_tranlogs.Parameters.Add(new SqlParameter("reqParam9", reqParam9));
                    insert_tranlogs.Parameters.Add(new SqlParameter("reqParam10", reqParam10));
                    insert_tranlogs.Parameters.Add(new SqlParameter("reqParam11", reqParam11));
                    insert_tranlogs.Parameters.Add(new SqlParameter("reqParam12", reqParam12));
                    insert_tranlogs.Parameters.Add(new SqlParameter("reqParam13", reqParam13));
                    insert_tranlogs.Parameters.Add(new SqlParameter("reqParam14", reqParam14));
                    insert_tranlogs.Parameters.Add(new SqlParameter("reqParam15", reqParam15));
                    insert_tranlogs.Parameters.Add(new SqlParameter("reqParam16", reqParam16));
                    insert_tranlogs.Parameters.Add(new SqlParameter("reqParam17", reqParam17));
                    insert_tranlogs.Parameters.Add(new SqlParameter("reqParam18", reqParam18));
                    insert_tranlogs.Parameters.Add(new SqlParameter("reqParam19", reqParam19));
                    insert_tranlogs.Parameters.Add(new SqlParameter("reqParam20", reqParam20));
                    insert_tranlogs.Parameters.Add(new SqlParameter("reqUser", reqUser));
                    insert_tranlogs.Parameters.Add(new SqlParameter("reqDate", reqDate));

                    if (insert_tranlogs.ExecuteNonQuery() == 1)
                    {
                        isLogged = true;
                    }

                }
                catch (Exception mm)
                {
                    // ... logging application failures
                    string failure_message = "Transaction Logging Error. Details(Msisdn: " + reqParam4 + " AccountNo: " + reqUser + " ERROR MESSAGE: " + mm.Message + ")";
                    ApplicationLogger.WriteToFile("{0} SYSTEM FAILURE: " + failure_message);
                }   // ... end of try catch
            }
            // ... end of connection




            return isLogged;
        }
        #endregion


        #region ... METHOD 3: UMEME Save transaction
        public string saveUmemeTransaction(Transaction tran)
        {
            string save = "0";

            using (SqlConnection conn2 = new SqlConnection(DB_CONN_STRING))
            {
                try
                {
                    conn2.Open();

                    // ... Inserting the item into database
                    SqlCommand save_umeme_tran = new SqlCommand(DbSettingsUtilities.SAVE_UMEME_TRANSACTION, conn2);
                    //save_umeme_tran.Parameters.Add(new SqlParameter("statusDescription", tran.StatusDescription));
                    //save_umeme_tran.Parameters.Add(new SqlParameter("statusCode", tran.StatusCode));
                    save_umeme_tran.Parameters.Add(new SqlParameter("paymentDate", tran.PaymentDate));
                    save_umeme_tran.Parameters.Add(new SqlParameter("tranAmount", tran.TranAmount));
                    save_umeme_tran.Parameters.Add(new SqlParameter("teller", tran.Teller));
                    save_umeme_tran.Parameters.Add(new SqlParameter("tranNarration", tran.TranNarration));
                    save_umeme_tran.Parameters.Add(new SqlParameter("vendorTranId", tran.VendorTranId));
                    //save_umeme_tran.Parameters.Add(new SqlParameter("tranIdToReverse", tran.TranIdToReverse));
                    save_umeme_tran.Parameters.Add(new SqlParameter("paymentType", tran.PaymentType));
                    save_umeme_tran.Parameters.Add(new SqlParameter("tranType", tran.TranType));
                    save_umeme_tran.Parameters.Add(new SqlParameter("customerRef", tran.CustomerRef));
                    save_umeme_tran.Parameters.Add(new SqlParameter("customerName", tran.CustomerName));
                    save_umeme_tran.Parameters.Add(new SqlParameter("customerType", tran.CustomerType));
                    save_umeme_tran.Parameters.Add(new SqlParameter("customerTel", tran.CustomerTel));
                    save_umeme_tran.Parameters.Add(new SqlParameter("reversal", tran.Reversal));
                    save_umeme_tran.Parameters.Add(new SqlParameter("offline", tran.Offline));
                    save_umeme_tran.Parameters.Add(new SqlParameter("vendor", "UMEME"));
                    save_umeme_tran.Parameters.Add(new SqlParameter("requestTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm.ss")));

                    if (save_umeme_tran.ExecuteNonQuery() == 1)
                    {
                        //transaction saved
                        save = "1";
                    }

                    //s_id = ((int)save_umeme_tran.ExecuteScalar()).ToString();

                    //if (s_id != null && s_id != "0")
                    //{
                    //    // transaction saved
                    //    save = "1";
                    //    applogger.logToFile("Transaction saved", "SUCCESS");
                    //}
                    //else
                    //{
                    //    applogger.logToFile("Transaction not saved", "SYSTEM FAILURE");
                    //}


                    conn2.Close();
                }
                catch (Exception mm)
                {
                    // ... logging application failures
                    string request_message = "Saving UMEME transaction With Customer Ref: " + tran.CustomerRef + ", ERROR MESSAGE: " + mm.Message + ")";
                    ApplicationLogger.WriteToFile("{0} SYSTEM FAILURE: " + request_message);
                }   // ... end of try catch
            }   // ... end of connection
            return save;
        }
        #endregion


        #region ... METHOD 4: UMEME Update transaction
        public string updateUmemeTransaction(Transaction tran, string sent)
        {
            string save = "0";

            using (SqlConnection conn2 = new SqlConnection(DB_CONN_STRING))
            {
                try
                {
                    conn2.Open();

                    // ... Inserting the item into database
                    SqlCommand save_umeme_tran = new SqlCommand(DbSettingsUtilities.UPDATE_UMEME_TRANSACTION, conn2);
                    save_umeme_tran.Parameters.Add(new SqlParameter("statusDescription", tran.StatusDescription));
                    save_umeme_tran.Parameters.Add(new SqlParameter("statusCode", tran.StatusCode));
                    //save_umeme_tran.Parameters.Add(new SqlParameter("paymentDate", tran.PaymentDate));
                    //save_umeme_tran.Parameters.Add(new SqlParameter("tranAmount", tran.TranAmount));
                    //save_umeme_tran.Parameters.Add(new SqlParameter("teller", tran.Teller));
                    //save_umeme_tran.Parameters.Add(new SqlParameter("tranNarration", tran.TranNarration));
                    save_umeme_tran.Parameters.Add(new SqlParameter("vendorTranId", tran.VendorTranId));
                    //save_umeme_tran.Parameters.Add(new SqlParameter("tranIdToReverse", tran.TranIdToReverse));
                    //save_umeme_tran.Parameters.Add(new SqlParameter("paymentType", tran.PaymentType));
                    //save_umeme_tran.Parameters.Add(new SqlParameter("tranType", tran.TranType));
                    //save_umeme_tran.Parameters.Add(new SqlParameter("customerRef", tran.CustomerRef));
                    //save_umeme_tran.Parameters.Add(new SqlParameter("customerName", tran.CustomerName));
                    //save_umeme_tran.Parameters.Add(new SqlParameter("customerType", tran.CustomerType));
                    //save_umeme_tran.Parameters.Add(new SqlParameter("customerTel", tran.CustomerTel));
                    save_umeme_tran.Parameters.Add(new SqlParameter("reversal", tran.Reversal));
                    save_umeme_tran.Parameters.Add(new SqlParameter("offline", tran.Offline));
                    save_umeme_tran.Parameters.Add(new SqlParameter("sent", sent));
                    //save_umeme_tran.Parameters.Add(new SqlParameter("requestTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm.ss")));

                    if (save_umeme_tran.ExecuteNonQuery() == 1)
                    {
                        //transaction saved
                        save = "1";
                    }

                    //s_id = ((int)save_umeme_tran.ExecuteScalar()).ToString();

                    //if (s_id != null && s_id != "0")
                    //{
                    //    // transaction saved
                    //    save = "1";
                    //    applogger.logToFile("Transaction saved", "SUCCESS");
                    //}
                    //else
                    //{
                    //    applogger.logToFile("Transaction not saved", "SYSTEM FAILURE");
                    //}


                    conn2.Close();
                }
                catch (Exception mm)
                {
                    // ... logging application failures
                    string request_message = "Updating UMEME transaction With Customer Ref: " + tran.CustomerRef + ", ERROR MESSAGE: " + mm.Message + ", stackTrace1: " + mm.StackTrace + ")";
                    ApplicationLogger.WriteToFile("{0} SYSTEM FAILURE: " + request_message);
                }   // ... end of try catch
            }   // ... end of connection
            return save;
        }
        #endregion


        #region ... METHOD 5: UMEME Save core banking response
        internal void saveCBResponse(string tranID, string msg, string code)
        {
            string save = "0";

            using (SqlConnection conn2 = new SqlConnection(DB_CONN_STRING))
            {
                try
                {
                    conn2.Open();

                    // ... Inserting the item into database
                    SqlCommand save_cb_response = new SqlCommand(DbSettingsUtilities.SAVE_CB_RESPONSE, conn2);
                    save_cb_response.Parameters.Add(new SqlParameter("respCode", code));
                    save_cb_response.Parameters.Add(new SqlParameter("respMsg", msg));
                    save_cb_response.Parameters.Add(new SqlParameter("respTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm.ss")));
                    save_cb_response.Parameters.Add(new SqlParameter("tranId", tranID));

                    if (save_cb_response.ExecuteNonQuery() == 1)
                    {
                        //transaction saved
                        save = "1";
                    }

                    conn2.Close();
                }
                catch (Exception mm)
                {
                    // ... logging application failures
                    string request_message = "Saving core banking response With Transaction ID: " + tranID + ", ERROR MESSAGE: " + mm.Message + ")";
                    ApplicationLogger.WriteToFile("{0} SYSTEM FAILURE: " + request_message);
                }   // ... end of try catch
            }   // ... end of connection
        }
        #endregion


        #region ... METHOD 6: UMEME Save vendor response
        internal string saveUmemeVendorResponse(string tranID, string receiptNum, string respCode, string respMsg, string status)
        {
            string save = "0";

            using (SqlConnection conn2 = new SqlConnection(DB_CONN_STRING))
            {
                try
                {
                    conn2.Open();

                    // ... Inserting the item into database
                    SqlCommand save_vendor_response = new SqlCommand(DbSettingsUtilities.SAVE_VENDOR_RESPONSE, conn2);
                    save_vendor_response.Parameters.Add(new SqlParameter("receiptNumber", receiptNum));
                    save_vendor_response.Parameters.Add(new SqlParameter("vendorRespCode", respCode));
                    save_vendor_response.Parameters.Add(new SqlParameter("vendorRespMsg", respMsg));
                    save_vendor_response.Parameters.Add(new SqlParameter("respTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm.ss")));
                    save_vendor_response.Parameters.Add(new SqlParameter("vendorTranId", tranID));
                    save_vendor_response.Parameters.Add(new SqlParameter("status", status));

                    if (save_vendor_response.ExecuteNonQuery() == 1)
                    {
                        //transaction saved
                        save = "1";
                    }

                    conn2.Close();
                }
                catch (Exception mm)
                {
                    // ... logging application failures
                    string request_message = "Saving vendor response With Transaction ID: " + tranID + ", ERROR MESSAGE: " + mm.Message + ")";
                    ApplicationLogger.WriteToFile("{0} SYSTEM FAILURE: " + request_message);
                }   // ... end of try catch
            }   // ... end of connection
            return save;
        }
        #endregion


        #region ... METHOD 7: UMEME Get offline transaction
        public Transaction offlineTransaction()
        {
            Transaction result = new Transaction();

            using (SqlConnection conn2 = new SqlConnection(DB_CONN_STRING))
            {
                try
                {
                    conn2.Open();

                    // ... Reading the item from database
                    SqlCommand get_umeme_offline_tran = new SqlCommand(DbSettingsUtilities.PROC_GET_UMEME_OFFLINE_TRANSACTION, conn2);
                    get_umeme_offline_tran.CommandType = System.Data.CommandType.StoredProcedure;

                    using (SqlDataReader reader = get_umeme_offline_tran.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            try
                            {
                                result.CustomerName = reader["Customer_Name"].ToString();
                                result.CustomerRef = reader["Customer_Ref"].ToString();
                                result.CustomerTel = reader["Customer_Tel"].ToString();
                                result.CustomerType = reader["Customer_Type"].ToString();
                                result.Offline = reader["Transaction_Mode"].ToString();
                                result.PaymentDate = reader["Payment_Date"].ToString();
                                result.PaymentType = reader["Payment_Type"].ToString();
                                result.Reversal = reader["Reversed"].ToString();
                                result.Teller = reader["Teller_ID"].ToString();
                                result.TranAmount = reader["Tran_Amount"].ToString();
                                result.TranNarration = reader["Narrative"].ToString();
                                result.TranType = reader["Payment_Method"].ToString();
                                result.VendorTranId = reader["Transaction_ID"].ToString();

                                result.DigitalSignature = GenerateSignature.UMEME_signature(result);


                            }
                            catch (IndexOutOfRangeException e)
                            {
                                ApplicationLogger.WriteToFile("{0} SYSTEM FAILURE: " + e.Message);
                            }

                        }   // ... end of while loop
                    }   // ... end of reader                    

                    conn2.Close();
                }
                catch (Exception mm)
                {
                    // ... logging application failures
                    string request_message = "Getting UMEME (Offline) Transaction; " + ", ERROR MESSAGE: " + mm.Message + ")";
                    ApplicationLogger.WriteToFile("{0} SYSTEM FAILURE: " + request_message);
                }   // ... end of try catch
            }   // ... end of connection
            return result;
        }

        #endregion
    }
}
