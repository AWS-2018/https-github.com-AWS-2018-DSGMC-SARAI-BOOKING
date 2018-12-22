using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using BusinessObjects.Common;

using FrameWork.DataBase;
using FrameWork.Core;

using Dapper;

namespace DataLayer.Common
{
    public class UserMasterDao 
    {
      
        private DataTable ConvertUserRightsToDataTable(List<UserMenuMaster> LstData)
        {
            DataTable dataTable = Db.GetUserDefinedDataTable();

            foreach (UserMenuMaster data in LstData)
            {
                DataRow dataRow = dataTable.NewRow();

                dataRow["AMOUNT1"] = data.MenuId;
                dataRow["BIT1"] = data.CanAccess;
                dataRow["BIT2"] = data.CanCreate;
                dataRow["BIT3"] = data.CanEdit;
                dataRow["BIT4"] = data.CanChangeStatus;

                dataTable.Rows.Add(dataRow);
            }

            return dataTable;
        }

        /// <summary>
        /// Method to Save the Entry
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int SaveData(UserMaster obj)
        {
            int Id = 0;

            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@ID", obj.Id));
            parameters.Add(new SqlParameter("@NAME", obj.Name));
            parameters.Add(new SqlParameter("@LOGIN_PASSWORD", obj.Password));
            parameters.Add(new SqlParameter("@ENCRYPTED_LOGIN_PASSWORD", Utility.EncryptData(obj.Password)));
            parameters.Add(new SqlParameter("@EMAIL_ID", obj.EmailId));
            parameters.Add(new SqlParameter("@ENCRYPTED_EMAIL_ID", Utility.EncryptData(obj.EmailId)));
            parameters.Add(new SqlParameter("@MOBILE_NO", obj.MobileNo));
            parameters.Add(new SqlParameter("@ENCRYPTED_MOBILE_NO", Utility.EncryptData(obj.MobileNo)));
            parameters.Add(new SqlParameter("@PAGE_SIZE", obj.PageSize));
            parameters.Add(new SqlParameter("@DATE_FORMAT", obj.DateFormat));
            parameters.Add(new SqlParameter("@AMOUNT_FORMAT", obj.AmountFormat));
            parameters.Add(new SqlParameter("@IS_SENIORADMIN", obj.IsSeniorAdmin));
            parameters.Add(new SqlParameter("@IS_SUPERADMIN", obj.IsSuperAdmin));
            parameters.Add(new SqlParameter("@DEFAULT_FIN_YEAR_MASTER_ID", obj.DefaultFinYearMasterId));
            parameters.Add(new SqlParameter("@CAN_APPROVE_EMPLOYEE_MASTER", obj.CanApproveEmployeeMaster));
            parameters.Add(new SqlParameter("@CAN_APPROVE_EMPLOYEE_INCREMENT_MASTER", obj.CanApproveEmployeeIncrementMaster));
            parameters.Add(new SqlParameter("@CAN_APPROVE_HOMEWORK", obj.CanApproveHomeWork));
            parameters.Add(new SqlParameter("@IS_OPERATOR", obj.IsOperator));
            parameters.Add(new SqlParameter("@IS_AUDITOR", obj.IsAuditor));
            parameters.Add(new SqlParameter("@IS_HEAD_OFFICE", obj.IsHeadOffice));
            parameters.Add(new SqlParameter("@EXTRA_SECURITY_REQUIRED", obj.ExtraSecurityRequired));
            parameters.Add(new SqlParameter("@CAN_LOGIN_ON_SUNDAY", obj.CanLoginOnSunday));
            parameters.Add(new SqlParameter("@CAN_LOGIN_ON_HOLIDAY", obj.CanLoginOnHoliday));
            parameters.Add(new SqlParameter("@IS_TIME_BOUND", obj.IsTimeBound));
            parameters.Add(new SqlParameter("@FROM_TIME", obj.FromTime));
            parameters.Add(new SqlParameter("@TO_TIME", obj.ToTime));
            parameters.Add(new SqlParameter("@SAVE_SUCCESS_MESSAGE_REQUIRED_IN_POPUP", obj.SaveSuccessMessageRequiredInPopUp));
            parameters.Add(new SqlParameter("@ROW_VERSION", obj.RowVersion));
            parameters.Add(new SqlParameter("@IS_ACTIVE", obj.IsActive));
            parameters.Add(new SqlParameter("@REMARKS", obj.Remarks));
            parameters.Add(new SqlParameter("@CHECKSUM_VALUE", obj.CheckSumValue));
            parameters.Add(new SqlParameter("@SUPERADMIN_USERNAME", Localizer.CurrentUser.SuperAdminName));
            parameters.Add(new SqlParameter("@USER_MASTER_ID", Localizer.CurrentUser.Id));

            DataTable dt = Db.GetDataTable("SAVE_UPDATE_USER_MASTER", parameters);

            if (dt.Rows.Count > 0)
                Id = Convert.ToInt16(dt.Rows[0][0]);

            return Id;
        }

     
        /// <summary>
        /// Method to Update the Settings
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool UpdateSettings(UserMaster obj)
        {
            bool isDone = false;

            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@ID", obj.Id));
            parameters.Add(new SqlParameter("@NAME", obj.Name));
            parameters.Add(new SqlParameter("@EMAIL_ID", obj.EmailId));
            parameters.Add(new SqlParameter("@MOBILE_NO", obj.MobileNo));
            parameters.Add(new SqlParameter("@PAGE_SIZE", obj.PageSize));
            parameters.Add(new SqlParameter("@PAYROLL_ATTENDANCE_PAGE_SIZE", obj.PayrollAttendancePageSize));
            parameters.Add(new SqlParameter("@DATE_FORMAT", obj.DateFormat));
            parameters.Add(new SqlParameter("@AMOUNT_FORMAT", obj.AmountFormat));
            parameters.Add(new SqlParameter("@DEFAULT_EMPLOYEE_BLOOD_GROUP_MASTER_ID", obj.DefaultEmployeeBloodGroupMasterId));
            parameters.Add(new SqlParameter("@DEFAULT_EMPLOYEE_RELIGION_MASTER_ID", obj.DefaultEmployeeReligionMasterId));
            parameters.Add(new SqlParameter("@DEFAULT_EMPLOYEE_CASTE_MASTER_ID", obj.DefaultEmployeeCasteMasterId));
            parameters.Add(new SqlParameter("@ROW_VERSION", obj.RowVersion));
            parameters.Add(new SqlParameter("@IS_ACTIVE", obj.IsActive));
            parameters.Add(new SqlParameter("@REMARKS", obj.Remarks));
            parameters.Add(new SqlParameter("@CHECKSUM_VALUE", obj.CheckSumValue));
            parameters.Add(new SqlParameter("@SUPERADMIN_USERNAME", Localizer.CurrentUser.SuperAdminName));
            parameters.Add(new SqlParameter("@USER_MASTER_ID", Localizer.CurrentUser.Id));

            DataTable dt = Db.GetDataTable("UPDATE_USER_SETTINGS", parameters);

            if (dt.Rows.Count > 0)
                if (Convert.ToInt16(dt.Rows[0][0]) > 0)
                    isDone = true;

            return isDone;
        }

        /// <summary>
        /// Method to Update the Password
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="NewPassword"></param>
        /// <returns></returns>
        public bool UpdatePassword(UserMaster obj, string NewPassword)
        {
            bool isDone = false;

            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@ID", obj.Id));
            parameters.Add(new SqlParameter("@CURRENT_PASSWORD", obj.Password));
            parameters.Add(new SqlParameter("@ENCRYPTED_CURRENT_PASSWORD", Utility.EncryptData(obj.Password)));
            parameters.Add(new SqlParameter("@NEW_PASSWORD", NewPassword));
            parameters.Add(new SqlParameter("@ENCRYPTED_NEW_PASSWORD", Utility.EncryptData(NewPassword)));
            parameters.Add(new SqlParameter("@ROW_VERSION", obj.RowVersion));
            parameters.Add(new SqlParameter("@SUPERADMIN_USERNAME", Localizer.CurrentUser.SuperAdminName));
            parameters.Add(new SqlParameter("@USER_MASTER_ID", Localizer.CurrentUser.Id));

            DataTable dt = Db.GetDataTable("UPDATE_USER_PASSWORD", parameters);

            if (dt.Rows.Count > 0)
                if (Convert.ToInt16(dt.Rows[0][0]) > 0)
                    isDone = true;

            return isDone;
        }

        /// <summary>
        /// Method to Update the Password
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="NewPassword"></param>
        /// <returns></returns>
        public bool ResetPassword(UserMaster obj, string NewPassword)
        {
            bool isDone = false;

            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@ID", obj.Id));
            parameters.Add(new SqlParameter("@NEW_PASSWORD", NewPassword));
            parameters.Add(new SqlParameter("@ENCRYPTED_NEW_PASSWORD", Utility.EncryptData(NewPassword)));
            parameters.Add(new SqlParameter("@ROW_VERSION", obj.RowVersion));
            parameters.Add(new SqlParameter("@SUPERADMIN_USERNAME", Localizer.CurrentUser.SuperAdminName));
            parameters.Add(new SqlParameter("@USER_MASTER_ID", Localizer.CurrentUser.Id));

            DataTable dt = Db.GetDataTable("CHANGE_USER_PASSWORD", parameters);

            if (dt.Rows.Count > 0)
                if (Convert.ToInt16(dt.Rows[0][0]) > 0)
                    isDone = true;

            return isDone;
        }

        /// <summary>
        /// Method to get the List of entries
        /// </summary>
        /// <param name="sectionName"></param>
        /// <returns></returns>
        public List<UserMaster> GetList(string name)
        {
            List<UserMaster> lstData = new List<UserMaster>();

            string strQuery = "";
            strQuery = strQuery + System.Environment.NewLine + "SELECT		M.ID						AS Id,";
            strQuery = strQuery + System.Environment.NewLine + "			M.NAME					    AS Name,";
            strQuery = strQuery + System.Environment.NewLine + "			M.EMAIL_ID					AS EmailId,";
            strQuery = strQuery + System.Environment.NewLine + "			M.MOBILE_NO					AS MobileNo,";
            strQuery = strQuery + System.Environment.NewLine + "			M.ENCRYPTED_LOGIN_PASSWORD	AS Password,";
            strQuery = strQuery + System.Environment.NewLine + "			M.USER_IMAGE				AS UserImage,";
            strQuery = strQuery + System.Environment.NewLine + "			M.LAST_LOGIN_DATE_TIME		AS LastLogInDateTime,";
            strQuery = strQuery + System.Environment.NewLine + "			M.IS_ACTIVE				    AS IsActive,";
            strQuery = strQuery + System.Environment.NewLine + "			M.REMARKS					AS Remarks,";
            strQuery = strQuery + System.Environment.NewLine + "			M.IS_SUPERADMIN				AS IsSuperAdmin,";
            strQuery = strQuery + System.Environment.NewLine + "			M.IS_SENIORADMIN			AS IsSeniorAdmin,";
            strQuery = strQuery + System.Environment.NewLine + "			M.IS_OPERATOR			    AS IsOperator,";
            strQuery = strQuery + System.Environment.NewLine + "			M.IS_AUDITOR			    AS IsAuditor,";
            strQuery = strQuery + System.Environment.NewLine + "			M.IS_HEAD_OFFICE			AS IsHeadOffice,";
            strQuery = strQuery + System.Environment.NewLine + "			M.ROW_VERSION				AS [RowVersion],";
            strQuery = strQuery + System.Environment.NewLine + "			M.CHECKSUM_VALUE			AS CheckSumValue,";
            strQuery = strQuery + System.Environment.NewLine + "			M.[GUID]					AS [GUID],";
            strQuery = strQuery + System.Environment.NewLine + "			M.CREATED_BY				AS CreatedByUserId,";
            strQuery = strQuery + System.Environment.NewLine + "			ISNULL(C_UM.NAME, '')		AS CreatedByUserName,";
            strQuery = strQuery + System.Environment.NewLine + "			M.CREATE_DATE				AS CreateDate,";
            strQuery = strQuery + System.Environment.NewLine + "			ISNULL(M.MODIFIED_BY, 0)	AS ModifiedByUserId,";
            strQuery = strQuery + System.Environment.NewLine + "			M.MODIFY_DATE				AS ModifyDate,";
            strQuery = strQuery + System.Environment.NewLine + "			ISNULL(M_UM.NAME, '')		AS ModifiedByUserName";
            strQuery = strQuery + System.Environment.NewLine + "FROM		USER_MASTER M";
            strQuery = strQuery + System.Environment.NewLine + "LEFT JOIN 	USER_MASTER C_UM";
            strQuery = strQuery + System.Environment.NewLine + "ON			M.CREATED_BY = C_UM.ID";
            strQuery = strQuery + System.Environment.NewLine + "LEFT JOIN	USER_MASTER M_UM";
            strQuery = strQuery + System.Environment.NewLine + "ON			M.MODIFIED_BY = M_UM.ID";
            strQuery = strQuery + System.Environment.NewLine + "WHERE		M.NAME LIKE '%" + name + "%'";
            strQuery = strQuery + System.Environment.NewLine + "ORDER BY	M.NAME";

            DataTable dataTable = new DataTable();
            dataTable = Db.GetDataTableFromQuery(strQuery);

            lstData = ObjectMapper<UserMaster>.MapDataToListObject(dataTable);

            return lstData;
        }

        /// <summary>
        /// Method to get the List of entries for List Page
        /// </summary>
        /// <param name="page">Page Number</param>
        /// <param name="pageSize">Size of the Page (No. of Records)</param>
        /// <returns></returns>
        public List<UserMaster> GetList(int page, int pageSize, string searchCriteria)
        {
            List<UserMaster> lstData = new List<UserMaster>();

            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@PAGE_NUMBER", page));
            parameters.Add(new SqlParameter("@PAGE_SIZE", pageSize));
            parameters.Add(new SqlParameter("@SEARCH_CRITERIA", searchCriteria));

            DataTable dt = Db.GetDataTable("GET_USER_MASTER_LIST", parameters);

            lstData = ObjectMapper<UserMaster>.MapDataToListObject(dt);

            return lstData;
        }

      

    

        /// <summary>
        /// Method to Get the Particular Record from Database
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public UserMaster GetSingleRecordDetail(int Id)
        {
            UserMaster obj = new UserMaster();

            SqlConnection con = new SqlConnection(Db.GetConnectionString());

            DynamicParameters param = new DynamicParameters();
            param.Add("@ID", Id, DbType.Int32);

            using (var grid = con.QueryMultiple("GET_USER_MASTER_DETAIL_BY_ID", param, null, null, CommandType.StoredProcedure))
            {
                obj = grid.Read<UserMaster>().First();
                //obj.LstParentMenu = grid.Read<UserMenuMaster>().ToList();
                //obj.LstChildMenu = grid.Read<UserMenuMaster>().ToList();
                //obj.FavoriteMenus = grid.Read<UserFavoriteMenuMaster>().ToList();
            }

            return obj;
        }

        /// <summary>
        /// Method to Validate the User Credentials
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="IP Address"></param>
        /// <param name="userType">U -> User, T -> Employee, P -> Parent</param>
        /// <returns></returns>
        public int ValidateUserCredentials(UserMaster obj, string IPAddress, string userType)
        {
            int Id = 0;

            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@USER_NAME", Utility.DecryptData(obj.EmailId)));
            parameters.Add(new SqlParameter("@LOGIN_PASSWORD", Utility.DecryptData(obj.Password)));
            parameters.Add(new SqlParameter("@ENCRYPTED_USER_NAME", obj.EmailId));
            parameters.Add(new SqlParameter("@ENCRYPTED_LOGIN_PASSWORD", obj.Password));
            parameters.Add(new SqlParameter("@IP_ADDRESS", IPAddress));
            parameters.Add(new SqlParameter("@MAX_ATTEMPTS", Localizer.MaxLoginAttempts));
            parameters.Add(new SqlParameter("@USER_TYPE", userType));

            DataTable dt = Db.GetDataTable("VALIDATE_USER_CREDENTIALS", parameters);

            if (dt.Rows.Count > 0)
                Id = Convert.ToInt16(dt.Rows[0][0]);

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


            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@ID", obj.Id));
            parameters.Add(new SqlParameter("@USER_RIGHTS", ConvertUserRightsToDataTable(obj.LstChildMenu)));
            parameters.Add(new SqlParameter("@SUPERADMIN_USERNAME", Localizer.CurrentUser.SuperAdminName));
            parameters.Add(new SqlParameter("@USER_MASTER_ID", Localizer.CurrentUser.Id));

            DataTable dt = Db.GetDataTable("UPDATE_USER_RIGHTS", parameters);

            isDone = true;

            return isDone;
        }

        /// <summary>
        /// Method to Update the User Rights
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool DeleteUserRights(int userId)
        {
            string strQuery = "";
            strQuery = strQuery + System.Environment.NewLine + "UPDATE      OVERRIDE_USER_MENU_RIGHTS";
            strQuery = strQuery + System.Environment.NewLine + "SET         IS_ACTIVE = 0";
            strQuery = strQuery + System.Environment.NewLine + "WHERE       USER_MASTER_ID = " + userId;

            Db.ExecuteQuery(strQuery);

            return true;
        }



        /// <summary>
        /// Method to Restore the Default Settings of a User
        /// </summary>
        /// <returns></returns>
        public void ResetDefaultSettings(int Id)
        {
            string strQuery = "";

            strQuery = strQuery + System.Environment.NewLine + "UPDATE      USER_MASTER";
            strQuery = strQuery + System.Environment.NewLine + "SET         PAGE_SIZE = 10,";
            strQuery = strQuery + System.Environment.NewLine + "            DATE_FORMAT = 'MMM dd, yyyy',";
            strQuery = strQuery + System.Environment.NewLine + "            AMOUNT_FORMAT = '0.00',";
            strQuery = strQuery + System.Environment.NewLine + "            ROW_VERSION = ROW_VERSION + 1,";
            strQuery = strQuery + System.Environment.NewLine + "            HAS_SYNCED = 0,";
            strQuery = strQuery + System.Environment.NewLine + "            SUPERADMIN_USERNAME = '" + Localizer.CurrentUser.SuperAdminName + "',";
            strQuery = strQuery + System.Environment.NewLine + "            MODIFIED_BY = " + Localizer.CurrentUser.Id + ",";
            strQuery = strQuery + System.Environment.NewLine + "            MODIFY_DATE = GETDATE()";
            strQuery = strQuery + System.Environment.NewLine + "WHERE       ID = " + Id;

            Db.ExecuteQuery(strQuery);
        }

        /// <summary>
        /// Method to Save the Login Session History
        /// </summary>
        /// <returns></returns>
        public int SaveLoginSessionHistory()
        {
            int Id = 0;

            List<SqlParameter> parameters = new List<SqlParameter>();

            UserMaster user = Localizer.CurrentUser;

            parameters.Add(new SqlParameter("@ID", user.Id));
            parameters.Add(new SqlParameter("@IP_ADDRESS", user.IPAddress));
            parameters.Add(new SqlParameter("@BROWSER_INFORMATION", user.BrowserInformation));
            parameters.Add(new SqlParameter("@SESSION_ID", Localizer.SessionId));

            DataTable dt = Db.GetDataTable("SAVE_LOGIN_HISTORY", parameters);

            if (dt.Rows.Count > 0)
                Id = Convert.ToInt32(dt.Rows[0][0]);

            return Id;
        }

        /// <summary>
        /// Method to Save the Logout History
        /// </summary>
        /// <returns></returns>
        public bool SaveLogoutSessionHistory()
        {
            bool isDone = false;

            UserMaster user = Localizer.CurrentUser;

            if (user != null)
            {
                string strQuery = "";
                strQuery = strQuery + System.Environment.NewLine + "UPDATE      USER_LOGIN_HISTORY";
                strQuery = strQuery + System.Environment.NewLine + "SET         LOGOUT_DATE = GETDATE(),";
                strQuery = strQuery + System.Environment.NewLine + "            IS_ACTIVE = 0";
                strQuery = strQuery + System.Environment.NewLine + "WHERE       USER_MASTER_ID = " + user.Id;
                strQuery = strQuery + System.Environment.NewLine + "AND         IP_ADDRESS = '" + user.IPAddress + "'";
                strQuery = strQuery + System.Environment.NewLine + "AND         SESSION_ID = '" + Localizer.SessionId + "'";
                strQuery = strQuery + System.Environment.NewLine + "AND         ID = " + user.LoginHistoryId;

                if (Db.ExecuteQuery(strQuery) == 1)
                    isDone = true;
            }
            else
            {
                isDone = true;
            }

            return isDone;
        }

        /// <summary>
        /// Method to check whether is valid Password Reset Token
        /// </summary>
        /// <param name="UserMasterObject"></param>
        /// <returns></returns>
        public bool IsValidPasswordResetToken(string Token)
        {
            string strQuery = "";

            strQuery = strQuery + System.Environment.NewLine + "SELECT        1";
            strQuery = strQuery + System.Environment.NewLine + "FROM          USER_MASTER";
            strQuery = strQuery + System.Environment.NewLine + "WHERE         RESET_PASSWORD_TOKEN = '" + Token + "'";

            DataTable dt = new DataTable();
            dt = Db.GetDataTableFromQuery(strQuery);

            if (dt.Rows.Count <= 0)
                return false;
            else
                return true;

        }


        /// <summary>
        /// Method to reset the Password
        /// </summary>
        /// <param name="UserMasterObject"></param>
        /// <returns></returns>
        public UserMaster UpdatePassword(UserMaster objUserMaster)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@USER_MASTER_ID", objUserMaster.Id));
            parameters.Add(new SqlParameter("@NEW_PASSWORD", objUserMaster.NewPassword));
            parameters.Add(new SqlParameter("@ENCRYPTED_NEW_PASSWORD", Utility.EncryptData(objUserMaster.NewPassword)));
            parameters.Add(new SqlParameter("@PASSWORD_RESET_TOKEN", objUserMaster.PasswordResetToken));

            DataTable dt = Db.GetDataTable("RESET_USER_PASSWORD", parameters);

            objUserMaster = ObjectMapper<UserMaster>.MapDataToObject(dt);

            return objUserMaster;
        }



        /// <summary>
        /// Method to reset the Password
        /// </summary>
        /// <param name="UserMasterObject"></param>
        /// <param name="passwordResetToken"></param>
        /// <returns></returns>
        public UserMaster SendPasswordResetLink(UserMaster obj)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@EMAIL_ID", obj.EmailId));
            parameters.Add(new SqlParameter("@PASSWORD_RESET_TOKEN", obj.PasswordResetToken));

            DataTable dt = Db.GetDataTable("SAVE_PASSWORD_RESET_LINK", parameters);

            obj = ObjectMapper<UserMaster>.MapDataToObject(dt);

            return obj;
        }


        /// <summary>
        /// Method to Get the default Email settings
        /// </summary>
        /// <returns></returns>
        public UserMaster GetDefaultEmailSettings(int UserMasterId, string EmailId, string MobileNo)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@USER_MASTER_ID", UserMasterId));
            parameters.Add(new SqlParameter("@MOBILE_NUMBER", MobileNo));
            parameters.Add(new SqlParameter("@EMAIL_ID", EmailId));

            DataTable dt = Db.GetDataTable("GET_EMAIL_SETTINGS", parameters);

            UserMaster obj = new UserMaster();

            obj = ObjectMapper<UserMaster>.MapDataToObject(dt);

            return obj;
        }

        /// <summary>
        /// Method to Get the default Email settings
        /// </summary>
        /// <returns></returns>
        public List<TempTableObject> GetUserLoginHistory(int userId)
        {
            string strQuery = "";

            strQuery = strQuery + System.Environment.NewLine + "SELECT		IP_ADDRESS		AS DESC1,";
            strQuery = strQuery + System.Environment.NewLine + "			LOGIN_DATE		AS DATE1,";
            strQuery = strQuery + System.Environment.NewLine + "			LOGOUT_DATE		AS DATE2";
            strQuery = strQuery + System.Environment.NewLine + "FROM		USER_LOGIN_HISTORY";
            strQuery = strQuery + System.Environment.NewLine + "WHERE		USER_MASTER_ID = " + userId;
            strQuery = strQuery + System.Environment.NewLine + "ORDER BY	LOGIN_DATE";

            List<TempTableObject> lstData = Db.DapperGetList<TempTableObject>(strQuery);

            return lstData;
        }

        //#
        // <summary>
        /// Method to Save the Entry
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int SaveUserFavoriteMenus(UserMaster obj)
        {
            int Id = 0;

            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@ID", obj.Id));
            parameters.Add(new SqlParameter("@FAVORITE_MENUS", ConvertFavoriteMenusToDataTable(obj.FavoriteMenus)));

            DataTable dt = Db.GetDataTable("SAVE_UPDATE_USER_FAVORITE_MENUS", parameters);

            if (dt.Rows.Count > 0)
                Id = Convert.ToInt32(dt.Rows[0][0]);

            return Id;
        }

        //#
        private DataTable ConvertFavoriteMenusToDataTable(List<UserFavoriteMenuMaster> LstData)
        {
            DataTable dataTable = Db.GetUserDefinedDataTable();

            foreach (UserFavoriteMenuMaster data in LstData)
            {
                DataRow dataRow = dataTable.NewRow();

                dataRow["AMOUNT1"] = data.Id;
                dataRow["AMOUNT2"] = data.MenuId;
                dataRow["AMOUNT3"] = data.UserMasterId;
                dataRow["AMOUNT4"] = data.SortId;
                dataRow["BIT1"] = data.IsSelected;

                dataTable.Rows.Add(dataRow);
            }

            return dataTable;
        }

        /// <summary>
        /// Method to get the List of entries
        /// </summary>
        /// <param name="sectionName"></param>
        /// <returns></returns>
        public List<UserFavoriteMenuMaster> GetUserFavoriteMenus(int userId)
        {
            List<UserFavoriteMenuMaster> lstData = new List<UserFavoriteMenuMaster>();

            string strQuery = "";
            strQuery = strQuery + System.Environment.NewLine + "SELECT		M.ID						AS Id,";
            strQuery = strQuery + System.Environment.NewLine + "			M.USER_MASTER_ID			AS UserMasterId,";
            strQuery = strQuery + System.Environment.NewLine + "			M.MENU_MASTER_ID			AS MenuId,";
            strQuery = strQuery + System.Environment.NewLine + "			M.SORTID					AS SortId,";
            strQuery = strQuery + System.Environment.NewLine + "			M.IS_ACTIVE			        AS IsSelected,";
            strQuery = strQuery + System.Environment.NewLine + "			MM.NAME						AS MenuName,";
            strQuery = strQuery + System.Environment.NewLine + "			MM.MODULE_MASTER_ID			AS ModuleId,";
            strQuery = strQuery + System.Environment.NewLine + "			MDM.NAME					AS ModuleName,";
            strQuery = strQuery + System.Environment.NewLine + "			MM.PARENT_MENU_MASTER_ID	AS ParentMenuId,";
            strQuery = strQuery + System.Environment.NewLine + "			PMM.NAME					AS ParentMenuName";
            strQuery = strQuery + System.Environment.NewLine + "FROM		USER_FAVORITE_MENU_MASTER M";
            strQuery = strQuery + System.Environment.NewLine + "INNER JOIN 	MENU_MASTER MM";
            strQuery = strQuery + System.Environment.NewLine + "ON			M.MENU_MASTER_ID = MM.ID";
            strQuery = strQuery + System.Environment.NewLine + "INNER JOIN 	USER_APPLICATION_ROLE_MASTER UARM";
            strQuery = strQuery + System.Environment.NewLine + "ON			M.USER_MASTER_ID = UARM.USER_MASTER_ID";
            strQuery = strQuery + System.Environment.NewLine + "INNER JOIN 	APPLICATION_ROLE_MENU_MASTER ARMM";
            strQuery = strQuery + System.Environment.NewLine + "ON			UARM.APPLICATION_ROLE_MASTER_ID = ARMM.APPLICATION_ROLE_MASTER_ID";
            strQuery = strQuery + System.Environment.NewLine + "AND			ARMM.MENU_MASTER_ID = MM.ID";
            strQuery = strQuery + System.Environment.NewLine + "INNER JOIN	MODULE_MASTER MDM";
            strQuery = strQuery + System.Environment.NewLine + "ON			MM.MODULE_MASTER_ID = MDM.ID";
            strQuery = strQuery + System.Environment.NewLine + "INNER JOIN	PARENT_MENU_MASTER PMM";
            strQuery = strQuery + System.Environment.NewLine + "ON			MM.PARENT_MENU_MASTER_ID = PMM.ID";
            strQuery = strQuery + System.Environment.NewLine + "WHERE		M.USER_MASTER_ID = " + userId;
            strQuery = strQuery + System.Environment.NewLine + "AND         M.IS_ACTIVE = 1";
            strQuery = strQuery + System.Environment.NewLine + "ORDER BY	M.SORTID";

            DataTable dataTable = new DataTable();
            dataTable = Db.GetDataTableFromQuery(strQuery);

            lstData = ObjectMapper<UserFavoriteMenuMaster>.MapDataToListObject(dataTable);

            return lstData;
        }

        /// <summary>
        /// Method to Update Fav. Menu entry for user
        /// </summary>
        /// <param name="MenuId"></param>
        /// <param name="isFavoriteMenu">Flag to check if menu has been added or removed from fav. menu</param>
        /// <returns></returns>
        public bool UpdateUserFavoriteMenu(int menuId, bool isFavoriteMenu)
        {
            bool isDone = false;

            string strQuery = "";
            strQuery = strQuery + System.Environment.NewLine + "IF EXISTS (SELECT 1 FROM USER_FAVORITE_MENU_MASTER WHERE MENU_MASTER_ID = " + menuId + " AND USER_MASTER_ID = " + Localizer.CurrentUser.Id + ")";
            strQuery = strQuery + System.Environment.NewLine + "BEGIN";
            strQuery = strQuery + System.Environment.NewLine + "    UPDATE      USER_FAVORITE_MENU_MASTER";
            strQuery = strQuery + System.Environment.NewLine + "    SET         IS_ACTIVE = '" + isFavoriteMenu + "'";
            strQuery = strQuery + System.Environment.NewLine + "    WHERE       MENU_MASTER_ID = " + menuId;
            strQuery = strQuery + System.Environment.NewLine + "    AND         USER_MASTER_ID = " + Localizer.CurrentUser.Id;
            strQuery = strQuery + System.Environment.NewLine + "END";
            strQuery = strQuery + System.Environment.NewLine + "ELSE";
            strQuery = strQuery + System.Environment.NewLine + "BEGIN";
            strQuery = strQuery + System.Environment.NewLine + "    INSERT INTO USER_FAVORITE_MENU_MASTER   (   MENU_MASTER_ID,";
            strQuery = strQuery + System.Environment.NewLine + "                                                USER_MASTER_ID,";
            strQuery = strQuery + System.Environment.NewLine + "                                                IS_ACTIVE";
            strQuery = strQuery + System.Environment.NewLine + "                                            )";
            strQuery = strQuery + System.Environment.NewLine + "                                    VALUES  (   " + menuId + ",";
            strQuery = strQuery + System.Environment.NewLine + "                                                " + Localizer.CurrentUser.Id + ",";
            strQuery = strQuery + System.Environment.NewLine + "                                               '" + isFavoriteMenu + "'";
            strQuery = strQuery + System.Environment.NewLine + "                                            )";
            strQuery = strQuery + System.Environment.NewLine + "END";

            Db.ExecuteQuery(strQuery);

            isDone = true;

            return isDone;
        }

        /// <summary>
        /// Method to Update Fav. Menu entry for user
        /// </summary>
        /// <param name="lstData">List of Fav. Menu Items to be Sorted
        /// <returns></returns>
        public bool SortUserFavoriteMenus(List<UserFavoriteMenuMaster> lstData)
        {
            bool isDone = false;

            string strQuery = "";

            foreach (UserFavoriteMenuMaster item in lstData)
            {
                strQuery = strQuery + System.Environment.NewLine + "UPDATE      USER_FAVORITE_MENU_MASTER";
                strQuery = strQuery + System.Environment.NewLine + "SET         SORTID = " + item.SortId;
                strQuery = strQuery + System.Environment.NewLine + "WHERE       MENU_MASTER_ID = " + item.MenuId;
                strQuery = strQuery + System.Environment.NewLine + "AND         USER_MASTER_ID = " + Localizer.CurrentUser.Id + ";";
                strQuery = strQuery + System.Environment.NewLine + "";
            }

            Db.ExecuteQuery(strQuery);

            isDone = true;

            return isDone;
        }

  
        /// <summary>
        /// Method to Update Encrypted UserName / Mobile No. / Password
        /// </summary>
        /// <returns></returns>
        public bool EncryptAllUsersLoginData()
        {
            bool isDone = false;

            string strQuery = "";
            strQuery = strQuery + System.Environment.NewLine + "SELECT      ID,";
            strQuery = strQuery + System.Environment.NewLine + "            EMAIL_ID,";
            strQuery = strQuery + System.Environment.NewLine + "            MOBILE_NO,";
            strQuery = strQuery + System.Environment.NewLine + "            LOGIN_PASSWORD";
            strQuery = strQuery + System.Environment.NewLine + "FROM        USER_MASTER";

            DataTable dt = Db.GetDataTableFromQuery(strQuery);

            strQuery = "";

            if (dt.Rows.Count > 0)
                for (int i = 0; i < dt.Rows.Count; i++)
                    strQuery = strQuery + System.Environment.NewLine + "UPDATE USER_MASTER SET ENCRYPTED_EMAIL_ID = '" + Utility.EncryptData(dt.Rows[i]["EMAIL_ID"].ToString()) + "', ENCRYPTED_MOBILE_NO = '" + Utility.EncryptData(dt.Rows[i]["MOBILE_NO"].ToString()) + "', ENCRYPTED_LOGIN_PASSWORD = '" + Utility.EncryptData(dt.Rows[i]["LOGIN_PASSWORD"].ToString()) + "' WHERE ID = " + dt.Rows[i]["ID"].ToString() + ";";

            Db.ExecuteQuery(strQuery);


            isDone = true;

            return isDone;
        }

      
    }
}
