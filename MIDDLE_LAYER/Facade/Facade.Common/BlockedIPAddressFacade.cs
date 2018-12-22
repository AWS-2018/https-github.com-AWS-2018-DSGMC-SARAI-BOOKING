using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BusinessObjects.Common;
using DataLayer.Common;

using FrameWork.Core;
using Facade.Common;
using System.ComponentModel;
using System.Web;
using System.Web.Mvc;


namespace Facade.Common
{
    public class BlockedIPAddressFacade
    {
        BlockedIPAddressDao daoObject = new BlockedIPAddressDao();
        
        /// <summary>
        /// Method to Save/Update the Information
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Update)]
        public int SaveBlockedIPAddress(string IPAddress, int maxLoginAttempts, string filePath, string protocol)
        {
            int Id = 0;
            
            try
            {
                //Checking the Validations
                if (IPAddress.Trim() == "")
                    throw new ApplicationException("Invalid IP Address");

                //Starting Transaction
                using (TransactionDecorator transaction = new TransactionDecorator())
                {
                    //Saving Data by calling Dao Method
                    Id = daoObject.SaveBlockedIPAddress(IPAddress);

                    // Code to send email to admin for the blocked user                                
                    string toEmailAddress = "manishaggarwal@akaalwebsoft.com";
                    string userName = "Admin";
                    string title = "Unblock IP Address : " + IPAddress;

                    var requestContext = HttpContext.Current.Request.RequestContext;
                    var urlHelper = new UrlHelper(requestContext);

                    string url = urlHelper.Action("UnBlockIPAddress", "Home", new { Token = FrameWork.Core.Utility.EncryptData("`ID=" + Id + "`IP_ADDRESS=" + IPAddress) }, protocol);

                    string description = "The IP Address ( <b>" + IPAddress + "</b> ) has been blocked due to " + maxLoginAttempts + " consecutive failure attempts for login.";
                    string mailSubject = "IP Address ( " + IPAddress + " ) blocked in Web School Manager";

                    (new CommonFacade()).SendMail(filePath, toEmailAddress, userName, title, url, description, mailSubject);

                    //If no Error, then Commiting Transaction
                    transaction.Complete();
                }
            }
            catch (ApplicationException ex)
            {
                Id = 0;
                throw new ApplicationException(ex.Message, null);
            }

            return Id;
        }

        /// <summary>
        /// Method to Check if an IP Address is blocked or not
        /// </summary>
        /// <returns>List of Data</returns>
        public bool IsBlockedIPAddress(string IPAddress)
        {
            bool isBlocked = false;

            try
            {
                //calling Dao Method to get the List of Data
                isBlocked = daoObject.IsBlockedIPAddress(IPAddress);
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message, null);
            }

            return isBlocked;
        }

        /// <summary>
        /// Method to return the List of Data
        /// </summary>
        /// <returns>List of Data</returns>
        public List<BlockedIPAddress> GetList(DateTime FromDate, DateTime ToDate)
        {
            List<BlockedIPAddress> lstData = new List<BlockedIPAddress>();

            try
            {
                //calling Dao Method to get the List of Data
                lstData = daoObject.GetList(FromDate, ToDate);
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message, null);
            }

            return lstData;
        }

        /// <summary>
        /// Method to Unblock IP Address
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        public void UnBlockIPAddress(string ipAddress)
        {
            ipAddress = ipAddress.Trim();

            try
            {
                if (ipAddress.Length <= 0)
                    throw new Exception("<li>Please pass a valid I.P. Address.</li>");

                //calling Dao Method to get the Details
                daoObject.UnBlockIPAddress(ipAddress);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, null);
            }
        }

        /// <summary>
        /// Method to Explicitly Block an IP Address
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public void BlockIPAddress(string ipAddress)
        {
            ipAddress = ipAddress.Trim();
            try
            {
                if (ipAddress.Length <= 0)
                    throw new Exception("<li>Please pass a valid I.P. Address.</li>");

                //calling Dao Method to get the Details
                daoObject.BlockIPAddress(ipAddress);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, null);
            }
        }
    }
}

