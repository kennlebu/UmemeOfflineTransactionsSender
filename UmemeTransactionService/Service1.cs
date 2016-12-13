using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UmemeTransactionService.Helpers;

namespace UmemeTransactionService
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            ApplicationLogger.WriteToFile("{0} UMEME Transaction Service started");
            this.ScheduleService();
        }

        protected override void OnStop()
        {
            ApplicationLogger.WriteToFile("{0} UMEME Transaction Service stopped");
            this.Schedular.Dispose();
        }

        private Timer Schedular;

        public void ScheduleService()
        {
            try
            {
                Schedular = new Timer(new TimerCallback(SchedularCallback));
                string mode = ConfigurationManager.AppSettings["Mode"].ToUpper();
                //ApplicationLogger.WriteToFile("UMEME Transaction Service Mode: " + mode + " {0}");

                //Set the Default Time.
                DateTime scheduledTime = DateTime.MinValue;

                if (mode == "DAILY")
                {
                    //Get the Scheduled Time from AppSettings.
                    scheduledTime = DateTime.Parse(System.Configuration.ConfigurationManager.AppSettings["ScheduledTime"]);
                    if (DateTime.Now > scheduledTime)
                    {
                        //If Scheduled Time is passed set Schedule for the next day.
                        scheduledTime = scheduledTime.AddDays(1);
                    }
                }

                if (mode.ToUpper() == "INTERVAL")
                {
                    //Get the Interval in Minutes from AppSettings.
                    int intervalMinutes = Convert.ToInt32(ConfigurationManager.AppSettings["IntervalMinutes"]);

                    //Set the Scheduled Time by adding the Interval to Current Time.
                    scheduledTime = DateTime.Now.AddMinutes(intervalMinutes);
                    if (DateTime.Now > scheduledTime)
                    {
                        //If Scheduled Time is passed set Schedule for the next Interval.
                        scheduledTime = scheduledTime.AddMinutes(intervalMinutes);
                    }
                }

                TimeSpan timeSpan = scheduledTime.Subtract(DateTime.Now);
                string schedule = string.Format("{0} day(s) {1} hour(s) {2} minute(s) {3} seconds(s)", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

                //ApplicationLogger.WriteToFile("UMEME Transaction Service scheduled to run after: " + schedule + " {0}");

                //Get the difference in Minutes between the Scheduled and Current Time.
                int dueTime = Convert.ToInt32(timeSpan.TotalMilliseconds);

                //Change the Timer's Due Time.
                Schedular.Change(dueTime, Timeout.Infinite);
            }
            catch (Exception ex)
            {
                ApplicationLogger.WriteToFile("{0} UMEME Transaction Service Error on: " + ex.Message + ex.StackTrace);

                //Stop the Windows Service.
                using (System.ServiceProcess.ServiceController serviceController = new System.ServiceProcess.ServiceController("SimpleService"))
                {
                    serviceController.Stop();
                }
            }
        }

        private void SchedularCallback(object e)
        {
            ////
            // My code here
            ////
            //ApplicationLogger.WriteToFile("The very top");
            sendUmemeTransaction sm = new sendUmemeTransaction();
            //ApplicationLogger.WriteToFile("After object initialization");
            sm.UMEME_PostBankUmemePayment();
            //ApplicationLogger.WriteToFile("After calling the method");


            //this.WriteToFile("Simple Service Log: {0}");
            this.ScheduleService();
            //ApplicationLogger.WriteToFile("After scheduling the service");
        }

        //private void WriteToFile(string text)
        //{
        //    string path = "C:\\UMM-"+ DateTime.Today.ToString("dd-MM-yyyy") +".log";
        //    using (StreamWriter writer = new StreamWriter(path, true))
        //    {
        //        writer.WriteLine(string.Format(text, DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt")));
        //        writer.Close();
        //    }
        //}
    }
}
