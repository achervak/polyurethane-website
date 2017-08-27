using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polyurethane.Core.Enums
{
    public enum OrderStatus
    {
        [Description("None")]
        None,
        [Description("Очікує опрацювання")]
        PendingApprove,
        [Description("Підтверджено")]
        Approved,
        [Description("Очікує оплати")]
        WaitingForPayment,
        [Description("Очікує відправки")]
        WaitingToSend,
        [Description("Відправлено")]
        DeliveryInProgress,
        [Description("Доставлено")]
        Delivered,
        [Description("Замовлення скасовано")]
        Canceled
    }
}
