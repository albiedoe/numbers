
using System;

namespace Algo.Models
{
    public class Run
    {
        public int Principal { get; set; }
        public int MonthlyContribution { get; set; }
        public DateTime startDate => DateTime.MinValue;
        public DateTime EndDate => DateTime.MaxValue;
        public decimal balance  { get; set; }

        public override string ToString()
        {
            return $"Principal: {Principal}, Monthly Contr: {MonthlyContribution}";
        }
    }
}