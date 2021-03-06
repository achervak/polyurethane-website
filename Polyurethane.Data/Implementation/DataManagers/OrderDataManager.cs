﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Polyurethane.Core.Enums;
using Polyurethane.Data.DbContext;
using Polyurethane.Data.Entities;
using Polyurethane.Data.Interfaces;
using Polyurethane.Data.Interfaces.DataManagers;
using Polyurethane.Data.Models.Orders;

namespace Polyurethane.Data.Implementation.DataManagers
{
    public class OrderDataManager : IOrderDataManager
    {
        private readonly IDataProvider _dataProvider;
        private readonly IMapper _mapper;
        private readonly IEventLogger _logger;

        public OrderDataManager(IDataProvider dataProvider, IMapper mapper, IEventLogger eventLogger)
        {
            _dataProvider = dataProvider;
            _mapper = mapper;
            _logger = eventLogger;
        }

        //public async Task<List<OrderEntity>> 

        public async Task<Guid> CreateOrder(CreateOrder order)
        {
            using (var db = new PolyurethaneContext(_dataProvider.ConnectionString))
            {
                var dbCustomer = await db.Customers.FirstOrDefaultAsync(x => x.Email == order.Email);
                if (dbCustomer == null)
                {
                    dbCustomer = new UserEntity()
                    {
                        Email = order.Email,
                        // TODO: Add autogeneration
                        Password = "autogenerated"
                    };

                    db.Customers.Add(dbCustomer);
                    await db.SaveChangesAsync();
                }
                //-- update user details
                dbCustomer.PhoneNumber = order.PhoneNumber;
                dbCustomer.FirstName = order.FirstName;
                dbCustomer.LastName = order.LastName;

                var dbOrder = new OrderEntity()
                {
                    CustomerId = dbCustomer.Id,
                    OrderDate = DateTime.Now,
                    DeliveryDetails = order.DeliveryDetails(),
                    OrderDetails = order.OrderDetails,
                    OrderStatus = OrderStatus.PendingApprove
                };
                db.Orders.Add(dbOrder);

                //-- add products 
                var dbProducts = db.Details.Where(x => order.Details.Contains(x.Id.ToString()));
                foreach (var dbProduct in dbProducts)
                {
                    dbOrder.Details.Add(dbProduct);
                }
                dbOrder.Price = await dbProducts.SumAsync(x => x.Price);
                await db.SaveChangesAsync();


                await _logger.LogEventAsync(EventType.OrderCreated, order.Email,
                    $"{dbCustomer.FirstName} - {dbCustomer.LastName}", dbOrder.DeliveryDetails);
            }

            return Guid.NewGuid();
        }

        public async Task<List<Order>> GetOrders(int count = 1000)
        {
            using (var db = new PolyurethaneContext(_dataProvider.ConnectionString))
            {
                var dbOrders = await db.Orders
                    .Include(x => x.Customer)
                    .OrderByDescending(x => x.OrderDate)
                    .Take(1000).ToListAsync();

                return dbOrders.Select(x => _mapper.Map<Order>(x)).ToList();
            }
        }

        public async Task<Order> GetOrder(Guid guid)
        {
            using (var db = new PolyurethaneContext(_dataProvider.ConnectionString))
            {
                var dbOrder = await db.Orders.FirstOrDefaultAsync(x => x.Id == guid);
                if (dbOrder == null)
                    return null;

                return _mapper.Map<Order>(dbOrder);
            }
        }
    }
}
