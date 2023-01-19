using ProvisionCase.BL.Abstract;
using ProvisionCase.DAL.Context;
using ProvisionCase.Entities.Models;

namespace ProvisionCase.BL.Concrete
{
    public class ExchangeRatesManager : ManagerBase<ExchangeRates>, IExchangeRatesManager
    {

        public ExchangeRatesManager(ProvisionCaseDbContext dbContext) : base(dbContext)
        {

        }


    }
}