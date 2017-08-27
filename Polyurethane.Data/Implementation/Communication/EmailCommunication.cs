using System;
using System.Data.Entity;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Polyurethane.Core.Enums;
using Polyurethane.Data.DbContext;
using Polyurethane.Data.Interfaces;
using Polyurethane.Data.Interfaces.Communication;

namespace Polyurethane.Data.Implementation.Communication
{
    public class EmailCommunication : IEmailCommunication
    {
        private readonly IDataProvider _dataProvider;

        public EmailCommunication(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public async Task SendEmail(string receiver, string subject, string message)
        {
            try
            {
                var smtp = await GetSmtpClient(SmtpConfigurationType.Base);

                var from = new MailAddress((smtp.Credentials as NetworkCredential).UserName, "polyurethane.website");
                var to = new MailAddress(receiver);
                var mail = new MailMessage(from, to)
                {
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = true
                };

                smtp.Send(mail);

            }
            catch (Exception ex)
            {
                var a = 1;
            }
        }

        private async Task<SmtpClient> GetSmtpClient(SmtpConfigurationType clientType)
        {
            using (var db = new PolyurethaneContext(_dataProvider.ConnectionString))
            {
                var smtpConfiguration =
                    await db.SmtpConfigurations.FirstOrDefaultAsync(x => x.ConfigurationType == clientType);
                if (smtpConfiguration == null)
                    throw new Exception(
                        "There is no SMTP configuration was found. Please contact system administrator");

                var smtp = new SmtpClient(smtpConfiguration.Host, smtpConfiguration.Port)
                {
                    Credentials = new NetworkCredential(smtpConfiguration.UserName, smtpConfiguration.Password),
                    EnableSsl = smtpConfiguration.EnableSsl
                };

                //client.Send("myusername@gmail.com", "myusername@gmail.com", "test", "testbody");
                //Console.WriteLine("Sent");
                //Console.ReadLine();


                //SmtpClient smtp = new SmtpClient
                //{
                //    Host = smtpConfiguration.Host,
                //    Port = smtpConfiguration.Port,
                //    EnableSsl = smtpConfiguration.EnableSsl,
                //    UseDefaultCredentials = smtpConfiguration.UseDefaultCredentials
                //};

                //if (!string.IsNullOrEmpty(smtpConfiguration.UserName))
                //    smtp.Credentials =
                //        new System.Net.NetworkCredential(smtpConfiguration.UserName, smtpConfiguration.Password);
                return smtp;
            }
        }
    }
}