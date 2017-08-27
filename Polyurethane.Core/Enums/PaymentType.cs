using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polyurethane.Core.Enums
{
    public enum PaymentType
    {
        None = 0,
        [Description("Переказ на карту")]
        DebitCard = 1,
        [Description("Накладений платіж")]
        Cod = 2,
    }
}
