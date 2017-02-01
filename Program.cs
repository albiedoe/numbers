using Algo.Models;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Algo
{
    public class Program
    {
       Config config;
        static void Main(string[] args)
        {
            System.Console.WriteLine("SAGSAG");
            XmlSerializer serializer = new XmlSerializer(typeof(Config));

            
            StreamReader reader = new StreamReader( new FileStream("AppConfig.xml", FileMode.Open));
        

            Config config = (Config) serializer.Deserialize(reader);

            reader.Dispose();
            World world = new World();
            world.StartWorld(config);
System.Console.WriteLine("SAGSAG");


            int s = 1;
        }
    }
}
