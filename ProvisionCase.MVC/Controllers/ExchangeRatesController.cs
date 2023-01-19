using Microsoft.AspNetCore.Mvc;
using ProvisionCase.DAL.Context;
using ProvisionCase.MVC.Models;

namespace ProvisionCase.MVC.Controllers
{
    public class ExchangeRatesController : Controller
    {
        private readonly ProvisionCaseDbContext dbContext;

        public ExchangeRatesController(ProvisionCaseDbContext dbContext)
        {
            this.dbContext = dbContext;

        }
        [HttpGet]
        public IActionResult Index(ExchangeRatesDTO exchangeRatesDTO)
        {
            ExchangeRatesDTO indexDtos = new ExchangeRatesDTO();

            var exchangeRates = this.dbContext.ExchangeRates.ToList();


            // Amele Yontemi

            //ExchangeRatesDTO exchangeRatesDTO1 = new ExchangeRatesDTO
            //{
            //    Code = exchangeRatesDTO.Code,
            //    ForexBuying = exchangeRatesDTO.ForexBuying,
            //    Name = exchangeRatesDTO.Name,
            //    Id = exchangeRatesDTO.Id
            //};



            return View(exchangeRates);
        }



        //public IActionResult Index(ExchangeRatesDTO exchangeRatesDTO)
        //{
        //    //ExchangeRatesDTO exchangeRatesDTO = new ExchangeRatesDTO();
        //    //return View(exchangeRatesDTO);



        //    var sonuc = dbContext.ExchangeRates.Where(p => p.Code == "USD").ToList();

        //    if (sonuc != null)
        //    {
        //        return View(sonuc);

        //    }
        //    return View(exchangeRatesDTO);
        //}



        [HttpPost]
        public IActionResult Index(string code)
        {

            //ExchangeRatesDTO exchangeRatesDTO1 = new ExchangeRatesDTO
            //{
            //    Code = exchangeRatesDTO.Code,
            //    ForexBuying = exchangeRatesDTO.ForexBuying,
            //    Name = exchangeRatesDTO.Name,
            //    Id = exchangeRatesDTO.Id
            //};


            var sonuc = dbContext.ExchangeRates.Where(p => p.Code == code).ToList();

            if (sonuc != null)
            {
                return View(sonuc);

            }
            return View(sonuc);

        }



    }
}
