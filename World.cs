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
        private List<DayInTheMarket> _marketList = new List<DayInTheMarket>();
        private List<Run> ObservingRuns = new List<Run>();
        private decimal marketStart;

        public World()
        {
            Initialize();
        }

        public void Process(IEnumerable<Run> runs)
        {

            foreach (Run run in runs)
            {
                StringBuilder sb = new StringBuilder($"{run.Mode}:");
                // Debug.WriteLine($"{run.Mode}:");

                //Run control = run.GetControl();
                List<DayInTheMarket> thisRunsMarket = _marketList.Where(x => x.MarketDate >= run.StartDate && x.MarketDate <= run.EndDate).ToList();
                marketStart = thisRunsMarket.First().OpenPrice;
                //control.Balance = run.Principal;
                run.Balance = run.Principal;

                ObservingRuns.Add(run);
                //ObservingRuns.Add(control);
                ContributionManager runManager = new ContributionManager(run.Mode){
                    ManagedRun = run
                };
                // ContributionManager controlManager = new ContributionManager(ContributionManager.ContributionMode.MonthlyAll){
                //     ManagedRun = control
                // };

                int count = 0;
                DayInTheMarket lastDay = null;
                foreach (DayInTheMarket day in thisRunsMarket)
                {
                   // controlManager.Contribute(day);
                    runManager.Contribute(lastDay);
                    CalculateChange(day);
                    if (count % 1000 == 0)
                    {
                        int s = 1;
                   //     Debug.WriteLine(run.Balance + "|" + control.Balance+"|"+marketStart);
                        // Debug.Write($"{run.Balance}|");
                        sb.Append($"{run.Balance.ToString("#")}|");
                    }
                    count++;
                    lastDay = day;
                }
                // Debug.Write($"{run.Balance}");
                sb.Append($"{run.Balance.ToString("#")}");
                sb.AppendLine("");
                sb.AppendLine($"Too much: {runManager.TooMuchFactor}, {runManager.TooMuchFactor/runManager.Total}");
                sb.AppendLine($"Too little: {runManager.TooLittleFactor}, {runManager.TooLittleFactor/runManager.Total}");
                Debug.WriteLine(sb.ToString());
                ObservingRuns.Clear();
            }
            int bp = 1;
        }

        private void CalculateChange(DayInTheMarket day){
            // decimal multiplier = day.DayChangePercent;
            // decimal timesBigger = run.Balance / marketStart;
            // marketStart = (marketStart * multiplier);
            // decimal diff = day.ClosingPrice - marketStart;

            // marketStart += diff;
            // run.Balance = run.Balance * multiplier + (timesBigger * diff);
            decimal multiplier = day.DayChangePercent;
            decimal newMarketStart = marketStart * multiplier;
            decimal diff = day.ClosingPrice - newMarketStart;
            foreach(Run run in ObservingRuns){
                decimal timesBigger = run.Balance / marketStart;    
                run.Balance = run.Balance * multiplier + (timesBigger * diff);
            }

            marketStart = newMarketStart + diff;
        }

        public void StartWorld()
        {
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

                decimal diff = day.ClosingPrice - princ;


                princ += diff;

                if (day.DayChangePercent != 1)
                {
                    int s = 1;
                    Debug.WriteLine(actual);
                }

                //actual = actual * multiplier;
                actual = actual * multiplier + (timesBigger * diff);
                if (count % 30 == 0)
                {
                    int s = 1;
                    // Debug.WriteLine(actual);
                }
                count++;
            }
            var c = _market.Last();
            princ = princ;
            int next = 1;
        }

        private void Initialize()
        {
            StreamReader reader = new StreamReader(new FileStream("data2.csv", FileMode.Open));
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
                _marketList.Add(day);
            }
            _marketList = _marketList.OrderBy(day => day.MarketDate).ToList();
        }
    }
}
