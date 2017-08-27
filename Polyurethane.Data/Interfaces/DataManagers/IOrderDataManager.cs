using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Polyurethane.Data.Entities;
using Polyurethane.Data.Models.Orders;

namespace Polyurethane.Data.Interfaces.DataManagers
{
    public interface IOrderDataManager 
    {
        Task<Guid> CreateOrder(CreateOrder order);
        Task<List<Order>> GetOrders(int count = 1000);
        Task<Order> GetOrder(Guid guid);
    }
}
