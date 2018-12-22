using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using BusinessObjects.Common;
using DataLayer.Common;

using FrameWork.Core;
using System.Web;
using System.Data;
using System.Net.Http;

namespace Facade.Common
{
    public class UserMasterFacade
    {
        UserMasterDao daoObject = new UserMasterDao();
        CommonFacade common = new CommonFacade();

        /// <summary>
        /// Method to Save/Update the Information
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Update)]
        public int SaveData(UserMaster obj)
        {
            int Id = 0;
            string errorMessage = "";

            try
            {
                //Checking the Validations
                string validationResult = obj.CheckValidation();

                if (validationResult.Length > 0)
                    throw new ApplicationException(validationResult);

                //Checking if the Name already exists
                if (common.CheckValueAlreadyExists(obj.Id, "NAME", obj.Name, obj.DatabaseTableName))
                    errorMessage += ("<li>User Name already exists.</li>");

                //Checking if the Email already exists
                if (common.CheckValueAlreadyExists(obj.Id, "EMAIL_ID", obj.EmailId, obj.DatabaseTableName))
                    errorMessage += ("<li>Email Id already exists.</li>");

                //Checking if the Mobile Number already exists
                if (common.CheckValueAlreadyExists(obj.Id, "MOBILE_NO", obj.MobileNo, obj.DatabaseTableName))
                    errorMessage += ("<li>Mobile No. already exists.</li>");

                if (obj.Id != 0)
                    if (common.CheckRowVersion(obj.Id, obj.RowVersion, obj.DatabaseTableName) == false)
                        errorMessage += ("<li>Record has been modified by some other user. Please reload the record and then modify.</li>");

                if (errorMessage != "")
                    throw new ApplicationException(errorMessage);

                //Starting Transaction
                using (TransactionDecorator transaction = new TransactionDecorator())
                {
                    //Saving Data by calling Dao Method
                    Id = daoObject.SaveData(obj);

                    //If Id is returned as Zero from Data Layer, then throwing Exception and rolling back the Transaction
                    if (Id == 0 && obj.Id == 0)
                        throw new ApplicationException("Error Saving Data.", null);

                    //If Id is returned as Zero from Data Layer, then throwing Exception and rolling back the Transaction
                    if (Id == 0 && obj.Id != 0)
                        throw new ApplicationException("Record has been modified by some other user. Please reload the record and then modify.", null);

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
        /// Method to Update the Settings
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Update)]
        public bool UpdateSettings(UserMaster obj)
        {
            bool isDone = false;

            if (obj.Id <= 0)
                throw new Exception("<li>Invalid User selected</li>");

            string errorMessage = obj.CheckValidation();

            if (errorMessage.Trim().Length > 0)
                throw new ApplicationException(errorMessage);

            //Starting Transaction
            using (TransactionDecorator transaction = new TransactionDecorator())
            {
                //Saving Data by calling Dao Method
                isDone = daoObject.UpdateSettings(obj);

                //If Id is returned as Zero from Data Layer, then throwing Exception and rolling back the Transaction
                if (!isDone)
                    throw new Exception("Error Saving Data.", null);

                //If no Error, then Commiting Transaction
                transaction.Complete();
            }

            return isDone;
        }

        /// <summary>
        /// Method to Update the Password
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="NewPassword"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Update)]
        public bool UpdatePassword(UserMaster obj, string NewPassword)
        {
            bool isDone = false;

            if (obj.Id <= 0)
                throw new Exception("<li>Invalid User selected</li>");

            if (obj.Password == null)
                obj.Password = "";

            if (NewPassword == null)
                NewPassword = "";

            obj.Password = obj.Password.Trim();
            NewPassword = NewPassword.Trim();

            if (obj.Password == "")
                throw new Exception("<li>Please give Current Password.</li>");

            if (NewPassword == "")
                throw new Exception("<li>Please give New Password.</li>");

            if (NewPassword == obj.Password)
                throw new Exception("<li>Current password and New password cannot be same.</li>");

            //Starting Transaction
            using (TransactionDecorator transaction = new TransactionDecorator())
            {
                //Saving Data by calling Dao Method
                isDone = daoObject.UpdatePassword(obj, NewPassword);

                //If Id is returned as Zero from Data Layer, then throwing Exception and rolling back the Transaction
                if (!isDone)
                    throw new Exception("Error Saving Data.", null);

                //If no Error, then Commiting Transaction
                transaction.Complete();
            }

            return isDone;
        }

        /// <summary>
        /// Method to Update the Password
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="NewPassword"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Update)]
        public bool ResetPassword(UserMaster obj, string NewPassword)
        {
            bool isDone = false;

            if (obj.Id <= 0)
                throw new Exception("<li>Invalid User selected</li>");

            if (NewPassword == null)
                NewPassword = "";

            NewPassword = NewPassword.Trim();

            if (NewPassword == "")
                throw new Exception("<li>Please give New Password.</li>");

            if (NewPassword == obj.Password)
                throw new Exception("<li>Current password and New password cannot be same.</li>");

            //Starting Transaction
            using (TransactionDecorator transaction = new TransactionDecorator())
            {
                //Saving Data by calling Dao Method
                isDone = daoObject.ResetPassword(obj, NewPassword);

                //If Id is returned as Zero from Data Layer, then throwing Exception and rolling back the Transaction
                if (!isDone)
                    throw new Exception("Error Saving Data.", null);

                //If no Error, then Commiting Transaction
                transaction.Complete();
            }

            return isDone;
        }

        /// <summary>
        /// Method to return the List of Data
        /// </summary>
        /// <returns>List of Data</returns>
        public List<UserMaster> GetList()
        {
            List<UserMaster> lstData = new List<UserMaster>();

            try
            {
                //calling Dao Method to get the List of Data
                lstData = daoObject.GetList("");
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message, null);
            }

            return lstData;
        }

        /// <summary>
        /// Method to return the List of Data
        /// </summary>
        /// <returns>List of Data</returns>
        public List<UserMaster> GetListForListPage(int page, string searchCriteria = "")
        {
            List<UserMaster> lstData = new List<UserMaster>();

            try
            {
                //calling Dao Method to get the List of Data
                lstData = daoObject.GetList(page, Localizer.CurrentUser.PageSize, searchCriteria);
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message, null);
            }

            return lstData;
        }

        /// <summary>
        /// Method to return the List of Data based on the passed Name
        /// </summary>
        /// <returns>List of Data</returns>
        public List<UserMaster> GetList(string name)
        {
            List<UserMaster> lstData = new List<UserMaster>();

            try
            {
                //calling Dao Method to get the List of Data
                lstData = daoObject.GetList(name.Trim());
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message, null);
            }

            return lstData;
        }

     
       
        /// <summary>
        /// Method used to Get the Details based on the ID Passed
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserMaster GetDetailById(int id)
        {
            UserMaster obj = new UserMaster();

            try
            {
                if (id <= 0)
                    throw new Exception("<li>Please pass a valid Id</li>");

                //calling Dao Method to get the Details
                obj = daoObject.GetSingleRecordDetail(id);

                //If Id value is Zero, then raising exception
                if (obj.Id == 0)
                    throw new Exception("<li>Error reading Details</li>");

                obj.SuperAdminName = "";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, null);
            }

            return obj;
        }
        
        /// <summary>
        /// Method to check if the UserName and Password is Valid or Not
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>true/false</returns>
        public int ValidateUserCredentials(UserMaster obj, string IPAddress, string loginUrl, string userType)
        {
            int Id = 0;
            userType = userType == null ? "" : userType.ToUpper().Trim();

            try
            {
                obj.EmailId = obj.EmailId;
                obj.Password = obj.Password;
                IPAddress = IPAddress.Trim();

                if (obj.EmailId == "")
                    throw new ApplicationException("Please give Email Id / Mobile No.");

                if (obj.Password == "")
                    throw new ApplicationException("Please give Password.");

                if (IPAddress == "")
                    throw new ApplicationException("Invalid I.P. Address.");

                //calling Dao Method to check if the Credentials are Valid or Not
                Id = daoObject.ValidateUserCredentials(obj, IPAddress, userType);
                
                if (Id < 0)
                {
                    (new CommonFacade()).SendBlockedIPAddressEmail(loginUrl, IPAddress);

                    throw new ApplicationException("Sorry! Your I.P. Address [" + IPAddress + "] has been blocked for today due to " + Localizer.MaxLoginAttempts + " consecutive wrong attempts. Please contact Administrator to get it unblocked.");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, null);
            }

            return Id;
        }

        /// <summary>
        /// Method to Update the User Rights
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool UpdateUserRights(UserMaster obj)
        {
            bool isDone = false;

            try
            {
                if (obj.Id <= 0)
                    throw new ApplicationException("<li>Please pass a Valid User.</li>");

                if (obj.LstChildMenu.Count<=0)
                    throw new ApplicationException("<li>Menu list is empty.</li>");

                if (daoObject.UpdateUserRights(obj) == false)
                    throw new Exception("<li>Error Updating Record</li>");
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message, null);
            }

            return isDone;
        }

       

        /// <summary>
        /// Method to Restore the Default Settings of a User
        /// </summary>
        /// <returns></returns>
        public void ResetDefaultSettings(int Id)
        {
            try
            {
                if (Id <= 0)
                    throw new ApplicationException("Invalid User.");

                daoObject.ResetDefaultSettings(Id);
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message, null);
            }
        }

        /// <summary>
        /// Method to Save the Login Session History
        /// </summary>
        /// <returns></returns>
        public int SaveLoginSessionHistory()
        {
            int Id = 0;

            try
            {
                Id = daoObject.SaveLoginSessionHistory();

                if (Id <= 0)
                    throw new ApplicationException("Sorry! Unable to save Login History.");
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message, null);
            }

            return Id;
        }

        /// <summary>
        /// Method to Save the Logout Session History
        /// </summary>
        /// <returns></returns>
        public void SaveLogoutSessionHistory()
        {
            try
            {
                daoObject.SaveLogoutSessionHistory();
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message, null);
            }
        }

        /// <summary>
        /// Method to Update the User Rights
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool DeleteUserRights(int userId)
        {
            bool isDone = false;

            try
            {
                if (userId <= 0)
                    throw new ApplicationException("Invalid User selected.");

                isDone = daoObject.DeleteUserRights(userId);
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message, null);
            }

            return isDone;
        }

        /// <summary>
        /// Method to Validate Login Days and Time of User
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool isValidLoginDaysAndTime(UserMaster obj)
        {
            string errorMessage = string.Empty;

            if (obj.IsSuperAdmin == false && obj.IsSeniorAdmin == false)
            {
                if (obj.CanLoginOnSunday == false)
                {
                    if (DateTime.Now.DayOfWeek.ToString().ToLower() == "sunday")
                    {
                        errorMessage = "Sorry! You are not authorized to login on 'Sunday'.";
                        throw new ApplicationException(errorMessage);
                    }
                }

                if (obj.CanLoginOnHoliday == false)
                {
                    //Holiday objHolidayMaster = (new HolidayMasterFacade()).GetDetailByDateAndInstituteId(DateTime.Now.Date, Localizer.CurrentInstitute.Id);

                    //if (objHolidayMaster != null)
                    //{
                    //    errorMessage = "Sorry! You can not login on holiday [" + objHolidayMaster.Name + "].";
                    //    throw new ApplicationException(errorMessage);
                    //}
                }

                if (obj.IsTimeBound == true)
                {
                    if (DateTime.Now.TimeOfDay < obj.FromTime || DateTime.Now.TimeOfDay > obj.ToTime)
                    {
                        string[] arrFromTime = obj.FromTime.ToString().Split(':');
                        TimeSpan tsFromTime = new TimeSpan(int.Parse(arrFromTime[0]), int.Parse(arrFromTime[1]), int.Parse(arrFromTime[2]));
                        DateTime timeFromTime = DateTime.Today.Add(tsFromTime);
                        string displayFromTime = timeFromTime.ToString("hh:mm tt");

                        string[] arrToTime = obj.ToTime.ToString().Split(':');
                        TimeSpan tsToTime = new TimeSpan(int.Parse(arrToTime[0]), int.Parse(arrToTime[1]), int.Parse(arrToTime[2]));
                        DateTime timeToTime = DateTime.Today.Add(tsToTime);
                        string displayToTime = timeToTime.ToString("hh:mm tt");

                        errorMessage = "Sorry! You can login between " + displayFromTime + " and " + displayToTime + " only.";
                        throw new ApplicationException(errorMessage);
                    }
                }
            }

            if (errorMessage.Length > 0)
                return false;
            else
                return true;
        }

        public void MailVerificationCode(UserMaster objUserMaster, string verificationCode, string filePath)
        {
            try
            {
                string description = "<b>Verification Code: </b><span style='text-decoration:none;background:#26B99A;border:1px solid #169F85;border-radius:3px;margin-bottom:5px;margin-right:5px;color:#fff;display:inline-block;padding:6px 12px;font-size:14px;font-weight:400;line-height:1.42857143;text-align:center;white-space:nowrap;vertical-align:middle;font-family:inherit;text-transform:none;overflow:visible;'>" + verificationCode + "</span><br/><br/>";

                string toEmailAddress = objUserMaster.EmailId;

                string userName = objUserMaster.Name;
                string title = string.Empty;
                string url = string.Empty;
                string mailSubject = "Login Verification Code";
                string CC = "";

                (new CommonFacade()).SendMail(filePath, toEmailAddress, userName, title, url, description, mailSubject, CC);
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message, null);
            }
        }

        /// <summary>
        /// Method to Check whether is Valid Password Reset Link
        /// </summary>
        /// <param name="objUserMaster"></param>
        /// <returns></returns>
        public bool IsValidPasswordResetToken(UserMaster objUserMaster)
        {
            bool isDone = false;

            try
            {
                if (objUserMaster.Id <= 0)
                    throw new Exception("Invalid User.");

                if (objUserMaster.PasswordResetToken == null)
                    objUserMaster.PasswordResetToken = "";

                objUserMaster.PasswordResetToken = objUserMaster.PasswordResetToken.Trim().ToLower();

                if (objUserMaster.PasswordResetToken == "")
                    throw new Exception("Invalid Password Reset Link.");

                isDone = daoObject.IsValidPasswordResetToken(objUserMaster.PasswordResetToken);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, null);
            }

            return isDone;
        }


        /// <summary>
        /// Method to Update the Password
        /// </summary>
        /// <param name="objUserMaster"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Update)]
        public UserMaster UpdatePassword(UserMaster objUserMaster)
        {
            if (objUserMaster.PasswordResetToken.Trim() == "")
                throw new Exception("Invalid User selected");

            if (objUserMaster.Id <= 0)
                throw new Exception("Invalid User");

            if (objUserMaster.NewPassword == null)
                objUserMaster.NewPassword = "";


            objUserMaster.NewPassword = objUserMaster.NewPassword.Trim();

            if (objUserMaster.NewPassword != objUserMaster.ConfirmPassword)
                throw new Exception("<li>New Password and Confirm Password don't match.</li>");



            //Saving Data by calling Dao Method
            objUserMaster = daoObject.UpdatePassword(objUserMaster);

            return objUserMaster;
        }

        /// <summary>
        /// Method to update the Particular Record from Database
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool SendPasswordResetLink(string emailId, string templatePath, string url)
        {
            bool isDone = false;
            int tokenValidity = 1440; //Token will be valid for 24 Hours

            CommonFacade commonFacade = new CommonFacade();

            try
            {
                string passwordResetToken = commonFacade.CreateRandomCode(50, true);

                //Starting Transaction
                using (TransactionDecorator transaction = new TransactionDecorator())
                {
                    UserMaster obj = new UserMaster();
                    obj.EmailId = emailId.Trim().ToLower();
                    obj.PasswordResetToken = passwordResetToken.Trim();

                    //calling Dao Method to get the Details
                    obj = daoObject.SendPasswordResetLink(obj);

                    if (obj.Id <= 0)
                        throw new ApplicationException("Sorry! Email Id does not exists.");


                    url = url + "/Home/ResetPassword?Token=" + Utility.EncryptData("`PASSWORD_RESET_TOKEN=" + obj.PasswordResetToken + "`ID=" + obj.Id + "`EMAIL_ID=" + obj.EmailId + "`USER_NAME=" + obj.Name + "`TOKEN_GENERATE_TIME=" + DateTime.Now + "`VALID_FOR=" + tokenValidity + "`TOKEN_EXPIRE_TIME=" + DateTime.Now.AddMinutes(tokenValidity));

                    string toEmailAddress = emailId;
                    string title = "Reset Password";

                    string resetLink = string.Format("<a style = 'text-decoration:none;background:#26B99A;border:1px solid #169F85;border-radius:3px;margin-bottom:5px;margin-right:5px;color:#fff;display:inline-block;padding:6px 12px;font-size:14px;font-weight:400;line-height:1.42857143;text-align:center;white-space:nowrap;vertical-align:middle;cursor:pointer;font-family:inherit;text-transform:none;overflow:visible;' href = {0}>{1}</a>", url, title);

                    StringBuilder sb = new StringBuilder();
                    sb.Append("We have received a request to reset your Web School Manager account password.");
                    sb.Append("<br/>");
                    sb.Append(Environment.NewLine);
                    sb.Append("You can reset the password by clicking on the below button:");
                    sb.Append("<br/>");
                    sb.Append("<br/>");
                    sb.Append(resetLink);
                    sb.Append("<br/>");
                    sb.Append("<br/>");
                    sb.Append("<b>Note:</b> This link is valid for next 24 hours only.");
                    sb.Append("<br/>");
                    sb.Append("<br/>");
                    sb.Append("If you did not request a new password, please ignore this email.");

                    string description = sb.ToString();

                    string mailSubject = "Reset your Web School Manager password";

                    isDone = commonFacade.SendPasswordResetMail(templatePath, toEmailAddress, obj.Name, title, url, description, mailSubject);

                    //If Id is returned as Zero from Data Layer, then throwing Exception and rolling back the Transaction
                    if (isDone == false)
                        throw new ApplicationException("Sorry! Unable to send Password reset Link. Please try again.", null);

                    //If no Error, then Commiting Transaction
                    transaction.Complete();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, null);
            }

            return isDone;
        }

        /// <summary>
        /// Method to get default Email Settings
        /// </summary>
        /// <returns>true/false</returns>
        public UserMaster GetDefaultEmailSettings(int UserMasterId, string EmailId, string MobileNo)
        {
            UserMaster objUserMaster = new UserMaster();

            UserMasterId = UserMasterId < 0 ? 0 : UserMasterId;
            EmailId = (EmailId == null ? "" : EmailId).TrimStart().TrimEnd().Trim().ToLower();
            MobileNo = (MobileNo == null ? "" : MobileNo).TrimStart().TrimEnd().Trim();
            
            try
            {

                if (UserMasterId > 0)
                {
                    EmailId = "";
                    MobileNo = "";
                }

                if (MobileNo != "" && EmailId != "")
                {
                    throw new ApplicationException("Sorry! Unable to find Email settings for multiple pattern. Please contact Administrator.");
                }

                //calling Dao Method to check if the record
                objUserMaster = daoObject.GetDefaultEmailSettings(UserMasterId, EmailId, MobileNo);
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message, null);
            }

            return objUserMaster;
        }

        //#
        /// <summary>
        /// Method to Save the Entry
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Update)]
        public bool SaveUserFavoriteMenus(UserMaster obj)
        {
            bool isDone = false;
            int recordCount = 0;

            try
            {
                string errorMessage = "";

                if (obj.Id <= 0)
                    errorMessage += ("Invalid User Id.");

                //if (obj.FavoriteMenus.Where(m => m.IsSelected == true).ToList().Count > maxFavoriteMenusCount)
                //    errorMessage += ("Maximum " + maxFavoriteMenusCount + " Menus can be saved as Favorite Menus");

                if (errorMessage != "")
                    throw new ApplicationException(errorMessage);

                //Starting Transaction
                using (TransactionDecorator transaction = new TransactionDecorator())
                {
                    //Saving Data by calling Dao Method
                    recordCount = daoObject.SaveUserFavoriteMenus(obj);

                    if (recordCount != obj.FavoriteMenus.Where(m => m.IsSelected).Count())
                        throw new ApplicationException("Error Saving Data.", null);

                    isDone = true;
                    //If no Error, then Commiting Transaction
                    transaction.Complete();
                }
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message, null);
            }

            return isDone;
        }

        //#
        /// <summary>
        /// Method to return the List of Data based on the passed Name
        /// </summary>
        /// <returns>List of Data</returns>
        public List<UserFavoriteMenuMaster> GetUserFavoriteMenus(int userId)
        {
            List<UserFavoriteMenuMaster> lstData = new List<UserFavoriteMenuMaster>();

            try
            {
                string errorMessage = "";

                if (userId <= 0)
                {
                    errorMessage += ("Invalid User Id.");
                }

                if (errorMessage != "")
                    throw new ApplicationException(errorMessage);

                //calling Dao Method to get the List of Data
                lstData = daoObject.GetUserFavoriteMenus(userId);
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message, null);
            }

            return lstData;
        }

        //#
        /// <summary>
        /// Method to Sort the Entry
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Update)]
        public bool SortUserFavoriteMenus(List<UserFavoriteMenuMaster> lstData)
        {
            bool isDone = false;

            try
            {
                string errorMessage = "";

                for (int i = 0; i < lstData.Count; i++)
                {
                    if (lstData[i].Id <= 0)
                    {
                        errorMessage += ("Invalid User Id.");
                    }

                    if (lstData[i].SortId <= 0)
                    {
                        errorMessage += ("Invalid Sort Id.");
                    }
                }

                if (errorMessage != "")
                    throw new ApplicationException(errorMessage);
                //Starting Transaction
                using (TransactionDecorator transaction = new TransactionDecorator())
                {
                    //Saving Data by calling Dao Method
                    isDone = daoObject.SortUserFavoriteMenus(lstData);

                    if (isDone == false)
                        throw new ApplicationException("Error Sorting Data.", null);

                    //If no Error, then Commiting Transaction
                    transaction.Complete();
                }
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message, null);
            }

            return isDone;
        }

        /// <summary>
        /// Method to Update the Favorite menu of a user
        /// </summary>
        /// <param name="menuId"></param>
        /// <param name="IsFavoriteMenu"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Update)]
        public bool UpdateUserFavoriteMenu(int menuId, bool isFavoriteMenu)
        {
            bool isDone = false;

            if (menuId <= 0)
                throw new Exception("<li>Invalid Menu</li>");

            //Starting Transaction
            using (TransactionDecorator transaction = new TransactionDecorator())
            {
                //Saving Data by calling Dao Method
                isDone = daoObject.UpdateUserFavoriteMenu(menuId, isFavoriteMenu);

                //If Id is returned as Zero from Data Layer, then throwing Exception and rolling back the Transaction
                if (!isDone)
                    throw new Exception("Error Saving Data.", null);

                //If no Error, then Commiting Transaction
                transaction.Complete();
            }

            UserMaster user = Localizer.CurrentUser;

            foreach (UserMenuMaster menu in user.LstChildMenu)
                if (menu.MenuId == menuId)
                    menu.IsFavoriteMenu = isFavoriteMenu;

            foreach (UserFavoriteMenuMaster menu in user.FavoriteMenus)
                if (menu.MenuId == menuId)
                    menu.IsSelected = isFavoriteMenu;

            HttpContext.Current.Session["WSM_USER_MASTER_SESSION"] = user;

            return isDone;
        }

        /// <summary>
        /// Method to Update Encrypted UserName / Mobile No. / Password
        /// </summary>
        /// <returns></returns>
        public bool EncryptAllUsersLoginData()
        {
            bool isDone = false;

            try
            {
                isDone = daoObject.EncryptAllUsersLoginData();
            }
            catch(Exception ex)
            {
                throw new ApplicationException(ex.Message, null);
            }

            return isDone;
        }

    
    }
}
