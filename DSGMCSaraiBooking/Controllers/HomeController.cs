using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.Net;
using System.Xml.Linq;
using System.Threading.Tasks;

using System.Text;
using System.Xml;
using System.Data;
using System.IO;
using SaraiBooking.App_Start;
using BusinessObjects.Common;
using Facade.Common;
using FrameWork.Core;
using SaraiBooking.ViewModels;

namespace SaraiBooking.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        //private int LoginAttempt = 0;
        private string IP = string.Empty;

        private string base64String = null;

        public ActionResult Index(string token)
        {
            Session["APP_PREFIX"] = "DES";
            Session[Session["APP_PREFIX"] + "_USER_MASTER_SESSION"] = null;
            Session[Session["APP_PREFIX"] + "_SERVER_ROOT_PATH"] = Server.MapPath("~");

            ViewBag.CredentialError = " ";

            UserMaster um = new UserMaster();

            return View(um);
        }

        [HttpPost]
        public ActionResult Index()
        {
            UserMaster userMaster = new UserMaster();
            UserMasterFacade facade = new UserMasterFacade();
            BlockedIPAddressFacade blockedIPAddressFacade = new BlockedIPAddressFacade();

            try
            {
                // Code for validating the CAPTCHA  
                if (Request.Form["txtCaptcha"] != HttpContext.Session["CaptchaString"].ToString())
                {
                    ViewBag.CredentialError = "Sorry! Invalid Captcha";
                    return View();
                }


                userMaster.EmailId = Request.Form["txtUserName"];
                userMaster.Password = Request.Form["txtPassword"];

                #region Authenticate Username and Passowrd

                int Id = facade.ValidateUserCredentials(userMaster, Request.ServerVariables["REMOTE_ADDR"].ToString(), Request.Url.Scheme + "://" + Request.Url.Host + ":" + Request.Url.Port, UserType.GURUDWARA);

                //If Id is less than or Equal to ZERO, then Invalid Username or Password
                if (Id <= 0)
                {
                    ViewBag.CredentialError = "Invalid Credentials. To generate new password, use Forgot Password option.";
                }


                if (Id > 0)
                {
                    userMaster = facade.GetDetailById(Id);
                    userMaster.UserType = UserType.GURUDWARA;
                    userMaster.IPAddress = Request.ServerVariables["REMOTE_ADDR"].ToString();
                    userMaster.BrowserInformation = Request.ServerVariables["HTTP_USER_AGENT"].ToString();

                    Session.Timeout = 60;

                    Session[Session["APP_PREFIX"] + "_SessionId"] = Session.SessionID;
                    Session[Session["APP_PREFIX"] + "_USER_MASTER_SESSION"] = userMaster;

                    //Check for Extra Security Checks
                    if (facade.isValidLoginDaysAndTime(userMaster))
                    {
                        if (userMaster.ExtraSecurityRequired)
                        {
                            CommonFacade facadeCommon = new CommonFacade();
                            string verificationCode = facadeCommon.CreateRandomCode(6, true);

                            facade.MailVerificationCode(userMaster, verificationCode, Server.MapPath("~/EmailTemplates/VerificationCode.htm"));

                            return RedirectToAction("Security", "Home", new { Token = SaraiBooking.App_Start.Common.EncryptData("`VERIFICATION_CODE=" + verificationCode + "`RECORD_STATUS=VCSS") });
                        }
                        else
                        {
                            userMaster.LoginHistoryId = facade.SaveLoginSessionHistory();

                            Session[Session["APP_PREFIX"] + "_USER_MASTER_SESSION"] = userMaster;

                            return RedirectToAction("About", "AboutUs");
                        }
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                Session[Session["APP_PREFIX"] + "_SessionId"] = null;
                Session[Session["APP_PREFIX"] + "_USER_MASTER_SESSION"] = null;

                ViewBag.CredentialError = ex.Message;
            }

            return View(userMaster);
        }

        public ActionResult LogOut()
        {
            new CommonFacade().LogOutSession();


            Session[Session["APP_PREFIX"] + "_USER_MASTER_SESSION"] = null;
            Session[Session["APP_PREFIX"] + "_SERVER_ROOT_PATH"] = null;
            Session[Session["APP_PREFIX"] + "_SessionId"] = null;

            return RedirectToAction("Index");
        }

        

        public ActionResult Security(string Token, string errorMessage = "")
        {
            try
            {
                string verificationCode = SaraiBooking.App_Start.Common.GetDataFromEncryptedToken(Token, "VERIFICATION_CODE", "");

                ViewBag.VerificationCode = App_Start.Common.EncryptData(verificationCode);

                ViewBag.ErrorMessage = (errorMessage.Trim() == "" ? MvcHtmlString.Create("") : MvcHtmlString.Create(SaraiBooking.App_Start.Common.RefineErrorMessage(errorMessage)));

                if (errorMessage.Trim() == "")
                    ViewBag.DisplayMessage = SaraiBooking.App_Start.Common.GetDataFromEncryptedToken(Token, "RECORD_STATUS", "");

                return View();
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = MvcHtmlString.Create(SaraiBooking.App_Start.Common.RefineErrorMessage(ex.Message));
                return View();
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Security()
        {
            ViewBag.VerificationCode = Request.Form["hdnVerificationCode"];

            HttpRequestBase request = ControllerContext.HttpContext.Request;
            ViewBag.DisplayMessage = request.Unvalidated.Form.Get("hdnDisplayMessage");

            string inputVerificationCode = Request.Form["txtVerificationCode"].ToLower();

            try
            {
                if (Utility.DecryptData(Request.Form["hdnVerificationCode"]).ToLower() == Utility.DecryptData(Request.Form["txtVerificationCode"]).ToLower())
                {
                    UserMasterFacade facade = new UserMasterFacade();

                    UserMaster userMaster = Localizer.CurrentUser;

                    userMaster.LoginHistoryId = facade.SaveLoginSessionHistory();

                    Session["WSM_USER_MASTER_SESSION"] = userMaster;

                    return RedirectToAction("About", "AboutUs");
                }
                else
                {
                    ViewBag.ErrorMessage = "Invalid Verification Code";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = MvcHtmlString.Create(SaraiBooking.App_Start.Common.RefineErrorMessage(ex.Message));
                return View();
            }
        }

        public JsonResult Encrypt(string username, string password)
        {
            UserMaster user = new UserMaster();

            try
            {
                username = SaraiBooking.App_Start.Common.EncryptData(username.ToLower().Trim());
                password = SaraiBooking.App_Start.Common.EncryptData(password.Trim());

                user.EmailId = username;
                user.Password = password;
                user.UserCultureInfo = null;
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
            }

            return Json(user, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ForgotPassword(string Token)
        {
            UserMaster userMasterObject = new UserMaster();

            try
            {
                ViewBag.EmailId = string.Empty;
                ViewBag.SuccessMessage = "UnSuccess";

                if (Token != null)
                {
                    ViewBag.EmailId = SaraiBooking.App_Start.Common.GetDataFromEncryptedToken(Token, "EMAIL_ID", "");

                    if (!string.IsNullOrEmpty(SaraiBooking.App_Start.Common.GetDataFromEncryptedToken(Token, "RECORD_STATUS", "")))
                    {
                        ViewBag.ErrorMessage = SaraiBooking.App_Start.Common.GetRecordStatusMessage(SaraiBooking.App_Start.Common.GetDataFromEncryptedToken(Token, "RECORD_STATUS", ""));
                        ViewBag.SuccessMessage = "Success";
                    }
                }

                return View(userMasterObject);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = MvcHtmlString.Create(SaraiBooking.App_Start.Common.RefineErrorMessage(ex.Message));
                ViewBag.SuccessMessage = "UnSuccess";
                return View(userMasterObject);
            }
        }

        [HttpPost]
        public ActionResult ForgotPassword(UserMaster objUserMaster)
        {
            try
            {
                ViewBag.SuccessMessage = "UnSuccess";

                UserMasterFacade facadeUserMaster = new UserMasterFacade();
                //string url = Url.Action("ResetPassword", "Home", new { Token = SaraiBooking.App_Start.Common.EncryptData("USER_MASTER_ID=" + objUserMaster.Id + "`PASSWORD_RESET_TOKEN=" + "" + "`EMAIL_ID=" + "") }, Request.Url.Scheme);

                facadeUserMaster.SendPasswordResetLink(objUserMaster.EmailId.Trim().ToLower(), Server.MapPath("~/EmailTemplates/ResetPasswordTemplate.htm"), Request.Url.Scheme + "://" + Request.Url.Host + ":" + Request.Url.Port);

                return RedirectToAction("ForgotPassword", "Home", new { Token = SaraiBooking.App_Start.Common.EncryptData("`EMAIL_ID=" + "" + "`RECORD_STATUS=RP") });
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = MvcHtmlString.Create(SaraiBooking.App_Start.Common.RefineErrorMessage(ex.Message));
                ViewBag.SuccessMessage = "UnSuccess";
                return View(objUserMaster);
            }
        }

        public ActionResult ResetPassword(string Token)
        {
            UserMaster obj = new UserMaster();
            UserMasterFacade facadeUserMaster = new UserMasterFacade();

            try
            {
                if (Token == null)
                    throw new ApplicationException("Invalid Password Reset Link.");

                if (Token == "")
                    throw new ApplicationException("Invalid Password Reset Link.");

                ViewBag.IsLinkExpired = "0";

                if (DateTime.Now > Convert.ToDateTime(SaraiBooking.App_Start.Common.GetDataFromEncryptedToken(Token, "TOKEN_EXPIRE_TIME", "")))
                {
                    ViewBag.IsLinkExpired = "1";
                    throw new ApplicationException("Sorry! This link has been expired now.");
                }

                obj.PasswordResetToken = SaraiBooking.App_Start.Common.GetDataFromEncryptedToken(Token, "PASSWORD_RESET_TOKEN", "");
                obj.Id = SaraiBooking.App_Start.Common.GetDataFromEncryptedToken(Token, "ID", 0);
                obj.Name = SaraiBooking.App_Start.Common.GetDataFromEncryptedToken(Token, "USER_NAME", "");
                obj.EmailId = SaraiBooking.App_Start.Common.GetDataFromEncryptedToken(Token, "EMAIL_ID", "");

                return View(obj);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = MvcHtmlString.Create(SaraiBooking.App_Start.Common.RefineErrorMessage(ex.Message));
                return View(obj);
            }
        }

        [HttpPost]
        public ActionResult ResetPassword(UserMaster objUserMaster)
        {
            try
            {
                SaraiBooking.App_Start.Common.ValidatePasswordPolicy(objUserMaster.NewPassword.Trim());

                UserMasterFacade facadeUserMaster = new UserMasterFacade();

                objUserMaster = facadeUserMaster.UpdatePassword(objUserMaster);

                string filePath = Server.MapPath("~/EmailTemplates/ResetPasswordSuccessTemplate.htm");
                string toEmailAddress = objUserMaster.EmailId;
                string userName = objUserMaster.Name;
                string title = "Login";
                string url = Request.Url.Scheme + "://" + Request.Url.Host + ":" + Request.Url.Port;
                string description = "Your password has been changed successfully.";
                string mailSubject = "Password changed successfully.";

                (new CommonFacade()).SendPasswordResetSuccessMail(filePath, toEmailAddress, userName, title, url, description, mailSubject);

                return RedirectToAction("ResetPasswordSuccess", "Home");

            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = MvcHtmlString.Create(SaraiBooking.App_Start.Common.RefineErrorMessage(ex.Message));
                return View(objUserMaster);
            }
        }

        public ActionResult ResetPasswordSuccess()
        {
            return View();
        }

        public ActionResult UnBlockIPAddress(string Token)
        {
            Token = (Token == null ? "" : Token.Trim());
            try
            {
                if (Token == "")
                    throw new ApplicationException("Invalid IP Address.");

                string ipAddress = SaraiBooking.App_Start.Common.GetDataFromEncryptedToken(Token, "IP_ADDRESS", "");

                (new BlockedIPAddressFacade()).UnBlockIPAddress(ipAddress);

                ViewBag.ErrorMessage = SaraiBooking.App_Start.Common.GetRecordStatusMessage("UIP", ipAddress);

                return View();
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = MvcHtmlString.Create(SaraiBooking.App_Start.Common.RefineErrorMessage(ex.Message));
                return View();
            }
        }

        [NoCache]
        public CaptchaImageResult ShowCaptchaImage()
        {
            //Session["Base64String"] = ImageToBase64();
            Session["Base64String"] = "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";

            return new CaptchaImageResult();
        }


        public string ImageToBase64()
        {
            string path = Server.MapPath("~/Content/Images/captcha_bg.png");
            using (System.Drawing.Image image = System.Drawing.Image.FromFile(path))
            {
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    byte[] imageBytes = m.ToArray();
                    base64String = Convert.ToBase64String(imageBytes);
                    return base64String;
                }
            }
        }


    }
}