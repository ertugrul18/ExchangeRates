using System;
using System.Collections.Generic;

namespace ProvisionCase.DAL.Entities;

public partial class ExchangeRate
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public double ForexBuying { get; set; }
}
