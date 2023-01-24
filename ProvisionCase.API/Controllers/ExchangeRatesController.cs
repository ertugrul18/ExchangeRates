using Microsoft.AspNetCore.Mvc;
using ProvisionCase.BL.Abstract;
using ProvisionCase.Entities.Models;
using System.Xml;

namespace ProvisionCase.API.Controllers
{
    [Route("api/[controller]/action")]
    [ApiController]
    public class ExchangeRatesController : ControllerBase
    {

        public ExchangeRatesController(IExchangeRatesManager exchangeRatesManager)
        {
            this.exchangeRatesManager = exchangeRatesManager;
        }

        List<Dictionary<string, ExchangeRates>> listOfkeyValuePairs = new List<Dictionary<string, ExchangeRates>>();
        private readonly IExchangeRatesManager exchangeRatesManager;



        [HttpGet]
        public async Task<ActionResult> Get()
        {
            string exchangeRate = @"http://www.tcmb.gov.tr/kurlar/today.xml";
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(exchangeRate);




            for (int i = 2; i <= 60; i++)
            {
                try
                {


                    var result = ExchangeRatesController.GetAllHistoricalExchangeRates(DateTime.Now.Date.Year, DateTime.Now.Date.Month, i);

                    foreach (var item in result)
                    {
                        ExchangeRates exchangeRates = new ExchangeRates
                        {
                            Id = item.Value.Id,//id = 0 dı
                            Code = item.Value.Code,
                            ForexBuying = item.Value.ForexBuying,
                            Name = item.Value.Name,
                            Date = new DateTime(DateTime.Now.Date.Year, DateTime.Now.Date.Month, i)

                        };
                        await exchangeRatesManager.CreateAsync(exchangeRates);
                    }
                }
                catch (Exception ex)
                {

                    continue;
                }
            }
            return Ok(listOfkeyValuePairs);
        }




        public static Dictionary<string, ExchangeRates> GetAllHistoricalExchangeRates(int Year, int Month, int Day)
        {
            try
            {
                string SYear = String.Format("{0:0000}", Year);
                string SMonth = String.Format("{0:00}", Month);
                string SDay = String.Format("{0:00}", Day);

                return GetExchangeRates("http://www.tcmb.gov.tr/kurlar/" + SYear + SMonth + "/" + SDay + SMonth + SYear + ".xml");
            }
            catch (Exception ex)
            {
                throw new Exception("The date specified may be a weekend or a public holiday!");
            }
        }





        private static Dictionary<string, ExchangeRates> GetExchangeRates(string Link)
        {
            try
            {
                XmlTextReader rdr = new XmlTextReader(Link);
                // XmlTextReader nesnesini yaratıyoruz ve parametre olarak xml dokümanın urlsini veriyoruz
                // XmlTextReader urlsi belirtilen xml dokümanlarına hızlı ve forward-only giriş imkanı sağlar.
                XmlDocument myxml = new XmlDocument();
                // XmlDocument nesnesini yaratıyoruz.
                myxml.Load(rdr);
                // Load metodu ile xml yüklüyoruz
                XmlNode tarih = myxml.SelectSingleNode("/Tarih_Date/@Tarih");
                XmlNodeList mylist = myxml.SelectNodes("/Tarih_Date/Currency");
                XmlNodeList adi = myxml.SelectNodes("/Tarih_Date/Currency/Isim");
                XmlNodeList kod = myxml.SelectNodes("/Tarih_Date/Currency/@Kod");
                XmlNodeList doviz_alis = myxml.SelectNodes("/Tarih_Date/Currency/ForexBuying");

                Dictionary<string, ExchangeRates> ExchangeRates = new Dictionary<string, ExchangeRates>();

                ExchangeRates.Add("TRY", new ExchangeRates()
                {
                    Name = "TRY",
                    Code = "TRY",
                    ForexBuying = 1,
                    Date = Convert.ToDateTime(tarih.InnerText)


                });

                for (int i = 0; i < adi.Count; i++)
                {
                    ExchangeRates cur = new ExchangeRates()
                    {
                        Code = kod.Item(i).InnerText.ToString(),
                        Name = kod.Item(i).InnerText.ToString() + "/TRY",
                        ForexBuying = double.Parse(doviz_alis.Item(i).InnerText.ToString())

                    };

                    ExchangeRates.Add(kod.Item(i).InnerText.ToString(), cur);
                }
                return ExchangeRates;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
