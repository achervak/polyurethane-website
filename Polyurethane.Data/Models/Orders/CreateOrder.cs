using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Polyurethane.Core.Enums;
using Polyurethane.Utils.Helpers;

namespace Polyurethane.Data.Models.Orders
{
    public class CreateOrder
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

        public string DeliveryDetails()
        {
            return $"{FirstName} {LastName}\n{PhoneNumber}\n{Email}\n\n{PaymentType.Description()}\n{DeliveryType.Description()}\n{DeiveryService.Description()}\n{City} - {DeliveryAddress}";
        }
    }
}
