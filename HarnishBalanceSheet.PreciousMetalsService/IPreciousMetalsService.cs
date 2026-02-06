using System;
using System.Collections.Generic;
using HarnishBalanceSheet.Models;

namespace HarnishBalanceSheet.PreciousMetalsService
{
    public interface IPreciousMetalsService
    {
        Task<IEnumerable<PreciousMetalPrice>> GetPreciousMetalPricesAsync();
    }
}
