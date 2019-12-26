using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace TimeKeeper.Mail.Services
{
    public static class MailService
    {
        public static void Send(string mailTo, string subject, string body)
        {
            SmtpClient client = new SmtpClient
            {
                Port = 587,
                Host = "smtp.gmail.com",
                EnableSsl = true,
                Timeout = 10000,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("ntg.infodesk@gmail.com", "Company19892019")
            };

            MailMessage message = new MailMessage("ntg.infodesk@gmail.com", "charliemaestral@gmail.com" /*Startup.Configuration["mailSettings:mailTo"]*/, subject, body);
            message.BodyEncoding = UTF8Encoding.UTF8;
            message.IsBodyHtml = true;
            message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            message.ReplyToList.Add(mailTo);

            client.Send(message);
        }
    }
}
