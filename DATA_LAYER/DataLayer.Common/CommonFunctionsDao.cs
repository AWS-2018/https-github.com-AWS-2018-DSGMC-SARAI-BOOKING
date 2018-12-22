using FrameWork.DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BusinessObjects.Common;
using FrameWork.Core;
using Dapper;

namespace DataLayer.Common
{
    public class CommonFunctionsDao
    {
    
        /// <summary>
        /// Method used to Enable / Disable a Record
        /// </summary>
        /// <param name="Id">Record Id</param>
        /// <param name="RowVersion">Row Version of Record</param>
        /// <param name="Status">New Status to be Updated</param>
        /// <param name="ModifiedByUserId">Id of User who is Modifying the Record</param>
        /// <param name="TableName">Name of the Table to which the record belogs</param>
        /// <returns></returns>
        public bool UpdateRecordStatus(int Id, int RowVersion, bool Status, int ModifiedByUserId, string TableName, string DatabaseName = "")
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@ID", Id));
            parameters.Add(new SqlParameter("@ROW_VERSION", RowVersion));
            parameters.Add(new SqlParameter("@NEW_STATUS", Status));
            parameters.Add(new SqlParameter("@TABLE_NAME",TableName));
            parameters.Add(new SqlParameter("@SUPERADMIN_USERNAME", Localizer.CurrentUser.SuperAdminName));
            parameters.Add(new SqlParameter("@USER_MASTER_ID", Localizer.CurrentUser.Id));

            DataTable dt = Db.GetDataTable("UPDATE_RECORD_STATUS", parameters, DatabaseName);

            return true;
        }

        /// <summary>
        /// Method used to Get the Number of Records (For List Page)
        /// </summary>
        /// <param name="TableName">Name of the Table to which the record belogs</param>
        /// <returns></returns>
        public int GetRecordCountForListPage(string TableName, string SearchCriteria = "", string DatabaseName = "")
        {
            int recordCount = 0;

            string strQuery = "";
            strQuery += System.Environment.NewLine + "SELECT    COUNT(*) AS RECORD_COUNT";
            strQuery += System.Environment.NewLine + "FROM      " + TableName;
            strQuery += System.Environment.NewLine + "WHERE     1 = 1";
            strQuery += SearchCriteria;

            DataTable dataTable = new DataTable();
            dataTable = Db.GetDataTableFromQuery(strQuery, DatabaseName);

            if (dataTable.Rows.Count > 0)
                recordCount = Convert.ToInt16(dataTable.Rows[0][0].ToString());

            return recordCount;
        }

        /// <summary>
        /// Method used to Check If String Column Value already exists
        /// </summary>
        /// <param name="Id">Record Id</param>
        /// <param name="ColumnName">Column Name</param>
        /// <param name="ColumnValue">Column Value</param>
        /// <param name="TableName">Name of the Table to which the record belogs</param>
        /// <returns></returns>
        public bool CheckValueAlreadyExists(int Id, string ColumnName, string ColumnValue, string TableName, string DatabaseName = "", string WhereCondition = "")
        {
            bool isExists = false;

            string strQuery = "";
            strQuery += System.Environment.NewLine + "SELECT     1";
            strQuery += System.Environment.NewLine + "FROM       " + TableName;
            strQuery += System.Environment.NewLine + "WHERE      " + ColumnName + " = '" + ColumnValue + "'";
            strQuery += System.Environment.NewLine + "AND        ID != " + Id;

            if (WhereCondition.Length != 0)
                strQuery += System.Environment.NewLine + "AND        " + WhereCondition;

            DataTable dataTable = new DataTable();
            dataTable = Db.GetDataTableFromQuery(strQuery, DatabaseName);

            if (dataTable.Rows.Count > 0)
                isExists = true;

            return isExists;
        }

        /// <summary>
        /// Method used to Check If Integer Column Value already exists
        /// </summary>
        /// <param name="Id">Record Id</param>
        /// <param name="ColumnName">Column Name</param>
        /// <param name="ColumnValue">Column Value</param>
        /// <param name="TableName">Name of the Table to which the record belogs</param>
        /// <returns></returns>
        public bool CheckValueAlreadyExists(int Id, string ColumnName, int ColumnValue, string TableName, string DatabaseName = "", string WhereCondition = "")
        {
            bool isExists = false;

            string strQuery = "";
            strQuery += System.Environment.NewLine + "SELECT     1";
            strQuery += System.Environment.NewLine + "FROM       " + TableName;
            strQuery += System.Environment.NewLine + "WHERE      " + ColumnName + " = " + ColumnValue;
            strQuery += System.Environment.NewLine + "AND        ID != " + Id;

            if (WhereCondition.Length != 0)
                strQuery += System.Environment.NewLine + "AND        " + WhereCondition;

            DataTable dataTable = new DataTable();
            dataTable = Db.GetDataTableFromQuery(strQuery, DatabaseName);

            if (dataTable.Rows.Count > 0)
                isExists = true;

            return isExists;
        }

        /// <summary>
        /// Method used to Get the New SortId
        /// </summary>
        /// <param name="TableName">Name of the Table to which the record belogs</param>
        /// <returns></returns>
        public int GetNewSortId(string TableName, string DatabaseName = "")
        {
            int newSortId = 0;

            string strQuery = "";
            strQuery += System.Environment.NewLine + "SELECT     ISNULL(MAX(SORTID), 0) AS MAX_ID";
            strQuery += System.Environment.NewLine + "FROM       " + TableName;

            DataTable dataTable = new DataTable();
            dataTable = Db.GetDataTableFromQuery(strQuery, DatabaseName);

            if (dataTable.Rows.Count > 0)
                newSortId = Convert.ToInt16(dataTable.Rows[0][0].ToString());

            return (newSortId + 1);
        }

        /// <summary>
        /// Method used to Check the Row Version
        /// </summary>
        /// <param name="Id">Record Id</param>
        /// <param name="RowVersion">Row version of Record</param>
        /// <param name="TableName">Name of the Table to which the record belogs</param>
        /// <returns></returns>
        public bool CheckRowVersion(int Id, int RowVersion, string TableName, string DatabaseName = "")
        {
            bool isValid = false;

            string strQuery = "";
            strQuery += System.Environment.NewLine + "SELECT        1";
            strQuery += System.Environment.NewLine + "FROM          " + TableName;
            strQuery += System.Environment.NewLine + "WHERE         ID = " + Id;
            strQuery += System.Environment.NewLine + "AND           ROW_VERSION = " + RowVersion;

            DataTable dataTable = new DataTable();
            dataTable = Db.GetDataTableFromQuery(strQuery, DatabaseName);

            if (dataTable.Rows.Count > 0)
                if (Convert.ToInt16(dataTable.Rows[0][0].ToString()) == 1)
                    isValid = true;

            return isValid;
        }
        
        /// <summary>
        /// Method used to Remove All the Temp Data from Database at the Time of Logout
        /// </summary>
        /// <returns></returns>
        public void LogOutSession()
        {
            //string strQuery = "";

            //strQuery += System.Environment.NewLine + "DELETE    ";
            //strQuery += System.Environment.NewLine + "FROM      TEMP_DOCUMENTS_OBJECT";
            //strQuery += System.Environment.NewLine + "WHERE     RECORD_DATE != GETDATE()";

            //Db.ExecuteDeleteQuery(strQuery, Localizer.CurrentInstitute.CommonDatabaseName);
        }

        public DataTable ListToDataTable<T>(List<T> items)
        {
            return Db.ListToDataTable<T>(items);
        }

  
        /// <summary>
        /// Method to get the Current Date
        /// <param name="hostName">hostName</param>
        /// </summary>
        /// <returns></returns>
        public DateTime GetCurrentDateFromServer()
        {
            DateTime currentDate = DateTime.Now;

            if (Utility.IsInternetConnected())
            {
                string strQuery = "SELECT GETDATE()";

                DataTable dt = new DataTable();
                dt = Db.GetDataTableFromQuery(strQuery);

                if (dt.Rows.Count > 0)
                    currentDate = Convert.ToDateTime(dt.Rows[0][0]);
            }

            return currentDate;
        }

      
    

        /// <summary>
        /// Method used to Save User Report History
        /// </summary>
        /// <param name="ReportName">Name of the Report</param>
        /// <param name="FilterCriteriaIds">Filter Criteria for Ids to be used for current report</param>
        /// <param name="FilterCriteriaNames">Filter Criteria for Names to be used for current report</param>
        /// <returns></returns>
        public int SaveUserReportHistory(string ReportName, DataTable filterCriteria)
        {
            int reportHistoryId = 0;

            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@REPORT_NAME", ReportName));
            parameters.Add(new SqlParameter("@FILTER_DATA", filterCriteria));
            parameters.Add(new SqlParameter("@USER_MASTER_ID", Localizer.CurrentUser.Id));
            parameters.Add(new SqlParameter("@MODULE_MASTER_ID", Localizer.CurrentUser.CurrentModuleId));

            DataTable dataTable = new DataTable();
            dataTable = Db.GetDataTable("SAVE_USER_REPORT_HISTORY", parameters);

            if (dataTable.Rows.Count > 0)
                reportHistoryId = Convert.ToInt32(dataTable.Rows[0][0].ToString());

            return reportHistoryId;
        }

    }
}
