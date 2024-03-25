using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.Core.Services
{
    public interface IService<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        IQueryable<T> Where(Expression<Func<T, bool>> predicate);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        Task<T> AddAsync(T entity);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
        Task UpdateAsync(T entity);
        Task RemoveAsync(T entity);
        Task RemoveRangeAsync(IEnumerable<T> entities);

        // Repository katmanından alınan veri dönüştürülebilir veya farklı iş kuralları yazılabilir. Bu nedenle Generic Repository tarafındaki imzalar burada da mevcut. En basit örneği ile dönüş tipleri değişebilir.
        
        // Farklı bir örnek olması adına GetAll methodunun imzasını değiştirdik.

        // IService tarafında veri tabanına SaveChangesAsync ile değişiklikleri aktaracağımız için Update ve Remove methodlarını Async olarak kullanabiliriz.
    }
}
