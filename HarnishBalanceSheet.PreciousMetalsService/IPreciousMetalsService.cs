using System;
using System.Collections.Generic;
using System.Text;
using HarnishBalanceSheet.Models;

namespace HarnishBalanceSheet.PreciousMetalsService
{
    public interface IPreciousMetalsService
    {
        Task<IEnumerable<PreciousMetalPrice>> GetPreciousMetalsPricesAsync();
    }
}
