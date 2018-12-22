using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web;
using System.IO;
using System.Data;
using System.Net.Mail;

using BusinessObjects.Common;
using DataLayer.Common;
using FrameWork.Core;
using FrameWork.DataBase;

namespace Facade.Common
{
    public class CommonFacade
    {
        CommonFunctionsDao daoObject = new CommonFunctionsDao();
        
        /// <summary>
        /// Method used to Enable / Disable a Record
        /// </summary>
        /// <param name="Id">Record Id</param>
        /// <param name="RowVersion">Row Version of Record</param>
        /// <param name="Status">New Status to be Updated</param>
        /// <param name="ModifiedByUserId">Id of User who is Modifying the Record</param>
        /// <param name="TableName">Name of the Table to which the record belogs</param>
        /// <returns></returns>
        public bool UpdateRecordStatus(int Id, int RowVersion, bool Status, string TableName, string DatabaseName = "")
        {
            bool isDone = true;
            string errorMessage = "";
            
            try
            {
                if (Id <= 0)
                    errorMessage += ("<li>Invalid Record Id</li>");

                if (RowVersion <= 0)
                    errorMessage += ("<li>Invalid Row Version</li>");

                if (TableName.Trim().Length <= 0)
                    errorMessage += ("<li>Invalid Table Details</li>");

                if (errorMessage != "")
                    throw new ApplicationException(errorMessage);

                //Starting Transaction
                using (TransactionDecorator transaction = new TransactionDecorator())
                {
                    //Saving Data by calling Dao Method
                    if (daoObject.UpdateRecordStatus(Id, RowVersion, Status, Localizer.CurrentUser.Id, TableName, DatabaseName) == false)
                        throw new ApplicationException("<li>Error Updating Record Status</li>", null);

                    //If no Error, then Commiting Transaction
                    transaction.Complete();
                }
            }
            catch (ApplicationException ex)
            {
                isDone = false;
                throw new ApplicationException(ex.Message, null);
            }

            return isDone;
        }

        /// <summary>
        /// Method used to Enable / Disable a Record
        /// </summary>
        /// <param name="Id">Record Id</param>
        /// <param name="RowVersion">Row Version of Record</param>
        /// <param name="Status">New Status to be Updated</param>
        /// <param name="ModifiedByUserId">Id of User who is Modifying the Record</param>
        /// <param name="TableName">Name of the Table to which the record belogs</param>
        /// <returns></returns>
        public bool UpdateRecordStatus(string Guid, int RowVersion, bool Status, string TableName, string DatabaseName = "")
        {
            bool isDone = true;
            string errorMessage = "";
            int Id = 1;
            try
            {
                if (Id <= 0)
                    errorMessage += ("<li>Invalid Record Id</li>");

                if (RowVersion <= 0)
                    errorMessage += ("<li>Invalid Row Version</li>");

                if (TableName.Trim().Length <= 0)
                    errorMessage += ("<li>Invalid Table Details</li>");

                if (errorMessage != "")
                    throw new ApplicationException(errorMessage);

                //Starting Transaction
                using (TransactionDecorator transaction = new TransactionDecorator())
                {
                    //Saving Data by calling Dao Method
                    if (daoObject.UpdateRecordStatus(Id, RowVersion, Status, Localizer.CurrentUser.Id, TableName, DatabaseName) == false)
                        throw new ApplicationException("<li>Error Updating Record Status</li>", null);

                    //If no Error, then Commiting Transaction
                    transaction.Complete();
                }
            }
            catch (ApplicationException ex)
            {
                isDone = false;
                throw new ApplicationException(ex.Message, null);
            }

            return isDone;
        }

        /// <summary>
        /// Method used to Get the Number of Records
        /// </summary>
        /// <param name="TableName">Name of the Table to which the record belogs</param>
        /// <returns></returns>
        public int GetRecordCountForListPage(string TableName, string SearchCriteria = "", string DatabaseName = "")
        {
            int recordCount = 0;

            try
            {
                TableName = TableName.TrimEnd().TrimStart().Trim();
                SearchCriteria = SearchCriteria.TrimEnd().TrimStart().Trim();

                if (TableName.Length <= 0)
                    throw new ApplicationException("<li>Please pass a Valid Table Name</li>");

                recordCount = daoObject.GetRecordCountForListPage(TableName, SearchCriteria, DatabaseName);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, null);
            }

            return recordCount;
        }

        /// <summary>
        /// Method used to Check If Integer Column Value already exists
        /// </summary>
        /// <param name="Id">Record Id</param>
        /// <param name="ColumnName">Column Name</param>
        /// <param name="ColumnValue">Column Value</param>
        /// <param name="TableName">Name of the Table to which the record belogs</param>
        /// <returns></returns>
        public bool CheckValueAlreadyExists(int Id, string ColumnName, int ColumnValue, string TableName, string DatabaseName = "", string WhereCondition = "")
        {
            bool isExists = false;
            string errorMessage = "";

            try
            {
                if (ColumnName == null)
                    ColumnName = "";

                TableName = TableName.TrimEnd().TrimStart().Trim();
                ColumnName = ColumnName.TrimEnd().TrimStart().Trim();
                WhereCondition = WhereCondition.TrimEnd().TrimStart().Trim();

                
                if (Id < 0)
                    Id = 0;

                if (TableName.Length <= 0)
                    errorMessage += ("<li>Please pass a Valid Table Name</li>");

                if (ColumnName.Length <= 0)
                    errorMessage += ("<li>Please pass a Valid Column Name</li>");

                if (errorMessage != "")
                    throw new ApplicationException(errorMessage);

                isExists = daoObject.CheckValueAlreadyExists(Id, ColumnName, ColumnValue, TableName, DatabaseName, WhereCondition); ;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, null);
            }

            return isExists;
        }

        /// <summary>
        /// Method used to Check If String Column Value already exists
        /// </summary>
        /// <param name="Id">Record Id</param>
        /// <param name="ColumnName">Column Name</param>
        /// <param name="ColumnValue">Column Value</param>
        /// <param name="TableName">Name of the Table to which the record belogs</param>
        /// <returns></returns>
        public bool CheckValueAlreadyExists(int Id, string ColumnName, string ColumnValue, string TableName, string DatabaseName = "", string WhereCondition = "")
        {
            bool isExists = false;
            string errorMessage = "";

            try
            {
                if (ColumnName == null)
                    ColumnName = "";

                if (ColumnValue == null)
                    ColumnValue = "";

                TableName = TableName.TrimEnd().TrimStart().Trim();
                ColumnName = ColumnName.TrimEnd().TrimStart().Trim();
                WhereCondition = WhereCondition.TrimEnd().TrimStart().Trim();

                if (Id < 0)
                    Id = 0;

                if (TableName.Length <= 0)
                    errorMessage += ("<li>Please pass a Valid Table Name</li>");

                if (ColumnName.Length <= 0)
                    errorMessage += ("<li>Please pass a Valid Column Name</li>");

                if (ColumnName.Length <= 0)
                    errorMessage += ("<li>Please pass a Valid Column Value</li>");

                if (errorMessage != "")
                    throw new ApplicationException(errorMessage);

                isExists = daoObject.CheckValueAlreadyExists(Id, ColumnName, ColumnValue, TableName, DatabaseName, WhereCondition); ;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, null);
            }

            return isExists;
        }

        /// <summary>
        /// Method used to Get the New SortId
        /// </summary>
        /// <param name="TableName">Name of the Table to which the record belogs</param>
        /// <returns></returns>
        public int GetNewSortId(string TableName, string DatabaseName = "")
        {
            int newSortId = 0;

            try
            {
                TableName = TableName.TrimEnd().TrimStart().Trim();

                if (TableName.Length <= 0)
                    throw new ApplicationException("<li>Please pass a Valid Table Name</li>");

                newSortId = daoObject.GetNewSortId(TableName, DatabaseName);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, null);
            }

            return newSortId;
        }

        /// <summary>
        /// Method used to Check the Row Version
        /// </summary>
        /// <param name="Id">Record Id</param>
        /// <param name="RowVersion">Row version of Record</param>
        /// <param name="TableName">Name of the Table to which the record belogs</param>
        /// <returns></returns>
        public bool CheckRowVersion(int Id, int RowVersion, string TableName, string DatabaseName = "")
        {
            bool isValid = true;
            string errorMessage = "";

            try
            {
                if (Id <= 0)
                    errorMessage += ("<li>Invalid Record Id</li>");

                if (RowVersion <= 0)
                    errorMessage += ("<li>Invalid Row Version</li>");

                if (TableName.Trim().Length <= 0)
                    errorMessage += ("<li>Invalid Table Details</li>");

                if (errorMessage != "")
                    throw new ApplicationException(errorMessage);

                isValid = daoObject.CheckRowVersion(Id, RowVersion, TableName, DatabaseName);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, null);
            }

            return isValid;
        }
        
        /// <summary>
        /// Method used to Remove All the Temp Data from Database at the Time of Logout
        /// </summary>
        /// <returns></returns>
        public void LogOutSession()
        {
            try
            {
                //calling Dao Method
                daoObject.LogOutSession();

                new UserMasterFacade().SaveLogoutSessionHistory();
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message, null);
            }
        }

     
        public DataTable ListToDataTable<T>(List<T> items)
        {
            return daoObject.ListToDataTable<T>(items);
        }

        public string CreateRandomCode(int codeLength, bool isAlphaNumeric)
        {
            string allChar = string.Empty;
            int t = 0;

            if (isAlphaNumeric == false)
                allChar = "0,1,2,3,4,5,6,7,8,9";
            else
                allChar = "0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";

            string[] allCharArray = allChar.Split(',');
            string randomCode = "";
            int temp = -1;

            Random rand = new Random();
            for (int i = 0; i < codeLength; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(i * temp * ((int)DateTime.Now.Ticks));
                }

                if (isAlphaNumeric == false)
                    t = rand.Next(9);
                else
                    t = rand.Next(35);

                if (temp != -1 && temp == t)
                {
                    return CreateRandomCode(codeLength, isAlphaNumeric);
                }

                temp = t;
                randomCode += allCharArray[t];
            }
            return randomCode;
        }

        public static string PopulateBody(string filePath, string userName, string title, string url, string description)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(filePath))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{UserName}", userName);
            body = body.Replace("{Title}", title);
            body = body.Replace("{Url}", url);
            body = body.Replace("{Description}", description);
            return body;
        }

        public static void SendHtmlFormattedEmail(string recepientEmail, string subject, string body, string CC)
        {
            using (MailMessage mailMessage = new MailMessage())
            {
                UserMaster objUserMaster = null;

                if (Localizer.CurrentUser != null)
                    objUserMaster = Localizer.CurrentUser;
                else
                    objUserMaster = (new UserMasterFacade()).GetDefaultEmailSettings(Localizer.CurrentUser.Id, "", "");

                
                if (objUserMaster.EmailUserName == null)
                    objUserMaster = (new UserMasterFacade()).GetDefaultEmailSettings(Localizer.CurrentUser.Id, "", "");


                mailMessage.From = new MailAddress(objUserMaster.EmailDisplayName);

                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;

                mailMessage.To.Add(new MailAddress(recepientEmail));
                if (!string.IsNullOrEmpty(CC))
                    mailMessage.CC.Add(new MailAddress(CC));

                SmtpClient smtp = new SmtpClient();

                smtp.Host = objUserMaster.EmailHostName;
                smtp.EnableSsl = objUserMaster.EmailEnableSSL;

                System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();

                NetworkCred.UserName = objUserMaster.EmailUserName;
                NetworkCred.Password = objUserMaster.EmailPassword;

                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;

                smtp.Port = objUserMaster.EmailPortNumber;

                smtp.Send(mailMessage);
            }
        }

        public bool SendMail(string filePath, string toEmailAddress, string userName, string title, string url, string description, string mailSubject, string CC = "")
        {
            try
            {
                string body = PopulateBody(filePath, userName, title, url, description);
                SendHtmlFormattedEmail(toEmailAddress, mailSubject, body, CC);

                return true;
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message, null);
            }
        }

        public bool SendPasswordResetSuccessMail(string filePath, string toEmailAddress, string userName, string title, string url, string description, string mailSubject, string CC = "")
        {
            try
            {
                string body = PopulateBody(filePath, userName, title, url, description);

                using (MailMessage mailMessage = new MailMessage())
                {
                    UserMaster objUserMaster = (new UserMasterFacade()).GetDefaultEmailSettings(0, toEmailAddress, "");

                    mailMessage.From = new MailAddress(objUserMaster.EmailDisplayName);
                    mailMessage.Subject = mailSubject;
                    mailMessage.Body = body;
                    mailMessage.IsBodyHtml = true;
                    mailMessage.To.Add(new MailAddress(toEmailAddress));

                    SmtpClient smtp = new SmtpClient();

                    smtp.Host = objUserMaster.EmailHostName;
                    smtp.EnableSsl = objUserMaster.EmailEnableSSL;

                    System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();

                    NetworkCred.UserName = objUserMaster.EmailUserName;
                    NetworkCred.Password = objUserMaster.EmailPassword;

                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;

                    smtp.Port = objUserMaster.EmailPortNumber;

                    smtp.Send(mailMessage);
                }

                return true;
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message, null);
            }
        }

        public bool SendPasswordResetMail(string filePath, string toEmailAddress, string userName, string title, string url, string description, string mailSubject, string CC = "")
        {
            try
            {
                string body = PopulateBody(filePath, userName, title, url, description);

                using (MailMessage mailMessage = new MailMessage())
                {
                    UserMaster objUserMaster = (new UserMasterFacade()).GetDefaultEmailSettings(0, toEmailAddress, ""); 

                    mailMessage.From = new MailAddress(objUserMaster.EmailDisplayName);
                    mailMessage.Subject = mailSubject;
                    mailMessage.Body = body;
                    mailMessage.IsBodyHtml = true;
                    mailMessage.To.Add(new MailAddress(toEmailAddress));

                    SmtpClient smtp = new SmtpClient();

                    smtp.Host = objUserMaster.EmailHostName;
                    smtp.EnableSsl = objUserMaster.EmailEnableSSL;

                    System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();

                    NetworkCred.UserName = objUserMaster.EmailUserName;
                    NetworkCred.Password = objUserMaster.EmailPassword;

                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;

                    smtp.Port = objUserMaster.EmailPortNumber;

                    smtp.Send(mailMessage);
                }

                return true;
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message, null);
            }
        }

        public bool IsNumericDataTableColumnType(DataColumn col)
        {
            if (col == null)
                return false;
            // Make this const
            var numericTypes = new[] { typeof(Byte), typeof(Decimal), typeof(Double), typeof(Int16), typeof(Int32), typeof(Int64), typeof(SByte), typeof(Single), typeof(UInt16), typeof(UInt32), typeof(UInt64) };
            return numericTypes.Contains(col.DataType);
        }

        public bool SendBlockedIPAddressEmail(string currentURL, string IPAddress)
        {
            try
            {
                UserMaster adminUser = Localizer.AdminUser;

                string strEmailBody = "";
                strEmailBody = strEmailBody + System.Environment.NewLine + "<html xmlns='http://www.w3.org/1999/xhtml'>";
                strEmailBody = strEmailBody + System.Environment.NewLine + "    <head>";
                strEmailBody = strEmailBody + System.Environment.NewLine + "        <title></title>";
                strEmailBody = strEmailBody + System.Environment.NewLine + "    </head>";
                strEmailBody = strEmailBody + System.Environment.NewLine + "    ";
                strEmailBody = strEmailBody + System.Environment.NewLine + "    <body>";
                strEmailBody = strEmailBody + System.Environment.NewLine + "        <span style='font-family:Arial;font-size:10pt'>";
                strEmailBody = strEmailBody + System.Environment.NewLine + "            Dear <b>" + adminUser.Name + "</b>, <br/><br/>";
                strEmailBody = strEmailBody + System.Environment.NewLine + "            ";
                strEmailBody = strEmailBody + System.Environment.NewLine + "            The IP Address [<b>" + IPAddress + "</b>] has been blocked due to many fail attempts for login.";
                strEmailBody = strEmailBody + System.Environment.NewLine + "            <br/><br/>";
                strEmailBody = strEmailBody + System.Environment.NewLine + "            To Unblock this IP Address, please click on the following button:";
                strEmailBody = strEmailBody + System.Environment.NewLine + "            <br/><br/>";
                strEmailBody = strEmailBody + System.Environment.NewLine + "            <a style='text-decoration:none;background:#26B99A;border:1px solid #169F85;border-radius:3px;margin-bottom:5px;margin-right:5px;color:#fff;display:inline-block;padding:6px 12px;font-size:14px;font-weight:400;line-height:1.42857143;text-align:center;white-space:nowrap;vertical-align:middle;cursor:pointer;font-family:inherit;text-transform:none;overflow:visible;' href=" + currentURL + "/Home/UnBlockIPAddress?Token=" + FrameWork.Core.Utility.EncryptData("`IP_ADDRESS=" + IPAddress + "`RECORD_DATE=" + DateTime.Now) + ">Unblock [" + IPAddress + "]</a>";
                strEmailBody = strEmailBody + System.Environment.NewLine + "            <br/><br/>";
                strEmailBody = strEmailBody + System.Environment.NewLine + "            Regards,";
                strEmailBody = strEmailBody + System.Environment.NewLine + "            <br/>";
                strEmailBody = strEmailBody + System.Environment.NewLine + "            <b>Admin Team</b>";
                strEmailBody = strEmailBody + System.Environment.NewLine + "        </span>";
                strEmailBody = strEmailBody + System.Environment.NewLine + "    </body>";
                strEmailBody = strEmailBody + System.Environment.NewLine + "</html>";
                
                

                using (MailMessage mailMessage = new MailMessage())
                {
                    UserMaster objUserMaster = (new UserMasterFacade()).GetDefaultEmailSettings(0, adminUser.EmailId, "");

                    mailMessage.From = new MailAddress(objUserMaster.EmailDisplayName);
                    mailMessage.Subject = "ERP - IP Address[" + IPAddress + "] blocked";
                    mailMessage.Body = strEmailBody;
                    mailMessage.IsBodyHtml = true;
                    mailMessage.To.Add(new MailAddress(adminUser.EmailId));

                    SmtpClient smtp = new SmtpClient();

                    smtp.Host = objUserMaster.EmailHostName;
                    smtp.EnableSsl = objUserMaster.EmailEnableSSL;

                    System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();

                    NetworkCred.UserName = objUserMaster.EmailUserName;
                    NetworkCred.Password = objUserMaster.EmailPassword;

                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;

                    smtp.Port = objUserMaster.EmailPortNumber;

                    smtp.Send(mailMessage);
                }

                return true;
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message, null);
            }
        }


        /// <summary>
        /// Method to get the Current Date
        /// <param name="hostName">hostName</param>
        /// </summary>
        /// <returns></returns>
        public DateTime GetCurrentDateFromServer()
        {
            return daoObject.GetCurrentDateFromServer();
        }

     
        public bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }
        
    }
}
