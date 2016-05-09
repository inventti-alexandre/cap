namespace Cap.Domain.Abstract.Email
{
    public interface IEmail
    {
        bool Enviar(string nome, string destinatario, string assunto, string mensagem);
    }
}
