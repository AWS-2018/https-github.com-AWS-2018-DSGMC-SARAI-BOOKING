using FrameWork.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;

namespace FrameWork.Core
{
    public static class SMS
    {
        public static bool SendSingleSMSFromAkaalWebSoft1(string MobileNumber, string strMessage)
        {
            string SMSPortalURL = "http://sms.akaalwebsoft.net/api/swsend.asp?";
            string SMSPortalUserId = "akaal";
            string SMSPortalPassword = "66830994";
            string SMSPortalSenderId = "AKLWEB";

            string createdURL = SMSPortalURL + "username=" + HttpUtility.UrlEncode(SMSPortalUserId) + "&password=" + HttpUtility.UrlEncode(SMSPortalPassword) + "&sender=" + HttpUtility.UrlEncode(SMSPortalSenderId) + "&sendto=" + HttpUtility.UrlEncode(MobileNumber) + "&message=" + HttpUtility.UrlEncode(strMessage);

            Localizer.WriteLog("SMS_Log.txt", createdURL);


            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(createdURL);
            HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();

            System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
            string responseString = respStreamReader.ReadToEnd();

            respStreamReader.Close();
            myResp.Close();

            if (responseString == "sent")
                return true;
            else
                return false;
        }

        public static bool SendLoginOTP_Old(string MobileNumber, string OTPValue)
        {
            bool isDone = false;

            string SMSPortalURL = "http://sms.akaalwebsoft.net/api/swsend.asp?";
            string SMSPortalUserId = "akaal";
            string SMSPortalPassword = "66830994";
            string SMSPortalSenderId = "AKLWEB";

            string strMessage = "OTP to login for Web School Manager is " + OTPValue + ". Never share your OTP or account details with anyone.";

            string createdURL = SMSPortalURL + "username=" + HttpUtility.UrlEncode(SMSPortalUserId) + "&password=" + HttpUtility.UrlEncode(SMSPortalPassword) + "&sender=" + HttpUtility.UrlEncode(SMSPortalSenderId) + "&sendto=" + HttpUtility.UrlEncode(MobileNumber) + "&message=" + HttpUtility.UrlEncode(strMessage);

            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(createdURL);
            HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();

            System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
            string responseString = respStreamReader.ReadToEnd();

            respStreamReader.Close();
            myResp.Close();

            if (responseString == "sent")
                isDone = true;
            else
                isDone = false;


            return isDone;
        }


        public static bool SendLoginOTP(string MobileNumber, string OTPValue)
        {
            bool isDone = false;

            string SMSPortalURL = "http://tsms.akaalwebsoft.net/SMSApi/rest/send?";
            string SMSPortalUserId = "akaal";
            string SMSPortalPassword = "8ulbpweF";
            string SMSPortalSenderId = "AKLWEB";

            string strMessage = "OTP to login for Web School Manager is " + OTPValue + ". Never share your OTP or account details with anyone.";

            string createdURL = SMSPortalURL + "userId=" + HttpUtility.UrlEncode(SMSPortalUserId) + "&password=" + HttpUtility.UrlEncode(SMSPortalPassword) + "&senderId=" + HttpUtility.UrlEncode(SMSPortalSenderId) + "&sendMethod=simpleMsg&msgType=TEXT&msg=" + HttpUtility.UrlEncode(strMessage) + "&mobile=" + HttpUtility.UrlEncode(MobileNumber) + "&duplicateCheck=true&format=json";

            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(createdURL);
            HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();

            /*
            http://tsms.akaalwebsoft.net/SMSApi/rest/send?userId=akaal&password=8ulbpweF&senderId=AKLWEB&sendMethod=simpleMsg&msgType=TEXT&msg=hello&mobile=7009012882&duplicateCheck=true&format=json
            */

            System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
            string responseString = respStreamReader.ReadToEnd();

            respStreamReader.Close();
            myResp.Close();

            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            var result = jsSerializer.DeserializeObject(responseString);

            Dictionary<string, object> objResponse = new Dictionary<string, object>();
            objResponse = (Dictionary<string, object>)(result);

            string responseStatus = objResponse["status"].ToString();

            if (responseStatus.ToLower() == "success")
                isDone = true;
            else
                isDone = false;


            return isDone;
        }

        public static string GetDefaulterSMSContents()
        {
            string contents = "";

            contents = "Dear Parent, This is to inform you that Rs. [PENDING_AMOUNT] is pending towards [STUDENT_NAME].";

            return contents;
        }
    }
}