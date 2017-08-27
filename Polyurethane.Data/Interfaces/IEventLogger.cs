using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Polyurethane.Core.Enums;
using Polyurethane.Data.Entities;

namespace Polyurethane.Data.Interfaces
{
    public interface IEventLogger
    {
        Task LogEventAsync(EventType eventType, string loggedBy = "System", string details = "", string moreDetails = "");
        void LogEvent(EventType eventType, string loggedBy = "System", string details = "", string moreDetails = "");
    }
}
