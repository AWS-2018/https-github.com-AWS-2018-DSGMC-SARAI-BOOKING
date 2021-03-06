﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessObjects.Common
{
    public class SaraiRoomAvailability : BusinessObject<SaraiRoomAvailability>
    {
        public int Id { get; set; }
        public int SaraiMasterId { get; set; }
        public int RoomCategoryMasterId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int OnlineRoomsCount { get; set; }
        public int OfflineRoomsCount { get; set; }
        
        public SaraiRoomAvailability()
        {
            DatabaseTableName = "SARAI_ROOM_AVAILABILITY";
        }

        public void TrimData()
        {
            if (SaraiMasterId < 0)
                SaraiMasterId = 0;

            if (RoomCategoryMasterId < 0)
                RoomCategoryMasterId = 0;

            if (OnlineRoomsCount < 0)
                OnlineRoomsCount = 0;

            if (OfflineRoomsCount < 0)
                OfflineRoomsCount = 0;
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

            result = result.TrimEnd().TrimStart().Trim();

            if (result != "")
                result = "<ul>" + result + "</ul>";

            return result.ToString();
        }

        #endregion Validations
    }
}
