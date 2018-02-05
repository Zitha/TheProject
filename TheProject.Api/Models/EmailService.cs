using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace TheProject.Api.Models
{
    public class EmailService
    {
        public void SendResertPasswordEmail(string email, string decriptedPassword)
        {
            string emailUserName = ConfigurationManager.AppSettings["EmailUserName"];
            string emailPassword = ConfigurationManager.AppSettings["EmailPassword"];
            string emailHost = ConfigurationManager.AppSettings["EmailHost"];
            int emailPot = Convert.ToInt16(ConfigurationManager.AppSettings["EmailPot"]);

            SmtpClient client = new SmtpClient
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Host = emailHost,
                Timeout = 100000,
                Port = emailPot,
                Credentials = new NetworkCredential(emailUserName, emailPassword),
                EnableSsl = false
            };

            MailMessage mail = new MailMessage(emailUserName, email);
            mail.Subject = "Password Resert : The Project.";
            string Body = string.Format("Your password: {0}", decriptedPassword);
            mail.Body = Body;

            try
            {
                client.Send(mail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}