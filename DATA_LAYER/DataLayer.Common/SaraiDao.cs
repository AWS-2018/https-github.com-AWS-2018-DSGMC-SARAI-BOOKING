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
    public class SaraiDao
    {
        /// <summary>
        /// Method to Save the Entry
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int SaveData(Sarai obj)
        {
            int Id = 0;

            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@ID", obj.Id));
            parameters.Add(new SqlParameter("@NAME", obj.Name));
            parameters.Add(new SqlParameter("@REMARKS", obj.Remarks));
            parameters.Add(new SqlParameter("@IS_ACTIVE", obj.IsActive));
            parameters.Add(new SqlParameter("@ROW_VERSION", obj.RowVersion));
            parameters.Add(new SqlParameter("@CHECKSUM_VALUE", obj.CheckSumValue));
            parameters.Add(new SqlParameter("@SUPERADMIN_USERNAME", Localizer.CurrentUser.SuperAdminName));
            parameters.Add(new SqlParameter("@USER_MASTER_ID", Localizer.CurrentUser.Id));

            DataTable dt = Db.GetDataTable("SAVE_UPDATE_SARAI_MASTER", parameters);

            if (dt.Rows.Count > 0)
                Id = Convert.ToInt32(dt.Rows[0][0]);

            return Id;
        }

        /// <summary>
        /// Method to get the List of entries
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<Sarai> GetList(string name)
        {
            List<Sarai> lstData = new List<Sarai>();

            string strQuery = "";
            strQuery = strQuery + System.Environment.NewLine + "SELECT		M.ID						                                AS Id,";
            strQuery = strQuery + System.Environment.NewLine + "			M.NAME					                                    AS Name,";
            strQuery = strQuery + System.Environment.NewLine + "			M.ADDRESS					                                AS Address,";
            strQuery = strQuery + System.Environment.NewLine + "			M.CITY					                                    AS City,";
            strQuery = strQuery + System.Environment.NewLine + "			M.PINCODE			                                        AS Pincode,";
            strQuery = strQuery + System.Environment.NewLine + "			M.STATE			                                            AS State,";
            strQuery = strQuery + System.Environment.NewLine + "			M.IS_ACTIVE				                                    AS IsActive,";
            strQuery = strQuery + System.Environment.NewLine + "			M.MAX_ADULT_COUNT			                                AS MaxAdultCount,";
            strQuery = strQuery + System.Environment.NewLine + "			M.MAX_CHILDREN_COUNT		                                AS MaxChildrenCount,";
            strQuery = strQuery + System.Environment.NewLine + "			M.CHECK_IN_TIME				                                AS CheckInTime,";
            strQuery = strQuery + System.Environment.NewLine + "			M.CHECK_OUT_TIME				                            AS CheckOutTime,";
            strQuery = strQuery + System.Environment.NewLine + "			M.MAX_ROOM_BOOKING_DAYS_FROM_ONLINE_PORTAL		            AS MaxRoomBookingDaysFromOnlinePortal,";
            strQuery = strQuery + System.Environment.NewLine + "			M.MAX_ROOMS_COUNT_FOR_SINGLE_BOOKING_FROM_ONLINE_PORTAL	    AS MaxRoomsCountForSingleBookingFromOnlinePortal,";
            strQuery = strQuery + System.Environment.NewLine + "			M.PRIOR_DAYS_COUNT_FOR_ONLINE_PORTAL	                    AS PriorDaysCountForOnlinePortal,";
            strQuery = strQuery + System.Environment.NewLine + "			M.DAYS_COUNT_FOR_NEXT_BOOKING_FROM_ONLINE_PORTAL		    AS DaysCountForNextBookingFromOnlinePortal";
            strQuery = strQuery + System.Environment.NewLine + "FROM		SARAI_MASTER M";
            strQuery = strQuery + System.Environment.NewLine + "WHERE		M.NAME LIKE @name";
            strQuery = strQuery + System.Environment.NewLine + "ORDER BY	M.NAME";

            lstData = Db.DapperGetList<Sarai>(strQuery, new { name = "%" + name + "%" });

            return lstData;
        }

        /// <summary>
        /// Method to get the List of entries for List Page
        /// </summary>
        /// <param name="page">Page Number</param>
        /// <param name="pageSize">Size of the Page (No. of Records)</param>
        /// <returns></returns>
        public List<Sarai> GetList(int page, int pageSize)
        {
            List<Sarai> lstData = new List<Sarai>();

            string strQuery = "";

            strQuery = strQuery + System.Environment.NewLine + "SELECT	    *";
            strQuery = strQuery + System.Environment.NewLine + "FROM		(	SELECT      ROW_NUMBER() OVER(ORDER BY M.NAME)                          AS SerialNumber,";
            strQuery = strQuery + System.Environment.NewLine + "		                    M.ID						                                AS Id,";
            strQuery = strQuery + System.Environment.NewLine + "			                M.NAME					                                    AS Name,";
            strQuery = strQuery + System.Environment.NewLine + "			                M.ADDRESS					                                AS Address,";
            strQuery = strQuery + System.Environment.NewLine + "			                M.CITY					                                    AS City,";
            strQuery = strQuery + System.Environment.NewLine + "			                M.PINCODE			                                        AS Pincode,";
            strQuery = strQuery + System.Environment.NewLine + "			                M.STATE			                                            AS State,";
            strQuery = strQuery + System.Environment.NewLine + "			                M.IS_ACTIVE				                                    AS IsActive,";
            strQuery = strQuery + System.Environment.NewLine + "			                M.MAX_ADULT_COUNT			                                AS MaxAdultCount,";
            strQuery = strQuery + System.Environment.NewLine + "			                M.MAX_CHILDREN_COUNT		                                AS MaxChildrenCount,";
            strQuery = strQuery + System.Environment.NewLine + "			                M.CHECK_IN_TIME				                                AS CheckInTime,";
            strQuery = strQuery + System.Environment.NewLine + "			                M.CHECK_OUT_TIME				                            AS CheckOutTime,";
            strQuery = strQuery + System.Environment.NewLine + "			                M.MAX_ROOM_BOOKING_DAYS_FROM_ONLINE_PORTAL		            AS MaxRoomBookingDaysFromOnlinePortal,";
            strQuery = strQuery + System.Environment.NewLine + "			                M.MAX_ROOMS_COUNT_FOR_SINGLE_BOOKING_FROM_ONLINE_PORTAL	    AS MaxRoomsCountForSingleBookingFromOnlinePortal,";
            strQuery = strQuery + System.Environment.NewLine + "			                M.PRIOR_DAYS_COUNT_FOR_ONLINE_PORTAL	                    AS PriorDaysCountForOnlinePortal,";
            strQuery = strQuery + System.Environment.NewLine + "			                M.DAYS_COUNT_FOR_NEXT_BOOKING_FROM_ONLINE_PORTAL		    AS DaysCountForNextBookingFromOnlinePortal";
            strQuery = strQuery + System.Environment.NewLine + "                FROM		SARAI_MASTER M";
            strQuery = strQuery + System.Environment.NewLine + "			)DATA";
            strQuery = strQuery + System.Environment.NewLine + "WHERE		SerialNumber >= " + (((page - 1) * pageSize) + 1);
            strQuery = strQuery + System.Environment.NewLine + "AND		    SerialNumber <= " + (page * pageSize);
            strQuery = strQuery + System.Environment.NewLine + "ORDER BY	SerialNumber";

            lstData = Db.DapperGetList<Sarai>(strQuery);

            return lstData;
        }

        /// <summary>
        /// Method to Get the Particular Record from Database
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Sarai GetSingleRecordDetail(int Id)
        {
            Sarai obj = new Sarai();

            string strQuery = "";
            strQuery = strQuery + System.Environment.NewLine + "SELECT		M.ID						                                AS Id,";
            strQuery = strQuery + System.Environment.NewLine + "			M.NAME					                                    AS Name,";
            strQuery = strQuery + System.Environment.NewLine + "			M.ADDRESS					                                AS Address,";
            strQuery = strQuery + System.Environment.NewLine + "			M.CITY					                                    AS City,";
            strQuery = strQuery + System.Environment.NewLine + "			M.PINCODE			                                        AS Pincode,";
            strQuery = strQuery + System.Environment.NewLine + "			M.STATE			                                            AS State,";
            strQuery = strQuery + System.Environment.NewLine + "			M.IS_ACTIVE				                                    AS IsActive,";
            strQuery = strQuery + System.Environment.NewLine + "			M.MAX_ADULT_COUNT			                                AS MaxAdultCount,";
            strQuery = strQuery + System.Environment.NewLine + "			M.MAX_CHILDREN_COUNT		                                AS MaxChildrenCount,";
            strQuery = strQuery + System.Environment.NewLine + "			M.CHECK_IN_TIME				                                AS CheckInTime,";
            strQuery = strQuery + System.Environment.NewLine + "			M.CHECK_OUT_TIME				                            AS CheckOutTime,";
            strQuery = strQuery + System.Environment.NewLine + "			M.MAX_ROOM_BOOKING_DAYS_FROM_ONLINE_PORTAL		            AS MaxRoomBookingDaysFromOnlinePortal,";
            strQuery = strQuery + System.Environment.NewLine + "			M.MAX_ROOMS_COUNT_FOR_SINGLE_BOOKING_FROM_ONLINE_PORTAL	    AS MaxRoomsCountForSingleBookingFromOnlinePortal,";
            strQuery = strQuery + System.Environment.NewLine + "			M.PRIOR_DAYS_COUNT_FOR_ONLINE_PORTAL	                    AS PriorDaysCountForOnlinePortal,";
            strQuery = strQuery + System.Environment.NewLine + "			M.DAYS_COUNT_FOR_NEXT_BOOKING_FROM_ONLINE_PORTAL		    AS DaysCountForNextBookingFromOnlinePortal";
            strQuery = strQuery + System.Environment.NewLine + "FROM		SARAI_MASTER M";
            strQuery = strQuery + System.Environment.NewLine + "WHERE		M.ID = " + Id.ToString();
                                 
            obj = Db.DapperGet<Sarai>(strQuery);

            return obj;
        }

   
    }
}
