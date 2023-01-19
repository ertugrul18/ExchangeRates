using System.Linq.Expressions;

namespace ProvisionCase.BL.Abstract
{
    public interface IManagerBase<T> where T : class, new()

    {
        Task<int> CreateAsync(T entity);
        //Task<int> DeleteAsync(T entity);
        //Task<int> UpdateAsync(T entity);

        Task<T> GetByCodeAsync(string code, Expression<Func<T, bool>> filter = null);
        Task<ICollection<T>> GetAllAsync(Expression<Func<T, bool>> filter = null);
        //Task<bool> CreateKeyValue(List<Dictionary<string, ExchangeRates>> listOfkeyValuePairs);


    }
}
