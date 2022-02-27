using Microsoft.Extensions.Options;
using SpaceTask.Data.DataModels;
using SpaceTask.Service.Configuration;
using SpaceTask.Service.Service.Interface;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SpaceTask.Service.Service.Implementation
{
    public class EmailService : IEmailService
    {
        private readonly ApiOptions _apiOptions;

        public EmailService(IOptions<ApiOptions> options)
        {
            _apiOptions = options.Value;
        }
       

        public async Task SendEmailAsync(EmailInfo emailInfo)
        {
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(emailInfo.FromEmail, emailInfo.Password)
            };
            using (var msg = new MailMessage(emailInfo.FromEmail, emailInfo.ToEmail)
            {
                Subject = emailInfo.Subject,
                Body = emailInfo.Message
            }) 

            try
            {
                await smtp.SendMailAsync(msg);
            }
            catch (Exception ex)
            { 
                Debug.Fail(ex.Message);
                throw;
            }
        }
    }
}
