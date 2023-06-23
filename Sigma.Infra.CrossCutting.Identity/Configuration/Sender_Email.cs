using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace Sigma.Infra.CrossCutting.Identity.Configuration
{
    public class Sender_Email
    {
        string _sender = "";
        string _password = "";
        public Sender_Email()
        {
            //_sender = "sigma_web_bng@hotmail.com";
            //_password = "@Sigma123";

            //_sender = "controle_despesa@hotmail.com";
            //_password = "@123Controle";

            //_sender   = "i9campo@hotmail.com";
            //_password = "projetoreact2020";

            _sender   = "contato@i9campo.site";
            _password = "d7PH5bvUSgpe";
        }

        public void SendMail(string recipient, string subject, string message)
        {
            SmtpClient client = new SmtpClient("smtp.zoho.com");
            NetworkCredential credentials = new NetworkCredential(_sender, _password);
            client.Port = 587;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.EnableSsl = true;
            client.Credentials = credentials;

            try
            {
                var mail = new MailMessage(_sender.Trim(), recipient.Trim());
                mail.Subject = subject;
                mail.Body = message;
                mail.IsBodyHtml = true;
                client.Send(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //throw ex;
            }

        }
    }
}
