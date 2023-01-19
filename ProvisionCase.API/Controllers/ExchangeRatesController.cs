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
        public string BaseUrl { get; } = @"https://northwind.vercel.app/";

        public HttpClient Client { get; }
        public ExchangeRatesController(IExchangeRatesManager exchangeRatesManager)
        {
            Client = new HttpClient();
            Client.BaseAddress = new Uri(BaseUrl);
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

            for (int i = 2; i <= 78; i++)
            {
                try
                {
                    var result = ExchangeRatesController.GetAllHistoricalExchangeRates(2023, 1, i);

                    foreach (var item in result)
                    {
                        ExchangeRates exchangeRates1 = new ExchangeRates
                        {
                            Id = 0,
                            Code = item.Value.Code,
                            ForexBuying = item.Value.ForexBuying,
                            Name = item.Value.Name
                        };
                        await exchangeRatesManager.CreateAsync(exchangeRates1);
                    }
                }
                catch (Exception ex)
                {

                    continue;
                }
            }


            //  Console.WriteLine(depo);
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
                    ForexBuying = 1
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
