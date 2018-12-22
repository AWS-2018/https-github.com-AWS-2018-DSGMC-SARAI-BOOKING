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
    public class BlockedIPAddressDao
    {
        /// <summary>
        /// Method to Save the Blocked IP Address
        /// </summary>
        /// <param name="IPAddress"></param>
        /// <returns></returns>
        public int SaveBlockedIPAddress(string IPAddress)
        {
            int Id = 0;

            List<SqlParameter> parameters = new List<SqlParameter>();

            UserMaster user = Localizer.CurrentUser;

            parameters.Add(new SqlParameter("@IP_ADDRESS", IPAddress));

            DataTable dt = Db.GetDataTable("SAVE_BLOCKED_IP_ADDRESS", parameters);

            if (dt.Rows.Count > 0)
                Id = Convert.ToInt32(dt.Rows[0][0]);

            return Id;
        }


        /// <summary>
        /// Method to Check if current IP Address is Blocked
        /// </summary>
        /// <param name="IPAddress"></param>
        /// <returns></returns>
        public bool IsBlockedIPAddress(string IPAddress)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@IP_ADDRESS", IPAddress));

            DataTable dt = Db.GetDataTable("VALIDATE_IP_ADDRESS", parameters);

            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Method to Get the List of Blocked IP Addresses
        /// </summary>
        /// <param name="IPAddress"></param>
        /// <returns></returns>
        public List<BlockedIPAddress> GetList(DateTime FromDate, DateTime ToDate)
        {
            List<BlockedIPAddress> lstData = new List<BlockedIPAddress>();

            string strQuery = "";
            strQuery = strQuery + System.Environment.NewLine + "SELECT      ROW_NUMBER() OVER(ORDER BY IP_ADDRESS) AS SerialNumber,";
            strQuery = strQuery + System.Environment.NewLine + "            ID              AS Id,";
            strQuery = strQuery + System.Environment.NewLine + "            IP_ADDRESS      AS IPAddress,";
            strQuery = strQuery + System.Environment.NewLine + "            RECORD_DATE     AS RecordDate,";
            strQuery = strQuery + System.Environment.NewLine + "            IS_ACTIVE       AS IsActive";
            strQuery = strQuery + System.Environment.NewLine + "FROM        BLOCKED_IP_ADDRESS";
            strQuery = strQuery + System.Environment.NewLine + "WHERE       CONVERT(NVARCHAR(50), RECORD_DATE, 107) >= '" + FromDate.ToString("MMM dd, yyyy") + "'";
            strQuery = strQuery + System.Environment.NewLine + "AND         CONVERT(NVARCHAR(50), RECORD_DATE, 107) <= '" + ToDate.ToString("MMM dd, yyyy") + "'";
            strQuery = strQuery + System.Environment.NewLine + "ORDER BY    IP_ADDRESS";

            lstData = Db.DapperGetList<BlockedIPAddress>(strQuery);

            return lstData;
        }

        /// <summary>
        /// Method to Unblock IP Address
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        public void UnBlockIPAddress(string ipAddress)
        {
            Db.ExecuteQuery("UPDATE BLOCKED_IP_ADDRESS SET IS_ACTIVE = 0 WHERE IP_ADDRESS = '" + ipAddress + "' AND CONVERT(NVARCHAR(50), RECORD_DATE, 107) = CONVERT(NVARCHAR(50), GETDATE(), 107)");
        }

        /// <summary>
        /// Method to Block IP Address
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        public void BlockIPAddress(string ipAddress)
        {
            Db.ExecuteQuery("UPDATE BLOCKED_IP_ADDRESS SET IS_ACTIVE = 1 WHERE IP_ADDRESS = '" + ipAddress + "' AND CONVERT(NVARCHAR(50), RECORD_DATE, 107) = CONVERT(NVARCHAR(50), GETDATE(), 107)");
        }
    }
}
