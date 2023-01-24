using Microsoft.EntityFrameworkCore;
using ProvisionCase.DAL.Abstract;
using ProvisionCase.DAL.Context;
using System.Linq.Expressions;

namespace ProvisionCase.DAL.EFCore.Concrete
{
    public class RepositoryBaseDAL<T> : IRepositoryBaseDAL<T> where T : class, new()
    {
        private readonly ProvisionCaseDbContext dbContext;

        public RepositoryBaseDAL(ProvisionCaseDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async virtual Task<int> CreateAsync(T entity)
        {
            try
            {
                await dbContext.Set<T>().AddAsync(entity);
                return await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw ex;

            }
            return 0;
        }


        public async virtual Task<ICollection<T>> GetAllAsync(Expression<Func<T, bool>> filter = null)
        {
            if (filter != null)
            {

                return await dbContext.Set<T>().Where(filter).ToListAsync();
            }
            else
            {
                return await dbContext.Set<T>().ToListAsync();

            }
        }

        public async virtual Task<T> GetByCodeAsync(string code, Expression<Func<T, bool>> filter = null)
        {
            if (filter != null)
            {

                return await dbContext.Set<T>().FirstOrDefaultAsync(filter);
            }
            else
            {


                return await dbContext.Set<T>().FirstOrDefaultAsync();

            }
        }
    }
}
