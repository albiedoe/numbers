
using System;

namespace Algo.Models
{
    public class Run
    {
        public decimal Principal { get; set; }
        public decimal MonthlyContribution { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Balance  { get; set; }
        public decimal FreeCash  { get; set; }
        public ContributionManager.ContributionMode Mode { get; set; }
        public bool isControl;
        public Run(){
            StartDate = DateTime.MinValue;
            EndDate = DateTime.MaxValue; 
        }

        public override string ToString()
        {
            return $"Principal: {Principal}, Monthly Contr: {MonthlyContribution}";
        }

        public Run GetControl(){
            return new Run{
                MonthlyContribution = MonthlyContribution,
                StartDate = StartDate,
                EndDate = EndDate,
                isControl = true
            };
        }
    }
}