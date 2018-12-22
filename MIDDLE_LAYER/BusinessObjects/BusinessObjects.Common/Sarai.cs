using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessObjects.Common
{
    public class Sarai : BusinessObject<Sarai>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Pincode { get; set; }
        public string State { get; set; }

        public int MaxAdultCount { get; set; }
        public int MaxChildrenCount { get; set; }

        public TimeSpan CheckInTime { get; set; }
        public TimeSpan CheckOutTime { get; set; }

        public int MaxRoomBookingDaysFromOnlinePortal { get; set; }
        public int MaxRoomCountForSingleBookingFromOnlinePortal { get; set; }
        public int PriorDaysCountForOnlinePortal { get; set; }
        public int DaysCountForNextBookingFromOnlinePortal { get; set; }


        public Sarai()
        {
            DatabaseTableName = "SARAI_MASTER";
        }

        public void TrimData()
        {
            if (Name == null)
                Name = "";

            if (Address == null)
                Address = "";

            if (City == null)
                City = "";

            if (Pincode == null)
                Pincode = "";

            if (State == null)
                State = "";
           
            if (MaxAdultCount < 0)
                MaxAdultCount = 0;

            if (MaxChildrenCount < 0)
                MaxChildrenCount = 0;

            if (MaxRoomBookingDaysFromOnlinePortal < 0)
                MaxRoomBookingDaysFromOnlinePortal = 0;

            if (MaxRoomCountForSingleBookingFromOnlinePortal < 0)
                MaxRoomCountForSingleBookingFromOnlinePortal = 0;

            if (PriorDaysCountForOnlinePortal < 0)
                PriorDaysCountForOnlinePortal = 0;

            if (DaysCountForNextBookingFromOnlinePortal < 0)
                DaysCountForNextBookingFromOnlinePortal = 0;

            Name = Name.Trim().ToUpper();
            Address = Address.Trim();
            City = City.Trim();
            Pincode = Pincode.Trim();
            State = State.Trim();
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

            if (Name.Length == 0)
                result += "<li>Sarai Name is mandatory.</li>";

            if (Address.Length == 0)
                result += "<li>Sarai Address is mandatory.</li>";

            if (City.Length == 0)
                result += "<li>Sarai City is mandatory.</li>";

            if (Pincode.Length == 0)
                result += "<li>Sarai Pincode is mandatory.</li>";

            if (State.Length == 0)
                result += "<li>Sarai State is mandatory.</li>";

            if (MaxAdultCount <= 0)
                result += "<li>Maximum Adult Count must be greater than Zero.</li>";

            if (MaxRoomBookingDaysFromOnlinePortal <= 0)
                result += "<li>Maximum Room Booking Days from Online Portal must be greater than Zero.</li>";

            if (MaxRoomCountForSingleBookingFromOnlinePortal <= 0)
                result += "<li>Maximum Room Count for Single Booking from Online Portal must be greater than Zero.</li>";

            if (PriorDaysCountForOnlinePortal <= 0)
                result += "<li>Prior Days Count for Online Portal must be greater than Zero.</li>";

            result = result.TrimEnd().TrimStart().Trim();

            if (result != "")
                result = "<ul>" + result + "</ul>";

            return result.ToString();
        }

        #endregion Validations
    }
}
