using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaraiBooking.ViewModels
{
    public class SearchUserViewModel
    {
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        

        public bool IsSeniorAdmin { get; set; }
        public bool IsSuperAdmin { get; set; }
        public bool IsOperator { get; set; }
        public bool IsAuditor { get; set; }
        public bool IsHeadOffice { get; set; }
        public bool CanApproveEmployeeMaster { get; set; }
        public bool CanApproveEmployeeIncrementMaster { get; set; }

        public bool IsSeniorAdminChecked { get; set; }
        public bool IsSuperAdminChecked { get; set; }
        public bool IsOperatorChecked { get; set; }
        public bool IsAuditorChecked { get; set; }
        public bool IsHeadOfficeChecked { get; set; }
        public bool CanApproveEmployeeMasterChecked { get; set; }
        public bool CanApproveEmployeeIncrementMasterChecked { get; set; }

        public void TrimData()
        {
            if (Name == null)
                Name = "";
            
            if (EmailId == null)
                EmailId = "";

            if (MobileNo == null)
                MobileNo = "";
            
            Name = Name.Trim();
            EmailId = EmailId.Trim();
            MobileNo = MobileNo.Trim();

        }

    }
}