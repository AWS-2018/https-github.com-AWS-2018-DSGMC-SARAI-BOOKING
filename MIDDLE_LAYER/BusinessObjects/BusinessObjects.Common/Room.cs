using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessObjects.Common
{
    public class Room : BusinessObject<Room>
    {
        public int Id { get; set; }
        public int SaraiMasterId { get; set; }
        public int RoomCategoryMasterId { get; set; }
        public string RoomNumber { get; set; }
        public decimal SecurityAmount { get; set; }
        public bool IsOccupied { get; set; }
        
        public Room()
        {
            DatabaseTableName = "ROOM_MASTER";
        }

        public void TrimData()
        {
            if (SaraiMasterId < 0)
                SaraiMasterId = 0;

            if (RoomCategoryMasterId < 0)
                RoomCategoryMasterId = 0;

            if (RoomNumber == null)
                RoomNumber = "";

            if (SecurityAmount < 0)
                SecurityAmount = 0;
        }

        #region Validations

        public string CheckValidation()
        {
            TrimData();

            string result = "";

            if (Id < 0)
                Id = 0;

            if (SaraiMasterId <= 0)
                result += "<li>Sarai Master Id must be greater than Zero.</li>";

            if (RoomCategoryMasterId <= 0)
                result += "<li>Room Category Master Id must be greater than Zero.</li>";

            if (RoomNumber.Length == 0)
                result += "<li>Room Number is mandatory.</li>";

            result = result.TrimEnd().TrimStart().Trim();

            if (result != "")
                result = "<ul>" + result + "</ul>";

            return result.ToString();
        }

        #endregion Validations
    }
}
