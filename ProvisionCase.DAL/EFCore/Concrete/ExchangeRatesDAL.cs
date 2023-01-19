using ProvisionCase.DAL.Abstract;
using ProvisionCase.DAL.Context;
using ProvisionCase.Entities.Models;

namespace ProvisionCase.DAL.EFCore.Concrete
{
    public class ExchangeRatesDAL : RepositoryBaseDAL<ExchangeRates>, IExchangeRatesDAL
    {
        public ExchangeRatesDAL(ProvisionCaseDbContext dbContext) : base(dbContext)
        {

        }
    }
}
