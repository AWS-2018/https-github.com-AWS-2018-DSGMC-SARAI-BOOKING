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


namespace DataLayer.Common
{
    public class HolidayMasterDao
    {
        /// <summary>
        /// Method to Save the Entry
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int SaveData(Holiday obj)
        {
            int Id = 0;

            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@ID", obj.Id));
            parameters.Add(new SqlParameter("@NAME", obj.Name));
            parameters.Add(new SqlParameter("@FROM_DATE", obj.FromDate));
            parameters.Add(new SqlParameter("@TO_DATE", obj.ToDate));
            parameters.Add(new SqlParameter("@IS_FOR_STUDENT", obj.IsForStudent));
            parameters.Add(new SqlParameter("@IS_FOR_TEACHER", obj.IsForTeacher));
            parameters.Add(new SqlParameter("@REMARKS", obj.Remarks));
            parameters.Add(new SqlParameter("@IS_ACTIVE", obj.IsActive));
            parameters.Add(new SqlParameter("@ROW_VERSION", obj.RowVersion));
            parameters.Add(new SqlParameter("@CHECKSUM_VALUE", obj.CheckSumValue));
            parameters.Add(new SqlParameter("@SUPERADMIN_USERNAME", Localizer.CurrentUser.SuperAdminName));
            parameters.Add(new SqlParameter("@USER_MASTER_ID", Localizer.CurrentUser.Id));

            DataTable dt = Db.GetDataTable("SAVE_UPDATE_HOLIDAY_MASTER", parameters);

            if (dt.Rows.Count > 0)
                Id = Convert.ToInt32(dt.Rows[0][0]);

            return Id;
        }

        /// <summary>
        /// Method to get the List of entries
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<Holiday> GetList(string name)
        {
            List<Holiday> lstData = new List<Holiday>();

            string strQuery = "";
            strQuery = strQuery + System.Environment.NewLine + "SELECT		M.ID						AS Id,";
            strQuery = strQuery + System.Environment.NewLine + "			M.NAME					    AS Name,";
            strQuery = strQuery + System.Environment.NewLine + "			M.FROM_DATE					AS FromDate,";
            strQuery = strQuery + System.Environment.NewLine + "			M.TO_DATE					AS ToDate,";
            strQuery = strQuery + System.Environment.NewLine + "			M.IS_FOR_STUDENT			AS IsForStudent,";
            strQuery = strQuery + System.Environment.NewLine + "			M.IS_FOR_TEACHER			AS IsForTeacher,";
            strQuery = strQuery + System.Environment.NewLine + "			M.INSTITUTE_MASTER_ID		AS InstituteMasterId,";
            strQuery = strQuery + System.Environment.NewLine + "			IM.INSTITUTE_NAME			AS InstituteName,";
            strQuery = strQuery + System.Environment.NewLine + "			M.REMARKS					AS Remarks,";
            strQuery = strQuery + System.Environment.NewLine + "			M.IS_ACTIVE				    AS IsActive,";
            strQuery = strQuery + System.Environment.NewLine + "			M.ROW_VERSION				AS [RowVersion],";
            strQuery = strQuery + System.Environment.NewLine + "			M.CHECKSUM_VALUE			AS CheckSumValue,";
            strQuery = strQuery + System.Environment.NewLine + "			M.[GUID]					AS [GUID],";
            strQuery = strQuery + System.Environment.NewLine + "			M.CREATED_BY				AS CreatedByUserId,";
            strQuery = strQuery + System.Environment.NewLine + "			ISNULL(C_UM.NAME, '')		AS CreatedByUserName,";
            strQuery = strQuery + System.Environment.NewLine + "			M.CREATE_DATE				AS CreateDate,";
            strQuery = strQuery + System.Environment.NewLine + "			ISNULL(M.MODIFIED_BY, 0)	AS ModifiedByUserId,";
            strQuery = strQuery + System.Environment.NewLine + "			M.MODIFY_DATE				AS ModifyDate,";
            strQuery = strQuery + System.Environment.NewLine + "			ISNULL(M_UM.NAME, '')		AS ModifiedByUserName";
            strQuery = strQuery + System.Environment.NewLine + "FROM		HOLIDAY_MASTER M";
            strQuery = strQuery + System.Environment.NewLine + "INNER JOIN 	INSTITUTE_MASTER IM";
            strQuery = strQuery + System.Environment.NewLine + "ON			M.INSTITUTE_MASTER_ID = IM.ID";
            strQuery = strQuery + System.Environment.NewLine + "INNER JOIN 	USER_MASTER C_UM";
            strQuery = strQuery + System.Environment.NewLine + "ON			M.CREATED_BY = C_UM.ID";
            strQuery = strQuery + System.Environment.NewLine + "LEFT JOIN	USER_MASTER M_UM";
            strQuery = strQuery + System.Environment.NewLine + "ON			M.MODIFIED_BY = M_UM.ID";
            strQuery = strQuery + System.Environment.NewLine + "WHERE		M.NAME LIKE @name";
            strQuery = strQuery + System.Environment.NewLine + "ORDER BY	M.FROM_DATE";

            lstData = Db.DapperGetList<Holiday>(strQuery, new { name = "%" + name + "%" });

            return lstData;
        }

        /// <summary>
        /// Method to get the List of entries for List Page
        /// </summary>
        /// <param name="page">Page Number</param>
        /// <param name="pageSize">Size of the Page (No. of Records)</param>
        /// <returns></returns>
        public List<Holiday> GetList(int page, int pageSize)
        {
            List<Holiday> lstData = new List<Holiday>();

            string strQuery = "";

            strQuery = strQuery + System.Environment.NewLine + "SELECT	    *";
            strQuery = strQuery + System.Environment.NewLine + "FROM		(	SELECT      ROW_NUMBER() OVER(ORDER BY M.NAME) AS SerialNumber,";
            strQuery = strQuery + System.Environment.NewLine + "                            M.ID						AS Id,";
            strQuery = strQuery + System.Environment.NewLine + "			                M.NAME					    AS Name,";
            strQuery = strQuery + System.Environment.NewLine + "                			M.FROM_DATE					AS FromDate,";
            strQuery = strQuery + System.Environment.NewLine + "                			M.TO_DATE					AS ToDate,";
            strQuery = strQuery + System.Environment.NewLine + "			                M.IS_FOR_STUDENT			AS IsForStudent,";
            strQuery = strQuery + System.Environment.NewLine + "			                M.IS_FOR_TEACHER			AS IsForTeacher,";
            strQuery = strQuery + System.Environment.NewLine + "                			M.INSTITUTE_MASTER_ID		AS InstituteMasterId,";
            strQuery = strQuery + System.Environment.NewLine + "                			IM.INSTITUTE_NAME			AS InstituteName,";
            strQuery = strQuery + System.Environment.NewLine + "                			M.REMARKS					AS Remarks,";
            strQuery = strQuery + System.Environment.NewLine + "                			M.IS_ACTIVE				    AS IsActive,";
            strQuery = strQuery + System.Environment.NewLine + "                			M.ROW_VERSION				AS [RowVersion],";
            strQuery = strQuery + System.Environment.NewLine + "                			M.CHECKSUM_VALUE			AS CheckSumValue,";
            strQuery = strQuery + System.Environment.NewLine + "                			M.[GUID]					AS [GUID],";
            strQuery = strQuery + System.Environment.NewLine + "                			M.CREATED_BY				AS CreatedByUserId,";
            strQuery = strQuery + System.Environment.NewLine + "                			ISNULL(C_UM.NAME, '')		AS CreatedByUserName,";
            strQuery = strQuery + System.Environment.NewLine + "                			M.CREATE_DATE				AS CreateDate,";
            strQuery = strQuery + System.Environment.NewLine + "                			ISNULL(M.MODIFIED_BY, 0)	AS ModifiedByUserId,";
            strQuery = strQuery + System.Environment.NewLine + "                			M.MODIFY_DATE				AS ModifyDate,";
            strQuery = strQuery + System.Environment.NewLine + "                			ISNULL(M_UM.NAME, '')		AS ModifiedByUserName";
            strQuery = strQuery + System.Environment.NewLine + "                FROM		HOLIDAY_MASTER M";
            strQuery = strQuery + System.Environment.NewLine + "                INNER JOIN 	INSTITUTE_MASTER IM";
            strQuery = strQuery + System.Environment.NewLine + "                ON			M.INSTITUTE_MASTER_ID = IM.ID";
            strQuery = strQuery + System.Environment.NewLine + "                INNER JOIN 	USER_MASTER C_UM";
            strQuery = strQuery + System.Environment.NewLine + "                ON			M.CREATED_BY = C_UM.ID";
            strQuery = strQuery + System.Environment.NewLine + "                LEFT JOIN	USER_MASTER M_UM";
            strQuery = strQuery + System.Environment.NewLine + "                ON			M.MODIFIED_BY = M_UM.ID";
            strQuery = strQuery + System.Environment.NewLine + "			)DATA";
            strQuery = strQuery + System.Environment.NewLine + "WHERE		SerialNumber >= " + (((page - 1) * pageSize) + 1);
            strQuery = strQuery + System.Environment.NewLine + "AND		    SerialNumber <= " + (page * pageSize);
            strQuery = strQuery + System.Environment.NewLine + "ORDER BY	SerialNumber";

            lstData = Db.DapperGetList<Holiday>(strQuery);

            return lstData;
        }

        /// <summary>
        /// Method to Get the Particular Record from Database
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Holiday GetSingleRecordDetail(int Id, DateTime? holidayDate, int instituteId)
        {
            Holiday obj = new Holiday();

            string strQuery = "";
            strQuery = strQuery + System.Environment.NewLine + "SELECT		M.ID						AS Id,";
            strQuery = strQuery + System.Environment.NewLine + "			M.NAME					    AS Name,";
            strQuery = strQuery + System.Environment.NewLine + "			M.FROM_DATE					AS FromDate,";
            strQuery = strQuery + System.Environment.NewLine + "			M.TO_DATE					AS ToDate,";
            strQuery = strQuery + System.Environment.NewLine + "			M.IS_FOR_STUDENT			AS IsForStudent,";
            strQuery = strQuery + System.Environment.NewLine + "			M.IS_FOR_TEACHER			AS IsForTeacher,";
            strQuery = strQuery + System.Environment.NewLine + "			M.INSTITUTE_MASTER_ID		AS InstituteMasterId,";
            strQuery = strQuery + System.Environment.NewLine + "			IM.INSTITUTE_NAME			AS InstituteName,";
            strQuery = strQuery + System.Environment.NewLine + "			M.REMARKS					AS Remarks,";
            strQuery = strQuery + System.Environment.NewLine + "			M.IS_ACTIVE				    AS IsActive,";
            strQuery = strQuery + System.Environment.NewLine + "			M.ROW_VERSION				AS [RowVersion],";
            strQuery = strQuery + System.Environment.NewLine + "			M.CHECKSUM_VALUE			AS CheckSumValue,";
            strQuery = strQuery + System.Environment.NewLine + "			M.[GUID]					AS [GUID],";
            strQuery = strQuery + System.Environment.NewLine + "			M.CREATED_BY				AS CreatedByUserId,";
            strQuery = strQuery + System.Environment.NewLine + "			ISNULL(C_UM.NAME, '')		AS CreatedByUserName,";
            strQuery = strQuery + System.Environment.NewLine + "			M.CREATE_DATE				AS CreateDate,";
            strQuery = strQuery + System.Environment.NewLine + "			ISNULL(M.MODIFIED_BY, 0)	AS ModifiedByUserId,";
            strQuery = strQuery + System.Environment.NewLine + "			M.MODIFY_DATE				AS ModifyDate,";
            strQuery = strQuery + System.Environment.NewLine + "			ISNULL(M_UM.NAME, '')		AS ModifiedByUserName";
            strQuery = strQuery + System.Environment.NewLine + "FROM		HOLIDAY_MASTER M";
            strQuery = strQuery + System.Environment.NewLine + "INNER JOIN 	INSTITUTE_MASTER IM";
            strQuery = strQuery + System.Environment.NewLine + "ON			M.INSTITUTE_MASTER_ID = IM.ID";
            strQuery = strQuery + System.Environment.NewLine + "INNER JOIN 	USER_MASTER C_UM";
            strQuery = strQuery + System.Environment.NewLine + "ON			M.CREATED_BY = C_UM.ID";
            strQuery = strQuery + System.Environment.NewLine + "LEFT JOIN	USER_MASTER M_UM";
            strQuery = strQuery + System.Environment.NewLine + "ON			M.MODIFIED_BY = M_UM.ID";
            strQuery = strQuery + System.Environment.NewLine + "WHERE		1 = 1";

            if (Id > 0)
            {
                strQuery = strQuery + System.Environment.NewLine + "AND     M.ID = " + Id.ToString();
            }

            if (holidayDate != null && instituteId > 0)
            {
                strQuery = strQuery + System.Environment.NewLine + "AND		    '" + holidayDate.Value.ToString("MMM dd, yyyy") + "' BETWEEN M.FROM_DATE AND M.TO_DATE";
                strQuery = strQuery + System.Environment.NewLine + "AND         M.INSTITUTE_MASTER_ID = " + instituteId;
            }


            obj = Db.DapperGet<Holiday>(strQuery);

            return obj;
        }

        /// <summary>
        /// Method to get the List of Calendar Events
        /// </summary>
        /// <returns></returns>
        public List<CalendarEntries> GetInstituteCalendar(int Month, int Year, string UserType)
        {
            List<CalendarEntries> lstData = new List<CalendarEntries>();

            DateTime fromDate = new DateTime(Year, Month, 1);
            DateTime toDate = new DateTime(Year, Month, 1);

            toDate = toDate.AddMonths(1).AddDays(-1);

            string strQuery = "";
           
            strQuery = strQuery + System.Environment.NewLine + "AND         IS_ACTIVE = 1";

            lstData = Db.DapperGetList<CalendarEntries>(strQuery);

            DateTime item = fromDate;
            while (item <= toDate)
            {
                if (lstData.Where(m => m.CalendarDate == item).Count() <= 0)
                    lstData.Add(new CalendarEntries { CalendarDate = item, Remarks = "", LegendText = ""});

                item = item.AddDays(1);
            }

            return lstData.OrderBy(m => m.CalendarDate).ToList();
        }

        /// <summary>
        /// Method to get the List of Holidays for Mobile Application
        /// </summary>
        /// <returns></returns>
        public List<Holiday> GetHolidayListForMobileApp()
        {
            List<Holiday> lstData = new List<Holiday>();
            
            string strQuery = "";
            strQuery = strQuery + System.Environment.NewLine + "SELECT		FROM_DATE                   AS FromDate,";
            strQuery = strQuery + System.Environment.NewLine + "			TO_DATE                     AS ToDate,";
            strQuery = strQuery + System.Environment.NewLine + "			NAME                        AS Name";
            strQuery = strQuery + System.Environment.NewLine + "FROM		HOLIDAY_MASTER";
            strQuery = strQuery + System.Environment.NewLine + "WHERE       IS_ACTIVE = 1";


            strQuery = strQuery + System.Environment.NewLine + "ORDER BY    FROM_DATE";

            lstData = Db.DapperGetList<Holiday>(strQuery);

            return lstData;
        }
    }
}
