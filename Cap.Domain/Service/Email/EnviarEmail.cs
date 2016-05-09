using Cap.Domain.Abstract.Email;
using Cap.Domain.Models.Email;
using System;
using System.Net.Mail;

namespace Cap.Domain.Service.Email
{
    public class EnviarEmail : IEmail
    {
        EmailCredential credential;

        public EnviarEmail()
        {
            credential = new EmailCredential();
        }

        public bool Enviar(string nome, string destinatario, string assunto, string mensagem)
        {
            try
            {
                using (var smtpClient = new SmtpClient())
                {
                    // configuracoes para envio
                    smtpClient.EnableSsl = credential.UseSsl;
                    smtpClient.Host = credential.ServerSmtp;
                    smtpClient.Port = credential.ServerPort;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new System.Net.NetworkCredential(credential.Sender, credential.SenderPassword);

                    // mensagem
                    var message = new MailMessage(credential.Sender, destinatario, assunto, mensagem);
                    message.IsBodyHtml = false;

                    // envia o email
                    smtpClient.Send(message);

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
