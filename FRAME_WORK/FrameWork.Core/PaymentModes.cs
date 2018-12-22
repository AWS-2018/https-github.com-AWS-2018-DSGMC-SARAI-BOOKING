using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameWork.Core
{
    //For any changes made in this file, please make relevant changes in Financial Year View 'VW_PAYMENT_MODE_LIST'
    public enum PaymentModes
    {
        CASH = 1,
        BANK = 2
    }

    public enum SubPaymentModes
    {
        HARD_CASH = 1,
        CHEQUE = 2,
        DRAFT = 3,
        ONLINE_PAYMENT = 4,
        DIRECT_TRANSFER = 5,
        DEBIT_CARD = 6,
        CREDIT_CARD = 7
    }
}