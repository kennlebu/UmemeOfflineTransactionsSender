using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmemeTransactionService.Helpers
{
    static class ApplicationLogger
    {
        public static void WriteToFile(string text)
        {
            string path = "C:\\Toran\\Logs\\UmemeServiceLogs\\UMM-" + DateTime.Today.ToString("dd-MM-yyyy") + ".log";
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(string.Format(text, DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt")));
                writer.Close();
            }
        }
    }
}
