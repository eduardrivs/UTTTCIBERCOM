using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace UTTTCIBERCOM.Control
{
    public class EmailManager
    {

        private String _eMail;

        public EmailManager(String email)
        {
            this._eMail = email;
        }

        public void enviarMensaje(String mensaje)
        {
            var fromAddress = new MailAddress("prueba4SMTP1@gmail.com");
            var toAddress = new MailAddress(_eMail);
            const string fromPassword = "Ws4m6jx98UeN!sm";
            const string subject = "ERROR en sitio http://www.JoseEduardoRivas.somee.com";
            var body = "Error del " + System.DateTime.Now.ToString() + "\n\n" + mensaje;

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                try
                {
                    smtp.Send(message);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
    }
}
