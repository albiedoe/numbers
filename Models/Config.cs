using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;

namespace Algo.Models
{
    [XmlRoot]
    public class Config
    {
        [XmlElement]
        public string DataPath;
        [XmlElement]
        public string RunsFile;
        [XmlElement]
        public Investor Investor;
    }
}
