using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algo.Models;
using System.IO;
using System.Diagnostics;

namespace Algo
{
    public class MetricManager
    {
        public FixedQueue queue365 = new FixedQueue(365);
        public FixedQueue queue180 = new FixedQueue(180);
        public FixedQueue queue90 = new FixedQueue(90);
        public FixedQueue queue30 = new FixedQueue(30);
        public FixedQueue queue10 = new FixedQueue(10);
        public FixedQueue queue5 = new FixedQueue(5);
        public decimal VolatilityIndex;

        private DayInTheMarket today { get; set; }

        public void AddDay(DayInTheMarket day)
        {
            queue365.Add(day);
            queue180.Add(day);
            queue90.Add(day);
            queue30.Add(day);
            queue10.Add(day);
            queue5.Add(day);
            if(day.MarketDate.Month==1){
                int sw=2;
            }
            if(day.MarketDate.Month==2){
                int ssw=2;
            }
            int s =1;
        }

        
    }
}