using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polyurethane.Core.Enums
{
    public enum EventType
    {
        [Description("None")]
        None = 0,
        Debug = 1,
        Error = 2,
        [Description("Оформлено покупку")]
        OrderCreated = 3,
    }
}
