namespace Cap.Domain.Abstract.Admin
{
    public interface ITrocaSenha
    {
        void TrocarSenha(int idUsuario, string senhaAnterior, string novaSenha, bool enviarEmail = true);
        void RedefinirSenha(int idUsuario);
    }
}
