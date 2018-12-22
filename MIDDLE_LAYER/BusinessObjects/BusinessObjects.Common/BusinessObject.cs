using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessObjects.Common
{
    [Serializable]
    public abstract class BusinessObject<T> : IDisposable where T : BusinessObject<T>
    {
        public string EncryptedId { get; set; }
        public int SerialNumber { get; set; } = 0;
        public int RowVersion { get; set; } = 1;
        public bool IsActive { get; set; } = true;
        public string Remarks { get; set; } = "";
        public int CheckSumValue { get; set; } = 0;
        public Guid GUID { get; set; } = Guid.NewGuid();
        public string DatabaseTableName { get; set; } = "";
        public string Token { get; set; } = "";
        public string CRCValue { get; set; } = "";
        public string SuperAdminName { get; set; } = "";
        //public int CanDelete { get; set; }

        public int CreatedByUserId { get; set; } = 0;
        public string CreatedByUserName { get; set; } = "";
        public DateTime CreateDate { get; set; } = DateTime.Now;

        public int ModifiedByUserId { get; set; } = 0;
        public string ModifiedByUserName { get; set; } = "";
        public DateTime ModifyDate { get; set; } = DateTime.Now;

        public int RecordCount { get; set; }

        public string ErrorMessage { get; set; }

        public int TimeStamp { get; set; }

        public string QRFileName { get; set; }

        public void Dispose()
        {

        }

        public bool IsValidEmail(string source)
        {
            return new System.ComponentModel.DataAnnotations.EmailAddressAttribute().IsValid(source);
        }
    }
}
