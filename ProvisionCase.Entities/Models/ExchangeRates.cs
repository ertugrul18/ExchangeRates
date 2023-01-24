namespace ProvisionCase.Entities.Models
{
    public class ExchangeRates
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public double ForexBuying { get; set; }
        public DateTime? Date { get; set; }
        public ExchangeRates()
        {

        }

    }
}