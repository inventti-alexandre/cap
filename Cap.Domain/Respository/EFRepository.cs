using System;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;

namespace Cap.Domain.Respository
{
    public class EFRepository<T> : IDisposable, IBaseRepository<T> where T : class
    {
        private EFDbContext db = new EFDbContext();

        public T Alterar(T item)
        {
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return item;
        }

        public T Excluir(int id)
        {
            var item = db.Set<T>().Find(id);

            if (item != null)
            {
                try
                {
                    db.Set<T>().Remove(item);
                    db.SaveChanges();
                }
                catch (DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                        }
                    }
                }
            }

            return item;
        }

        public T Incluir(T item)
        {
            try
            {
                db.Set<T>().Add(item);
                db.SaveChanges();
                return item;
            }
            catch (DbEntityValidationException dbEx)
            {
                var se = new System.Text.StringBuilder();
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                        se.Append(string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage));
                    }
                }
                throw new InvalidOperationException(se.ToString());
            }
        }

        public IQueryable<T> Listar()
        {
            return db.Set<T>();
        }

        public T Find(int id)
        {
            return db.Set<T>().Find(id);
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
