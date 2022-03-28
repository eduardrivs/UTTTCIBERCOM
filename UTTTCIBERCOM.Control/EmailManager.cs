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
            var fromAddress = new MailAddress("eduardo.rivas.uttt@gmail.com");
            var toAddress = new MailAddress(_eMail);
            const string fromPassword = "Uurxi!5b.F8tAYQ";
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
                smtp.Send(message);
            }
        }
    }
}
