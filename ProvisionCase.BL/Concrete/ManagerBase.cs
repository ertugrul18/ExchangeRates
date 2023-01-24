using ProvisionCase.BL.Abstract;
using ProvisionCase.DAL.Context;
using ProvisionCase.DAL.EFCore.Concrete;
using System.Linq.Expressions;

namespace ProvisionCase.BL.Concrete
{
    public class ManagerBase<T> : IManagerBase<T> where T : class, new()
    {
        public RepositoryBaseDAL<T> RepositoryBaseDAL { get; }

        public ManagerBase(ProvisionCaseDbContext dbContext)
        {
            RepositoryBaseDAL = new RepositoryBaseDAL<T>(dbContext); ;
        }

        public async virtual Task<int> CreateAsync(T entity)
        {
            return await RepositoryBaseDAL.CreateAsync(entity);
        }

        public async virtual Task<T> GetByCodeAsync(string code, Expression<Func<T, bool>> filter = null)
        {
            return await RepositoryBaseDAL.GetByCodeAsync(code, filter);
        }

        public async virtual Task<ICollection<T>> GetAllAsync(Expression<Func<T, bool>> filter = null)
        {
            return await RepositoryBaseDAL.GetAllAsync(filter);
        }


    }
}
