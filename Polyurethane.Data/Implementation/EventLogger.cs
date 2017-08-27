using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Polyurethane.Core.Enums;
using Polyurethane.Data.DbContext;
using Polyurethane.Data.Entities;
using Polyurethane.Data.Interfaces;

namespace Polyurethane.Data.Implementation
{
    public class EventLogger : IEventLogger
    {
        private readonly IDataProvider _dataProvider;
        public EventLogger(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public async Task LogEventAsync(EventType eventType, string loggedBy = "System", string details = "", string moreDetails = "")
        {
            try
            {
                using (var db = new PolyurethaneContext(_dataProvider.ConnectionString))
                {
                    var dbEntity = new EventEntity()
                    {
                        EventTime = DateTime.Now,
                        Details = details,
                        LoggedBy = loggedBy,
                        Event = eventType,
                        MoreDetils = moreDetails
                    };

                    db.Events.Add(dbEntity);
                    await db.SaveChangesAsync();
                }
            }
            catch { }
        }

        public void LogEvent(EventType eventType, string loggedBy = "System", string details = "", string moreDetails = "")
        {
            LogEventAsync(eventType, loggedBy, details, moreDetails).Wait();
        }
    }
}
