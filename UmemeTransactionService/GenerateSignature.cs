using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using UmemeTransactionService.Helpers;
using UmemeTransactionService.UmemeBillingInterface;

namespace UmemeTransactionService
{
    class GenerateSignature
    {
        public static string UMEME_signature(Transaction t)
        {
            string text = t.CustomerRef + t.CustomerName + t.CustomerTel + t.CustomerType + t.VendorTranId + t.VendorCode
              + t.Password + t.PaymentDate + t.PaymentType + t.Teller + t.TranAmount + t.TranNarration + t.TranType;
            //return Convert.ToBase64String(Encoding.UTF8.GetBytes(text));

            return siiign0(text);
        }

        public static string siiign0(string text)
        {
            // retrieve private key
            try
            {
                string certificate = @"C:\Users\Administrator\Desktop\cert\ToranUmeme.pfx";
                X509Certificate2 cert = new X509Certificate2(certificate, "r7tkm_posta", X509KeyStorageFlags.UserKeySet);
                //X509Certificate2 cert = get_certificate();
                RSACryptoServiceProvider rsa = (RSACryptoServiceProvider)cert.PrivateKey;

                // Hash the data
                SHA1Managed sha1 = new SHA1Managed();
                ASCIIEncoding encoding = new ASCIIEncoding();
                byte[] data = encoding.GetBytes(text);
                byte[] hash = sha1.ComputeHash(data);

                // Sign the hash
                byte[] digitalCert = rsa.SignHash(hash, CryptoConfig.MapNameToOID("SHA1"));
                string s = hash.ToString();
                string strDigCert = Convert.ToBase64String(digitalCert);
                return strDigCert;
            }
            catch (CryptographicException e)
            {
                ApplicationLogger.WriteToFile("{0} DEBUG: " + e.Message);
                return null;
            }
        }

        /// <summary>
        /// Finds a certificate using the X509FindType and Value in all stores
        /// </summary>
        /// <param name="findType"></param>
        /// <param name="findValue"></param>
        /// <returns></returns>
        //public static X509Certificate2Collection FindCertificateInAllStores(X509FindType findType, string findValue)
        //{
        //    //Get Certificates from Local Machine Store
        //    X509Certificate2Collection certificateCollectionLocalMachine =
        //    FindCertificateInStore(findType, findValue, StoreLocation.LocalMachine);

        //    //Get Certificates from Current User Store
        //    X509Certificate2Collection certificateCollectionCurrentUser =
        //    FindCertificateInStore(findType, findValue, StoreLocation.CurrentUser);

        //    //Merge Certificate/s collection
        //    certificateCollectionLocalMachine.AddRange(certificateCollectionCurrentUser);

        //    return certificateCollectionLocalMachine;
        //}

        /// <summary>
        /// Gets certificate, using  the specified X509FindType and Value, from a store location
        /// </summary>
        /// <param name="findType"></param>
        /// <param name="findValue"></param>
        /// <param name="storeLocation"></param>
        /// <returns></returns>
        //public static X509Certificate2Collection FindCertificateInStore(X509FindType findType, string findValue, StoreLocation storeLocation)
        //{

        //    X509Certificate2Collection certificateCollection = null;

        //    //Check in Local Machine Store
        //    X509Store store = new X509Store(StoreName.My, storeLocation);

        //    //Open Store
        //    store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);

        //    //Get the cert collection
        //    certificateCollection = store.Certificates.Find(findType, findValue, false);

        //    //Close Store
        //    store.Close();

        //    return certificateCollection;
        //}

        /// <summary>
        /// Sign Data using Certificate
        /// </summary>
        /// <param name="certificate"></param>
        /// <param name="dataToBeSigned"></param>
        /// <returns></returns>
        //public static byte[] SignData(X509Certificate2 certificate, byte[] dataToBeSigned)
        //{
        //    try
        //    {
        //        if (!certificate.HasPrivateKey)
        //            return null;

        //        //Create a RSA Provider, using the private key
        //        RSACryptoServiceProvider rsaCryptoServiceProvider = (RSACryptoServiceProvider)certificate.PrivateKey;

        //        //Sign the data using a desired hashing algorithm
        //        return rsaCryptoServiceProvider.SignData(dataToBeSigned, new SHA1CryptoServiceProvider());

        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}

        //public static string siiign1(string text)
        //{
        //    try
        //    {
        //        X509Certificate2 cert = FindCertificateInStore(X509FindType.FindByIssuerName, "ToranUmeme", StoreLocation.LocalMachine)[0];
        //        return Convert.ToBase64String(SignData(cert, Encoding.ASCII.GetBytes(text)));
        //    }
        //    catch (Exception e)
        //    {
        //        new ApplicationLogger().logToFile(e.Message, "DEBUG");
        //        return null;
        //    }
        //}

        //public static string siiign2(string text)
        //{
        //    X509Certificate2 publicCert = new X509Certificate2(@"C:\Users\Administrator\Desktop\PBUCert\PostBankUmeme.cer");

        //    //Fetch private key from the local machine store
        //    X509Certificate2 privateCert = null;
        //    X509Store store = new X509Store(StoreLocation.LocalMachine);
        //    store.Open(OpenFlags.ReadOnly);
        //    foreach (X509Certificate2 cert in store.Certificates)
        //    {
        //        if (cert.GetCertHashString() == publicCert.GetCertHashString())
        //            privateCert = cert;
        //    }

        //    //Round-trip the key to XML and back, there might be a better way but this works
        //    RSACryptoServiceProvider key = new RSACryptoServiceProvider();
        //    key.FromXmlString(privateCert.PrivateKey.ToXmlString(true));

        //    //Create some data to sign
        //    byte[] data = Encoding.ASCII.GetBytes(text);

        //    //Sign the data
        //    byte[] sig = key.SignData(data, CryptoConfig.MapNameToOID("SHA1"));

        //    return Convert.ToBase64String(sig);
        //}

        //public static string siiign3(string text)
        //{
        //    // Open certificate store of current user
        //    X509Store my = new X509Store(StoreName.My, StoreLocation.CurrentUser);
        //    //X509Store my = new X509Store("ToranUmeme", StoreLocation.LocalMachine);
        //    my.Open(OpenFlags.ReadOnly);

        //    // Look for the certificate with specific subject 
        //    RSACryptoServiceProvider csp = null;
        //    foreach (X509Certificate2 cert in my.Certificates)
        //    {
        //        //if (cert.Subject.Contains("CN=WINGROUP\\micwein"))
        //        if (cert.Subject.Contains("ToranUmeme"))
        //        {
        //            // retrieve private key
        //            csp = (RSACryptoServiceProvider)cert.PrivateKey;
        //        }
        //    }
        //    if (csp == null)
        //    {
        //        throw new Exception("Valid certificate was not found");
        //    }

        //    // Hash the data
        //    SHA1Managed sha1 = new SHA1Managed();
        //    //UnicodeEncoding encoding = new UnicodeEncoding();
        //    ASCIIEncoding encoding = new ASCIIEncoding();
        //    byte[] data = encoding.GetBytes(text);
        //    byte[] hash = sha1.ComputeHash(data);
        //    byte[] sig = csp.SignHash(hash, CryptoConfig.MapNameToOID("SHA1"));
        //    string strSig = Convert.ToBase64String(sig);
        //    // Sign the hash
        //    return strSig;
        //}

        //public static string siiign4(string text)
        //{
        //    // Access Personal (MY) certificate store of current user
        //    X509Store my = new X509Store(StoreName.My, StoreLocation.CurrentUser);
        //    my.Open(OpenFlags.ReadOnly);

        //    // Find the certificate we’ll use to sign
        //    RSACryptoServiceProvider csp = null;

        //    foreach (X509Certificate2 cert in my.Certificates)
        //    {
        //        if (cert.Subject.Contains("ToranUmeme"))
        //        {
        //            // We found it.
        //            // Get its associated CSP and private key
        //            csp = (RSACryptoServiceProvider)cert.PrivateKey;
        //        }
        //    }

        //    if (csp == null)
        //    {
        //        throw new Exception("No valid cert was found");
        //    }

        //    // Hash the data
        //    SHA1Managed sha1 = new SHA1Managed();
        //    UnicodeEncoding encoding = new UnicodeEncoding();

        //    byte[] data = encoding.GetBytes(text);
        //    byte[] hash = sha1.ComputeHash(data);

        //    // Sign the hash
        //    return Convert.ToBase64String(csp.SignHash(hash, CryptoConfig.MapNameToOID("SHA1")));
        //}

        //private static X509Certificate2 get_certificate()
        //{
        //    // Access Personal (MY) certificate store of current user
        //    X509Store my = new X509Store(StoreName.My, StoreLocation.CurrentUser);
        //    my.Open(OpenFlags.ReadOnly);

        //    // Find the certificate we’ll use to sign
        //    //RSACryptoServiceProvider csp = null;

        //    foreach (X509Certificate2 cert in my.Certificates)
        //    {
        //        if (cert.Subject.Contains("ToranUmeme"))
        //        {
        //            // We found it.
        //            return cert;
        //            // Get its associated CSP and private key
        //            //csp = (RSACryptoServiceProvider)cert.PrivateKey;
        //        }
        //    }

        //    //if (csp == null)
        //    //{
        //    //    throw new Exception("No valid cert was found");
        //    //}

        //    return null;
        //}
    }
}
