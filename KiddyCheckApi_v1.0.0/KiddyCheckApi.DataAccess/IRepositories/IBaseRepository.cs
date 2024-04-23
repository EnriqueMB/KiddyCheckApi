using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace KiddyCheckApi.DataAccess.IRepositories
{
    public interface IBaseRepository<T> : IDisposable where T : class
    {
        Task<IEnumerable<T>> GetAll(ILogger log);
        Task<T> GetById(int id, ILogger log);
        Task<T> GetById(long id, ILogger log);
        Task<T> Add(T entity, ILogger log);
        Task<IEnumerable<T>> AddAll(IEnumerable<T> entity, ILogger log);
        Task<T> Update(T entity, ILogger log);
        Task<int> Delete(int id, ILogger log);
        Task<long> Delete(long id, ILogger log);
        Task<IEnumerable<T>> GetAll(ILogger log, string[] include);
        Task<IEnumerable<T>> GetAll(ILogger log, string[] include, Expression<Func<T, bool>> query);
        Task<IEnumerable<T>> GetAll(ILogger log, string[] include, Expression<Func<T, bool>> query, int skip, int take);
    }
}
