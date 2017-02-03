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
    public class ContributionManager
    {
        public enum ContributionMode{MonthlyAll, Mondays, TuesDays, Wednesdays, Thursdays, Fridays, Calculated};

        public ContributionMode Mode{ get;set; }
        public Run ManagedRun {get; set;}
        public MetricManager metrics{get;set;} = new MetricManager();
        private int DayOfWeek {get; set;}
        private int LastMonth {get; set;}
        private int LastDay {get; set;}

        public ContributionManager(ContributionMode mode){
            this.Mode = mode;
        }

        public void Contribute(DayInTheMarket day ){
            if(LastMonth != day.MarketDate.Month){
                ManagedRun.FreeCash += ManagedRun.MonthlyContribution;
            }

            switch(Mode){
                case ContributionMode.MonthlyAll:
                    ContributeMonthlyAll(day);
                break;
                case ContributionMode.Mondays:
                    ContributeDayOfWeek(day, System.DayOfWeek.Monday);
                break;
                case ContributionMode.TuesDays:
                    ContributeDayOfWeek(day, System.DayOfWeek.Tuesday);
                break;
                case ContributionMode.Wednesdays:
                    ContributeDayOfWeek(day, System.DayOfWeek.Wednesday);
                break;
                case ContributionMode.Thursdays:
                    ContributeDayOfWeek(day, System.DayOfWeek.Thursday);
                break;
                case ContributionMode.Fridays:
                    ContributeDayOfWeek(day, System.DayOfWeek.Friday);
                break;
            }
            LastMonth = day.MarketDate.Month;
            LastDay = day.MarketDate.Day;
        
        }

        private void ContributeMonthlyAll(DayInTheMarket day){
            if(LastMonth != day.MarketDate.Month){
                ManagedRun.Balance += ManagedRun.FreeCash;
                ManagedRun.FreeCash -= ManagedRun.FreeCash;
            }
        }
        private void ContributeDayOfWeek(DayInTheMarket day, System.DayOfWeek dayOfWeek){
            if(day.MarketDate.DayOfWeek == dayOfWeek){
                decimal amountToContribute = ManagedRun.MonthlyContribution / 4;
                if(ManagedRun.FreeCash > ManagedRun.MonthlyContribution){
                    amountToContribute += ManagedRun.FreeCash - ManagedRun.MonthlyContribution;
                }
                ManagedRun.Balance += amountToContribute;
                ManagedRun.FreeCash -= amountToContribute;
            }
        }
        
    }


}