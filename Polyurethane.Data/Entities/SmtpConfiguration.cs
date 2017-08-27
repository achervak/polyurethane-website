using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Polyurethane.Core.Enums;

namespace Polyurethane.Data.Entities
{
    [Table("SMTPConfigutation")]
    public class SmtpConfiguration : BaseEntity
    {
        public SmtpConfigurationType ConfigurationType { get; set; }
        public string ConnectionName { get; set; }
        public string SenderAddress { get; set; }
        public string SenderTitle { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public bool EnableSsl { get; set; }
        public bool UseDefaultCredentials { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
    }
}
