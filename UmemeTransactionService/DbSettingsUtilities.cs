using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmemeTransactionService
{
    class DbSettingsUtilities
    {

        // ... CONNECTION STRING
        public static string DB_CONN_STRING = System.Configuration.ConfigurationManager.ConnectionStrings["TORAN_TEST"].ConnectionString;


        // ... QUERY 1: Save Umeme customer if they dont exist in the table
        public static string PROC_SAVE_UMEME_CUST = "saveUmemeCustomer";

        // ... QUERY 2: Save Utility Customer Inqury
        public static string SAVE_UMEME_UTILITY_CUST_INQUIRY = "INSERT INTO [dbo].[TBL_UTILITY_CUST_INQUIRIES] (Vendor_CustID,Requesting_User"
           + ",Utility_Company,Inquiry_Date) VALUES"
           + "(@Vendor_CustID,@Requesting_User,@Utility_Company,@Inquiry_Date)";

        // .. QUERY 3: Update Utility Customer Inquiry
        public static string UPDATE_UMEME_UTILITY_CUST_INQUIRY = "UPDATE [dbo].[TBL_UTILITY_CUST_INQUIRIES] SET Response_Status=@Response_Status,"
            + "Balance=@Balance, Credit=@Credit, Cust_Name=@Cust_Name "
            //+ ", Cust_Address=@Cust_Address,Field1=@Field1, Field2=@Field2, Field3=@Field3"
            //+ ",Field4=@Field4, Field5=@Field5, Field6=@Field6, Field7=@Field7, Field8=@Field8, Field9=@Field9, Field10=@Field10"
            + "WHERE Vendor_CustID=@Vendor_CustID AND Inquiry_Date=@Inqury_Date";

        // ... QUERY 4: Get UMEME customer
        public static string GET_UMEME_CUSTOMER = "SELECT * FROM [dbo].[TBL_UMEME_CUSTOMERS] where Customer_Ref=@cust_ref";

        // ... QUERY 5: UMEME Save transaction
        public static string SAVE_UMEME_TRANSACTION = "INSERT INTO [dbo].[TBL_UTILITY_TRANSACTIONS] "
           + "(Tran_Amount,Customer_Ref,Customer_Name,Vendor,Transaction_ID"
           + ",Request_Time,Transaction_Mode"
           + ",Payment_Method,Customer_Tel,Narrative"
           + ",Teller_ID,Reversed,Payment_Type,Payment_Date,Customer_Type"
           + ") VALUES "

           + "(@tranAmount,@customerRef,@customerName,@vendor,@vendorTranId,"
           + "@requestTime,@offline,@tranType,@customerTel,"
           + "@tranNarration,@teller,@reversal,@paymentType,@paymentDate,@customerType"
           + ")";

        // .. QUERY 6: UMEME Update transaction
        public static string UPDATE_UMEME_TRANSACTION = "UPDATE [dbo].[TBL_UTILITY_TRANSACTIONS] SET "
            + "Status=@statusDescription, Reversed=@reversal, Transaction_Mode=@offline, Sent=@sent "
            + "WHERE Transaction_ID=@vendorTranId";

        // ... QUERY 7: UMEME Save core banking response
        public static string SAVE_CB_RESPONSE = "UPDATE [dbo].[TBL_UTILITY_TRANSACTIONS] SET "
            + "CB_ResponseCode=@respCode, CB_ResponseMsg=@respMsg, CB_ResponseTime=@respTime "
            + "WHERE Transaction_ID=@tranId";

        // ... QUERY 7: UMEME Save vendor response
        public static string SAVE_VENDOR_RESPONSE = "UPDATE [dbo].[TBL_UTILITY_TRANSACTIONS] SET "
            + "Vendor_TranID=@receiptNumber, Vendor_ResponseCode=@vendorRespCode, Vendor_ResponseMsg=@vendorRespMsg,"
            + "Vendor_ResponseTime=@respTime, Status=@status WHERE Transaction_ID=@vendorTranId";

        // ... QUERY 8: UMEME Save Yaka token
        public static string SAVE_YAKA_TOKEN = "INSERT INTO [dbo].[TBL_UMEME_TOKENS] "
           + "(RemainingCredit, LifeLine, ServiceFee, PayAccount, DebtRecovery, ReceiptNumber, StatusDescription, StatusCode, MeterNumber "
           + ",Units, TokenValue, Inflation, Tax, Fx, Fuel, TotalAmount, PrepaidToken, RecordDate"
           //+ ", Field1, Field2, Field3, Field4' Field5"
           + ") VALUES ("
           + "@RemainingCredit, @LifeLine, @ServiceFee, @PayAccount, @DebtRecovery, @ReceiptNumber, @StatusDescription, @StatusCode, @MeterNumber"
           + ", @Units, @TokenValue, @Inflation, @Tax, @Fx, @Fuel, @TotalAmount, @PrepaidToken, @RecordDate"
           // + ", @Field1, @Field2, @Field3, @Field4, @Field5"
           + ")";

        // ... QUERY 9: Logging activity to the db
        public static string INSERT_ACTIVITY_LOGS = "INSERT INTO [dbo].[TBL_ACTIVITY_LOGS](log_date, log_message) VALUES(@log_date, @log_message)";


        // ... QUERY 10: Updating the logged transactions. Parameters(MethodName, Msisdn, LocalStan, SourceAccount)
        public static string UPDATE_TRAN_LOGS = "UPDATE [dbo].[TBL_TRANSACTION_LOG] SET respParam1=@respParam1, respParam2=@respParam2, " +
                                                        "respParam3=@respParam3, respParam4=@respParam4, respParam5=@respParam5, " +
                                                        "respParam6=@respParam6, respParam7=@respParam7, respParam8=@respParam8, " +
                                                        "respParam9=@respParam9, respParam10=@respParam10, respParam11=@respParam11, " +
                                                        "respParam12=@respParam12, respParam13=@respParam13, respParam14=@respParam14, " +
                                                        "respParam15=@respParam15, respParam16=@respParam16, respParam17=@respParam17, " +
                                                        "respParam18=@respParam18, respParam19=@respParam19, respParam20=@respParam20 " +
                                                 "WHERE methodName=@methodName " +      // ... MethodName
                                                   "AND reqParam4=@reqParam4 " +        // ... Msisdn
                                                   "AND reqParam10=@reqParam10 " +      // ... Local Stan
                                                   "AND reqUser=@reqUser";              // ... Source Account


        // ... QUERY 11: Inserting the Transaction Logs
        public static string INSERT_TRAN_LOGS = "INSERT INTO [dbo].[TBL_TRANSACTION_LOG](methodName, reqParam1, reqParam2, reqParam3, " +
                                                                            "reqParam4, reqParam5, reqParam6, reqParam7, " +
                                                                            "reqParam8, reqParam9, reqParam10, reqParam11, " +
                                                                            "reqParam12, reqParam13, reqParam14, reqParam15, " +
                                                                            "reqParam16, reqParam17, reqParam18, reqParam19, " +
                                                                            "reqParam20, reqUser, reqDate) " +
                                                                     "VALUES(@methodName, @reqParam1, @reqParam2, @reqParam3, " +
                                                                            "@reqParam4, @reqParam5, @reqParam6, @reqParam7, " +
                                                                            "@reqParam8, @reqParam9, @reqParam10, @reqParam11, " +
                                                                            "@reqParam12, @reqParam13, @reqParam14, @reqParam15, " +
                                                                            "@reqParam16, @reqParam17, @reqParam18, @reqParam19, " +
                                                                            "@reqParam20, @reqUser, @reqDate)";

        // ... QUERY 12: UMEME Get offline customer
        public static string PROC_GET_UMEME_OFFLINE_TRANSACTION = "getUmemeOfflineTransaction";

    }
}
