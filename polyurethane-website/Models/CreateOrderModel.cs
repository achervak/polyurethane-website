using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Polyurethane.Core.Enums;

namespace polyurethane_website.Models
{
    public class CreateOrderModel
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public DeliveryType DeliveryType { get; set; }
        public PaymentType PaymentType { get; set; }
        public DeliveryServiceType DeiveryService { get; set; }
        public string DeliveryAddress { get; set; }
        public string OrderDetails { get; set; }
        public List<string> Details { get; set; } = new List<string>();
    }
}