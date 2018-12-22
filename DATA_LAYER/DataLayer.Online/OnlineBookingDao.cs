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
using BusinessObjects.Online;

namespace DataLayer.Online
{
    public class OnlineBookingDao
    {
        /// <summary>
        /// Method to Save the Entry
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int SaveData(OnlineBooking obj)
        {
            int Id = 0;

            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@ID", obj.Id));
            parameters.Add(new SqlParameter("@SESSION_ID", obj.SessionId));
            parameters.Add(new SqlParameter("@ENTRY_DATE", obj.EntryDate));
            parameters.Add(new SqlParameter("@SARAI_MASTER_ID", obj.SaraiMasterId));
            parameters.Add(new SqlParameter("@CUSTOMER_MASTER_ID", obj.CustomerMasterId));
            parameters.Add(new SqlParameter("@FROM_DATE", obj.FromDate));
            parameters.Add(new SqlParameter("@TO_DATE", obj.ToDate));
            parameters.Add(new SqlParameter("@NO_OF_ROOMS", obj.NumberOfRooms));
            parameters.Add(new SqlParameter("@ADULT_COUNT", obj.AdultCount));
            parameters.Add(new SqlParameter("@CHILDREN_COUNT", obj.ChildrenCount));
            parameters.Add(new SqlParameter("@ARRIVAL_TIME", obj.ArrivalTime));
            parameters.Add(new SqlParameter("@PROOF_MASTER_ID", obj.ProofMasterId));
            parameters.Add(new SqlParameter("@PROOF_DOCUMENT_MASTER_ID", obj.ProofDocumentMasterId));
            parameters.Add(new SqlParameter("@PROOF_DOCUMENT_NUMBER", obj.ProofDocumentNumber));
            parameters.Add(new SqlParameter("@AMOUNT", obj.Amount));
            parameters.Add(new SqlParameter("@REMARKS", obj.Remarks));
            parameters.Add(new SqlParameter("@IP_ADDRESS", obj.IPAddress));
            parameters.Add(new SqlParameter("@BOOKING_STATUS", obj.BookingStatus));

            DataTable dt = Db.GetDataTable("SAVE_UPDATE_ONLINE_BOOKING", parameters);

            if (dt.Rows.Count > 0)
                Id = Convert.ToInt32(dt.Rows[0][0]);

            return Id;
        }

        /// <summary>
        /// Method to get the List of entries
        /// </summary>
        /// <returns></returns>
        public List<OnlineBooking> GetList()
        {
            List<OnlineBooking> lstData = new List<OnlineBooking>();

            string strQuery = "";
            strQuery = strQuery + System.Environment.NewLine + "SELECT		M.ID						AS Id,";
            strQuery = strQuery + System.Environment.NewLine + "			M.SESSION_ID			    AS SessionId,";
            strQuery = strQuery + System.Environment.NewLine + "			M.ENTRY_DATE	            AS EntryDate,";
            strQuery = strQuery + System.Environment.NewLine + "			M.SARAI_MASTER_ID			AS SaraiMasterId,";
            strQuery = strQuery + System.Environment.NewLine + "			M.CUSTOMER_MASTER_ID		AS CustomerMasterId,";
            strQuery = strQuery + System.Environment.NewLine + "			M.FROM_DATE		            AS FromDate,";
            strQuery = strQuery + System.Environment.NewLine + "			M.TO_DATE		            AS ToDate,";
            strQuery = strQuery + System.Environment.NewLine + "			M.NO_OF_ROOMS				AS NumberOfRooms,";
            strQuery = strQuery + System.Environment.NewLine + "			M.ADULT_COUNT				AS AdultCount,";
            strQuery = strQuery + System.Environment.NewLine + "			M.CHILDREN_COUNT			AS ChildrenCount,";
            strQuery = strQuery + System.Environment.NewLine + "			M.ARRIVAL_TIME		        AS ArrivalTime,";
            strQuery = strQuery + System.Environment.NewLine + "			M.PROOF_MASTER_ID			AS ProofMasterId,";
            strQuery = strQuery + System.Environment.NewLine + "			M.PROOF_DOCUMENT_MASTER_ID	AS ProofDocumentMasterId,";
            strQuery = strQuery + System.Environment.NewLine + "			M.PROOF_DOCUMENT_NUMBER		AS ProofDocumentNumber,";
            strQuery = strQuery + System.Environment.NewLine + "			M.AMOUNT		            AS Amount,";
            strQuery = strQuery + System.Environment.NewLine + "			M.REMARKS		            AS Remarks,";
            strQuery = strQuery + System.Environment.NewLine + "			M.IP_ADDRESS		        AS IPAddress,";
            strQuery = strQuery + System.Environment.NewLine + "			M.BOOKING_STATUS		    AS BookingStatus";
            strQuery = strQuery + System.Environment.NewLine + "FROM		ONLINE_BOOKING M";
            strQuery = strQuery + System.Environment.NewLine + "ORDER BY	M.ENTRY_DATE DESC";

            lstData = Db.DapperGetList<OnlineBooking>(strQuery, new {});

            return lstData;
        }

        /// <summary>
        /// Method to get the List of entries for List Page
        /// </summary>
        /// <param name="page">Page Number</param>
        /// <param name="pageSize">Size of the Page (No. of Records)</param>
        /// <returns></returns>
        public List<OnlineBooking> GetList(int page, int pageSize)
        {
            List<OnlineBooking> lstData = new List<OnlineBooking>();

            string strQuery = "";

            strQuery = strQuery + System.Environment.NewLine + "SELECT	    *";
            strQuery = strQuery + System.Environment.NewLine + "FROM		(	SELECT      ROW_NUMBER() OVER(ORDER BY M.ENTRY_DATE DESC)   AS SerialNumber,";
            strQuery = strQuery + System.Environment.NewLine + "                    		M.ID						                    AS Id,";
            strQuery = strQuery + System.Environment.NewLine + "			                M.SESSION_ID			                        AS SessionId,";
            strQuery = strQuery + System.Environment.NewLine + "			                M.ENTRY_DATE	                                AS EntryDate,";
            strQuery = strQuery + System.Environment.NewLine + "			                M.SARAI_MASTER_ID			                    AS SaraiMasterId,";
            strQuery = strQuery + System.Environment.NewLine + "			                M.CUSTOMER_MASTER_ID		                    AS CustomerMasterId,";
            strQuery = strQuery + System.Environment.NewLine + "			                M.FROM_DATE		                                AS FromDate,";
            strQuery = strQuery + System.Environment.NewLine + "			                M.TO_DATE		                                AS ToDate,";
            strQuery = strQuery + System.Environment.NewLine + "			                M.NO_OF_ROOMS				                    AS NumberOfRooms,";
            strQuery = strQuery + System.Environment.NewLine + "			                M.ADULT_COUNT				                    AS AdultCount,";
            strQuery = strQuery + System.Environment.NewLine + "			                M.CHILDREN_COUNT			                    AS ChildrenCount,";
            strQuery = strQuery + System.Environment.NewLine + "			                M.ARRIVAL_TIME		                            AS ArrivalTime,";
            strQuery = strQuery + System.Environment.NewLine + "			                M.PROOF_MASTER_ID			                    AS ProofMasterId,";
            strQuery = strQuery + System.Environment.NewLine + "			                M.PROOF_DOCUMENT_MASTER_ID	                    AS ProofDocumentMasterId,";
            strQuery = strQuery + System.Environment.NewLine + "			                M.PROOF_DOCUMENT_NUMBER		                    AS ProofDocumentNumber,";
            strQuery = strQuery + System.Environment.NewLine + "			                M.AMOUNT		                                AS Amount,";
            strQuery = strQuery + System.Environment.NewLine + "			                M.REMARKS		                                AS Remarks,";
            strQuery = strQuery + System.Environment.NewLine + "			                M.IP_ADDRESS		                            AS IPAddress,";
            strQuery = strQuery + System.Environment.NewLine + "			                M.BOOKING_STATUS		                        AS BookingStatus";
            strQuery = strQuery + System.Environment.NewLine + "                FROM		ONLINE_BOOKING M";
            strQuery = strQuery + System.Environment.NewLine + "			)DATA";
            strQuery = strQuery + System.Environment.NewLine + "WHERE		SerialNumber >= " + (((page - 1) * pageSize) + 1);
            strQuery = strQuery + System.Environment.NewLine + "AND		    SerialNumber <= " + (page * pageSize);
            strQuery = strQuery + System.Environment.NewLine + "ORDER BY	SerialNumber";

            lstData = Db.DapperGetList<OnlineBooking>(strQuery);

            return lstData;
        }

        /// <summary>
        /// Method to Get the Particular Record from Database
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public OnlineBooking GetSingleRecordDetail(int Id, DateTime? holidayDate, int instituteId)
        {
            OnlineBooking obj = new OnlineBooking();

            string strQuery = "";
            strQuery = strQuery + System.Environment.NewLine + "SELECT		M.ID						AS Id,";
            strQuery = strQuery + System.Environment.NewLine + "			M.SESSION_ID			    AS SessionId,";
            strQuery = strQuery + System.Environment.NewLine + "			M.ENTRY_DATE	            AS EntryDate,";
            strQuery = strQuery + System.Environment.NewLine + "			M.SARAI_MASTER_ID			AS SaraiMasterId,";
            strQuery = strQuery + System.Environment.NewLine + "			M.CUSTOMER_MASTER_ID		AS CustomerMasterId,";
            strQuery = strQuery + System.Environment.NewLine + "			M.FROM_DATE		            AS FromDate,";
            strQuery = strQuery + System.Environment.NewLine + "			M.TO_DATE		            AS ToDate,";
            strQuery = strQuery + System.Environment.NewLine + "			M.NO_OF_ROOMS				AS NumberOfRooms,";
            strQuery = strQuery + System.Environment.NewLine + "			M.ADULT_COUNT				AS AdultCount,";
            strQuery = strQuery + System.Environment.NewLine + "			M.CHILDREN_COUNT			AS ChildrenCount,";
            strQuery = strQuery + System.Environment.NewLine + "			M.ARRIVAL_TIME		        AS ArrivalTime,";
            strQuery = strQuery + System.Environment.NewLine + "			M.PROOF_MASTER_ID			AS ProofMasterId,";
            strQuery = strQuery + System.Environment.NewLine + "			M.PROOF_DOCUMENT_MASTER_ID	AS ProofDocumentMasterId,";
            strQuery = strQuery + System.Environment.NewLine + "			M.PROOF_DOCUMENT_NUMBER		AS ProofDocumentNumber,";
            strQuery = strQuery + System.Environment.NewLine + "			M.AMOUNT		            AS Amount,";
            strQuery = strQuery + System.Environment.NewLine + "			M.REMARKS		            AS Remarks,";
            strQuery = strQuery + System.Environment.NewLine + "			M.IP_ADDRESS		        AS IPAddress,";
            strQuery = strQuery + System.Environment.NewLine + "			M.BOOKING_STATUS		    AS BookingStatus";
            strQuery = strQuery + System.Environment.NewLine + "FROM		ONLINE_BOOKING M";
            strQuery = strQuery + System.Environment.NewLine + "WHERE		M.ID = " + Id.ToString();
        
            obj = Db.DapperGet<OnlineBooking>(strQuery);

            return obj;
        }     
    }
}
