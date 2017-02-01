using System.Xml.Serialization;


namespace Algo.Models
{
    [XmlRoot]
    public class Investor
    {
        [XmlElement]
        public double Principal;
        [XmlElement]
        public string DayOfWeek;
        [XmlElement]
        public double WeeklyContribution;

    }
}
