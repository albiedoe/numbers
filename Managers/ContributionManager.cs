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
        public enum ContributionMode{MonthlyAll, Mondays, TuesDays, Wednesdays, Thursdays, Fridays, Calculated, Daily,
        CalculatedSimpleDayChange};

        public ContributionMode Mode{ get;set; }
        public Run ManagedRun {get; set;}
        public MetricManager metrics{get;set;} = new MetricManager();
        private int DayOfWeek {get; set;}
        private int LastMonth {get; set;}
        private int LastDay {get; set;}

        public int TooMuchFactor {get; set;}
        public int Total {get; set;}
        public int TooLittleFactor {get; set;}
        public ContributionManager(ContributionMode mode){
            this.Mode = mode;
        }

        public void Contribute(DayInTheMarket day ){
            if(day == null){
                return;
            }
            if(LastMonth != day.MarketDate.Month){
                ManagedRun.FreeCash += ManagedRun.MonthlyContribution;
            }
            Total++;
            metrics.AddDay(day);
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
                case ContributionMode.Daily:
                    ContributeDaily();
                break;
                case ContributionMode.Calculated:
                    ContributeCalculated(day);
                break;
                case ContributionMode.CalculatedSimpleDayChange:
                    ContributeCalculatedSimpleDayChange(day);
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
        private void ContributeCalculatedSimpleDayChange(DayInTheMarket day){
            decimal amountToContribute = ManagedRun.MonthlyContribution / 20;

            if(day.DayChangePercent > (decimal) 1.01){
                amountToContribute -= ManagedRun.MonthlyContribution / 60;
            }
            if(day.DayChangePercent < 1){
                amountToContribute += ManagedRun.MonthlyContribution / 100;
            }
            //CalculatedSimpleDayChange:509|6292|20551|29111|36628|35213|66069|89348
            if(day.DayChangePercent < (decimal) .99){
                amountToContribute += ManagedRun.MonthlyContribution / 100;
            }
            if(day.DayChangePercent < (decimal) .98){
                amountToContribute += ManagedRun.MonthlyContribution / 100;
            }
            if(day.DayChangePercent < (decimal) .97){
                amountToContribute += ManagedRun.MonthlyContribution / 100;
            }
            if(day.DayChangePercent < (decimal) .96){
                amountToContribute += ManagedRun.MonthlyContribution / 100;
            }
            if (ManagedRun.FreeCash > ManagedRun.MonthlyContribution)
            {
                amountToContribute += ManagedRun.FreeCash - ManagedRun.MonthlyContribution;
                TooMuchFactor++;
            }
            AddContribution(amountToContribute);
        }

        private void AddContribution(decimal amountToContribute){
            if(amountToContribute < 0)
            {
                return;
            }

            if (ManagedRun.FreeCash >= amountToContribute)
            {
                ManagedRun.Balance += amountToContribute;
                ManagedRun.FreeCash -= amountToContribute;
            }else{
                ManagedRun.Balance += ManagedRun.FreeCash;
                ManagedRun.FreeCash -= ManagedRun.FreeCash;
                TooLittleFactor++;
            }
        }

        private void ContributeCalculated(DayInTheMarket day){
            decimal amountToContribute = ManagedRun.MonthlyContribution / 25;

            if (ManagedRun.FreeCash > ManagedRun.MonthlyContribution)
            {
                amountToContribute += ManagedRun.FreeCash - ManagedRun.MonthlyContribution;
            }
            if (ManagedRun.FreeCash >= amountToContribute)
            {
                ManagedRun.Balance += amountToContribute;
                ManagedRun.FreeCash -= amountToContribute;
            }
        }

        private void ContributeDaily()
        {
            decimal amountToContribute = ManagedRun.MonthlyContribution / 20;
            if (ManagedRun.FreeCash > ManagedRun.MonthlyContribution)
            {
                amountToContribute += ManagedRun.FreeCash - ManagedRun.MonthlyContribution;
            }
            if (ManagedRun.FreeCash >= amountToContribute)
            {
                ManagedRun.Balance += amountToContribute;
                ManagedRun.FreeCash -= amountToContribute;

            }
        }

        private void ContributeDayOfWeek(DayInTheMarket day, System.DayOfWeek dayOfWeek){
            if(day.MarketDate.DayOfWeek == dayOfWeek){
                decimal amountToContribute = ManagedRun.MonthlyContribution / 4;
                if(ManagedRun.FreeCash > ManagedRun.MonthlyContribution){
                    TooMuchFactor++;
                    amountToContribute += ManagedRun.FreeCash - ManagedRun.MonthlyContribution;
                }
                if(amountToContribute > 25){
                    int s = 1;
                }
                AddContribution(amountToContribute);
                
            }
        }
        
    }


}