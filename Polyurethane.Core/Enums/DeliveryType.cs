using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polyurethane.Core.Enums
{
    public enum DeliveryType
    {
        None = 0,
        [Description("У відділення")]
        Warehouse = 1,
        [Description("Кур'єром")]
        Courier = 2
    }
}
