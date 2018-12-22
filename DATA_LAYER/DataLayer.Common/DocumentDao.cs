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
    public class DocumentDao
    {
        /// <summary>
        /// Method to Save the Entry
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int SaveData(Document obj)
        {
            int Id = 0;

            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@ID", obj.Id));
            parameters.Add(new SqlParameter("@FULL_DOCUMENT_NAME", obj.FullDocumentName));
            parameters.Add(new SqlParameter("@DOCUMENT_NAME", obj.FullDocumentName));
            parameters.Add(new SqlParameter("@DOCUMENT_EXTENSION", obj.DocumentExtension));
            parameters.Add(new SqlParameter("@REMARKS", obj.Remarks));
            parameters.Add(new SqlParameter("@IS_ACTIVE", obj.IsActive));

            DataTable dt = Db.GetDataTable("SAVE_UPDATE_DOCUMENT_MASTER", parameters);

            if (dt.Rows.Count > 0)
                Id = Convert.ToInt32(dt.Rows[0][0]);

            return Id;
        }

        /// <summary>
        /// Method to get the List of entries
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<Document> GetList(string name)
        {
            List<Document> lstData = new List<Document>();

            string strQuery = "";
            strQuery = strQuery + System.Environment.NewLine + "SELECT		M.ID			        AS Id,";
            strQuery = strQuery + System.Environment.NewLine + "			M.FULL_DOCUMENT_NAME	AS FullDocumentName,";
            strQuery = strQuery + System.Environment.NewLine + "			M.DOCUMENT_NAME			AS DocumentName,";
            strQuery = strQuery + System.Environment.NewLine + "			M.DOCUMENT_EXTENSION	AS DocumentExtension,";
            strQuery = strQuery + System.Environment.NewLine + "			M.REMARKS			    AS Remarks,";
            strQuery = strQuery + System.Environment.NewLine + "			M.IS_ACTIVE		        AS IsActive";
            strQuery = strQuery + System.Environment.NewLine + "FROM		DOCUMENT_MASTER M";
            strQuery = strQuery + System.Environment.NewLine + "WHERE		M.DOCUMENT_NAME LIKE @name";
            strQuery = strQuery + System.Environment.NewLine + "ORDER BY	M.DOCUMENT_NAME";

            lstData = Db.DapperGetList<Document>(strQuery, new { name = "%" + name + "%" });

            return lstData;
        }

        /// <summary>
        /// Method to get the List of entries for List Page
        /// </summary>
        /// <param name="page">Page Number</param>
        /// <param name="pageSize">Size of the Page (No. of Records)</param>
        /// <returns></returns>
        public List<Document> GetList(int page, int pageSize)
        {
            List<Document> lstData = new List<Document>();

            string strQuery = "";

            strQuery = strQuery + System.Environment.NewLine + "SELECT	    *";
            strQuery = strQuery + System.Environment.NewLine + "FROM		(	SELECT      ROW_NUMBER() OVER(ORDER BY M.DOCUMENT_NAME)     AS SerialNumber,";
            strQuery = strQuery + System.Environment.NewLine + "    		                M.ID			                                AS Id,";
            strQuery = strQuery + System.Environment.NewLine + "			                M.FULL_DOCUMENT_NAME	                        AS FullDocumentName,";
            strQuery = strQuery + System.Environment.NewLine + "			                M.DOCUMENT_NAME			                        AS DocumentName,";
            strQuery = strQuery + System.Environment.NewLine + "			                M.DOCUMENT_EXTENSION	                        AS DocumentExtension,";
            strQuery = strQuery + System.Environment.NewLine + "			                M.REMARKS			                            AS Remarks,";
            strQuery = strQuery + System.Environment.NewLine + "			                M.IS_ACTIVE		                                AS IsActive";
            strQuery = strQuery + System.Environment.NewLine + "                FROM		DOCUMENT_MASTER M";
            strQuery = strQuery + System.Environment.NewLine + "			)DATA";
            strQuery = strQuery + System.Environment.NewLine + "WHERE		SerialNumber >= " + (((page - 1) * pageSize) + 1);
            strQuery = strQuery + System.Environment.NewLine + "AND		    SerialNumber <= " + (page * pageSize);
            strQuery = strQuery + System.Environment.NewLine + "ORDER BY	SerialNumber";

            lstData = Db.DapperGetList<Document>(strQuery);

            return lstData;
        }

        /// <summary>
        /// Method to Get the Particular Record from Database
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Document GetSingleRecordDetail(int Id)
        {
            Document obj = new Document();

            string strQuery = "";
            strQuery = strQuery + System.Environment.NewLine + "SELECT		M.ID			        AS Id,";
            strQuery = strQuery + System.Environment.NewLine + "			M.FULL_DOCUMENT_NAME	AS FullDocumentName,";
            strQuery = strQuery + System.Environment.NewLine + "			M.DOCUMENT_NAME			AS DocumentName,";
            strQuery = strQuery + System.Environment.NewLine + "			M.DOCUMENT_EXTENSION	AS DocumentExtension,";
            strQuery = strQuery + System.Environment.NewLine + "			M.REMARKS			    AS Remarks,";
            strQuery = strQuery + System.Environment.NewLine + "			M.IS_ACTIVE		        AS IsActive";
            strQuery = strQuery + System.Environment.NewLine + "FROM		DOCUMENT_MASTER M";
            strQuery = strQuery + System.Environment.NewLine + "WHERE		M.ID = " + Id.ToString();
            
            obj = Db.DapperGet<Document>(strQuery);

            return obj;
        }           
    }
}
