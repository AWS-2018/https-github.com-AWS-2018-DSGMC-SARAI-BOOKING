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
    public class RoomDao
    {
        /// <summary>
        /// Method to Save the Entry
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int SaveData(Room obj)
        {
            int Id = 0;

            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@ID", obj.Id));
            parameters.Add(new SqlParameter("@SARAI_MASTER_ID", obj.SaraiMasterId));
            parameters.Add(new SqlParameter("@ROOM_CATEGORY_MASTER_ID", obj.RoomCategoryMasterId));
            parameters.Add(new SqlParameter("@ROOM_NUMBER", obj.RoomNumber));
            parameters.Add(new SqlParameter("@SECURITY_AMOUNT", obj.SecurityAmount));
            parameters.Add(new SqlParameter("@IS_OCCUPIED", obj.IsOccupied));
            parameters.Add(new SqlParameter("@IS_ACTIVE", obj.IsActive));
            parameters.Add(new SqlParameter("@ROW_VERSION", obj.RowVersion));

            DataTable dt = Db.GetDataTable("SAVE_UPDATE_ROOM_MASTER", parameters);

            if (dt.Rows.Count > 0)
                Id = Convert.ToInt32(dt.Rows[0][0]);

            return Id;
        }

        /// <summary>
        /// Method to get the List of entries
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<Room> GetList(string RoomNumber)
        {
            List<Room> lstData = new List<Room>();

            string strQuery = "";
            strQuery = strQuery + System.Environment.NewLine + "SELECT		M.ID			            AS Id,";
            strQuery = strQuery + System.Environment.NewLine + "			M.SARAI_MASTER_ID			AS SaraiMasterId,";
            strQuery = strQuery + System.Environment.NewLine + "			M.ROOM_CATEGORY_MASTER_ID	AS RoomCategoryMasterId,";
            strQuery = strQuery + System.Environment.NewLine + "			M.ROOM_NUMBER		        AS RoomNumber,";
            strQuery = strQuery + System.Environment.NewLine + "			M.SECURITY_AMOUNT		    AS SecurityAmount,";
            strQuery = strQuery + System.Environment.NewLine + "			M.IS_OCCUPIED	    	    AS IsOccupied,";
            strQuery = strQuery + System.Environment.NewLine + "			M.IS_ACTIVE		            AS IsActive,";
            strQuery = strQuery + System.Environment.NewLine + "			M.ROW_VERSION		        AS RowVersion";
            strQuery = strQuery + System.Environment.NewLine + "FROM		ROOM_MASTER M";
            strQuery = strQuery + System.Environment.NewLine + "WHERE		M.ROOM_NUMBER LIKE @RoomNumber";
            strQuery = strQuery + System.Environment.NewLine + "ORDER BY	M.ROOM_NUMBER";

            lstData = Db.DapperGetList<Room>(strQuery, new { name = "%" + RoomNumber + "%" });

            return lstData;
        }

        /// <summary>
        /// Method to get the List of entries for List Page
        /// </summary>
        /// <param name="page">Page Number</param>
        /// <param name="pageSize">Size of the Page (No. of Records)</param>
        /// <returns></returns>
        public List<Room> GetList(int page, int pageSize)
        {
            List<Room> lstData = new List<Room>();

            string strQuery = "";

            strQuery = strQuery + System.Environment.NewLine + "SELECT	    *";
            strQuery = strQuery + System.Environment.NewLine + "FROM		(	SELECT      ROW_NUMBER() OVER(ORDER BY M.ROOM_NUMBER)   AS SerialNumber,";
            strQuery = strQuery + System.Environment.NewLine + "    		                M.ID			                            AS Id,";
            strQuery = strQuery + System.Environment.NewLine + "			                M.SARAI_MASTER_ID			                AS SaraiMasterId,";
            strQuery = strQuery + System.Environment.NewLine + "			                M.ROOM_CATEGORY_MASTER_ID	                AS RoomCategoryMasterId,";
            strQuery = strQuery + System.Environment.NewLine + "			                M.ROOM_NUMBER		                        AS RoomNumber,";
            strQuery = strQuery + System.Environment.NewLine + "			                M.SECURITY_AMOUNT		                    AS SecurityAmount,";
            strQuery = strQuery + System.Environment.NewLine + "			                M.IS_OCCUPIED	    	                    AS IsOccupied,";
            strQuery = strQuery + System.Environment.NewLine + "			                M.IS_ACTIVE		                            AS IsActive,";
            strQuery = strQuery + System.Environment.NewLine + "			                M.ROW_VERSION		                        AS RowVersion";
            strQuery = strQuery + System.Environment.NewLine + "                FROM		ROOM_MASTER M";
            strQuery = strQuery + System.Environment.NewLine + "			)DATA";
            strQuery = strQuery + System.Environment.NewLine + "WHERE		SerialNumber >= " + (((page - 1) * pageSize) + 1);
            strQuery = strQuery + System.Environment.NewLine + "AND		    SerialNumber <= " + (page * pageSize);
            strQuery = strQuery + System.Environment.NewLine + "ORDER BY	SerialNumber";

            lstData = Db.DapperGetList<Room>(strQuery);

            return lstData;
        }

        /// <summary>
        /// Method to Get the Particular Record from Database
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Room GetSingleRecordDetail(int Id)
        {
            Room obj = new Room();

            string strQuery = "";
            strQuery = strQuery + System.Environment.NewLine + "SELECT		M.ID			            AS Id,";
            strQuery = strQuery + System.Environment.NewLine + "			M.SARAI_MASTER_ID			AS SaraiMasterId,";
            strQuery = strQuery + System.Environment.NewLine + "			M.ROOM_CATEGORY_MASTER_ID	AS RoomCategoryMasterId,";
            strQuery = strQuery + System.Environment.NewLine + "			M.ROOM_NUMBER		        AS RoomNumber,";
            strQuery = strQuery + System.Environment.NewLine + "			M.SECURITY_AMOUNT		    AS SecurityAmount,";
            strQuery = strQuery + System.Environment.NewLine + "			M.IS_OCCUPIED	    	    AS IsOccupied,";
            strQuery = strQuery + System.Environment.NewLine + "			M.IS_ACTIVE		            AS IsActive,";
            strQuery = strQuery + System.Environment.NewLine + "			M.ROW_VERSION		        AS RowVersion";
            strQuery = strQuery + System.Environment.NewLine + "FROM		ROOM_MASTER M";
            strQuery = strQuery + System.Environment.NewLine + "WHERE		M.ID = " + Id.ToString();
            
            obj = Db.DapperGet<Room>(strQuery);

            return obj;
        }           
    }
}
