using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessObjects.Common
{
    public class Holiday : BusinessObject<Holiday>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public int InstituteMasterId { get; set; }
        public string InstituteName { get; set; }

        public bool IsForStudent { get; set; }
        public bool IsForTeacher { get; set; }

        public Holiday()
        {
            DatabaseTableName = "HOLIDAY_MASTER";
        }

        public void TrimData()
        {
            if (Name == null)
                Name = "";

            if (Remarks == null)
                Remarks = "";

            if (InstituteMasterId < 0)
                InstituteMasterId = 0;

            if (InstituteName == null)
                InstituteName = "";

            Name = Name.Trim().ToUpper();
            Remarks = Remarks.Trim().ToUpper();
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
                result += "<li>Description is mandatory.</li>";

            if (InstituteMasterId <= 0)
                result += "<li>Institute Id must be greater than Zero.</li>";

            if (FromDate > ToDate)
                result += "<li>\'From Date\' should be less than or equal to \'To Date\'.</li>";

            if (IsForStudent == false && IsForTeacher == false)
                result += "<li>Please select atleast Student or Teacher.</li>";

            result = result.TrimEnd().TrimStart().Trim();

            if (result != "")
                result = "<ul>" + result + "</ul>";

            return result.ToString();
        }

        #endregion Validations
    }
}
