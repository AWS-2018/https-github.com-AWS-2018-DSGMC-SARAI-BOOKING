using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Configuration;

using System.Resources;
using System.Globalization;

using BusinessObjects.Common;
using System.IO;

namespace FrameWork.Core
{
    public static class Localizer
    {

        public static string ProjectTitle
        {
            get
            {
                return "DSGMC Sarai Booking";
            }
        }

        public static string MainDatabaseName
        {   
            get
            {
                return "DSGMC_SARAI_BOOKING";
            }
        }

        public static string DatabaseServerName
        {
            get
            {
                return "192.168.0.109";
                //return "198.154.99.38,1092";
            }
        }

        public static string DatabaseServerUserName
        {
            get
            {
                return "sa";
            }
        }

        public static string DatabaseServerPassword
        {
            get
            {
                return "Ak@alW3bS0ft";
                //return "Akaal@Web)Soft)*23";
            }
        }

        public static int FeeSlipMenuId
        {
            get
            {
                return 4111;
            }
        }

        public static string PayrollMonthDescription(DateTime strDate)
        {
            return strDate.ToString("MMM yyyy") + " (Paid in " + strDate.AddMonths(1).ToString("MMM yyyy") + ")";
        }

        /*Validity Time period of SMS OTP (In Minutes)*/
        public static int OTPValidityTime
        {
            get
            {
                return 5;
            }
        }


        public static UserMaster CurrentUser
        {
            get
            {
                return (UserMaster)System.Web.HttpContext.Current.Session[System.Web.HttpContext.Current.Session["APP_PREFIX"] + "_USER_MASTER_SESSION"];
            }
        }

        public static string SessionId
        {
            get
            {
                return System.Web.HttpContext.Current.Session[System.Web.HttpContext.Current.Session["APP_PREFIX"] + "_SessionId"].ToString();
            }
        }

        public static int MaxLoginAttempts
        {
            get
            {
                return 5;
            }
        }

        public static string ServerRootPath
        {
            get
            {
                return System.Web.HttpContext.Current.Session[System.Web.HttpContext.Current.Session["APP_PREFIX"] + "_SERVER_ROOT_PATH"].ToString();
            }
        }

        public static string GenerateOTP
        {
            get
            {
                return (new Random()).Next(100000, 999999).ToString();
            }
        }

        public static UserMaster AdminUser
        {
            get
            {
                UserMaster user = new UserMaster();
                user.Name = "Manish";
                user.EmailId = "manishaggarwal@akaalwebsoft.com";
                return user;
            }
        }

        public static string PrintDetails
        {
            get
            {
                return "Printed by " + FrameWork.Core.Localizer.CurrentUser.Name + " on " + DateTime.Now.ToString("MMM dd, yyyy hh:mm:ss tt");
            }
        }

        public static string ReportPrintDetails
        {
            get
            {
                return "Printed by " + FrameWork.Core.Localizer.CurrentUser.Name + " on " + DateTime.Now.ToString("MMM dd, yyyy hh:mm:ss tt") + " from webschoolmanager.com";
            }
        }

    



        /*Max Size of Logo (in bytes) */
        public static int InstituteLogoSize
        {
            get
            {
                return 100;
            }
        }

    

        public static string APISuccessStatus
        {
            get
            {
                return "success";
            }
        }

        public static string APIErrorStatus
        {
            get
            {
                return "error";
            }
        }

        public static void WriteLog(string LogFileName, string contents)
        {
            StreamWriter sm = new StreamWriter(System.Web.HttpContext.Current.Server.MapPath("~/" + LogFileName), true);
            sm.WriteLine(DateTime.Now.ToString("MMM dd, yyyy hh:mm:ss tt") + ": " + contents);
            sm.WriteLine("");
            sm.Flush();
            sm.Close();
        }

        public static Int32 GetTimeStamp
        {
            get
            {
                Random rn = new Random();
                Int32 timeStamp = FrameWork.Core.Localizer.CurrentUser.Id + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + DateTime.Now.Millisecond + (rn.Next(0, 99999)) + (rn.Next(0, 999999));

                return timeStamp;
            }
        }

        public static DateTime MaxDateTime
        {
            get
            {
                return Convert.ToDateTime("12/31/2099 11:59:59 PM");
            }
        }

        public static DateTime MaxDate
        {
            get
            {
                return Convert.ToDateTime(MaxDateTime.ToString("MMM dd, yyyy"));
            }
        }

        public static DateTime MinDateTime
        {
            get
            {
                return Convert.ToDateTime("1/1/1900 12:00:00 AM");
            }
        }

        public static DateTime MinDate
        {
            get
            {
                return Convert.ToDateTime(MinDateTime.ToString("MMM dd, yyyy"));
            }
        }

        public static string UniqueIdForReport(string ReportName)
        {
            string UniqueId = System.Web.HttpContext.Current.Session.SessionID.ToString();
            UniqueId += "_" + GetTimeStamp.ToString();
            UniqueId += "_" + CurrentUser.Id.ToString();
            UniqueId += "_" + ReportName;

            return UniqueId;
        }

   

        public static string NoRecordFound
        {
            get
            {
                return "Sorry! No Record Found";
            }
        }

        //#
        public static string QRCodeImagePath
        {
            get
            {
                return "~/images/temp";
            }
        }
    }
}