using Cap.Domain.Service.Admin;
using System;
using System.Linq;

namespace Cap.Domain.Models.Email
{
    public class EmailCredential
    {
        #region [ Properties ]

        public bool UseSsl { get; set; }
        public string ServerSmtp { get; set; }
        public int ServerPort { get; set; }
        public string Sender { get; set; }
        public string SenderPassword { get; set; }

        #endregion

        SistemaParametroService service;

        public EmailCredential()
        {
            service = new SistemaParametroService();

            UseSsl = Convert.ToBoolean(GetParametro("EMAIL_USESSL"));
            ServerSmtp = GetParametro("EMAIL_SERVERSMTP");
            ServerPort = Convert.ToInt32(GetParametro("EMAIL_SERVERPORT"));
            Sender = GetParametro("EMAIL_SENDER");
            SenderPassword = GetParametro("EMAIL_SENDERPASSWORD");
        }

        private string GetParametro(string codigo)
        {
            var parametro = service.Listar().Where(x => x.Codigo == codigo).FirstOrDefault();

            if (parametro != null )
            {
                return parametro.Valor;
            }

            return null;
        }
    }
}
