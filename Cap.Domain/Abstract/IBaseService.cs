using System.Linq;

namespace Cap.Domain.Abstract
{
    public interface ILogin<T> where T : class
    {
        IQueryable<T> Listar();
        int Gravar(T item);
        T Excluir(int id);
        T Find(int id);
    }
}
