using EnviaEmail.Models;
using MySql.Data.MySqlClient.Memcached;
using System.Net;
using System.Net.Mail;

namespace EnviaEmail.BLL
{
    public class EnviaEmailBLL
    {
        public void EnviaEmail(EmailModel email)
        {
            try
            {
                using (SmtpClient Client = new SmtpClient(email.Host, email.Porta))
                {
                    Client.EnableSsl = false;
                    Client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    Client.UseDefaultCredentials = false;
                    Client.Credentials = new NetworkCredential(email.Origem, email.Senha);

                    MailMessage mailMessage = new MailMessage();
                    mailMessage.To.Add(email.Destino);
                    mailMessage.From = new MailAddress(email.Origem);
                    mailMessage.Subject = email.Assunto;
                    mailMessage.Body = email.Mensagem;

                    Client.Send(mailMessage);
                }
                
            }
            catch (System.Exception ex)
            {

                throw new System.Exception ("", ex);
            }

        }

    }
}
