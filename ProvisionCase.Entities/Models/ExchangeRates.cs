namespace ProvisionCase.Entities.Models
{
    public class ExchangeRates
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        // public string CrossRateName { get; set; }
        public double ForexBuying { get; set; }

        public ExchangeRates()
        {

        }
        //public double ForexSelling { get; }
        //public double BanknoteBuying { get; }
        //public double BanknoteSelling { get; }

        //public ExchangeRates(string name, string code, string crossRateName, double forexBuying)
        //{
        //    Name = name;
        //    Code = code;
        //    CrossRateName = crossRateName;
        //    ForexBuying = forexBuying;
        //    //   ForexSelling = forexSelling;
        //    //     BanknoteBuying = banknoteBuying;
        //    //    BanknoteSelling = banknoteSelling;

        //}
    }
}