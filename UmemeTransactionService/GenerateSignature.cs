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
            string text = text to sign;
            //return Convert.ToBase64String(Encoding.UTF8.GetBytes(text));

            return siiign0(text);
        }

        public static string siiign0(string text)
        {
            // retrieve private key
            try
            {
                string certificate = @"path to pfx file";
                X509Certificate2 cert = new X509Certificate2(certificate, "password", X509KeyStorageFlags.UserKeySet);
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
    }
}
