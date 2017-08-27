using System.Threading.Tasks;
using Polyurethane.Data.Entities;
using Polyurethane.Data.Models.Orders;

namespace Polyurethane.Data.Interfaces.Communication
{
    public interface IEmailCommunication
    {
        Task SendEmail(string receiver, string subject, string message);
        //Task SendOrderCreatedMail(OrderModel )
    }
}
