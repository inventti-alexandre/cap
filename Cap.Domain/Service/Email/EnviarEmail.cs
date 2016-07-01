using Cap.Domain.Abstract;
using Cap.Domain.Abstract.Email;
using Cap.Domain.Models.Admin;
using Cap.Domain.Models.Email;
using System;
using System.Linq;
using System.Net.Mail;

namespace Cap.Domain.Service.Email
{
    public class EnviarEmailWithCredentials
    {
        EmailCredential credential;

        public EnviarEmailWithCredentials()
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

    public class EnviarEmail : IEmail
    {
        IBaseService<EmailConfig> service;

        public EnviarEmail()
        {
            service = new EmailConfigService();
        }

        public bool Enviar(string nome, string destinatario, string assunto, string mensagem, int idEmpresa, bool isBodyHtml = false)
        {
            try
            {
                var config = service.Listar().FirstOrDefault(x => x.IdEmpresa == idEmpresa);

                if (config == null)
                {
                    return false;
                }

                using (var smtpClient = new SmtpClient())
                {
                    // configuracoes para envio
                    smtpClient.EnableSsl = config.UseSSL;
                    smtpClient.Host = config.ServerSmtp;
                    smtpClient.Port = config.ServerPort;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new System.Net.NetworkCredential(config.Sender, config.SenderPassword);

                    // mensagem
                    var message = new MailMessage(config.Sender, destinatario, assunto, mensagem);
                    message.IsBodyHtml = isBodyHtml;

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
