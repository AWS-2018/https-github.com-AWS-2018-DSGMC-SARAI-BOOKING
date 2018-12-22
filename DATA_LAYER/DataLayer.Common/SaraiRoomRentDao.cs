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
    public class SaraiRoomRentDao
    {
        /// <summary>
        /// Method to Save the Entry
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int SaveData(SaraiRoomRent obj)
        {
            int Id = 0;

            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@ID", obj.Id));
            parameters.Add(new SqlParameter("@SARAI_MASTER_ID", obj.FromDate));
            parameters.Add(new SqlParameter("@ROOM_CATEGORY_MASTER_ID", obj.ToDate));
            parameters.Add(new SqlParameter("@FROM_DATE", obj.Remarks));
            parameters.Add(new SqlParameter("@TO_DATE", obj.IsActive));
            parameters.Add(new SqlParameter("@AMOUNT", obj.RowVersion));
            parameters.Add(new SqlParameter("@IS_ACTIVE", obj.IsActive));
            parameters.Add(new SqlParameter("@ROW_VERSION", obj.RowVersion));
            parameters.Add(new SqlParameter("@SUPERADMIN_USERNAME", Localizer.CurrentUser.SuperAdminName));
            parameters.Add(new SqlParameter("@USER_MASTER_ID", Localizer.CurrentUser.Id));

            DataTable dt = Db.GetDataTable("SAVE_UPDATE_SARAI_ROOM_RENT", parameters);

            if (dt.Rows.Count > 0)
                Id = Convert.ToInt32(dt.Rows[0][0]);

            return Id;
        }

        /// <summary>
        /// Method to get the List of entries
        /// </summary>
        /// <returns></returns>
        public List<SaraiRoomRent> GetList()
        {
            List<SaraiRoomRent> lstData = new List<SaraiRoomRent>();

            string strQuery = "";
            strQuery = strQuery + System.Environment.NewLine + "SELECT		M.ID						AS Id,";
            strQuery = strQuery + System.Environment.NewLine + "			M.SARAI_MASTER_ID			AS SaraiMasterId,";
            strQuery = strQuery + System.Environment.NewLine + "			M.ROOM_CATEGORY_MASTER_ID	AS RoomCategoryMasterId,";
            strQuery = strQuery + System.Environment.NewLine + "			M.FROM_DATE					AS FromDate,";
            strQuery = strQuery + System.Environment.NewLine + "			M.TO_DATE			        AS ToDate,";
            strQuery = strQuery + System.Environment.NewLine + "			M.AMOUNT		            AS Amount,";
            strQuery = strQuery + System.Environment.NewLine + "			M.IS_ACTIVE				    AS IsActive,";
            strQuery = strQuery + System.Environment.NewLine + "			M.ROW_VERSION				AS [RowVersion],";
            strQuery = strQuery + System.Environment.NewLine + "			M.CREATED_BY				AS CreatedByUserId,";
            strQuery = strQuery + System.Environment.NewLine + "			ISNULL(C_UM.NAME, '')		AS CreatedByUserName,";
            strQuery = strQuery + System.Environment.NewLine + "			M.CREATE_DATE				AS CreateDate,";
            strQuery = strQuery + System.Environment.NewLine + "			ISNULL(M.MODIFIED_BY, 0)	AS ModifiedByUserId,";
            strQuery = strQuery + System.Environment.NewLine + "			M.MODIFY_DATE				AS ModifyDate,";
            strQuery = strQuery + System.Environment.NewLine + "			ISNULL(M_UM.NAME, '')		AS ModifiedByUserName";
            strQuery = strQuery + System.Environment.NewLine + "FROM		SARAI_ROOM_RENT M";
            strQuery = strQuery + System.Environment.NewLine + "INNER JOIN 	USER_MASTER C_UM";
            strQuery = strQuery + System.Environment.NewLine + "ON			M.CREATED_BY = C_UM.ID";
            strQuery = strQuery + System.Environment.NewLine + "LEFT JOIN	USER_MASTER M_UM";
            strQuery = strQuery + System.Environment.NewLine + "ON			M.MODIFIED_BY = M_UM.ID";
            strQuery = strQuery + System.Environment.NewLine + "ORDER BY	M.FROM_DATE";

            lstData = Db.DapperGetList<SaraiRoomRent>(strQuery, new {});

            return lstData;
        }

        /// <summary>
        /// Method to get the List of entries for List Page
        /// </summary>
        /// <param name="page">Page Number</param>
        /// <param name="pageSize">Size of the Page (No. of Records)</param>
        /// <returns></returns>
        public List<SaraiRoomRent> GetList(int page, int pageSize)
        {
            List<SaraiRoomRent> lstData = new List<SaraiRoomRent>();

            string strQuery = "";

            strQuery = strQuery + System.Environment.NewLine + "SELECT	    *";
            strQuery = strQuery + System.Environment.NewLine + "FROM		(	SELECT      ROW_NUMBER() OVER(ORDER BY M.FROM_DATE)     AS SerialNumber,";
            strQuery = strQuery + System.Environment.NewLine + "    		                M.ID						                AS Id,";
            strQuery = strQuery + System.Environment.NewLine + "			                M.SARAI_MASTER_ID			                AS SaraiMasterId,";
            strQuery = strQuery + System.Environment.NewLine + "			                M.ROOM_CATEGORY_MASTER_ID	                AS RoomCategoryMasterId,";
            strQuery = strQuery + System.Environment.NewLine + "			                M.FROM_DATE					                AS FromDate,";
            strQuery = strQuery + System.Environment.NewLine + "			                M.TO_DATE			                        AS ToDate,";
            strQuery = strQuery + System.Environment.NewLine + "			                M.AMOUNT		                            AS Amount,";
            strQuery = strQuery + System.Environment.NewLine + "			                M.IS_ACTIVE				                    AS IsActive,";
            strQuery = strQuery + System.Environment.NewLine + "			                M.ROW_VERSION				                AS [RowVersion],";
            strQuery = strQuery + System.Environment.NewLine + "			                M.CREATED_BY				                AS CreatedByUserId,";
            strQuery = strQuery + System.Environment.NewLine + "			                ISNULL(C_UM.NAME, '')		                AS CreatedByUserName,";
            strQuery = strQuery + System.Environment.NewLine + "			                M.CREATE_DATE				                AS CreateDate,";
            strQuery = strQuery + System.Environment.NewLine + "			                ISNULL(M.MODIFIED_BY, 0)	                AS ModifiedByUserId,";
            strQuery = strQuery + System.Environment.NewLine + "			                M.MODIFY_DATE				                AS ModifyDate,";
            strQuery = strQuery + System.Environment.NewLine + "			                ISNULL(M_UM.NAME, '')		                AS ModifiedByUserName";
            strQuery = strQuery + System.Environment.NewLine + "                FROM		SARAI_ROOM_RENT M";
            strQuery = strQuery + System.Environment.NewLine + "                INNER JOIN 	USER_MASTER C_UM";
            strQuery = strQuery + System.Environment.NewLine + "                ON			M.CREATED_BY = C_UM.ID";
            strQuery = strQuery + System.Environment.NewLine + "                LEFT JOIN	USER_MASTER M_UM";
            strQuery = strQuery + System.Environment.NewLine + "                ON			M.MODIFIED_BY = M_UM.ID";
            strQuery = strQuery + System.Environment.NewLine + "			)DATA";
            strQuery = strQuery + System.Environment.NewLine + "WHERE		SerialNumber >= " + (((page - 1) * pageSize) + 1);
            strQuery = strQuery + System.Environment.NewLine + "AND		    SerialNumber <= " + (page * pageSize);
            strQuery = strQuery + System.Environment.NewLine + "ORDER BY	SerialNumber";

            lstData = Db.DapperGetList<SaraiRoomRent>(strQuery);

            return lstData;
        }

        /// <summary>
        /// Method to Get the Particular Record from Database
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public SaraiRoomRent GetSingleRecordDetail(int Id)
        {
            SaraiRoomRent obj = new SaraiRoomRent();

            string strQuery = "";
            strQuery = strQuery + System.Environment.NewLine + "SELECT		M.ID						AS Id,";
            strQuery = strQuery + System.Environment.NewLine + "			M.SARAI_MASTER_ID			AS SaraiMasterId,";
            strQuery = strQuery + System.Environment.NewLine + "			M.ROOM_CATEGORY_MASTER_ID	AS RoomCategoryMasterId,";
            strQuery = strQuery + System.Environment.NewLine + "			M.FROM_DATE					AS FromDate,";
            strQuery = strQuery + System.Environment.NewLine + "			M.TO_DATE			        AS ToDate,";
            strQuery = strQuery + System.Environment.NewLine + "			M.AMOUNT		            AS Amount,";
            strQuery = strQuery + System.Environment.NewLine + "			M.IS_ACTIVE				    AS IsActive,";
            strQuery = strQuery + System.Environment.NewLine + "			M.ROW_VERSION				AS [RowVersion],";
            strQuery = strQuery + System.Environment.NewLine + "			M.CREATED_BY				AS CreatedByUserId,";
            strQuery = strQuery + System.Environment.NewLine + "			ISNULL(C_UM.NAME, '')		AS CreatedByUserName,";
            strQuery = strQuery + System.Environment.NewLine + "			M.CREATE_DATE				AS CreateDate,";
            strQuery = strQuery + System.Environment.NewLine + "			ISNULL(M.MODIFIED_BY, 0)	AS ModifiedByUserId,";
            strQuery = strQuery + System.Environment.NewLine + "			M.MODIFY_DATE				AS ModifyDate,";
            strQuery = strQuery + System.Environment.NewLine + "			ISNULL(M_UM.NAME, '')		AS ModifiedByUserName";
            strQuery = strQuery + System.Environment.NewLine + "FROM		SARAI_ROOM_RENT M";
            strQuery = strQuery + System.Environment.NewLine + "INNER JOIN 	USER_MASTER C_UM";
            strQuery = strQuery + System.Environment.NewLine + "ON			M.CREATED_BY = C_UM.ID";
            strQuery = strQuery + System.Environment.NewLine + "LEFT JOIN	USER_MASTER M_UM";
            strQuery = strQuery + System.Environment.NewLine + "ON			M.MODIFIED_BY = M_UM.ID";
            strQuery = strQuery + System.Environment.NewLine + "WHERE		M.ID = " + Id.ToString();
        
            obj = Db.DapperGet<SaraiRoomRent>(strQuery);

            return obj;
        }     
    }
}
