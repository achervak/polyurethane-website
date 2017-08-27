using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Polyurethane.Data.Interfaces;
using Polyurethane.Data.Interfaces.DataManagers;

namespace polyurethane_website.Controllers
{
    [RoutePrefix("event")]
    public class EventsController : Controller
    {
        private readonly IDataProvider _dataProvider;
        private readonly IOrderDataManager _orderDataManager;
        private readonly IMapper _mapper;

        public EventsController(IDataProvider dataProvider, IOrderDataManager orderDataManager, IMapper mapper)
        {
            _orderDataManager = orderDataManager;
            _dataProvider = dataProvider;
            _mapper = mapper;
        }

        [Route("latest")]
        public async Task<ViewResult> GetLatest(string filter)
        {
            var model = await _dataProvider.GetEvents();
            return View("EventList", model.ToList());
        }
    }
}