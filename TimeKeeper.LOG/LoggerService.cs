using NLog;
using System;
using TimeKeeper.Mail.Services;

namespace TimeKeeper.LOG
{
    public class LoggerService
    {
        private ILogger _logger = LogManager.GetCurrentClassLogger();

        public void Info(string message)
        {
            _logger.Info(message);
        }

        public void Warning(string message)
        {
            _logger.Warn(message);
        }

        public void Error(string message)
        {
            string errorMessage =
                $"<b>Time</b>: <br/>{DateTime.Now} <br/><br/>" +
                $"<b>Message</b>: <br/>{message} <br/><br/>";
            _logger.Error(message);
            string mailTo = "no-reply@gmail.com";
            string subject = "Error occured";
            string body = $"{errorMessage}";
            MailService.Send(mailTo, subject, body);
        }

        public void Fatal(string message)
        {
            _logger.Fatal(message);
            string mailTo = "no-repy@gmail.com";
            string subject = "Fatal occured";
            string body = $"{message}";
            MailService.Send(mailTo, subject, body);
        }

        public void Fatal(Exception ex)
        {
            string message = 
                $"<b>Time</b>: <br/>{DateTime.Now} <br/><br/>" +
                $"<b>Message</b>: <br/>{ex.Message} <br/><br/>" +
                $"<b>Source</b>: <br/>{ex.Source} <br/><br/>" +
                $"<b>StackTrace</b>: <br/>{ex.StackTrace}";

            _logger.Fatal(ex.Message);
            string mailTo = "no-repy@gmail.com";
            string subject = "Fatal occured";
            string body = $"{message}";
            MailService.Send(mailTo, subject, body);
        }

    }
}
