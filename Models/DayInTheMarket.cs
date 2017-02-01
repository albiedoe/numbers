using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algo.Models
{

    public class DayInTheMarket
    {
        public DateTime MarketDate;
        public decimal OpenPrice;
        public decimal ClosingPrice;
        public decimal AveragePrice => (OpenPrice + ClosingPrice) /2;
        public decimal High;
        public decimal Low;
        public decimal Volume;
        public decimal AdjustedPrice;
        public decimal DayChangePercent => (DayChange / OpenPrice ) + 1 ;
        public decimal DayChange => ClosingPrice - OpenPrice;

    }
}
