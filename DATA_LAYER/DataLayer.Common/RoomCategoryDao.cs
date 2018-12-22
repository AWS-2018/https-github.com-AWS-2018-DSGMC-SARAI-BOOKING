﻿using System;
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
    public class RoomCategoryDao
    {
        /// <summary>
        /// Method to Save the Entry
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int SaveData(RoomCategory obj)
        {
            int Id = 0;

            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@ID", obj.Id));
            parameters.Add(new SqlParameter("@NAME", obj.Name));
            parameters.Add(new SqlParameter("@SORTID", obj.SortId));

            DataTable dt = Db.GetDataTable("SAVE_UPDATE_ROOM_CATEGORY_MASTER", parameters);

            if (dt.Rows.Count > 0)
                Id = Convert.ToInt32(dt.Rows[0][0]);

            return Id;
        }

        /// <summary>
        /// Method to get the List of entries
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<RoomCategory> GetList(string name)
        {
            List<RoomCategory> lstData = new List<RoomCategory>();

            string strQuery = "";
            strQuery = strQuery + System.Environment.NewLine + "SELECT		M.ID			AS Id,";
            strQuery = strQuery + System.Environment.NewLine + "			M.NAME			AS Name,";
            strQuery = strQuery + System.Environment.NewLine + "			M.SORTID		AS SortId";
            strQuery = strQuery + System.Environment.NewLine + "FROM		ROOM_CATEGORY_MASTER M";
            strQuery = strQuery + System.Environment.NewLine + "WHERE		M.NAME LIKE @name";
            strQuery = strQuery + System.Environment.NewLine + "ORDER BY	M.NAME";

            lstData = Db.DapperGetList<RoomCategory>(strQuery, new { name = "%" + name + "%" });

            return lstData;
        }

        /// <summary>
        /// Method to get the List of entries for List Page
        /// </summary>
        /// <param name="page">Page Number</param>
        /// <param name="pageSize">Size of the Page (No. of Records)</param>
        /// <returns></returns>
        public List<RoomCategory> GetList(int page, int pageSize)
        {
            List<RoomCategory> lstData = new List<RoomCategory>();

            string strQuery = "";

            strQuery = strQuery + System.Environment.NewLine + "SELECT	    *";
            strQuery = strQuery + System.Environment.NewLine + "FROM		(	SELECT      ROW_NUMBER() OVER(ORDER BY M.NAME)  AS SerialNumber,";
            strQuery = strQuery + System.Environment.NewLine + "                    		M.ID						        AS Id,";
            strQuery = strQuery + System.Environment.NewLine + "			                M.NAME					            AS Name,";
            strQuery = strQuery + System.Environment.NewLine + "			                M.SORTID					        AS SortId";
            strQuery = strQuery + System.Environment.NewLine + "                FROM		ROOM_CATEGORY_MASTER M";
            strQuery = strQuery + System.Environment.NewLine + "			)DATA";
            strQuery = strQuery + System.Environment.NewLine + "WHERE		SerialNumber >= " + (((page - 1) * pageSize) + 1);
            strQuery = strQuery + System.Environment.NewLine + "AND		    SerialNumber <= " + (page * pageSize);
            strQuery = strQuery + System.Environment.NewLine + "ORDER BY	SerialNumber";

            lstData = Db.DapperGetList<RoomCategory>(strQuery);

            return lstData;
        }

        /// <summary>
        /// Method to Get the Particular Record from Database
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public RoomCategory GetSingleRecordDetail(int Id)
        {
            RoomCategory obj = new RoomCategory();

            string strQuery = "";
            strQuery = strQuery + System.Environment.NewLine + "SELECT		M.ID	    AS Id,";
            strQuery = strQuery + System.Environment.NewLine + "			M.NAME	    AS Name,";
            strQuery = strQuery + System.Environment.NewLine + "			M.SORTID    AS SortId";
            strQuery = strQuery + System.Environment.NewLine + "FROM		ROOM_CATEGORY_MASTER M";
            strQuery = strQuery + System.Environment.NewLine + "WHERE		M.ID = " + Id.ToString();
            
            obj = Db.DapperGet<RoomCategory>(strQuery);

            return obj;
        }           
    }
}
