using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;

namespace BusinessObjects.Common
{
    public class UserMaster : BusinessObject<UserMaster>
    {
        public int Id { get; set; } = 0;
        public Int64 LoginHistoryId { get; set; } = 0;

        public CultureInfo UserCultureInfo { get; set; }

        public string Name { get; set; } = "";
        public string Password { get; set; } = "";
        public string EmailId { get; set; } = "";
        public string MobileNo { get; set; } = "";

        public bool IsSuperAdmin { get; set; }
        public bool IsSeniorAdmin { get; set; }

        public bool IsOperator { get; set; }
        public bool IsAuditor { get; set; }
        public bool IsHeadOffice { get; set; }

        public byte[] UserImage { get; set; }

        public DateTime LastLogInDateTime { get; set; } = DateTime.Now;

        [Required]
        public int PageSize { get; set; } = 10;
        public int PayrollAttendancePageSize { get; set; } = 50;

        [Required]
        public string DateFormat { get; set; } = "MMM dd, yyyy";

        [Required]
        public string DateTimeFormat { get; set; } = "MMM dd, yyyy hh:mm tt";

        [Required]
        public string AmountFormat { get; set; } = "0.00";

        public int DigitAfterDecimalAmount { get; set; } = 2;

        public List<UserMenuMaster> LstParentMenu { get; set; }
        public List<UserMenuMaster> LstChildMenu { get; set; }

        public bool CanApproveEmployeeMaster { get; set; }
        public bool CanApproveEmployeeIncrementMaster { get; set; }

        public bool CanApproveHomeWork { get; set; }

        public string IPAddress { get; set; } = "";
        public string BrowserInformation { get; set; } = "";

        public int DefaultFinYearMasterId { get; set; } = 0;
        public int DefaultInstituteMasterId { get; set; } = 0;

        
        public bool CanLoginOnSunday { get; set; }
        public bool CanLoginOnHoliday { get; set; }
        public bool IsTimeBound { get; set; }
        public TimeSpan FromTime { get; set; } = DateTime.Now.TimeOfDay;
        public TimeSpan ToTime { get; set; } = DateTime.Now.TimeOfDay;
        public bool ExtraSecurityRequired { get; set; }
        public string VerificationCode { get; set; } = "";
        public string ResetToken { get; set; } = "";

        public string PasswordResetToken { get; set; } = "";

        public string CompanyName { get; set; } = "";
        public string EmailDisplayName { get; set; } = "";
        public string EmailHostName { get; set; } = "";
        public bool EmailEnableSSL { get; set; }
        public string EmailUserName { get; set; } = "";
        public string EmailPassword { get; set; } = "";
        public int EmailPortNumber { get; set; } = 427;


        public int DefaultEmployeeBloodGroupMasterId { get; set; } = 1;
        public int DefaultEmployeeReligionMasterId { get; set; } = 1;
        public int DefaultEmployeeCasteMasterId { get; set; } = 1;

        public string NewPassword { get; set; } = "";
        public string ConfirmPassword { get; set; } = "";

        public int AllocatedInstituteCount { get; set; } = 0;

        public int AllocatedModulesCount { get; set; } = 0;
        public int CurrentModuleId { get; set; } = 0;
        public string CurrentModuleName { get; set; } = "";

        public List<UserFavoriteMenuMaster> FavoriteMenus { get; set; }

        public string UserType { get; set; } = "";

        public string DeviceId { get; set; } = "";
        public string DeviceType { get; set; } = "";
        public string InstituteCode { get; set; } = "";

        public bool IsValidUser { get; set; }
        public bool OTPRequired { get; set; }
        public string OTPValue { get; set; } = "";
        public string AccessToken { get; set; } = "";

        public int EmployeeMasterId { get; set; } = 0;


        public bool CanAddStudentConcession { get; set; }
        public bool CanAddStudentAdditionalCharges { get; set; }
        public bool CanAddStudentRefund { get; set; }
        public bool CanAddStudentWaiveOff { get; set; }
        public bool CanAddStudentConveyance { get; set; }
        public bool CanAddStudentParking { get; set; }
        public bool CanAddStudentFine { get; set; }
        public bool CanAddStudentClass { get; set; }
        public bool CanAddStudentOpeningBalance { get; set; }

        public bool SaveSuccessMessageRequiredInPopUp { get; set; }

        public int LastFeeReceiptsDisplayCount { get; set; }

        public int FeeReceiptDefaultPaymentModeId { get; set; }
        public int FeeReceiptDefaultSubPaymentModeId { get; set; }

        public bool FeeReceiptPaymentModeEditable { get; set; }
        public bool FeeReceiptSubPaymentModeEditable { get; set; }

        public bool CanEditFeeSlipDueDays { get; set; }

        public bool IsFromMobileBrowser { get; set; }

        public string MobileAppToken { get; set; }

        public int TransportProductDefaultListCount { get; set; } //This is the default count or blank product rows while adding new Transport Repair Bill.

        public UserMaster()
        {
            LstParentMenu = new List<UserMenuMaster>();
            LstChildMenu = new List<UserMenuMaster>();
            FavoriteMenus = new List<UserFavoriteMenuMaster>();

            CanLoginOnSunday = false;
            CanLoginOnHoliday = false;
            IsTimeBound = true;
            FromTime = TimeSpan.Parse("08:00:00");
            ToTime = TimeSpan.Parse("16:00:00");
            ExtraSecurityRequired = false;
            VerificationCode = string.Empty;
            ResetToken = string.Empty;

            DefaultEmployeeBloodGroupMasterId = -1;
            DefaultEmployeeReligionMasterId = -1;
            DefaultEmployeeCasteMasterId = -1;

            UserCultureInfo = new System.Globalization.CultureInfo("hi-IN");

            CanAddStudentConcession = true;
            CanAddStudentAdditionalCharges = true;
            CanAddStudentRefund = true;
            CanAddStudentWaiveOff = true;
            CanAddStudentConveyance = true;
            CanAddStudentParking = true;
            CanAddStudentFine = true;
            CanAddStudentClass = true;
            CanAddStudentOpeningBalance = true;

            SaveSuccessMessageRequiredInPopUp = false;

            LastFeeReceiptsDisplayCount = 5;
            FeeReceiptDefaultPaymentModeId = 1;
            FeeReceiptDefaultSubPaymentModeId = 1;

            CanEditFeeSlipDueDays = true;

            TransportProductDefaultListCount = 5;
        }

        public void TrimData()
        {
            if (Name == null)
                Name = "";          

            if (Password == null)
                Password = "";

            if (EmailId == null)
                EmailId = "";

            if (MobileNo == null)
                MobileNo = "";

            if (DateFormat == null)
                DateFormat = "";

            if (AmountFormat == null)
                AmountFormat = "";

            if (Remarks == null)
                Remarks = "";


            MobileAppToken = MobileAppToken == null ? "" : MobileAppToken.Trim();

            Name = Name.Trim();
            Password = Password.Trim();
            EmailId = EmailId.Trim().ToLower();
            MobileNo = MobileNo.Trim().ToUpper();

            if (DateFormat == "")
                DateFormat = "MMM dd, yyyy";

            if (PageSize <= 0)
                PageSize = 10;

            if (AmountFormat == "")
                AmountFormat = "0.00";
        }

        #region Validations

        public string CheckValidation()
        {
            TrimData();

            string result = "";

            if (Name.Length == 0)
                result = result + "<li>Name is mandatory.</li>";

            if (EmailId.Length == 0)
                result = result + "<li>Email Id is mandatory.</li>";

            if (IsValidEmail(EmailId) == false)
                result = result + "<li>Invalid Email Id.</li>";

            if (MobileNo.Length == 0)
                result = result + "<li>Mobile No. is mandatory.</li>";

            if (MobileNo.Length > 0 && MobileNo.Length != 10)
                result = result + "<li>Mobile No. is not valid.</li>";


            if (IsSuperAdmin || IsSeniorAdmin)
            {
                IsOperator = false;
                IsAuditor = false;
                IsHeadOffice = false;
            }


            if (IsSeniorAdmin)
                IsSuperAdmin = true;


            if (IsSuperAdmin == false && IsSeniorAdmin == false)
            {
                if (IsOperator == false && IsAuditor == false && IsHeadOffice == false)
                    result = result + "<li>Please select one option from 'Operator', 'Auditor' or 'Head Office'.</li>";

                /*
                int i = 0;

                if (IsOperator)
                    i++;

                if (IsAuditor)
                    i++;

                if (IsHeadOffice)
                    i++;

                if (i > 1)
                    result = result + "<li>Please select only one option from 'Operator', 'Auditor' or 'Head Office'.</li>";
                */
            }

            DefaultFinYearMasterId = 1;

            return result;
        }

        #endregion Validations
    }


    public class UserMenuMaster : BusinessObject<UserMenuMaster>
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string IconClass { get; set; }
        public string MenuArea { get; set; }
        public string MenuAction { get; set; }
        public string MenuController { get; set; }

        public int ParentId { get; set; }
        public string ParentName { get; set; }
        public  int MenuSortId { get; set; }

        public int ModuleId { get; set; }
        public string ModuleName { get; set; }
        public int ModuleSortId { get; set; }

        public string ParameterList { get; set; }

        public bool CanAccess {get; set;}
        public bool CanCreate {get; set;}
        public bool CanEdit {get; set;}
        public bool CanChangeStatus {get; set;}
        public bool IsFavoriteMenu { get; set; }
    }

    public class UserFavoriteMenuMaster : BusinessObject<UserFavoriteMenuMaster>
    {
        public int Id { get; set; }

        public int UserMasterId { get; set; }

        public int ModuleId { get; set; }
        public string ModuleName { get; set; }
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public int ParentMenuId { get; set; }
        public string ParentMenuName { get; set; }
        public int SortId { get; set; }
        public bool IsSelected { get; set; }

        public string IconClass { get; set; }
        public string MenuArea { get; set; }
        public string MenuAction { get; set; }
        public string MenuController { get; set; }
        public string ParameterList { get; set; }

    }
}
