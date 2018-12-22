using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects.Common;

namespace BusinessObjects.Online
{
    public class PaymentGatewayTransaction : BusinessObject<PaymentGatewayTransaction>
    {
        public int Id { get; set; }

        public DateTime RecordDate { get; set; }

        public int OnlineBookingId { get; set; }

        public decimal Amount { get; set; }

        public long TransactionId { get; set; }

        public DateTime TransactionInitiateDate { get; set; }

        public DateTime TransactionCompleteDate { get; set; }

        public string BankTransactionId { get; set; }

        public string BankResponse { get; set; }

        public bool TransactionStatus { get; set; }

        public bool IsRejectedTransaction { get; set; }

        public bool IsManuallyUpdatedTransactionStatus { get; set; }
        
        public PaymentGatewayTransaction()
        {
            DatabaseTableName = "PAYMENT_GATEWAY_TRANSACTION";
        }
    }
}
