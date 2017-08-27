using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using polyurethane_website.Mappers;
using polyurethane_website.Models;
using Polyurethane.Data.Interfaces;
using Polyurethane.Data.Interfaces.DataManagers;
using Polyurethane.Data.Models.Orders;

namespace polyurethane_website.Controllers.Orders
{
    [RoutePrefix("order")]
    public class OrdersController : Controller
    {
        private readonly IDataProvider _dataProvider;
        private readonly IOrderDataManager _orderDataManager;
        private readonly IMapper _mapper;

        public OrdersController(IDataProvider dataProvider, IOrderDataManager orderDataManager, IMapper mapper)
        {
            _orderDataManager = orderDataManager;
            _dataProvider = dataProvider;
            _mapper = mapper;
        }

        [Route("latest")]
        public async Task<ViewResult> OrderList()
        {
            var model = await _orderDataManager.GetOrders();
            return View(model);
        }

        [Route("success")]
        public ViewResult SuccessOrderForm()
        {
            return View();
        }

        [Route("process/{guid}")]
        public async Task<ActionResult> ProcessOrder(Guid guid)
        {
            var order = await _orderDataManager.GetOrder(guid);
            //var orderId = await _orderDataManager.CreateOrder(_mapper.Map<CreateOrder>(order));
            return View(order);
        }

        [Route("create")]
        [HttpPost]
        public async Task<ActionResult> CreateOrder(CreateOrderModel order)
        {
            var orderId = await _orderDataManager.CreateOrder(_mapper.Map<CreateOrder>(order));
            return Redirect("/order/success");
        }

        [Route("create/{products}")]
        [Route("create")]
        public async Task<ViewResult> OrderForm(List<Guid> products)
        {
            var dbItems = await _dataProvider.GetDetails(x => products.Contains(x.Id), products.Count, 1);
            var model = new OrderFormModel()
            {
                Details = dbItems.Select(x => new DetailMapper().Map(x)).ToList()
            };

            return View(model);
        }
    }
}