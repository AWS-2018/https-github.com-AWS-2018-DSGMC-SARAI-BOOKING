using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessObjects.Common
{
    public class RoomCategory : BusinessObject<RoomCategory>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SortId { get; set; }

        public RoomCategory()
        {
            DatabaseTableName = "ROOM_CATEGORY_MASTER";
        }

        public void TrimData()
        {
            if (Name == null)
                Name = "";

            if (SortId < 0)
                SortId = 0;
            
            Name = Name.Trim().ToUpper();
        }

        #region Validations

        public string CheckValidation()
        {
            TrimData();

            string result = "";

            if (Id < 0)
                Id = 0;

            if (Name.Length == 0)
                result += "<li>Name is mandatory.</li>";

            if (SortId <= 0)
                result += "<li>Sort Id must be greater than Zero.</li>";

            result = result.TrimEnd().TrimStart().Trim();

            if (result != "")
                result = "<ul>" + result + "</ul>";

            return result.ToString();
        }

        #endregion Validations
    }
}
