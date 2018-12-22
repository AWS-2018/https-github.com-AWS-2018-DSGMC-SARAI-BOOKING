using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using BusinessObjects.Common;
using SaraiBooking;
using Facade.Common;
using SaraiBooking.Controllers;
using SaraiBooking.ViewModels;

namespace SaraiBooking.Controllers
{
    public class UserMasterController : BaseController
    {
        UserMasterFacade facade = new UserMasterFacade();
        Error objError = new Error();

        string page = "User";

        public ActionResult Index(string Token, string errorMessage = "")
        {
            try
            {
                if (FrameWork.Core.Localizer.CurrentUser.IsSeniorAdmin == false)
                    throw new ApplicationException("Sorry! You are not authorized to use this option.");


                if (SaraiBooking.App_Start.Common.GetDataFromEncryptedToken(Token, "RESET", 0) == 1)
                {
                    TempData.Remove("SearchCriteria");
                }

                string searchCriteria = string.Empty;
                string pagerSearchCriteria = string.Empty;

                if (TempData["SearchCriteria"] != null)
                    searchCriteria = TempData["SearchCriteria"].ToString();

                TempData.Keep("SearchCriteria");


                int pageNumber = 1;

                if (Token != null)
                    pageNumber = SaraiBooking.App_Start.Common.GetDataFromEncryptedToken(Token, "PAGE_NUMBER", 0);

                ViewBag.MenuId = SaraiBooking.App_Start.Common.GetDataFromEncryptedToken(Token, "MENU_ID", 0);

                ViewBag.Name = SaraiBooking.App_Start.Common.GetDataFromEncryptedToken(Token, "NAME", "");
                ViewBag.EmailId = SaraiBooking.App_Start.Common.GetDataFromEncryptedToken(Token, "EMAIL_ID", "");
                ViewBag.MobileNo = SaraiBooking.App_Start.Common.GetDataFromEncryptedToken(Token, "MOBILE_NO", "");

             
                ViewBag.IsSeniorAdminChecked = (string.IsNullOrEmpty(SaraiBooking.App_Start.Common.GetDataFromEncryptedToken(Token, "IS_SENIORADMIN_CHECKED", "")) == true ? false : Convert.ToBoolean(SaraiBooking.App_Start.Common.GetDataFromEncryptedToken(Token, "IS_SENIORADMIN_CHECKED", "")));
                ViewBag.IsSuperAdminChecked = (string.IsNullOrEmpty(SaraiBooking.App_Start.Common.GetDataFromEncryptedToken(Token, "IS_SUPERADMIN_CHECKED", "")) == true ? false : Convert.ToBoolean(SaraiBooking.App_Start.Common.GetDataFromEncryptedToken(Token, "IS_SUPERADMIN_CHECKED", "")));
                ViewBag.IsOperatorChecked = (string.IsNullOrEmpty(SaraiBooking.App_Start.Common.GetDataFromEncryptedToken(Token, "IS_OPERATOR_CHECKED", "")) == true ? false : Convert.ToBoolean(SaraiBooking.App_Start.Common.GetDataFromEncryptedToken(Token, "IS_OPERATOR_CHECKED", "")));
                ViewBag.IsAuditorChecked = (string.IsNullOrEmpty(SaraiBooking.App_Start.Common.GetDataFromEncryptedToken(Token, "IS_AUDITOR_CHECKED", "")) == true ? false : Convert.ToBoolean(SaraiBooking.App_Start.Common.GetDataFromEncryptedToken(Token, "IS_AUDITOR_CHECKED", "")));
                ViewBag.IsHeadOfficeChecked = (string.IsNullOrEmpty(SaraiBooking.App_Start.Common.GetDataFromEncryptedToken(Token, "IS_HEAD_OFFICE_CHECKED", "")) == true ? false : Convert.ToBoolean(SaraiBooking.App_Start.Common.GetDataFromEncryptedToken(Token, "IS_HEAD_OFFICE_CHECKED", "")));
                ViewBag.CanApproveEmployeeMasterChecked = (string.IsNullOrEmpty(SaraiBooking.App_Start.Common.GetDataFromEncryptedToken(Token, "CAN_APPROVE_EMPLOYEE_MASTER_CHECKED", "")) == true ? false : Convert.ToBoolean(SaraiBooking.App_Start.Common.GetDataFromEncryptedToken(Token, "CAN_APPROVE_EMPLOYEE_MASTER_CHECKED", "")));
                ViewBag.CanApproveEmployeeIncrementMasterChecked = (string.IsNullOrEmpty(SaraiBooking.App_Start.Common.GetDataFromEncryptedToken(Token, "CAN_APPROVE_EMPLOYEE_INCREMENT_MASTER_CHECKED", "")) == true ? false : Convert.ToBoolean(SaraiBooking.App_Start.Common.GetDataFromEncryptedToken(Token, "CAN_APPROVE_EMPLOYEE_INCREMENT_MASTER_CHECKED", "")));

                ViewBag.IsSeniorAdmin = (string.IsNullOrEmpty(SaraiBooking.App_Start.Common.GetDataFromEncryptedToken(Token, "IS_SENIORADMIN", "")) == true ? false : Convert.ToBoolean(SaraiBooking.App_Start.Common.GetDataFromEncryptedToken(Token, "IS_SENIORADMIN", "")));
                ViewBag.IsSuperAdmin = (string.IsNullOrEmpty(SaraiBooking.App_Start.Common.GetDataFromEncryptedToken(Token, "IS_SUPERADMIN", "")) == true ? false : Convert.ToBoolean(SaraiBooking.App_Start.Common.GetDataFromEncryptedToken(Token, "IS_SUPERADMIN", "")));
                ViewBag.IsOperator = (string.IsNullOrEmpty(SaraiBooking.App_Start.Common.GetDataFromEncryptedToken(Token, "IS_OPERATOR", "")) == true ? false : Convert.ToBoolean(SaraiBooking.App_Start.Common.GetDataFromEncryptedToken(Token, "IS_OPERATOR", "")));
                ViewBag.IsAuditor = (string.IsNullOrEmpty(SaraiBooking.App_Start.Common.GetDataFromEncryptedToken(Token, "IS_AUDITOR", "")) == true ? false : Convert.ToBoolean(SaraiBooking.App_Start.Common.GetDataFromEncryptedToken(Token, "IS_AUDITOR", "")));
                ViewBag.IsHeadOffice = (string.IsNullOrEmpty(SaraiBooking.App_Start.Common.GetDataFromEncryptedToken(Token, "IS_HEAD_OFFICE", "")) == true ? false : Convert.ToBoolean(SaraiBooking.App_Start.Common.GetDataFromEncryptedToken(Token, "IS_HEAD_OFFICE", "")));
                ViewBag.CanApproveEmployeeMaster = (string.IsNullOrEmpty(SaraiBooking.App_Start.Common.GetDataFromEncryptedToken(Token, "CAN_APPROVE_EMPLOYEE_MASTER", "")) == true ? false : Convert.ToBoolean(SaraiBooking.App_Start.Common.GetDataFromEncryptedToken(Token, "CAN_APPROVE_EMPLOYEE_MASTER", "")));
                ViewBag.CanApproveEmployeeIncrementMaster = (string.IsNullOrEmpty(SaraiBooking.App_Start.Common.GetDataFromEncryptedToken(Token, "CAN_APPROVE_EMPLOYEE_INCREMENT_MASTER", "")) == true ? false : Convert.ToBoolean(SaraiBooking.App_Start.Common.GetDataFromEncryptedToken(Token, "CAN_APPROVE_EMPLOYEE_INCREMENT_MASTER", "")));

                ViewBag.PageHeader = "Users";
                ViewBag.Page = page;
                ViewBag.AccordineHeader = page;
                ViewBag.PageNumber = pageNumber;

                ViewBag.ErrorMessage = (errorMessage.Trim() == "" ? MvcHtmlString.Create("") : MvcHtmlString.Create(SaraiBooking.App_Start.Common.RefineErrorMessage(errorMessage)));

                if (errorMessage.Trim() == "")
                    ViewBag.ErrorMessage = SaraiBooking.App_Start.Common.GetRecordStatusMessage(SaraiBooking.App_Start.Common.GetDataFromEncryptedToken(Token, "RECORD_STATUS", ""));

                CommonFacade obj = new CommonFacade();

                List<UserMaster> lstData = facade.GetListForListPage((int)pageNumber, searchCriteria);

                int recordCount = 0;

                if (lstData.Count > 0)
                    recordCount = lstData[0].RecordCount;


                var pager = new Pager(recordCount, pageNumber);

                var viewModel = new PageList<UserMaster>
                {
                    Items = facade.GetListForListPage((int)pageNumber, searchCriteria),
                    Pager = pager
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = MvcHtmlString.Create(SaraiBooking.App_Start.Common.RefineErrorMessage(ex.Message));
                return View();
            }
        }

        [HttpPost]
        public ActionResult Index(SearchUserViewModel modelSearchUser, string btnReset)
        {
            try
            {
                string searchCriteria = string.Empty;
                string pagerSearchCriteria = string.Empty;

                ViewBag.MenuId = Request.Form["hdnMenuId"];

                if (modelSearchUser != null)
                {
                    modelSearchUser.TrimData();

                    ViewBag.Name = modelSearchUser.Name;
                    ViewBag.EmailId = modelSearchUser.EmailId;
                    ViewBag.MobileNo = modelSearchUser.MobileNo;

                 
                    ViewBag.IsSeniorAdminChecked = Convert.ToBoolean(modelSearchUser.IsSeniorAdminChecked);
                    ViewBag.IsSuperAdminChecked = Convert.ToBoolean(modelSearchUser.IsSuperAdminChecked);
                    ViewBag.IsOperatorChecked = Convert.ToBoolean(modelSearchUser.IsOperatorChecked);
                    ViewBag.IsAuditorChecked = Convert.ToBoolean(modelSearchUser.IsAuditorChecked);
                    ViewBag.IsHeadOfficeChecked = Convert.ToBoolean(modelSearchUser.IsHeadOfficeChecked);
                    ViewBag.CanApproveEmployeeMasterChecked = Convert.ToBoolean(modelSearchUser.CanApproveEmployeeMasterChecked);
                    ViewBag.CanApproveEmployeeIncrementMasterChecked = Convert.ToBoolean(modelSearchUser.CanApproveEmployeeIncrementMasterChecked);

                    ViewBag.IsSeniorAdmin = Convert.ToBoolean(modelSearchUser.IsSeniorAdmin);
                    ViewBag.IsSuperAdmin = Convert.ToBoolean(modelSearchUser.IsSuperAdmin);
                    ViewBag.IsOperator = Convert.ToBoolean(modelSearchUser.IsOperator);
                    ViewBag.IsAuditor = Convert.ToBoolean(modelSearchUser.IsAuditor);
                    ViewBag.IsHeadOffice = Convert.ToBoolean(modelSearchUser.IsHeadOffice);
                    ViewBag.CanApproveEmployeeMaster = Convert.ToBoolean(modelSearchUser.CanApproveEmployeeMaster);
                    ViewBag.CanApproveEmployeeIncrementMaster = Convert.ToBoolean(modelSearchUser.CanApproveEmployeeIncrementMaster);
                                       

                    if (!string.IsNullOrEmpty(modelSearchUser.Name))
                    {
                        searchCriteria += System.Environment.NewLine + "AND M.NAME LIKE '%" + modelSearchUser.Name + "%'";
                    }

                    if (!string.IsNullOrEmpty(modelSearchUser.EmailId))
                    {
                        searchCriteria += System.Environment.NewLine + "AND M.EMAIL_ID LIKE '%" + modelSearchUser.EmailId + "%'";
                    }

                    if (!string.IsNullOrEmpty(modelSearchUser.MobileNo))
                    {
                        searchCriteria += System.Environment.NewLine + "AND M.MOBILE_NO LIKE '%" + modelSearchUser.MobileNo + "%'";
                    }

                    if (modelSearchUser.IsSeniorAdminChecked)
                    {
                        searchCriteria += System.Environment.NewLine + "AND M.IS_SENIORADMIN = '" + modelSearchUser.IsSeniorAdmin + "'";
                    }

                    if (modelSearchUser.IsSuperAdminChecked)
                    {
                        searchCriteria += System.Environment.NewLine + "AND M.IS_SUPERADMIN = '" + modelSearchUser.IsSuperAdmin + "'";
                    }

                    if (modelSearchUser.IsOperatorChecked)
                    {
                        searchCriteria += System.Environment.NewLine + "AND M.IS_OPERATOR = '" + modelSearchUser.IsOperator + "'";
                    }

                    if (modelSearchUser.IsAuditorChecked)
                    {
                        searchCriteria += System.Environment.NewLine + "AND M.IS_AUDITOR = '" + modelSearchUser.IsAuditor + "'";
                    }

                    if (modelSearchUser.IsHeadOfficeChecked)
                    {
                        searchCriteria += System.Environment.NewLine + "AND M.IS_HEAD_OFFICE = '" + modelSearchUser.IsHeadOffice + "'";
                    }

                    if (modelSearchUser.CanApproveEmployeeMasterChecked)
                    {
                        searchCriteria += System.Environment.NewLine + "AND M.CAN_APPROVE_EMPLOYEE_MASTER = '" + modelSearchUser.CanApproveEmployeeMaster + "'";
                    }

                    if (modelSearchUser.CanApproveEmployeeIncrementMasterChecked)
                    {
                        searchCriteria += System.Environment.NewLine + "AND M.CAN_APPROVE_EMPLOYEE_INCREMENT_MASTER = '" + modelSearchUser.CanApproveEmployeeIncrementMaster + "'";
                    }
                }

                TempData["SearchCriteria"] = searchCriteria;

                return RedirectToAction("Index", "UserMaster", new { Token = SaraiBooking.App_Start.Common.EncryptData("`MENU_ID=" + ViewBag.MenuId + "`PAGE_NUMBER=1" + "`NAME=" + (string)ViewBag.Name + "`EMAIL_ID=" + (string)ViewBag.EmailId + "`MOBILE_NO=" + (string)ViewBag.MobileNo + "`IS_SENIORADMIN=" + (bool)ViewBag.IsSeniorAdmin + "`IS_SUPERADMIN=" + (bool)ViewBag.IsSuperAdmin + "`IS_OPERATOR=" + (bool)ViewBag.IsOperator + "`IS_AUDITOR=" + (bool)ViewBag.IsAuditor + "`IS_HEAD_OFFICE=" + (bool)ViewBag.IsHeadOffice + "`CAN_APPROVE_EMPLOYEE_MASTER=" + (bool)ViewBag.CanApproveEmployeeMaster + "`CAN_APPROVE_EMPLOYEE_INCREMENT_MASTER=" + (bool)ViewBag.CanApproveEmployeeIncrementMaster + "`IS_SENIORADMIN_CHECKED=" + (bool)ViewBag.IsSeniorAdminChecked + "`IS_SUPERADMIN_CHECKED=" + (bool)ViewBag.IsSuperAdminChecked + "`IS_OPERATOR_CHECKED=" + (bool)ViewBag.IsOperatorChecked + "`IS_AUDITOR_CHECKED=" + (bool)ViewBag.IsAuditorChecked + "`IS_HEAD_OFFICE_CHECKED=" + (bool)ViewBag.IsHeadOfficeChecked + "`CAN_APPROVE_EMPLOYEE_MASTER_CHECKED=" + (bool)ViewBag.CanApproveEmployeeMasterChecked + "`CAN_APPROVE_EMPLOYEE_INCREMENT_MASTER_CHECKED=" + (bool)ViewBag.CanApproveEmployeeIncrementMasterChecked + "`INSTITUTE_CODE=" + ViewBag.instittuteCode + "`INSTITUTE_NAME=" + ViewBag.InstituteName + "`BRANCH_NAME=" + ViewBag.BranchName) });
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = MvcHtmlString.Create(SaraiBooking.App_Start.Common.RefineErrorMessage(ex.Message));
                return View();
            }
        }

        public ActionResult Edit(string Token)
        {
            UserMaster obj = new UserMaster();

            try
            {
                if (Token == null || Token.ToString().Trim().Length <= 0)
                    return RedirectToAction("Index");

                int id = SaraiBooking.App_Start.Common.GetDataFromEncryptedToken(Token, "ID", 0);

                if (id > 0)
                {

                    ViewBag.PageHeader = "Edit " + page;
                    ViewBag.Page = page;
                    ViewBag.AccordineHeader = page;

                    obj = facade.GetDetailById(id);
                }
                else
                {
                    ViewBag.PageHeader = "Create " + page;
                    ViewBag.Page = page;
                    ViewBag.AccordineHeader = page;

                    obj.PageSize = 10;
                    obj.DateFormat = "MMM dd, yyyy";
                }

                ViewBag.DateFormatList = UserDateFormatList(obj.DateFormat);
                ViewBag.PageNumber = SaraiBooking.App_Start.Common.GetDataFromEncryptedToken(Token, "PAGE_NUMBER", 0);
                ViewBag.MenuId = SaraiBooking.App_Start.Common.GetDataFromEncryptedToken(Token, "MENU_ID", 0);

                return View(obj);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = MvcHtmlString.Create(SaraiBooking.App_Start.Common.RefineErrorMessage(ex.Message));
                return View(obj);
            }
        }

        [HttpPost]
        public ActionResult Edit(UserMaster obj)
        {
            ViewBag.AccordineHeader = page;
            ViewBag.PageNumber = Request.Form["hdnPageNumber"];
            ViewBag.MenuId = Request.Form["hdnMenuId"];


            try
            {
                if (obj.Id > 0)
                {
                    ViewBag.PageHeader = "Edit " + page;
                }
                else
                {
                    ViewBag.PageHeader = "Create " + page;

                    SaraiBooking.App_Start.Common.ValidatePasswordPolicy(obj.Password);
                }

                facade.SaveData(obj);

                return RedirectToAction("Index", "UserMaster", new { area = "Common", Token = SaraiBooking.App_Start.Common.EncryptData("`PAGE_NUMBER=" + Request.Form["hdnPageNumber"] + "`MENU_ID=" + ViewBag.MenuId) });
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = MvcHtmlString.Create(SaraiBooking.App_Start.Common.RefineErrorMessage(ex.Message));
                return View(obj);
            }
        }

        new public ActionResult View(string Token)
        {
            UserMaster obj = new UserMaster();

            try
            {
                if (Token == null || Token.ToString().Trim().Length <= 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.PageHeader = "View " + page;
                    ViewBag.Page = page;
                    ViewBag.AccordineHeader = page;

                    ViewBag.PageNumber = SaraiBooking.App_Start.Common.GetDataFromEncryptedToken(Token, "PAGE_NUMBER", 0);
                    ViewBag.MenuId = SaraiBooking.App_Start.Common.GetDataFromEncryptedToken(Token, "MENU_ID", 0);

                    int id = SaraiBooking.App_Start.Common.GetDataFromEncryptedToken(Token, "ID", 0);
                    obj = facade.GetDetailById(id);

                    return View(obj);
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = MvcHtmlString.Create(SaraiBooking.App_Start.Common.RefineErrorMessage(ex.Message));
            }

            return View(obj);
        }

        public ActionResult ViewProfile()
        {
            UserMaster obj = new UserMaster();

            try
            {
                ViewBag.PageHeader = "Profile";
                ViewBag.AccordineHeader = page;

                obj = facade.GetDetailById(FrameWork.Core.Localizer.CurrentUser.Id);

                return View(obj);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = MvcHtmlString.Create(SaraiBooking.App_Start.Common.RefineErrorMessage(ex.Message));
            }

            return View(obj);
        }

        public ActionResult Settings(string Token)
        {
            UserMaster obj = new UserMaster();

            try
            {
                ViewBag.PageHeader = "Settings";
                ViewBag.AccordineHeader = page;

                ViewBag.PageHeader = "Edit " + page;
                ViewBag.Page = page;
                ViewBag.AccordineHeader = page;

                obj = facade.GetDetailById(FrameWork.Core.Localizer.CurrentUser.Id);

                ViewBag.DateFormatList = UserDateFormatList(obj.DateFormat);
                

                if (Token != null)
                {
                    ViewBag.ErrorMessage = SaraiBooking.App_Start.Common.GetRecordStatusMessage(SaraiBooking.App_Start.Common.GetDataFromEncryptedToken(Token, "RECORD_STATUS", ""));
                }

                return View(obj);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = MvcHtmlString.Create(SaraiBooking.App_Start.Common.RefineErrorMessage(ex.Message));
            }

            return View(obj);
        }

        [HttpPost]
        public ActionResult Settings(UserMaster obj)
        {
            try
            {
                ViewBag.DateFormatList = UserDateFormatList(obj.DateFormat);
              
                facade.UpdateSettings(obj);

                System.Web.HttpContext.Current.Session["WSM_USER_MASTER_SESSION"] = facade.GetDetailById(FrameWork.Core.Localizer.CurrentUser.Id);

                return RedirectToAction("Settings", "UserMaster", new { Token = SaraiBooking.App_Start.Common.EncryptData("`RECORD_STATUS=M") });
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
            }

            return View();
        }

        public ActionResult ResetPassword(string Token)
        {
            UserMaster obj = new UserMaster();

            try
            {
                if (Token == null || Token.ToString().Trim().Length <= 0)
                    return RedirectToAction("Index");

                ViewBag.PageHeader = "Reset Password";
                ViewBag.Page = page;
                ViewBag.AccordineHeader = page;

                int id = SaraiBooking.App_Start.Common.GetDataFromEncryptedToken(Token, "ID", 0);
                ViewBag.PageNumber = SaraiBooking.App_Start.Common.GetDataFromEncryptedToken(Token, "PAGE_NUMBER", 0);
                ViewBag.MenuId = SaraiBooking.App_Start.Common.GetDataFromEncryptedToken(Token, "MENU_ID", 0);

                //string recordStatus = SaraiBooking.App_Start.Common.GetDataFromEncryptedToken(Token, "RECORD_STATUS", "");

                ViewBag.ErrorMessage = SaraiBooking.App_Start.Common.GetRecordStatusMessage(SaraiBooking.App_Start.Common.GetDataFromEncryptedToken(Token, "RECORD_STATUS", ""));

                obj = facade.GetDetailById(id);
                
                return View(obj);

            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = MvcHtmlString.Create(SaraiBooking.App_Start.Common.RefineErrorMessage(ex.Message));
                return View(obj);
            }
        }

        [HttpPost]
        public ActionResult ResetPassword(UserMaster obj)
        {
            ViewBag.PageHeader = "Reset Password";
            ViewBag.AccordineHeader = page;
            ViewBag.PageNumber = Convert.ToInt16(Request.Form["hdnPageNumber"]);
            ViewBag.MenuId = Request.Form["hdnMenuId"];

            try
            {
                SaraiBooking.App_Start.Common.ValidatePasswordPolicy(Request.Form["txtNewPassword"].ToString());

                facade.UpdatePassword(obj, Request.Form["txtNewPassword"]);

                if (ViewBag.PageNumber != -1)
                    return RedirectToAction("Index", "UserMaster", new { Token = SaraiBooking.App_Start.Common.EncryptData("`PAGE_NUMBER=" + ViewBag.PageNumber + "`MENU_ID=" + ViewBag.MenuId) });
                else
                    return RedirectToAction("ResetPassword", "UserMaster", new { Token = SaraiBooking.App_Start.Common.EncryptData("`PAGE_NUMBER=" + ViewBag.PageNumber + "`MENU_ID=" + ViewBag.MenuId + "`RECORD_STATUS=M`ID=" + obj.Id) });
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = MvcHtmlString.Create(SaraiBooking.App_Start.Common.RefineErrorMessage(ex.Message));
                return View(obj);
            }
        }

        public ActionResult MenuSetting(string Token)
        {
            UserMaster obj = new UserMaster();

            try
            {
                if (Token == null || Token.ToString().Trim().Length <= 0)
                    return RedirectToAction("Index");

                ViewBag.PageHeader = "User Rights";
                ViewBag.Page = page;
                ViewBag.AccordineHeader = page;

                int id = SaraiBooking.App_Start.Common.GetDataFromEncryptedToken(Token, "ID", 0);
                int pageNumber = SaraiBooking.App_Start.Common.GetDataFromEncryptedToken(Token, "PAGE_NUMBER", 0);

                ViewBag.PageNumber = pageNumber;
                ViewBag.MenuId = SaraiBooking.App_Start.Common.GetDataFromEncryptedToken(Token, "MENU_ID", 0); ;

                obj = facade.GetDetailById(id);
                
                return View(obj);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = MvcHtmlString.Create(SaraiBooking.App_Start.Common.RefineErrorMessage(ex.Message));
                return View(obj);
            }
        }

        [HttpPost]
        public ActionResult MenuSetting(UserMaster obj)
        {
            ViewBag.PageHeader = "User Rights";
            ViewBag.AccordineHeader = page;
            ViewBag.PageNumber = Request.Form["hdnPageNumber"];
            ViewBag.MenuId = Request.Form["hdnMenuId"];

            try
            {
                UserMaster user = new UserMaster();
                user.Id = obj.Id;

                for (int i = 1; i<=  Convert.ToInt16(Request.Form["hdnMenuCount"].ToString()); i++)
                {
                    UserMenuMaster menuItem = new UserMenuMaster();
                    
                    menuItem.MenuId = Convert.ToInt16(Request.Form["hdnMenuId" + i.ToString()].ToString());
                    menuItem.CanAccess = (Request.Form["CanAccess" + i.ToString()]) == "on" ? true : false;
                    menuItem.CanCreate = (Request.Form["CanCreate" + i.ToString()] == "on" ? true : false);
                    menuItem.CanEdit = (Request.Form["CanEdit" + i.ToString()] == "on" ? true : false);
                    menuItem.CanChangeStatus = (Request.Form["CanChangeStatus" + i.ToString()] == "on" ? true : false);

                    user.LstChildMenu.Add(menuItem);
                }

                facade.UpdateUserRights(user);

                return RedirectToAction("Index", "UserMaster", new { Token = SaraiBooking.App_Start.Common.EncryptData("`PAGE_NUMBER=" + ViewBag.PageNUmber + "`MENU_ID=" + ViewBag.MenuId) });
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = MvcHtmlString.Create(SaraiBooking.App_Start.Common.RefineErrorMessage(ex.Message));
                return View(obj);
            }
        }

        public ActionResult ResetDefault()
        {
            try
            {
                facade.ResetDefaultSettings(FrameWork.Core.Localizer.CurrentUser.Id);

                System.Web.HttpContext.Current.Session["WSM_USER_MASTER_SESSION"] = facade.GetDetailById(FrameWork.Core.Localizer.CurrentUser.Id);
                
                return RedirectToAction("Settings", "UserMaster", new { Token = SaraiBooking.App_Start.Common.EncryptData("`RECORD_STATUS=M") });
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = MvcHtmlString.Create(SaraiBooking.App_Start.Common.RefineErrorMessage(ex.Message));
                return RedirectToAction("Settings", "UserMaster", new { errorMessage = ex.Message });
            }
        }

        public ActionResult RemoveAdditionalRights(string Token)
        {
            int id = SaraiBooking.App_Start.Common.GetDataFromEncryptedToken(Token, "ID", 0);
            int pageNumber = SaraiBooking.App_Start.Common.GetDataFromEncryptedToken(Token, "PAGE_NUMBER", 0);
            int menuId = SaraiBooking.App_Start.Common.GetDataFromEncryptedToken(Token, "MENU_ID", 0);

            try
            {
                if (Token == null || Token.ToString().Trim().Length <= 0)
                    return RedirectToAction("Index");

                ViewBag.PageNumber = pageNumber;
                ViewBag.MenuId = menuId;

                ViewBag.PageHeader = "Application Roles";
                ViewBag.Page = page;
                ViewBag.AccordineHeader = page;

                facade.DeleteUserRights(id);

                return RedirectToAction("Index", "UserMaster", new { area = "Common", Token = SaraiBooking.App_Start.Common.EncryptData("PAGE_NUMBER=" + ViewBag.PageNumber + "`MENU_ID=" + ViewBag.MenuId + "`RECORD_STATUS=M") });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "UserMaster", new { area = "Common", Token = SaraiBooking.App_Start.Common.EncryptData("PAGE_NUMBER=" + ViewBag.PageNumber + "`MENU_ID=" + ViewBag.MenuId), errorMessage = ex.Message });
            }
        }

   
        public ActionResult Password(string Token)
        {
            UserMaster obj = new UserMaster();

            try
            {
                if (Token == null || Token.ToString().Trim().Length <= 0)
                    return RedirectToAction("Index");

                ViewBag.PageHeader = "Reset Password";
                ViewBag.Page = page;
                ViewBag.AccordineHeader = page;

                int id = SaraiBooking.App_Start.Common.GetDataFromEncryptedToken(Token, "ID", 0);
                ViewBag.PageNumber = SaraiBooking.App_Start.Common.GetDataFromEncryptedToken(Token, "PAGE_NUMBER", 0);
                ViewBag.MenuId = SaraiBooking.App_Start.Common.GetDataFromEncryptedToken(Token, "MENU_ID", 0);

                ViewBag.ErrorMessage = SaraiBooking.App_Start.Common.GetRecordStatusMessage(SaraiBooking.App_Start.Common.GetDataFromEncryptedToken(Token, "RECORD_STATUS", ""));

                obj = facade.GetDetailById(id);

                return View(obj);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = MvcHtmlString.Create(SaraiBooking.App_Start.Common.RefineErrorMessage(ex.Message));
                return View(obj);
            }
        }

        [HttpPost]
        public ActionResult Password(UserMaster obj)
        {
            ViewBag.PageHeader = "Reset Password";
            ViewBag.AccordineHeader = page;
            ViewBag.PageNumber = Convert.ToInt16(Request.Form["hdnPageNumber"]);
            ViewBag.MenuId = Request.Form["hdnMenuId"];

            try
            {
                SaraiBooking.App_Start.Common.ValidatePasswordPolicy(Request.Form["txtNewPassword"].ToString());

                facade.ResetPassword(obj, Request.Form["txtNewPassword"]);

                return RedirectToAction("Index", "UserMaster", new { Token = SaraiBooking.App_Start.Common.EncryptData("`PAGE_NUMBER=" + ViewBag.PageNumber + "`MENU_ID=" + ViewBag.MenuId + "`RECORD_STATUS=M") });
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = MvcHtmlString.Create(SaraiBooking.App_Start.Common.RefineErrorMessage(ex.Message));
                return View(obj);
            }
        }

        public ActionResult FavoriteMenus(string Token, string errorMessage = "")
        {
            int id = SaraiBooking.App_Start.Common.GetDataFromEncryptedToken(Token, "ID", 0);

            try
            {
                if (Token == null || Token.ToString().Trim().Length <= 0)
                    return RedirectToAction("Index");

                ViewBag.PageHeader = "Favorite Menus";
                ViewBag.Page = page;
                ViewBag.AccordineHeader = page;

                ViewBag.ErrorMessage = (errorMessage.Trim() == "" ? MvcHtmlString.Create("") : MvcHtmlString.Create(SaraiBooking.App_Start.Common.RefineErrorMessage(errorMessage)));

                if (errorMessage.Trim() == "")
                    ViewBag.ErrorMessage = SaraiBooking.App_Start.Common.GetRecordStatusMessage(SaraiBooking.App_Start.Common.GetDataFromEncryptedToken(Token, "RECORD_STATUS", ""));

                UserMaster obj = facade.GetDetailById(id);

                return View(obj);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = MvcHtmlString.Create(SaraiBooking.App_Start.Common.RefineErrorMessage(ex.Message));
                return View();
            }
        }

        [HttpPost]
        public ActionResult FavoriteMenus(UserMaster obj)
        {
            try
            {
                ViewBag.PageHeader = "Favorite Menus";
                ViewBag.Page = page;
                ViewBag.AccordineHeader = page;

                facade.SaveUserFavoriteMenus(obj);

                return RedirectToAction("FavoriteMenus", "UserMaster", new { area = "Common", Token = SaraiBooking.App_Start.Common.EncryptData("`ID=" + obj.Id + "`RECORD_STATUS=M") });
            }
            catch (Exception ex)
            {
                return RedirectToAction("FavoriteMenus", "UserMaster", new { area = "Common", Token = SaraiBooking.App_Start.Common.EncryptData("`ID=" + obj.Id), errorMessage = ex.Message });
            }
        }

        public ActionResult SortFavoriteMenus(string Token, string errorMessage = "")
        {
            int userId = SaraiBooking.App_Start.Common.GetDataFromEncryptedToken(Token, "ID", 0);

            try
            {
                if (Token == null || Token.ToString().Trim().Length <= 0)
                    return RedirectToAction("Index");

                ViewBag.PageHeader = "Sort Favorite Menus";
                ViewBag.Page = page;
                ViewBag.AccordineHeader = page;

                ViewBag.ErrorMessage = (errorMessage.Trim() == "" ? MvcHtmlString.Create("") : MvcHtmlString.Create(SaraiBooking.App_Start.Common.RefineErrorMessage(errorMessage)));

                if (errorMessage.Trim() == "")
                    ViewBag.ErrorMessage = SaraiBooking.App_Start.Common.GetRecordStatusMessage(SaraiBooking.App_Start.Common.GetDataFromEncryptedToken(Token, "RECORD_STATUS", ""));

                List<UserFavoriteMenuMaster> lstData = facade.GetUserFavoriteMenus(userId);

                return View(lstData);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = MvcHtmlString.Create(SaraiBooking.App_Start.Common.RefineErrorMessage(ex.Message));
                return View();
            }
        }

        [HttpPost]
        public ActionResult SortFavoriteMenus(List<UserFavoriteMenuMaster> lstData)
        {
            try
            {
                ViewBag.PageHeader = "Sort Favorite Menus";
                ViewBag.Page = page;
                ViewBag.AccordineHeader = page;

                facade.SortUserFavoriteMenus(lstData);

                return RedirectToAction("SortFavoriteMenus", "UserMaster", new { area = "Common", Token = SaraiBooking.App_Start.Common.EncryptData("`ID=" + FrameWork.Core.Localizer.CurrentUser.Id + "`RECORD_STATUS=M") });
            }
            catch (Exception ex)
            {
                return RedirectToAction("SortFavoriteMenus", "UserMaster", new { area = "Common", Token = SaraiBooking.App_Start.Common.EncryptData("`ID=" + FrameWork.Core.Localizer.CurrentUser.Id), errorMessage = ex.Message });
            }
        }

        public ActionResult EncryptLoginData(string Token)
        {
            try
            {
                ViewBag.PageNumber = SaraiBooking.App_Start.Common.GetDataFromEncryptedToken(Token, "PAGE_NUMBER", 0);
                ViewBag.MenuId = SaraiBooking.App_Start.Common.GetDataFromEncryptedToken(Token, "MENU_ID", 0);

                facade.EncryptAllUsersLoginData();

                return RedirectToAction("Index", "UserMaster", new { area = "Common", Token = SaraiBooking.App_Start.Common.EncryptData("`PAGE_NUMBER=" + ViewBag.PageNumber + "`MENU_ID=" + ViewBag.MenuId) });
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = MvcHtmlString.Create(SaraiBooking.App_Start.Common.RefineErrorMessage(ex.Message));
                return RedirectToAction("Index", "UserMaster", new { area = "Common", Token = SaraiBooking.App_Start.Common.EncryptData("`PAGE_NUMBER=" + ViewBag.PageNumber + "`MENU_ID=" + ViewBag.MenuId), errorMessage = ex.Message });
            }
        }       
    }
}