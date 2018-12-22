using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;

namespace BusinessObjects.Common
{
    public class Customer : BusinessObject<Customer>
    {
        public int Id { get; set; } = 0;
       
        public string Name { get; set; } = "";

        public string EmailId { get; set; } = "";
        public string EncryptedEmailId { get; set; } = "";
        public bool IsEmailVerified { get; set; }

        public string MobileNo { get; set; } = "";
        public string EncryptedMobileNo { get; set; } = "";
        public bool IsMobileVerified { get; set; }

        public string LoginPassword { get; set; } = "";
        public string EncryptedLoginPassword { get; set; } = "";

        public string PasswordResetToken { get; set; } = "";
        public bool PasswordResetTokenUsed { get; set; }        

        public DateTime LastLogInDateTime { get; set; } = DateTime.Now;

        
        public Customer()
        {
            DatabaseTableName = "CUSTOMER_MASTER";
        }

        public void TrimData()
        {
            if (Name == null)
                Name = "";  

            if (EmailId == null)
                EmailId = "";

            if (EncryptedEmailId == null)
                EncryptedEmailId = "";

            if (MobileNo == null)
                MobileNo = "";

            if (EncryptedMobileNo == null)
                EncryptedMobileNo = "";

            if (LoginPassword == null)
                LoginPassword = "";

            if (EncryptedLoginPassword == null)
                EncryptedLoginPassword = "";

            if (PasswordResetToken == null)
                PasswordResetToken = "";

            if (Remarks == null)
                Remarks = "";

         
            Name = Name.Trim();
            EmailId = EmailId.Trim();
            EncryptedEmailId = EncryptedEmailId.Trim();
            MobileNo = MobileNo.Trim();
            EncryptedMobileNo = EncryptedMobileNo.Trim();

            LoginPassword = LoginPassword.Trim();
            EncryptedLoginPassword = EncryptedLoginPassword.Trim();

            PasswordResetToken = PasswordResetToken.Trim();
            Remarks = Remarks.Trim();          
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
            
            return result;
        }

        #endregion Validations
    }
}
