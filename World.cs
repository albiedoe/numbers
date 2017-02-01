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
    public class World
    {
        private Config config;
        private Stack<DayInTheMarket> _market = new Stack<DayInTheMarket>();

        public void StartWorld(Config inConfig)
        {
            config = inConfig;

            Initialize();
            int count = 0;
            decimal princ, control, actual;
            //princ = control = 1229.030029M;
            princ = control = 16.66M;
            actual = 100;
            foreach (DayInTheMarket day in _market)
            {
                decimal multiplier = day.DayChangePercent;
                decimal timesBigger = actual / princ;
                princ = (princ * multiplier);

                decimal diff  = day.ClosingPrice - princ;
                

                princ += diff;

                if(day.DayChangePercent != 1)
                {
                    int s = 1;
                    Debug.WriteLine(actual);
                }

                //actual = actual * multiplier;
                actual = actual * multiplier + (timesBigger * diff);
                if(count % 30 == 0)
                {
                    int s = 1;
                    Debug.WriteLine(actual);
                }
                count++;
            }
            var c = _market.Last();
            princ = princ;
            int next = 1;
        }

        private void Initialize()
        {
            StreamReader reader = new StreamReader( new FileStream("data2.csv", FileMode.Open));
            string line = reader.ReadLine();//skip first

            while ((line = reader.ReadLine()) != null)
            {
                string[] fields = line.Split(',');
                DayInTheMarket day = new DayInTheMarket()
                {
                    MarketDate = DateTime.Parse(fields[0]),
                    OpenPrice = decimal.Parse(fields[1]),
                    High = decimal.Parse(fields[2]),
                    Low = decimal.Parse(fields[3]),
                    ClosingPrice = decimal.Parse(fields[4]),
                    Volume = decimal.Parse(fields[5]),
                    AdjustedPrice = decimal.Parse(fields[6]),
                };
                _market.Push(day);
            }
        }
    }
}
