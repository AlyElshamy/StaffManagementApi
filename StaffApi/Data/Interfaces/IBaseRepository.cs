using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace StaffApi.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
         IEnumerable<T> GetAll();
         Task<T> AddAsync(T entity);
        Task<T> FindAsync(int? id);
        //Task<T> FindAsync(int? id, string[] includes = null);
        //Task<T> FindAsync(Expression<Func<T, bool>> criteria, string[] includes = null);
        //IEnumerable<T> FindAll( string[] includes = null);
        //IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, string[] includes = null);
        //IEnumerable<T> FindAllfamilies(Expression<Func<T, bool>> criteria, string[] includes = null);
        public T Update(T entity);
        ////public T ApproveInfo(T entity);
        //public Task<T> UpdateAsync(T entity);
        public void Delete(T entity);
        void Complete();


    }
}
