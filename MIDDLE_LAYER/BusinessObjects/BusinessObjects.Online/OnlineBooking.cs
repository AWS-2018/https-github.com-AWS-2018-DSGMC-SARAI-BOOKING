using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects.Common;

namespace BusinessObjects.Online
{
    public class OnlineBooking : BusinessObject<OnlineBooking>
    {
        public int Id { get; set; }

        public string SessionId { get; set; }

        public DateTime EntryDate { get; set; }

        public int SaraiMasterId { get; set; }
        public int CustomerMasterId { get; set; }
        
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public int NumberOfRooms { get; set; }
        public int AdultCount { get; set; }
        public int ChildrenCount { get; set; }

        public string ArrivalTime { get; set; }

        public int ProofMasterId { get; set; }
        public int ProofDocumentMasterId { get; set; }
        public string ProofDocumentNumber { get; set; }
        
        public decimal Amount { get; set; }

        public string IPAddress { get; set; }

        public bool BookingStatus { get; set; }

        public OnlineBooking()
        {
            DatabaseTableName = "ONLINE_BOOKING";
        }

        public void TrimData()
        {
            if (SessionId == null)
                SessionId = "";
            
            if (SaraiMasterId < 0)
                SaraiMasterId = 0;

            if (CustomerMasterId < 0)
                CustomerMasterId = 0;

            if (NumberOfRooms < 0)
                NumberOfRooms = 0;

            if (AdultCount < 0)
                AdultCount = 0;

            if (ChildrenCount < 0)
                ChildrenCount = 0;
            
            if (ArrivalTime == null)
                ArrivalTime = "";

            if (ProofMasterId < 0)
                ProofMasterId = 0;

            if (ProofDocumentMasterId < 0)
                ProofDocumentMasterId = 0;

            if (ProofDocumentNumber == null)
                ProofDocumentNumber = "";

            if (Amount < 0)
                Amount = 0;

            if (Remarks == null)
                Remarks = "";

            if (IPAddress == null)
                IPAddress = "";
         
            SessionId = SessionId.Trim();
            ArrivalTime = ArrivalTime.Trim();
            ProofDocumentNumber = ProofDocumentNumber.Trim();
            Remarks = Remarks.Trim();
            IPAddress = IPAddress.Trim();
        }

        #region Validations

        public string CheckValidation()
        {
            TrimData();

            string result = "";

            if (RowVersion <= 0 && Id > 0)
                result += "<li>Invalid Row Version</li>";

            if (Id < 0)
                Id = 0;

            if (SessionId.Length == 0)
                result += "<li>Session Id is mandatory.</li>";

            if (SaraiMasterId <= 0)
                result += "<li>Sarai Master Id must be greater than Zero.</li>";

            if (CustomerMasterId <= 0)
                result += "<li>Customer Master Id must be greater than Zero.</li>";

            if (NumberOfRooms <= 0)
                result += "<li>Number of Rooms must be greater than Zero.</li>";

            if (AdultCount <= 0)
                result += "<li>Adult Count must be greater than Zero.</li>";

            if (ProofMasterId <= 0)
                result += "<li>Proof is mandatory.</li>";

            if (ProofDocumentMasterId  <= 0)
                result += "<li>Proof Document is mandatory.</li>";

            if (Amount <= 0)
                result += "<li>Amount must be greater than Zero.</li>";
            
            result = result.TrimEnd().TrimStart().Trim();

            if (result != "")
                result = "<ul>" + result + "</ul>";

            return result.ToString();
        }

        #endregion Validations
    }
}
