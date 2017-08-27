using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Polyurethane.Core.Enums;

namespace Polyurethane.Data.Models.Orders
{
    public class Order
    {
        public Guid Id { get; set; }
        public int OrderId { get; set; }
        public double Price { get; set; }
        public DateTime OrderDate { get; set; }
        public Customer Customer { get; set; }
        public List<OrderedDetail> Details =  new List<OrderedDetail>();
        public string DeliveryDetails { get; set; }
        public string OrderDetails { get; set; }
        public OrderStatus OrderStatus { get; set; }
    }
}
