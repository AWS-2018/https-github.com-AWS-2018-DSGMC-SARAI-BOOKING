using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

using BusinessObjects.Common;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using FrameWork.Core;
using System.Net;
using System.Globalization;
using System.IO;
using Facade.Common;
using System.Drawing;

namespace SaraiBooking.App_Start
{
    public static class Common
    {
        public static string NoRecordFoundString = Localizer.NoRecordFound;
        public static string DataTemperedMessage = "Sorry! Cannot continue because data on this Page has been tampered. Please reload the page and try again.";
        public static string ChangeConfirmationMessage = "It looks like you have been editing something. If you leave before saving, your changes will be lost. Do you want to continue?";
        
        public static string RefineErrorMessage(string errorMessage)
        {
            string returnMessage = "";

            if (errorMessage.Contains("<li>") == false)
                errorMessage = "<li>" + errorMessage + "</li>";

            if (errorMessage.Contains("<ul>") == false)
                errorMessage = "<ul>" + errorMessage + "</ul>";

            if (errorMessage.Contains("<div class='alert alert-danger alert-dismissible' role='alert'>") == false)
            {
                returnMessage += System.Environment.NewLine + "<div class='alert alert-danger alert-dismissible' role='alert'>";
                returnMessage += System.Environment.NewLine + "    <button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button>";
                returnMessage += System.Environment.NewLine + "    <span class='fa fa-warning fa-lg'></span>";
                returnMessage += System.Environment.NewLine + "    <strong>Error!</strong><br />";
                returnMessage += System.Environment.NewLine + errorMessage;
                returnMessage += System.Environment.NewLine + "</div>";
            }

            return returnMessage;
        }

        public static string SuccessMessage()
        {
            string returnMessage = "";

            returnMessage += System.Environment.NewLine + "<div class='alert alert-success alert-dismissible' role='alert'>";
            returnMessage += System.Environment.NewLine + "    <button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button>";
            returnMessage += System.Environment.NewLine + "    <span class='fa fa-info fa-lg'></span>";
            returnMessage += System.Environment.NewLine + "    <strong>Info!</strong><br />";
            returnMessage += System.Environment.NewLine + "Success";
            returnMessage += System.Environment.NewLine + "</div>";
            return returnMessage;
        }

        public static string SuccessMessageForIssueLog()
        {
            string returnMessage = "";

            returnMessage = returnMessage + System.Environment.NewLine + "<div class='alert alert-success alert-dismissible' role='alert'>";
            returnMessage = returnMessage + System.Environment.NewLine + "  <button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button>";
            returnMessage = returnMessage + System.Environment.NewLine + "  <span class='fa fa-info fa-lg'></span>";
            returnMessage = returnMessage + System.Environment.NewLine + "  <strong>Success!</strong><br />";
            returnMessage = returnMessage + System.Environment.NewLine + "  Your issue has been logged successfully. Our support executive will contact you shortly.";
            returnMessage = returnMessage + System.Environment.NewLine + "</div>";

            return returnMessage;
        }

        public static bool CanUserAccessPage(int MenuId)
        {
            bool canContinue = false;

            if (MenuId <= 0)
                throw new Exception("Invalid Menu option selected.");

            if (FrameWork.Core.Localizer.CurrentUser.LstChildMenu.Where(m => m.MenuId == MenuId && m.CanAccess).Count() > 0)
                canContinue = true;
            
            if (!canContinue)
                throw new Exception("Sorry! You are not authorised to access this Page. Please contact Administrator for appropriate access rights.");

            return canContinue;
        }

        public static bool CanUserCreateRecord(int MenuId)
        {
            bool canContinue = false;

            if (MenuId <= 0)
                throw new Exception("Invalid Menu option selected.");

            if (FrameWork.Core.Localizer.CurrentUser.LstChildMenu.Where(m => m.MenuId == MenuId && m.CanCreate).Count() > 0)
                canContinue = true;

            if (!canContinue)
                throw new Exception("Sorry! You are not authorised to Create a New Record. Please contact Administrator for appropriate access rights.");

            return canContinue;
        }

        public static bool CanUserEditRecord(int MenuId, bool throwError = true)
        {
            bool canContinue = false;

            if (MenuId <= 0)
                throw new Exception("Invalid Menu option selected.");

            if (FrameWork.Core.Localizer.CurrentUser.LstChildMenu.Where(m => m.MenuId == MenuId && m.CanEdit).Count() > 0)
                canContinue = true;

            if (!canContinue && throwError)
                throw new Exception("Sorry! You are not authorised to Edit this Record. Please contact Administrator for appropriate access rights.");

            return canContinue;
        }

        public static bool CanUserChangeStatus(int MenuId)
        {
            bool canContinue = false;

            if (MenuId <= 0)
                throw new Exception("Invalid Menu option selected.");

            if (FrameWork.Core.Localizer.CurrentUser.LstChildMenu.Where(m => m.MenuId == MenuId && m.CanChangeStatus).Count() > 0)
                canContinue = true;

            if (!canContinue)
                throw new Exception("Sorry! You are not authorised to Change the Status. Please contact Administrator for appropriate access rights.");

            return canContinue;
        }

        public static int GetDataFromEncryptedToken(string Token, string key, int recognizeValue)
        {
            int data = 0;

            if (Token == null)
                Token = "";

            if (Token.Length > 0)
            {
                string[] deCryptedToken = DecryptData(Token).Split('`');

                for (int i = 0; i <= deCryptedToken.GetUpperBound(0); i++)
                {
                    string[] tokenKey = deCryptedToken[i].Split('=');

                    if (tokenKey[0] == key)
                        return Convert.ToInt32(tokenKey[1]);
                }
            }

            return data;
        }

        public static string GetDataFromEncryptedToken(string Token, string key, string recognizeValue)
        {
            string data = "";

            if (Token == null)
                Token = "";

            Token = Token.Replace(' ', '+');

            if (Token.Length > 0)
            {
                string[] deCryptedToken = DecryptData(Token).Split('`');

                for (int i = 0; i <= deCryptedToken.GetUpperBound(0); i++)
                {
                    string[] tokenKey = deCryptedToken[i].Split('=');

                    if (tokenKey[0] == key)
                        return tokenKey[1];
                }
            }

            return data;
        }

        public static string EncryptData(string toEncrypt)
        {
            return FrameWork.Core.Utility.EncryptData(toEncrypt);
        }

        public static string DecryptData(string cipherString)
        {
            return FrameWork.Core.Utility.DecryptData(cipherString);
        }

        public static MvcHtmlString GetRecordStatusMessage(string statusType, string message = "")
        {
            string returnMessage = "";

            if (statusType != "")
            {
                returnMessage += System.Environment.NewLine + "<div class='alert alert-success alert-dismissible status-message' role='alert'>";
                returnMessage += System.Environment.NewLine + "     <button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button>";
                returnMessage += System.Environment.NewLine + "     <span class='fa fa-success fa-lg'></span>";

                if (statusType == "A")
                    returnMessage += System.Environment.NewLine + "     <strong>Success!</strong> Record has been added successfully." + message;

                if (statusType == "M")
                    returnMessage += System.Environment.NewLine + "     <strong>Success!</strong> Record has been modified successfully." + message;

                if (statusType == "S")
                    returnMessage += System.Environment.NewLine + "     <strong>Success!</strong> Status has been modified successfully." + message;

                if (statusType == "UPLOAD_DONE")
                    returnMessage += System.Environment.NewLine + "     <strong>Success!</strong> File has been uploaded successfully." + message;

                if (statusType == "VCSS")
                    returnMessage += System.Environment.NewLine + "     Verification code has been sent at your registered email id. Please use that verification code to complete login process." + message;

                if (statusType == "RP")
                    returnMessage += System.Environment.NewLine + "     Password Reset Link has been sent to your email address. Please check your email inbox." + message;

                if (statusType == "RPS")
                    returnMessage += System.Environment.NewLine + "     Your password has been changed successfully. Click on below Login button to access your account." + message;

                if (statusType == "UIP")
                    returnMessage += System.Environment.NewLine + "     IP Address : " + message + " has been unblocked successfully.";

                if (statusType == "SOTP")
                    returnMessage += System.Environment.NewLine + "     OTP Sent successfully.";

                if (statusType == "ROTP")
                    returnMessage += System.Environment.NewLine + "     OTP Resent successfully.";

                if (statusType == "TPR") /*Teacher Password Reset*/
                    returnMessage += System.Environment.NewLine + "     <strong>Success!</strong> Password has been changed successfully.";

                if (statusType == "D")
                    returnMessage += System.Environment.NewLine + "     <strong>Success!</strong> Record has been deleted successfully." + message;

                if (statusType == "SFA") /*Request sent for Approval. For Bulk Update Student Data.*/
                    returnMessage += System.Environment.NewLine + "     <strong>Success!</strong> Your request has been submitted and sent for approval successfully." + message;

                returnMessage += System.Environment.NewLine + "</div>";
            }

            return MvcHtmlString.Create(returnMessage);
        }

        public static bool ValidatePasswordPolicy(string password, int lengthRequired = 6)
        {

            //Minimum 6 characters at least 1 Uppercase Alphabet, 1 Lowercase Alphabet, 1 Number and 1 Special Character
            string PasswordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&#])[A-Za-z\d$@$!%*?&#]{" + lengthRequired.ToString() + ",}";

            if (!Regex.IsMatch(password, PasswordPattern))
            {
                throw new ApplicationException(String.Format("The Password must have at least 1 Uppercase Alphabet, 1 Lowercase Alphabet, 1 Number and 1 Special Character and should be " + lengthRequired.ToString() + " characters long."));
            }

            return true;



            //Other Patterns are as follows:
            //1. Minimum 8 characters at least 1 Alphabet and 1 Number
            //      ^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$
            //2. Minimum 8 characters at least 1 Alphabet, 1 Number and 1 Special Character
            //      ^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{8,}$
            //3. Minimum 8 characters at least 1 Uppercase Alphabet, 1 Lowercase Alphabet and 1 Number
            //      ^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$
            //4. Minimum 8 and Maximum 10 characters at least 1 Uppercase Alphabet, 1 Lowercase Alphabet, 1 Number and 1 Special Character
            //  ^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{8,10}

            //string PasswordPattern = @"^(?=.*[0-9])(?=.*[!@#$%^&*])[0-9a-zA-Z!@#$%^&*0-9]{10,}$";\
            //The Password must have at least one numeric and one special character
        }

        public static MvcHtmlString ShowCalendar(List<CalendarEntries> lstEntries, string area, string controller, string action, string previousMonthToken, string nextMonthToken, int MonthNo = 0, int YearNo = 0)
        {
            string strCalendar = string.Empty;

            if (MonthNo == 0)
                MonthNo = DateTime.Now.Month;

            if (YearNo == 0)
                YearNo = DateTime.Now.Year;

            DateTime CalendarDate = new DateTime(YearNo, MonthNo, 1);

            try
            {
                string legendText = "Holiday"; ;
                string legendRow = "";
                legendRow = legendRow + System.Environment.NewLine + "<label class=\"badge-calendar bg-blue\">&nbsp;</label> Holiday";


                strCalendar = strCalendar + System.Environment.NewLine + "      <table id=\"datatable\" data-classes=\table\" class=\"table table-striped table-bordered jambo_table\">";
                strCalendar = strCalendar + System.Environment.NewLine + "              <tr style=\"color:#ffffff;background-color:#3C8DBC;\">";

                if (previousMonthToken != "")
                    strCalendar = strCalendar + System.Environment.NewLine + "                  <th class=\"text-center\"><a href=\"/" + area + "/" + controller + "/" + action + "?" + previousMonthToken + "\" title=\"Previous Month\"><i class=\"glyphicon glyphicon-menu-left\"></i></a></th>";
                else
                    strCalendar = strCalendar + System.Environment.NewLine + "                  <th class=\"text-center\"><a href=\"#\" title=\"Previous Month\"><i class=\"glyphicon glyphicon-menu-left\"></i></a></th>";

                strCalendar = strCalendar + System.Environment.NewLine + "                  <th colspan=\"5\" class=\"text-center\">" + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(MonthNo) + " " + YearNo + "</th>";


                if (nextMonthToken != "")
                    strCalendar = strCalendar + System.Environment.NewLine + "                  <th class=\"text-center\"><a href=\"/" + area + "/" + controller + "/" + action + "?" + nextMonthToken + "\"  title=\"Next Month\"><i class=\"glyphicon glyphicon-menu-right\"></i></a></th>";
                else
                    strCalendar = strCalendar + System.Environment.NewLine + "                  <th class=\"text-center\"><a href=\"#\"  title=\"Next Month\"><i class=\"glyphicon glyphicon-menu-right\"></i></a></th>";



                strCalendar = strCalendar + System.Environment.NewLine + "              </tr>";
                strCalendar = strCalendar + System.Environment.NewLine + "              <tr style=\"color:#9A94AE;background-color:#ECEDEE;\">";
                strCalendar = strCalendar + System.Environment.NewLine + "                  <th class=\"text-center\">S</th>";
                strCalendar = strCalendar + System.Environment.NewLine + "                  <th class=\"text-center\">M</th>";
                strCalendar = strCalendar + System.Environment.NewLine + "                  <th class=\"text-center\">T</th>";
                strCalendar = strCalendar + System.Environment.NewLine + "                  <th class=\"text-center\">W</th>";
                strCalendar = strCalendar + System.Environment.NewLine + "                  <th class=\"text-center\">T</th>";
                strCalendar = strCalendar + System.Environment.NewLine + "                  <th class=\"text-center\">F</th>";
                strCalendar = strCalendar + System.Environment.NewLine + "                  <th class=\"text-center\">S</th>";
                strCalendar = strCalendar + System.Environment.NewLine + "              </tr>";
                strCalendar = strCalendar + System.Environment.NewLine + "          ";
                strCalendar = strCalendar + System.Environment.NewLine + "          <tbody>";
                strCalendar = strCalendar + System.Environment.NewLine + "              <tr style=\"background-color:#ffffff !important;\" class=\"nohover\">";

                for (int i = 1; i <= (int)CalendarDate.DayOfWeek; i++)
                {
                    strCalendar = strCalendar + System.Environment.NewLine + "                  <td class=\"text-center\">";
                    strCalendar = strCalendar + System.Environment.NewLine + "                  </td>";
                }

                for (int i = 1; i <= CalendarDate.AddMonths(1).AddDays(-1).Day; i++)
                {
                    DateTime tempDate = new DateTime(YearNo, MonthNo, i);

                    CalendarEntries calendar = new CalendarEntries();
                    calendar.CalendarDate = tempDate;
                    calendar.Remarks = "";
                    calendar.HorizontalAllignment = "text-center";
                    calendar.BadgeColor = "";
                    calendar.TooltipRemarks = "";
                    calendar.LegendText = "Un-Marked";

                    if (lstEntries.Where(m => m.CalendarDate == tempDate).Count() > 0)
                        calendar = lstEntries.Where(m => m.CalendarDate == tempDate).ToList()[0];




                    if (legendText.IndexOf(calendar.LegendText) < 0)
                    {
                        legendText += calendar.LegendText;
                        legendRow = legendRow + System.Environment.NewLine + "<label style=\"margin-left: 10px !important\" class=\"badge-calendar" + (calendar.BadgeColor == "" ? "" : " " + calendar.BadgeColor) + "\">&nbsp;</label> " + calendar.LegendText;
                    }


                    string strEntry = "";
                    strEntry = strEntry + System.Environment.NewLine + "                  <td id=\"tdCalendar" + i + "\" class=\"" + calendar.HorizontalAllignment + "\" title=\"" + calendar.TooltipRemarks + "\">";
                    strEntry = strEntry + System.Environment.NewLine + "                      <label id=\"lblCalendarDay" + i + "\" style=\"margin-top:6px !important;\" class=\"badge-calendar" + (calendar.BadgeColor == "" && tempDate.DayOfWeek == DayOfWeek.Sunday ? " bg-blue" : " " + calendar.BadgeColor) + "\">" + i + "</label>";
                    strEntry = strEntry + System.Environment.NewLine + "                  </td>";


                    if (tempDate.DayOfWeek == DayOfWeek.Sunday)
                    {
                        strCalendar = strCalendar + System.Environment.NewLine + "                  <td id=\"tdCalendar" + i + "\" class=\"" + calendar.HorizontalAllignment + "\">";
                        strCalendar = strCalendar + System.Environment.NewLine + "                      <label id=\"lblCalendarDay" + i + "\" style=\"margin-top:10px !important;margin-bottom:10px !important;\" class=\"badge-calendar" + (calendar.BadgeColor == "" ? " bg-blue" : " " + calendar.BadgeColor) + "\">" + i + "</label>";
                        strCalendar = strCalendar + System.Environment.NewLine + "                  </td>";
                    }

                    if (tempDate.DayOfWeek == DayOfWeek.Monday)
                        strCalendar = strCalendar + strEntry;

                    if (tempDate.DayOfWeek == DayOfWeek.Tuesday)
                        strCalendar = strCalendar + strEntry;

                    if (tempDate.DayOfWeek == DayOfWeek.Wednesday)
                        strCalendar = strCalendar + strEntry;

                    if (tempDate.DayOfWeek == DayOfWeek.Thursday)
                        strCalendar = strCalendar + strEntry;

                    if (tempDate.DayOfWeek == DayOfWeek.Friday)
                        strCalendar = strCalendar + strEntry;

                    if (tempDate.DayOfWeek == DayOfWeek.Saturday)
                    {
                        strCalendar = strCalendar + strEntry;
                        strCalendar = strCalendar + System.Environment.NewLine + "              </tr>";
                        strCalendar = strCalendar + System.Environment.NewLine + "              ";
                        strCalendar = strCalendar + System.Environment.NewLine + "              <tr style=\"background-color:#ffffff !important;\" class=\"nohover\">";
                    }
                }

                strCalendar = strCalendar + System.Environment.NewLine + "              </tr>";
                strCalendar = strCalendar + System.Environment.NewLine + "          </tbody>";
                strCalendar = strCalendar + System.Environment.NewLine + "      </table>";


                strCalendar = strCalendar + legendRow;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

            //Removing Extra White Spaces
            strCalendar = Regex.Replace(strCalendar, @"\s+", " ");

            return MvcHtmlString.Create(strCalendar);
        }
        
      
        public static MvcHtmlString PrepareCalendar(List<CalendarEntries> lstEntries, string area, string controller, string action, string previousMonthToken, string nextMonthToken, int MonthNo, int YearNo, int lgSize, int mdSize, int smSize, int xsSize)
        {
            string strCalendar = string.Empty;
            string legendText = "Holiday ";
            string legendRow = string.Empty;


            legendRow = legendRow + System.Environment.NewLine + "          <div class='col-lg-4 col-md-6 col-sm-6 col-xs-6'>";
            legendRow = legendRow + System.Environment.NewLine + "              <div class='pull-left'>";
            legendRow = legendRow + System.Environment.NewLine + "                  <div class='color-box orange-box'></div>";
            legendRow = legendRow + System.Environment.NewLine + "              </div>";
            legendRow = legendRow + System.Environment.NewLine + "              Holiday";
            legendRow = legendRow + System.Environment.NewLine + "          </div>";


            if (MonthNo == 0)
                MonthNo = DateTime.Now.Month;

            if (YearNo == 0)
                YearNo = DateTime.Now.Year;

            DateTime CalendarDate = new DateTime(YearNo, MonthNo, 1);

            string actionClass = "";
            if (action == "AttendanceCalendar")
                actionClass = "get-attendance-calendar";
            if (action == "InstituteCalendar")
                actionClass = "get-institute-calendar";

            try
            {
                strCalendar = strCalendar + System.Environment.NewLine + "<div class='col-lg-" + lgSize + " col-md-" + mdSize + " col-sm-" + smSize + " col-xs-" + xsSize + "'>";
                strCalendar = strCalendar + System.Environment.NewLine + "  <div class='month'>";
                strCalendar = strCalendar + System.Environment.NewLine + "      <ul>";
                strCalendar = strCalendar + System.Environment.NewLine + "          <li class='prev'><a href=\"#\" class=\"" + actionClass +  "\" title=\"Previous Month\" data-token=\"" + previousMonthToken + "\" style='color: white'>&#10094;</a></li>";
                strCalendar = strCalendar + System.Environment.NewLine + "          <li class='next'><a href=\"#\" class=\"" + actionClass + "\" title=\"Next Month\" data-token=\"" + nextMonthToken + "\" style='color: white'>&#10095;</a></li>";
                strCalendar = strCalendar + System.Environment.NewLine + "          <li>" + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(MonthNo) + "  -  <span style='font -size:18px'>" + YearNo + "</span></li>";
                strCalendar = strCalendar + System.Environment.NewLine + "      </ul>";
                strCalendar = strCalendar + System.Environment.NewLine + "  </div>";
                strCalendar = strCalendar + System.Environment.NewLine + "  ";
                strCalendar = strCalendar + System.Environment.NewLine + "  <ul class='weekdays'>";
                strCalendar = strCalendar + System.Environment.NewLine + "      <li>Su</li>";
                strCalendar = strCalendar + System.Environment.NewLine + "      <li>Mo</li>";
                strCalendar = strCalendar + System.Environment.NewLine + "      <li>Tu</li>";
                strCalendar = strCalendar + System.Environment.NewLine + "      <li>We</li>";
                strCalendar = strCalendar + System.Environment.NewLine + "      <li>Th</li>";
                strCalendar = strCalendar + System.Environment.NewLine + "      <li>Fr</li>";
                strCalendar = strCalendar + System.Environment.NewLine + "      <li>Sa</li>";
                strCalendar = strCalendar + System.Environment.NewLine + "      <div class='clear'></div>";
                strCalendar = strCalendar + System.Environment.NewLine + "  </ul>";
                strCalendar = strCalendar + System.Environment.NewLine + "  ";
                strCalendar = strCalendar + System.Environment.NewLine + "  <ul class='days'>";


                int daysCount = 0;

                for (int i = 1; i <= (int)CalendarDate.DayOfWeek; i++)
                {
                    daysCount++;
                    strCalendar = strCalendar + System.Environment.NewLine + "      <li><a style='border:none !important'></a></li>";
                }

                for (int i = 1; i <= CalendarDate.AddMonths(1).AddDays(-1).Day; i++)
                {
                    DateTime tempDate = new DateTime(YearNo, MonthNo, i);

                    CalendarEntries calendar = new CalendarEntries();
                    calendar.CalendarDate = tempDate;
                    calendar.Remarks = "";
                    calendar.BadgeColor = "";
                    calendar.TooltipRemarks = "";
                    calendar.LegendText = "";

                    if (lstEntries.Where(m => m.CalendarDate == tempDate).Count() > 0)
                        calendar = lstEntries.Where(m => m.CalendarDate == tempDate).ToList()[0];


                    if (tempDate.DayOfWeek == DayOfWeek.Sunday)
                        strCalendar = strCalendar + System.Environment.NewLine + "      <li><div class='holiday'><a>" + i + "</a></div></li>";

                    else
                        strCalendar = strCalendar + System.Environment.NewLine + "      <li title='" + calendar.Remarks + "'><div class='" + calendar.CalendarClass + "'><a>" + i + "</a><div></li>";



                    

                    if (legendText.IndexOf(calendar.LegendText) <= 0)
                    {
                        legendText = legendText + " " + calendar.LegendText;

                        if (calendar.LegendText == "Present")
                        {
                            legendRow = legendRow + System.Environment.NewLine + "          <div class='col-lg-4 col-md-6 col-sm-6 col-xs-6'>";
                            legendRow = legendRow + System.Environment.NewLine + "              <div class='pull-left'>";
                            legendRow = legendRow + System.Environment.NewLine + "                  <div class='color-box blue-box'></div>";
                            legendRow = legendRow + System.Environment.NewLine + "              </div>";
                            legendRow = legendRow + System.Environment.NewLine + "              Present";
                            legendRow = legendRow + System.Environment.NewLine + "          </div>";
                        }

                        if (calendar.LegendText == "Absent")
                        {
                            legendRow = legendRow + System.Environment.NewLine + "          <div class='col-lg-4 col-md-6 col-sm-6 col-xs-6'>";
                            legendRow = legendRow + System.Environment.NewLine + "              <div class='pull-left'>";
                            legendRow = legendRow + System.Environment.NewLine + "                  <div class='color-box red-box'></div>";
                            legendRow = legendRow + System.Environment.NewLine + "              </div>";
                            legendRow = legendRow + System.Environment.NewLine + "              Absent";
                            legendRow = legendRow + System.Environment.NewLine + "          </div>";
                        }

                        if (calendar.LegendText == "Half Day")
                        {
                            legendRow = legendRow + System.Environment.NewLine + "          <div class='col-lg-4 col-md-6 col-sm-6 col-xs-6'>";
                            legendRow = legendRow + System.Environment.NewLine + "              <div class='pull-left'>";
                            legendRow = legendRow + System.Environment.NewLine + "                  <div class='color-box sky-box'></div>";
                            legendRow = legendRow + System.Environment.NewLine + "              </div>";
                            legendRow = legendRow + System.Environment.NewLine + "              Half Day";
                            legendRow = legendRow + System.Environment.NewLine + "          </div>";
                        }

                        if (calendar.LegendText == "Leave")
                        {
                            legendRow = legendRow + System.Environment.NewLine + "          <div class='col-lg-4 col-md-6 col-sm-6 col-xs-6'>";
                            legendRow = legendRow + System.Environment.NewLine + "              <div class='pull-left'>";
                            legendRow = legendRow + System.Environment.NewLine + "                  <div class='color-box grey-box'></div>";
                            legendRow = legendRow + System.Environment.NewLine + "              </div>";
                            legendRow = legendRow + System.Environment.NewLine + "              Leave";
                            legendRow = legendRow + System.Environment.NewLine + "          </div>";
                        }
                    }


                    daysCount++;

                    if (daysCount % 7 == 0)
                        strCalendar = strCalendar + System.Environment.NewLine + "      <div class='clear'></div>";

                }


                strCalendar = strCalendar + System.Environment.NewLine + "      <div class='clear'></div>";
                strCalendar = strCalendar + System.Environment.NewLine + "      <div class='sign'>";
                strCalendar = strCalendar + System.Environment.NewLine + legendRow;
                strCalendar = strCalendar + System.Environment.NewLine + "      </div>";
                strCalendar = strCalendar + System.Environment.NewLine + "      <div class='clear'></div>";
                strCalendar = strCalendar + System.Environment.NewLine + "  </ul>";
                strCalendar = strCalendar + System.Environment.NewLine + "  <div class='clear'></div>";
                strCalendar = strCalendar + System.Environment.NewLine + "</div>";
                //strCalendar = strCalendar + legendRow;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

            //Removing Extra White Spaces
            strCalendar = Regex.Replace(strCalendar, @"\s+", " ");

            return MvcHtmlString.Create(strCalendar);
        }

    }
}